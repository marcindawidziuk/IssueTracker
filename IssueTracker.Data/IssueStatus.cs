using System.Collections.Generic;

namespace IssueTracker.Data
{
    public class IssueStatus
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        
        public ICollection<Issue> Issues { get; set; }
    }
}