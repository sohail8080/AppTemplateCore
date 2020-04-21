using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public static class AppCookieConfig
    {

        public static void App_Cookie_Config(this IServiceCollection services)
        {

            services.ConfigureApplicationCookie(options =>
            {
                // If you try to navigate to a URL/Controller/Action not
                // allowed by Auth Rules, then this Controller Action will be invoked.
                options.AccessDeniedPath = new PathString("/Error/AccessDenied");
            });

        }


    }
}
