using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Data;
using IssueTracker.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Services
{
    public interface IIssuesService
    {
        Task<List<IssueDto>> GetIssuesForProject(int projectId);
        Task<int> AddIssue(AddIssueDto dto);
        Task UpdateIssue(UpdateIssueDto dto);
    }

    public class IssueDto
    {
        public int Id { get; set; }
        public string CaseReference { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class AddIssueDto
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int? AssignedUserId { get; set; }
        public int StatusId { get; set; }
    }
    
    public class UpdateIssueDto
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int? AssignedUserId { get; set; }
        public int StatusId { get; set; }
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
                .Select(x => new IssueDto
                {
                    Id = x.Id,
                    CaseReference = x.CaseReference,
                    Title = x.Title,
                    StatusId = x.IssueStatusId,
                    StatusName = x.IssueStatus.Name
                }).ToListAsync();

        }

        public async Task<int> AddIssue(AddIssueDto dto)
        {
            await using var db = _contextFactory.Create();

            var caseNumber = await db.Issues
                .Select(x => x.CaseNumber)
                .DefaultIfEmpty()
                .MaxAsync();

            var project = await db.Projects.SingleAsync(x => x.Id == dto.ProjectId);

            var issue = new Issue();
            issue.SetCaseNumber(++caseNumber, project.Abbreviation);
            issue.Title = dto.Title;
            issue.RawText = dto.Text;
            issue.AssignedUserId = dto.AssignedUserId;
            issue.IssueStatusId = dto.StatusId;
            db.Issues.Add(issue);
            
            await db.SaveChangesAsync();
            return issue.Id;
        }

        public async Task UpdateIssue(UpdateIssueDto dto)
        {
            await using var db = _contextFactory.Create();

            var issue = new Issue();
            issue.AssignedUserId = dto.AssignedUserId;
            issue.Title = dto.Title;
            issue.RawText = dto.Text;
            issue.IssueStatusId = dto.StatusId;

            await db.SaveChangesAsync();
        }
    }
}