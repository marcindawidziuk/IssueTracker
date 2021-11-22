namespace IssueTracker.Data
{
    public class LabelIssueMapping
    {
        public int Id { get; set; }
        public int LabelId { get; set; }
        public virtual Label Label { get; set; }
        
        public int IssueId { get; set; }
        public virtual Issue Issue { get; set; }
    }
}