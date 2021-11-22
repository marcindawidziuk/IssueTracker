using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Data.Infrastructure;
using IssueTracker.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Services
{
    public interface IAccountService
    {
        Task<AccountInfoDto> GetInfo(int userId);
    }
    
    public class AccountService : IAccountService
    {
        private readonly IContextFactory _contextFactory;
        public AccountService(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<AccountInfoDto> GetInfo(int userId)
        {
            await using var db = _contextFactory.Create();

            return await db.Users.Where(x => x.Id == userId)
                .Select(x => new AccountInfoDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.Name
                }).SingleAsync();
        }
    }
}