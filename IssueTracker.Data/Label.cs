using System.Collections.Generic;

namespace IssueTracker.Data
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        
        public ICollection<LabelIssueMapping> LabelIssueMappings { get; set; }
    }
}