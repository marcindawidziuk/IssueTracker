﻿using System.Collections.Generic;
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
        public Task<int> AddProject(AddProjectDto dto)
        {
            return _projectsService.AddProject(dto, CurrentUserId());
        }

        [HttpPost("update")]
        public Task<int> UpdateProject(UpdateProjectDto dto)
        {
            return _projectsService.UpdateProject(dto, CurrentUserId());
        }
        
        [HttpPost("details/{id}")]
        public Task<ProjectDetailsDto> Details(int id)
        {
            return _projectsService.GetDetails(id);
        }
    }
}