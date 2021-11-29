using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Data;
using IssueTracker.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Services
{
    public interface IProjectsService
    {
        Task<List<ProjectDto>> GetAllProjects();
        Task<int> AddProject(AddProjectDto dto, int userId);
        Task<int> EditProject(UpdateProjectDto dto, int userId);
        Task<ProjectDetailsDto> GetDetails(int id);
    }

    public class ProjectDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }

    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AddProjectDto
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public List<CreateIssueStatusDto> Statuses { get; set; }
    }

    public class CreateIssueStatusDto
    {
        public int Priority { get; set; }
        public string Name { get; set; }
    }
    
    public class UpdateProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public List<UpdateProjectStatusDto> Statuses { get; set; }

        public class UpdateProjectStatusDto
        {
            public int? Id { get; set; }
            public string Name { get; set; }
        }
    }
    
    public class ProjectsService : IProjectsService
    {
        private readonly IContextFactory _contextFactory;
        public ProjectsService(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<ProjectDto>> GetAllProjects()
        {
            await using var db = _contextFactory.Create();

            return await db.Projects
                .Select(x => new ProjectDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
        }

        public async Task<int> AddProject(AddProjectDto dto, int userId)
        {
            await using var db = _contextFactory.Create();

            var project = new Project
            {
                Name = dto.Name,
                Abbreviation = dto.Abbreviation,
                AdminUserId = userId
            };
            var statuses = dto.Statuses
                .Select(x => new IssueStatus
                {
                    Priority = x.Priority,
                    Name = x.Name,
                    Project = project
                }).ToList();
            
            db.IssueStatuses.AddRange(statuses);
            
            db.Projects.Add(project);
            await db.SaveChangesAsync();
            return project.Id;
        }

        public async Task<int> EditProject(UpdateProjectDto dto, int userId)
        {
            await using var db = _contextFactory.Create();

            var project = await db.Projects.SingleAsync(x => x.Id == dto.Id);
            // if (userId == project.AdminUserId)
            // {
            //     throw new InvalidOperationException("Only admin can edit");
            // }
            
            project.Name = dto.Name;
            project.Abbreviation = dto.Abbreviation;

            var statuses = await db.IssueStatuses
                .Where(x => x.ProjectId == project.Id)
                .ToListAsync();

            var statusPriority = 0;

            var statusIds = dto.Statuses.Select(x => x.Id).ToList();
            var statusesToRemove = statuses.Where(x => statusIds.Contains(x.Id) == false).ToList();
            db.IssueStatuses.RemoveRange(statusesToRemove);
                
            foreach (var updateProjectStatusDto in dto.Statuses)
            {
                var existingStatus = statuses.SingleOrDefault(x => x.Id == updateProjectStatusDto.Id);
                if (existingStatus == null)
                {
                    existingStatus = new IssueStatus
                    {
                        Project = project
                    };
                    db.IssueStatuses.Add(existingStatus);
                }

                existingStatus.Name = updateProjectStatusDto.Name;
                existingStatus.Priority = statusPriority++;
            }

            var issues = await db.Issues
                .Where(x => x.ProjectId == project.Id)
                .Where(x => x.CaseReference.StartsWith(project.Abbreviation) == false
                    || statusIds.Contains(x.IssueStatusId) == false)
                .ToListAsync();
            foreach (var issue in issues)
            {
                issue.SetCaseNumber(issue.CaseNumber, project.Abbreviation);

                var shouldChangeStatus = statusIds.Contains(issue.IssueStatusId) == false;
                if (shouldChangeStatus)
                {
                    issue.IssueStatus = statuses.First();
                }
            }
            
            await db.SaveChangesAsync();
            return project.Id;
        }

        public async Task<ProjectDetailsDto> GetDetails(int id)
        {
            await using var db = _contextFactory.Create();

            return await db.Projects
                .Where(x => x.Id == id)
                .Select(x => new ProjectDetailsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Abbreviation = x.Abbreviation
                }).SingleAsync();

        }
    }
}