using System;
using System.Collections.Generic;

namespace IssueTracker.Data
{
    public class Issue
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        
        public int CaseNumber { get; set; }
        public string CaseReference { get; set; }
        
        public string Title { get; set; }
        
        public string RawText { get; set; }
        
        public int? AssignedUserId { get; set; }
        public virtual User AssignedUser { get; set; }
        
        public int CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        
        public int IssueStatusId { get; set; }
        public virtual IssueStatus IssueStatus { get; set; }
        
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
        
        public ICollection<LabelIssueMapping> LabelIssueMappings { get; set; }

        public void SetCaseNumber(int caseNumber, string projectAbbreviation)
        {
            CaseNumber = caseNumber;
            CaseReference = $"{projectAbbreviation}-{caseNumber}";
        }
    }

    public class IssueStatus
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        
        public ICollection<Issue> Issues { get; set; }
    }

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

    public class ProjectUserMappings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
