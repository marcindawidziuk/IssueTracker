using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NJsonSchema.Annotations;

namespace IssueTracker.Services
{
    public interface IIssueStatusService
    {
        Task<List<IssueStatusDto>> GetStatusesForProject(int projectId);
    }

    public class IssueStatusDto
    {
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
    }
    
    public class IssueStatusService : IIssueStatusService
    {
        private readonly IContextFactory _contextFactory;
        public IssueStatusService(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<IssueStatusDto>> GetStatusesForProject(int projectId)
        {
            await using var db = _contextFactory.Create();

            return await db.IssueStatuses
                .Where(x => x.ProjectId == projectId)
                .OrderBy(x => x.Priority)
                .Select(x => new IssueStatusDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();


        }
    }
}