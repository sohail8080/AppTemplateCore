using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public class IsAirlineEmployeeAuthorizationHandler : AuthorizationHandler<IsVipRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsVipRequirement requirement)
        {
            if (context.User.HasClaim(claim =>
                claim.Type == "EmployeeNumber" && claim.Issuer == requirement.Airline))
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }


    }
}
