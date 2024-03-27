using Microsoft.AspNetCore.Authorization;

namespace Thrifty.Authentication
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; }

        public RoleRequirement(string role)
        {
            Role = role;
        }
    }
}
