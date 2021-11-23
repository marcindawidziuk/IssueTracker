using System.Collections.Generic;
using System.Threading.Tasks;
using IssueTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Api.Controllers
{
    public class IssueStatusesController : AuthorisedController
    {
        private readonly IIssueStatusService _statusIssueService;
        public IssueStatusesController(IIssueStatusService statusIssueService)
        {
            _statusIssueService = statusIssueService;
        }

        [HttpGet("get-for-project/{projectId}")]
        public Task<List<IssueStatusDto>> GetStatusesForProject(int projectId)
        {
            return _statusIssueService.GetStatusesForProject(projectId);
        }
    }
}