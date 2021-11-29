﻿namespace IssueTracker.Data
{
    public class ProjectUserMappings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}