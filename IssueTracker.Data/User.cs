﻿using System.Collections.Generic;

namespace IssueTracker.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public virtual ICollection<ApiToken> ApiTokens { get; set; }
    }
}