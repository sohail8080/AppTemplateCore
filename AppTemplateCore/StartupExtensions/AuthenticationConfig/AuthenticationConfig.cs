using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public static class AuthenticationConfig
    {

        public static void Add_Authentication_Config(this IServiceCollection services)
        {

            services.AddAuthentication()
                .AddGoogle(options =>
                    {
                        options.ClientId = "778586624168-8ouqb0ndob2fokefn5jbnn9j81halc9b.apps.googleusercontent.com";
                        options.ClientSecret = "MORpD7PIzC7EFCfS0vwQNTUJ";
                    })
                .AddFacebook(options =>
                    {
                        options.AppId = "2316662895109472";
                        options.AppSecret = "e25c1b8d4145034ed426d7a05efe1481";
                    });


        }

    }
}
