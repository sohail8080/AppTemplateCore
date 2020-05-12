using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public static class AppServicesConfig
    {

        public static void Add_AppServices_Config(this IServiceCollection services, IHostingEnvironment env)
        {
            //if (env.IsProduction())
            //{
            //    services.AddTransient<IProductService, BetterProductService>();
            //}
            //else
            //{
            //    services.AddTransient<IProductService, ProductService>();
            //}


            //services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            //services.AddTransient<IProductService, ProductService>();

            //Construction Injection
            //Action Injection
            //The Action Injection is done using the[FromServices] attribute to an argument in an
            //action method as shown below.
            //Use this method, if the service is used only in a single action method.
            //public IActionResult Index([FromServices] IProductService productService)
            //{
            //    return View(_productService.All());
            //}

            // TRANSIENT: OBJECT IS NOT REUSED WITHIN THE SCOPE/REQUEST
            // IF OBJECT IS NEEDED TWICE IN ONE REQUEST PROCESSING, IT WILL BE CREATED TWICE
            // LIGHT WEIGH SERVICES, NO STATE PERSERVATION DURING REQUEST
            // REQUEST ARE LESS FREQUENT INVOKED BY USERS, LITTLE OR NOT STATE.

            // SCOPED: OBJECT WILL WEB REUSED WITHIN THE SCOPE/REQUEST
            // IF OBJECT IS NEEDED TWICE OR MORE IN ONE REQUEST PROCESSING, IT WILL BE CREATED ONCE

            // SIGNLETON: OBJECT IS CREATED ONLY ONCE AND REUSED FOR ALL INCOMING REQUEST IN FUTURE.

        }


    }
}
