using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public static class CookiePolicyConfig
    {
        public static void Add_Cookie_Policy(this IServiceCollection services)
        {

            // Add Services & Configure their Settings needed by Cookie Middleware
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies 
                //is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

        }



        public static void Add_Cookie_Policy2(this IServiceCollection services)
        {

            // Add Services & Configure their Settings needed by Cookie Middleware
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies 
                //is needed for a given request.
                // Summary:
                //     Checks if consent policies should be evaluated on this request. 
                //The default is
                //     false.
                options.CheckConsentNeeded = context => true;

                // Summary:
                //     Affects the cookie's same site attribute.
                options.MinimumSameSitePolicy = SameSiteMode.None;

                //options.ConsentCookie = 

                // Summary:
                //     Affects whether cookies must be HttpOnly.
                //options.HttpOnly

                // Summary:
                //     Affects the cookie's same site attribute.
                //options.MinimumSameSitePolicy

                // Summary:
                //     Called when a cookie is appended.
                //options.OnAppendCookie

                // Summary:
                //     Called when a cookie is deleted.
                //options.OnDeleteCookie

                // Summary:
                //     Affects whether cookies must be Secure.
                //options.Secure


            });

        }



    }
}
