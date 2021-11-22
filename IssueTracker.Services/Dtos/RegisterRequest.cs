using System.Diagnostics.Contracts;

namespace IssueTracker.Services.Dtos
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ServiceReponse
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }

        public static ServiceReponse Fail(string message)
        {
            return new()
            {
                IsSuccessful = false,
                ErrorMessage = message
            };
        }

        public static ServiceReponse Ok()
        {
            return new()
            {
                IsSuccessful = true
            };
        }
    }
}