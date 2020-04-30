using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public class IsBannedAuthorizationHandler : AuthorizationHandler<IsVipRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsVipRequirement requirement)
        {
            if (context.User.HasClaim(claim => claim.Type == "IsBannedFromVIP"))
            {
                context.Fail();
            }
            return Task.FromResult(0);
        }


    }
}
