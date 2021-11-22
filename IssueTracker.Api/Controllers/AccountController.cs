using System.Threading.Tasks;
using IssueTracker.Services;
using IssueTracker.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;

namespace IssueTracker.Api.Controllers
{
    public class AccountController : AuthorisedController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("get-info")]
        public Task<AccountInfoDto> GetInfo()
        {
            return _accountService.GetInfo(CurrentUserId());
        }
    }
}