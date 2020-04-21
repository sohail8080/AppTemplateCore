using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public static class AuthorizationConfig
    {

        public static void Add_Authorization_Config(this IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {
                // Policy is about grouping multiple Authorization Rights under 
                // One umbrella of Policy. On Controller/Action Method level we 
                // Only apply that Policy, the whole group of Auth Rules are applied
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.RequireClaim("Edit Role"));
                //policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                options.AddPolicy("AdminRolePolicy",
                        policy => policy.RequireRole("Admin"));
            });


        }


        public static void Add_Authorization_Config2(this IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin"));
            });


        }

    }  
}
