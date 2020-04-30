using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public class RolesAndClaimsEditRequirement : IAuthorizationRequirement
    {

        // Only a marker interface
        // This requirement is handled by two handlers.
        // 1- CanEditOnlyOtherAdminRolesAndClaimsHandler
        // 2- SuperAdminHandler

    }
}
