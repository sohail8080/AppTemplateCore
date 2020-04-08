using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppTemplateCore.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppTemplateCore.Areas.AccessControl.Models;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace AppTemplateCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _contentRootPath = env.ContentRootPath;
        }

        private string _contentRootPath = "";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. 
        // Use this method to add services to the container.
        // 1- Add Default Services for Middleware
        // 2- Add Configured Services for Middleware
        // 3- Add Application Services needed in Application Code
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Services & Configure their Settings needed by Cookie Middleware
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString(DefaultConnection)));


            string DefaultConnection = Configuration.GetConnectionString("DefaultConnection");
            if (DefaultConnection.Contains("%CONTENTROOTPATH%"))
            {
                DefaultConnection = DefaultConnection.Replace("%CONTENTROOTPATH%", _contentRootPath);
            }

            // Add UOW Service to DI Container & Configure its Settings
            // This Service is now available throughout application Pages/Controllers
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Hooking UOW to the SQL Server
                // Configures UOW to connect to a Microsoft SQL Server database.
                // Set Connection String of the SQL Server we want to hook
                options.UseSqlServer(DefaultConnection);
            });


            // Add Services & Configure their Settings needed by Identity Middleware
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;

                options.Lockout = new LockoutOptions()
                {
                    AllowedForNewUsers = true,
                    DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0),
                    MaxFailedAccessAttempts = 5
                };
            })
                // Adds a default, self-contained UI for Identity to the application using Razor
                // Pages in an area named Identity.
                .AddDefaultUI(UIFramework.Bootstrap4)
                // Adds an Entity Framework implementation of identity information stores.
                // User EF + this UOW to persist the Identity Info
                .AddEntityFrameworkStores<ApplicationDbContext>();


            //services.Configure<IdentityOptions>(
            //    options =>
            //    {
            //        options.Password.RequiredLength = 6;
            //        options.Password.RequiredUniqueChars = 1;
            //        options.Password.RequireNonAlphanumeric = true;
            //        options.Password.RequireLowercase = true;
            //        options.Password.RequireUppercase = true;
            //        options.Password.RequireDigit = true;

            //        options.Lockout = new LockoutOptions()
            //        {
            //            AllowedForNewUsers = true,
            //            DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0),
            //            MaxFailedAccessAttempts = 5
            //        };
            //    });


            // Add Services & Configure their Settings needed by MVC Middleware
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //        {
            //            options.ClientId = "443892072779-3n0ljac2jnar4kmnnlkn74h4ic1tdq54.apps.googleusercontent.com";
            //            options.ClientSecret = "7C6TvX2SWEodUuXd3EpsoO1R";
            //        })
            //    .AddFacebook(options =>
            //        {
            //            options.AppId = "2316662895109472";
            //            options.AppSecret = "e25c1b8d4145034ed426d7a05efe1481";
            //        });


            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Error/AccessDenied");
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.RequireClaim("Edit Role"));
                //policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                options.AddPolicy("AdminRolePolicy",
                        policy => policy.RequireRole("Admin"));
            });

            //services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

        }

        // This method gets called by the runtime. 
        // Use this method to configure the HTTP request pipeline.
        // 1- Add Default Middleware in Request Processing Pipleline
        // 2- Add Configured Middleware in Requst Processing Pipleine
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");

                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. 
                // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // Save the Domain Info of Users Website and 
                // Always use the HTTPS
                // If the User Browser support HSTS, then this setting is applied
                app.UseHsts();
            }

            // Adds middleware for redirecting HTTP Requests to HTTPS.
            app.UseHttpsRedirection();

            // Enables static file serving for the current request path
            app.UseStaticFiles();

            // Enables cookie policy capabilities.
            app.UseCookiePolicy();

            // Enables authentication capabilities.
            app.UseAuthentication();

            // Add MVC to the request execution pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
