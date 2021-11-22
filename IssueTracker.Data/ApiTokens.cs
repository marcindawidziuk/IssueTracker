using System;

namespace IssueTracker.Data
{
    public class ApiToken
    {
        public int Id { get; set; }
        public string Token { get; set; }

        public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime ExpiryDateUtc { get; set; }
        
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}