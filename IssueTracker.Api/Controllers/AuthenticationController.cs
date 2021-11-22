using System.Threading.Tasks;
using IssueTracker.Services.Authentication;
using IssueTracker.Services.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IRegistrationService _registrationService;

        public AuthenticationController(ILoginService loginService, IRegistrationService registrationService)
        {
            _loginService = loginService;
            _registrationService = registrationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<string> Login([FromBody] LoginRequest request)
        {
            return await _loginService.Login(request.Email, request.Password);
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<ServiceReponse> Register([FromBody] RegisterRequest request)
        {
            return await _registrationService.Register(request.Name, request.Email, request.Password);
        }
    }
}