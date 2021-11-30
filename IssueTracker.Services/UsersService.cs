using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Data;
using IssueTracker.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Services
{
    public interface IUsersService
    {
        Task<List<ProjectUserDto>> GetUsersForProject(int projectId);
        Task<bool> AddUserToProject(string email, int projectId);
    }

    public class ProjectUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    
    public class UsersService : IUsersService
    {
        private readonly IContextFactory _contextFactory;
        public UsersService(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<ProjectUserDto>> GetUsersForProject(int projectId)
        {
            await using var db = _contextFactory.Create();
            return await db.ProjectUserMappings
                .Where(x => x.ProjectId == projectId)
                .Select(x => new ProjectUserDto
                {
                    Id = x.UserId,
                    Name = x.User.Name,
                    Email = x.User.Email
                }).ToListAsync();

        }

        public async Task<bool> AddUserToProject(string email, int projectId)
        {
            await using var db = _contextFactory.Create();

            var user = await db.Users
                .SingleOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return false;

            var userMapping = await db.ProjectUserMappings
                .Where(x => x.UserId == user.Id)
                .Where(x => x.ProjectId == projectId)
                .SingleOrDefaultAsync();

            if (userMapping != null)
                return true;

            var newUserMapping = new ProjectUserMappings()
            {
                UserId = user.Id,
                ProjectId = projectId
            };
            db.ProjectUserMappings.Add(newUserMapping);
            await db.SaveChangesAsync();
            return true;
        }
    }
}