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
        Task<int> EditProject(EditProjectDto dto, int userId);
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
    
    public class EditProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
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

        public async Task<int> EditProject(EditProjectDto dto, int userId)
        {
            await using var db = _contextFactory.Create();

            var project = await db.Projects.SingleAsync(x => x.Id == dto.Id);
            // if (userId == project.AdminUserId)
            // {
            //     throw new InvalidOperationException("Only admin can edit");
            // }
            
            project.Name = dto.Name;
            project.Abbreviation = dto.Abbreviation;

            var issues = await db.Issues
                .Where(x => x.ProjectId == project.Id)
                .Where(x => x.CaseReference.StartsWith(project.Abbreviation) == false)
                .ToListAsync();
            foreach (var issue in issues)
            {
                issue.SetCaseNumber(issue.CaseNumber, project.Abbreviation);
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