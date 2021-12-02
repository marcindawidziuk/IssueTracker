using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IssueTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<ApiToken> ApiTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueStatus> IssueStatuses { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUserMappings> ProjectUserMappings { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<LabelIssueMapping> LabelIssueMappings { get; set; }
    }
    
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext> 
    { 
        public ApplicationDbContext CreateDbContext(string[] args) 
        { 
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>(); 
            builder.UseNpgsql("User ID=postgres;Password=password;Server=localhost;Port=5432;Database=IssueTracker;Integrated Security=true;Pooling=true;"); 
            return new ApplicationDbContext(builder.Options); 
        } 
    }
}