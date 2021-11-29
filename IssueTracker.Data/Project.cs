using System.Collections.Generic;

namespace IssueTracker.Data
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // two-five letters used for case numbers
        public string Abbreviation { get; set; }
        
        public int AdminUserId { get; set; }
        public virtual User AdminUser { get; set; }
        
        public virtual ICollection<ProjectUserMappings> ProjectUserMappings { get; set; }
    }
}