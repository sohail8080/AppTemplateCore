using AppTemplateCore.Areas.AccessControl.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public class HasAdminRoleHandler : AuthorizationHandler<RolesAndClaimsEditRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RolesAndClaimsEditRequirement requirement)
        {

            if(context.User.IsInRole(RolesStore.Admin))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
