using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Data;
using IssueTracker.Data.Infrastructure;
using IssueTracker.Services.Dtos;

namespace IssueTracker.Services.Authentication
{
    public interface IRegistrationService
    {
        Task<ServiceReponse> Register(string name, string email, string password);
    }
    
    public class RegistrationService : IRegistrationService
    {
        private readonly IContextFactory _contextFactory;
        private readonly IHashService _hashService;

        public RegistrationService(IContextFactory contextFactory, IHashService hashService)
        {
            _contextFactory = contextFactory;
            _hashService = hashService;
        }

        public async Task<ServiceReponse> Register(string name, string email, string password)
        {
            await using var db = _contextFactory.Create();

            if (db.Users.Any(x => x.Email == email))
                return ServiceReponse.Fail("Email already registered");

            var hashedPassword = _hashService.Generate(password);
            
            //TODO: Email, Password Validation
            if (password.Length < 5)
                return ServiceReponse.Fail("Password too short");

            var user = new User
            {
                Name = name,
                Email = email,
                Password = hashedPassword
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return ServiceReponse.Ok();
        }
    }
}