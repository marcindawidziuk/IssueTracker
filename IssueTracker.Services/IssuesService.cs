using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Data;
using IssueTracker.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NJsonSchema.Annotations;

namespace IssueTracker.Services
{
    public interface IIssuesService
    {
        Task<List<IssueDto>> GetIssuesForProject(int projectId);
        Task<int> AddIssue(AddIssueDto dto, int currentUserId);
        Task UpdateIssue(UpdateIssueDto dto);
        Task UpdateIssueStatus(int id, int issueStatusId);
        Task<IssueDetailsDto> GetIssue(int id);
        Task AssignToUser(int issueId, int currentUserId);
        Task ReorderIssues(List<int> issueIds);
    }

    public class IssueDetailsDto
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public int? AssignedUserId { get; set; }
        public int StatusId { get; set; }
        public string Description { get; set; }
        public int CreatedByUserId { get; set; }
        public int ProjectId { get; set; }
        [NotNull]
        public List<LabelDto> Labels { get; set; }
    }

    public class IssueDto
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int? AssignedUserId { get; set; }
        public string UserName { get; set; }
        public List<LabelDto> Labels { get; set; }
    }

    public class AddIssueDto
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public int? AssignedUserId { get; set; }
        public int StatusId { get; set; }
    }
    
    public class UpdateIssueDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int? AssignedUserId { get; set; }
        public int StatusId { get; set; }
        public int Id { get; set; }
        public List<int> LabelIds { get; set; }
    }
    
    
    public class IssuesService : IIssuesService
    {
        private readonly IContextFactory _contextFactory;
        public IssuesService(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<IssueDto>> GetIssuesForProject(int projectId)
        {
            await using var db = _contextFactory.Create();

            return await db.Issues
                .Where(x => x.ProjectId == projectId)
                .OrderBy(x => x.Priority)
                .ThenBy(x => x.Id)
                .Select(x => new IssueDto
                {
                    Id = x.Id,
                    Reference = x.CaseReference,
                    Title = x.Title,
                    StatusId = x.IssueStatusId,
                    StatusName = x.IssueStatus.Name,
                    AssignedUserId = x.AssignedUserId,
                    UserName = x.AssignedUser.Name,
                    Labels = x.LabelIssueMappings.Select(a => new LabelDto
                    {
                        Id = a.LabelId,
                        Name = a.Label.Name
                    }).ToList()
                }).ToListAsync();

        }

        public async Task<int> AddIssue(AddIssueDto dto, int currentUserId)
        {
            await using var db = _contextFactory.Create();

            var caseNumber = await db.Issues
                .Where(x => x.ProjectId == dto.ProjectId)
                .Select(x => x.CaseNumber)
                .DefaultIfEmpty()
                .MaxAsync();

            var project = await db.Projects.SingleAsync(x => x.Id == dto.ProjectId);

            var issue = new Issue();
            issue.SetCaseNumber(++caseNumber, project.Abbreviation);
            issue.Title = dto.Title;
            issue.RawText = dto.Text;
            issue.ProjectId = project.Id;
            issue.AssignedUserId = dto.AssignedUserId;
            issue.CreatedByUserId = currentUserId;
            issue.IssueStatusId = dto.StatusId;
            db.Issues.Add(issue);
            
            await db.SaveChangesAsync();
            return issue.Id;
        }

        public async Task UpdateIssue(UpdateIssueDto dto)
        {
            await using var db = _contextFactory.Create();

            var issue = await db.Issues.SingleAsync(x => x.Id == dto.Id);
            issue.AssignedUserId = dto.AssignedUserId;
            issue.Title = dto.Title;
            issue.RawText = dto.Text;
            issue.IssueStatusId = dto.StatusId;

            var labelIssueMappings = await db.LabelIssueMappings
                .Where(x => x.IssueId == dto.Id)
                .ToListAsync();

            var labelIdsToAdd = dto.LabelIds
                .Where(x => labelIssueMappings.Any(l => l.Id == x) == false)
                .ToList();

            var newLabelMappings = labelIdsToAdd
                .Select(a => new LabelIssueMapping
                {
                    LabelId = a,
                    Issue = issue
                }).ToList();
            
            db.LabelIssueMappings.AddRange(newLabelMappings);
            
            var labelMappingsToRemove = labelIssueMappings
                .Where(x => dto.LabelIds.Contains(x.Id) == false)
                .ToList();
            db.RemoveRange(labelMappingsToRemove);
                
                

            await db.SaveChangesAsync();
        }

        public async Task UpdateIssueStatus(int id, int issueStatusId)
        {
            await using var db = _contextFactory.Create();

            var issue = await db.Issues.SingleAsync(a => a.Id == id);
            issue.IssueStatusId = issueStatusId;
            issue.LastModifiedDate = DateTimeOffset.UtcNow;

            await db.SaveChangesAsync();
        }

        public async Task<IssueDetailsDto> GetIssue(int id)
        {
            await using var db = _contextFactory.Create();

            return await db.Issues
                .Select(x => new IssueDetailsDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.RawText,
                    Reference = x.CaseReference,
                    StatusId = x.IssueStatusId,
                    AssignedUserId = x.AssignedUserId,
                    CreatedByUserId = x.CreatedByUserId,
                    ProjectId = x.ProjectId,
                    Labels = x.LabelIssueMappings.Select(a => new LabelDto()
                    {
                        Id = a.LabelId,
                        Name = a.Label.Name
                    }).ToList()
                })
                .SingleAsync(a => a.Id == id);
        }

        public async Task AssignToUser(int issueId, int currentUserId)
        {
            await using var db = _contextFactory.Create();

            var issue = await db.Issues.Where(x => x.Id == issueId)
                .SingleAsync();

            issue.AssignedUserId = currentUserId;
            await db.SaveChangesAsync();
        }

        public async Task ReorderIssues(List<int> issueIds)
        {
            await using var db = _contextFactory.Create();

            var issues = await db.Issues
                .Where(x => issueIds.Contains(x.Id))
                .ToListAsync();

            var i = 0;
            foreach (var id in issueIds)
            {
                var issue = issues.Single(x => x.Id == id);
                issue.Priority = ++i;
            }

            await db.SaveChangesAsync();
        }
    }
}