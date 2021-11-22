using System.Collections.Generic;
using System.Threading.Tasks;
using IssueTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Api.Controllers
{
    public class IssuesController : AuthorisedController
    {
        private readonly IIssuesService _issuesService;
        public IssuesController(IIssuesService issuesService)
        {
            _issuesService = issuesService;
        }

        [HttpGet("search")]
        public Task<List<IssueDto>> GetIssuesForProject(int projectId)
        {
            return _issuesService.GetIssuesForProject(projectId);
        }

        [HttpPost("add")]
        public async Task<int> AddIssue(AddIssueDto dto)
        {
            return await _issuesService.AddIssue(dto);
        }
        
        [HttpPost("update")]
        public async Task UpdateIssue(UpdateIssueDto dto)
        {
            await _issuesService.UpdateIssue(dto);
        }
    }
}