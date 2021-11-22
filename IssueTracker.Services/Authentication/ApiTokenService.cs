using System;
using System.Linq;

namespace IssueTracker.Services.Authentication
{
    public interface IApiTokenService
    {
        string GenerateToken();
    }
    
    public class ApiTokenService : IApiTokenService
    {
        public string GenerateToken()
        {
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }
    }
}