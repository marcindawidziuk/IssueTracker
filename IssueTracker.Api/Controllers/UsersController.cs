using System.Collections.Generic;
using System.Threading.Tasks;
using IssueTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Api.Controllers
{
    public class UsersController : AuthorisedController
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("users-for-project")]
        public async Task<List<ProjectUserDto>> GetUsersForProject(int projectId)
        {
            return await _usersService.GetUsersForProject(projectId);
        }

        [HttpPost("add-user-to-project")]
        public async Task<bool> AddUserToProject(string email, int projectId)
        {
            return await _usersService.AddUserToProject(email, projectId);
        }
        
    }
}