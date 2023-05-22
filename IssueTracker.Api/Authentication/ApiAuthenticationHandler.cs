using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IssueTracker.Data.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IssueTracker.Api.Authentication
{
    public class ApiAuthenticationHandler : AuthenticationHandler<ApiAuthenticationSchemeOptions>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiAuthenticationHandler(IOptionsMonitor<ApiAuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IServiceScopeFactory serviceScopeFactory,
            IHttpContextAccessor httpContextAccessor)
            : base(options, logger, encoder, clock)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var request = _httpContextAccessor.HttpContext?.Request;

            if (request is not null && !request.Path.StartsWithSegments("/api"))
            {
                return AuthenticateResult.NoResult();
            }

            if (!Request.Headers.ContainsKey(ApiAuthenticationSchemeOptions.TokenHeaderName))
                return AuthenticateResult.Fail("Missing authentication header");

            var tokenCollection = Request.Headers[ApiAuthenticationSchemeOptions.TokenHeaderName];

            if (!tokenCollection.Any())
                return AuthenticateResult.Fail("Missing authentication header");

            var token = tokenCollection[0];

            if (string.IsNullOrWhiteSpace(token))
                return AuthenticateResult.Fail("Missing token");

            using var scope = _serviceScopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<IContextFactory>();

            await using var db = service.Create();

            var apiToken = await db.ApiTokens
                .Where(x => x.Token == token)
                .SingleOrDefaultAsync();
            
            if (apiToken == null)
                return AuthenticateResult.Fail("No token found");

            if (apiToken.ExpiryDateUtc < DateTime.UtcNow)
            {
                return AuthenticateResult.NoResult();
                // return AuthenticateResult.Fail("Expired token");
            }

            var user = await db.Users
                .Where(x => x.Id == apiToken.UserId)
                .Select(x => new AuthenticationHandlerUser(x.Id, x.Email, x.Name))
                .SingleOrDefaultAsync();

            if (user is null)
            {
                return AuthenticateResult.Fail("User not found");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email)
            };

            var id = new ClaimsIdentity(claims, Scheme.Name);

            var principal = new ClaimsPrincipal(id);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        public record AuthenticationHandlerUser(int Id, string Email, string Name);
    }
}