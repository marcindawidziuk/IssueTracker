using Microsoft.AspNetCore.Authentication;

namespace IssueTracker.Api.Authentication
{
    public class ApiAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public const string DefaultSchemeName = "DefaultApiAuthenticationScheme";
        public const string TokenHeaderName = "AUTH-TOKEN";
    }
}