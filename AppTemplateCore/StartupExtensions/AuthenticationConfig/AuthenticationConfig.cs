using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public static class AuthenticationConfig
    {

        public static void Add_Authentication_Config(this IServiceCollection services, IConfiguration Configuration)
        {
            // Put Custom Configuration Object in the DI Container
            services.Configure<External_Authentication_Options>(Configuration.GetSection("External_Authentication_Options"));

            // In controller constructor we can get above object as follows form DI
            // private readonly IOptions<External_Authentication_Options> appSettings; 

            //public StudentController(IOptions<External_Authentication_Options> app)
            //{
            //    appSettings = app;
            //}


            // Method One
            //External_Authentication_Options authenticationOptions = new External_Authentication_Options();
            //Configuration.GetSection("External_Authentication_Options").Bind(authenticationOptions);

            // Method Two
            var authenticationOptions = Configuration.GetSection("External_Authentication_Options").Get<External_Authentication_Options>();


            services.AddAuthentication()
                .AddGoogle(options =>
                    {
                        //options.ClientId = "";
                        //options.ClientSecret = "";
                        //options.ClientId = Configuration.GetValue<string>("GoogleSettings:ClientId");
                        //options.ClientSecret = Configuration.GetValue<string>("GoogleSettings:ClientSecret");

                        options.ClientId = authenticationOptions.GoogleClientId;
                        options.ClientSecret = authenticationOptions.GoogleClientSecret;

                    })
                .AddFacebook(options =>
                    {
                        //options.AppId = "";
                        //options.AppSecret = "";
                        //options.AppId = Configuration.GetValue<string>("FacebookSettings:AppId");
                        //options.AppSecret = Configuration.GetValue<string>("FacebookSettings:AppSecret");

                        options.AppId = authenticationOptions.FacebookAppId;
                        options.AppSecret = authenticationOptions.FacebookAppSecret;

                    });
        }




        public static void Add_Authentication_Config2(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication()
                   .AddGoogle(options =>
                   {
                        IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");

                         options.ClientId = googleAuthNSection["ClientId"];
                            options.ClientSecret = googleAuthNSection["ClientSecret"];
        });



            // We recommend the AccessDeniedPath page contain the following information:
            // Remote authentication was canceled.
            //This app requires authentication.
            //To try sign -in again, select the Login link.
            //Test AccessDeniedPath
            //Navigate to facebook.com
            //If you are signed in, you must sign out.
            //Run the app and select Facebook sign -in.
            //Select Not now.You are redirected to the specified AccessDeniedPath page.

            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = Configuration["Authentication:Facebook:AppId"];
                options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                //options.AccessDeniedPath = "/AccessDeniedPathInfo"; NEW ONE
            });



            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
                microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
            });



            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerAPIKey"];
                twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
                twitterOptions.RetrieveUserDetails = true;
            });


            //LinkedIn(Instructions)
            //Instagram(Instructions)
            //Reddit(Instructions)
            //Github(Instructions)
            //Yahoo(Instructions)
            //Tumblr(Instructions)
            //Pinterest(Instructions)
            //Pocket(Instructions)
            //Flickr(Instructions)
            //Dribble(Instructions)
            //Vimeo(Instructions)
            //SoundCloud(Instructions)
            //VK(Instructions)



        }

    }
}
