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


        }

    }
}
