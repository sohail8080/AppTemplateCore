using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public class IsCEOAuthorizationHandler : AuthorizationHandler<IsVipRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsVipRequirement requirement)
        {
            if (context.User.IsInRole("CEO"))
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }


    }
}
