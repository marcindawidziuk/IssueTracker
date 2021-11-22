using System;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Data;
using IssueTracker.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Services.Authentication
{
    public interface ILoginService
    {
        Task Logout(string accessToken);
        Task<string> Login(string email, string password);
    }
    
    public class LoginService : ILoginService
    {
        private readonly IContextFactory _contextFactory;
        private readonly IHashService _hashService;
        private readonly IApiTokenService _apiTokenService;

        public LoginService(IContextFactory contextFactory, IHashService hashService, IApiTokenService apiTokenService)
        {
            _contextFactory = contextFactory;
            _hashService = hashService;
            _apiTokenService = apiTokenService;
        }

        public async Task Logout(string accessToken)
        {
            await using var db = _contextFactory.Create();

            var apiToken = await db.ApiTokens.SingleAsync(x => x.Token == accessToken);
            
            if (apiToken.ExpiryDateUtc > DateTime.UtcNow)
                return;
            
            apiToken.ExpiryDateUtc = DateTime.UtcNow;

            await db.SaveChangesAsync();
        }

        public async Task<string> Login(string email, string password)
        {
            await using var db = _contextFactory.Create();

            var user = await db.Users
                .Where(x => x.Email == email)
                .SingleOrDefaultAsync();
            if (user == null)
                return string.Empty;

            var isValid = _hashService.IsValid(password, user.Password);
            if (!isValid)
                return string.Empty;

            var token = _apiTokenService.GenerateToken();
            var apiToken = new ApiToken
            {
                UserId = user.Id,
                ExpiryDateUtc = DateTime.UtcNow + TimeSpan.FromDays(14),
                Token = token
            };

            db.ApiTokens.Add(apiToken);

            await db.SaveChangesAsync();
            return apiToken.Token;
        }
    }
}