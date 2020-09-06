using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;

namespace Aio.Presentation.Web {
    public class AdminPolicyRequirement : AuthorizationHandler<AdminPolicyRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminPolicyRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}