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
        
        [HttpGet("get")]
        public async Task<IssueDetailsDto> GetDetails(int issueId)
        {
            return await _issuesService.GetIssue(issueId);
        }

        [HttpGet("search")]
        public Task<List<IssueDto>> GetIssuesForProject(int projectId)
        {
            return _issuesService.GetIssuesForProject(projectId);
        }

        [HttpPost("add")]
        public async Task<int> AddIssue(AddIssueDto dto)
        {
            return await _issuesService.AddIssue(dto, CurrentUserId());
        }
        
        [HttpPost("update")]
        public async Task UpdateIssue(UpdateIssueDto dto)
        {
            await _issuesService.UpdateIssue(dto);
        }
        
        [HttpPost("update-status")]
        public async Task UpdateIssue(int issueId, int issueStatusId)
        {
            await _issuesService.UpdateIssueStatus(issueId, issueStatusId);
        }
    }
}