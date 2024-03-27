using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Thrifty.Authentication
{
    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            
            if(requirement.Role.Contains(','))
            {
                requirement.Role.Split(',').ToList().ForEach(role =>
                {
                    if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == role))
                    {
                        context.Succeed(requirement);
                    }
                });
            }else
            {
                if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == requirement.Role))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
