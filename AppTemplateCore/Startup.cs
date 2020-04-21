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
using AppTemplateCore.StartupExtensions;

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

            services.Add_Cookie_Policy();


            services.Add_DBContext_Config(Configuration, _contentRootPath);


            services.Add_Identity_Config();


            services.Add_MVC_Config();


            services.Add_Authentication_Config();


            services.App_Cookie_Config();


            services.Add_Authorization_Config();


            services.Add_AppServices_Config();

            

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
