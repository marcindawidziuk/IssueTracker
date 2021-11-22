using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Data.Infrastructure
{
    public interface IContextFactory
    {
        ApplicationDbContext Create();
    }
    
    public class ContextFactory : IContextFactory
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public ContextFactory(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public ApplicationDbContext Create()
        {
            return _contextFactory.CreateDbContext();
        }
    }
}