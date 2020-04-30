using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public class CanEditOnlyOtherAdminRolesAndClaimsHandler :
        AuthorizationHandler<RolesAndClaimsEditRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                             RolesAndClaimsEditRequirement requirement)
        {
            // Resource/Controller Action/Controller on which Authorization is being applied
            var authFilterContext = context.Resource as AuthorizationFilterContext;

            // Resource is null, then Authorization failed
            if (authFilterContext == null)
            {
                return Task.CompletedTask;
            }

            // Get the ID if logged in User.
            string loggedInAdminId =
                context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            // Get the ID of User being edited from Query String.
            string adminIdBeingEdited = authFilterContext.HttpContext.Request.Query["userId"];

            // Condition for Authorization 
            if (context.User.IsInRole("Admin") &&
            context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") &&
            adminIdBeingEdited.ToLower() != loggedInAdminId.ToLower())
            {
                // Auth condition satisfied, grant access to resource
                context.Succeed(requirement);
            }

            // Auth condition failed, deny access to the resource
            return Task.CompletedTask;
        }
    }
}
