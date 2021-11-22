using System.Collections.Generic;
using System.Threading.Tasks;
using IssueTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Api.Controllers
{
    public class ProjectsController : AuthorisedController
    {
        private readonly IProjectsService _projectsService;
        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet("get-all")]
        public Task<List<ProjectDto>> GetAllProjects()
        {
            return _projectsService.GetAllProjects();
        }

        [HttpPost("add")]
        public Task<int> AddProject(AddProjectDto dto, int userId)
        {
            return _projectsService.AddProject(dto, userId);
        }

        [HttpPost("update")]
        public Task<int> EditProject(EditProjectDto dto, int userId)
        {
            return _projectsService.EditProject(dto, userId);
        }
    }
}