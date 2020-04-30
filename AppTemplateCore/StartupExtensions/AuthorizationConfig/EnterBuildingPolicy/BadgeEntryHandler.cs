using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public class BadgeEntryHandler : AuthorizationHandler<EnterBuildingRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EnterBuildingRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Country &&
                                           c.Issuer == "http://microsoftsecurity"))
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }

            return Task.CompletedTask;
        }
    }
}
