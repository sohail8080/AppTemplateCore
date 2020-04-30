using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

                // A user can access the UI/Controller Action related to Creating Role Entity
                // Only if he is in Amdin Role & has Create Role Claim
                options.AddPolicy(PolicyStore.Create_Role_Policy,
                    policy => policy.RequireRole(RolesStore.Admin)
                                    .RequireClaim(ClaimsStore.Create_Role));

                // A user can access the UI/Controller Action related to Editing Role Entity
                // Only if he is in Amdin Role & has Edit Role Claim
                options.AddPolicy(PolicyStore.Edit_Role_Policy,
                    policy => policy.RequireRole(RolesStore.Admin)
                                    .RequireClaim(ClaimsStore.Edit_Role));


                // A user can access the UI/Controller Action related to Deleting Role Entity
                // Only if he is in Amdin Role & has Delete Role Claim
                options.AddPolicy(PolicyStore.Delete_Role_Policy,
                    policy => policy.RequireRole(RolesStore.Admin)
                                    .RequireClaim(ClaimsStore.Delete_Role));


                // A user can access the UI/Controller Action related to Website Admin Module
                // Only if he is in Amdin Role & has Create/Edit/Delete Role Claim
                // AND condition between Role and Claim Conditions
                options.AddPolicy(PolicyStore.Admin_Role_Policy,
                    policy => policy.RequireRole(RolesStore.Admin)
                                    .RequireClaim(ClaimsStore.Create_Role)
                                    .RequireClaim(ClaimsStore.Edit_Role)
                                    .RequireClaim(ClaimsStore.Delete_Role));


                options.AddPolicy(PolicyStore.Roles_And_Claims_Edit_Policy,
                             policy => policy.AddRequirements(new RolesAndClaimsEditRequirement()));


                // Mixed ADN n OR condition between the Role and Claims Conditions
                options.AddPolicy("PolicyName", policy => policy.RequireAssertion(context =>
                            context.User.IsInRole(RolesStore.Admin) &&
                            context.User.HasClaim(claim => claim.Type == ClaimsStore.Edit_Role && claim.Value == "true") ||
                            context.User.IsInRole(RolesStore.Super_Admin)
                    ));


                // Seperated Method: Mixed ADN n OR condition between the Role and Claims Conditions
                options.AddPolicy("PolicyName", policy => policy.RequireAssertion(context =>
                            AuthorizeAccess(context)
                    ));


                // OR condition between the Claim and it Values, If any value of claim, get the resource
                options.AddPolicy("Founders", policy =>
                        policy.RequireClaim("EmployeeNumber", "1", "2", "3", "4", "5"));


                // OR Condition between Roles values, Any one Role need to access the Resources
                options.AddPolicy("ElevatedRights", policy =>
                        policy.RequireRole("Administrator", "PowerUser", "BackupAdministrator"));

                //policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));


            });


            // Auth Handlers are registered as a service
            services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddSingleton<IAuthorizationHandler, HasAdminRoleHandler>();

        }

        // Func encapsulated in its own method
        private static bool AuthorizeAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole(RolesStore.Admin) &&
                            context.User.HasClaim(claim => claim.Type == ClaimsStore.Edit_Role && claim.Value == "true") ||
                            context.User.IsInRole(RolesStore.Normal_User);
        }



        public static void Add_Authorization_Config2(this IServiceCollection services)
        {
            string domain = string.Empty;//$"https://{Configuration["Auth0:Domain"]}/";

            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("DeleteRole"));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.AddRequirements(new RolesAndClaimsEditRequirement()));

                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin"));

                // Policy based on User Profile Data added in the Claim List
                // Following states that Logged in User must have EmployeeNumber

                options.AddPolicy("EmployeeOnly",
                        policy => policy.RequireClaim("EmployeeNumber"));

                //[Authorize]
                //[Authorize(Policy = "EmployeeOnly")]
                //  [AllowAnonymous]
                // OR Contidtion [Authorize(Roles = "SuperAdministrator, ChannelAdministrator")]

                //This controller would be only accessible by users who are members of 
                //the HRManager role or the Finance role.
                //[Authorize(Roles = "HRManager,Finance")]

                // AND Condition.When Stacked, then to gain access,
                //a user must be a member of both the PowerUser and ControlPanelUser role.
                //[Authorize(Roles = "PowerUser")]
                //[Authorize(Roles = "ControlPanelUser")]

                options.AddPolicy("ElevatedRights", policy =>
                        policy.RequireRole("Administrator", "PowerUser", "BackupAdministrator"));


                options.AddPolicy("Something",
                        policy => policy.RequireClaim("Permission", "CanViewPage", "CanViewAnything"));


                options.AddPolicy("Founders", policy =>
                         policy.RequireClaim("EmployeeNumber", "1", "2", "3", "4", "5"));


                options.AddPolicy("EmployeesOnly", policy => policy.RequireClaim("EmployeeId"));


                //[Authorize(Policy = "EmployeesOnly")]
                //[Authorize(Policy = "Over21Only")]

                options.AddPolicy("Over21Only",
                       policy => policy.Requirements.Add(new MinimumAgeRequirement(21)));


                options.AddPolicy("read:messages",
                    policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));

                options.AddPolicy("create:messages", policy => policy.Requirements.Add(new HasScopeRequirement("create:messages", domain)));

                options.AddPolicy("EmployeesOnly", policy => policy.RequireClaim("EmployeeId"));


                options.AddPolicy("CanAccessVIPArea",
                   policyBuilder => policyBuilder.RequireAssertion( context => context.User.HasClaim(
                       claim => claim.Type == "VIPNumber" || claim.Type == "EmployeeNumber")
                        || context.User.IsInRole("CEO"))
                    );


                options.AddPolicy(
           "CanAccessVIPArea",
           policyBuilder => policyBuilder.AddRequirements(
               new IsVipRequirement("British Airways")));



                options.AddPolicy("BadgeEntry",
                         policy => policy.RequireAssertion(context =>
                                 context.User.HasClaim(c =>
                                    (c.Type == ClaimTypes.Country ||
                                     c.Type == ClaimTypes.DateOfBirth)
                                     && c.Issuer == "https://microsoftsecurity")));




            });

            //[Authorize("read:messages")]
            //[Authorize("create:messages")]
            //[Authorize("CanAccessVIPArea")]


        services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


            services.AddSingleton<IAuthorizationHandler, IsCEOAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, HasVIPNumberAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, IsAirlineEmployeeAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, IsBannedAuthorizationHandler>();



        }

    }
}
