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

                // Summary:
                // Gets or sets the issuer that should be used for any claims that 
                // are created
                //options.ClaimsIssuer.

            //options.Cookie.Domain

            // Summary:
            //     This property is obsolete and will be removed in a future version. The recommended
            //     alternative is Microsoft.AspNetCore.Http.CookieBuilder.HttpOnly on Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions.Cookie.
            //     Determines if the browser should allow the cookie to be accessed by client-side
            //     javascript. The default is true, which means the cookie will only be passed to
            //     http requests and is not made available to script on the page.
            // [Obsolete("This property is obsolete and will be removed in a future version. The recommended alternative is Cookie.HttpOnly.")]
            //options.CookieHttpOnly


            // Summary:
            //     The component used to get cookies from the request or set them on the response.
            //     ChunkingCookieManager will be used by default.
            //options.CookieManager


            // Summary:
            //     This property is obsolete and will be removed in a future version. The recommended
            //     alternative is Microsoft.AspNetCore.Http.CookieBuilder.Name on Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions.Cookie.
            //     Determines the cookie name used to persist the identity. The default value is
            //     ".AspNetCore.Cookies". This value should be changed if you change the name of
            //     the AuthenticationScheme, especially if your system uses the cookie authentication
            //     handler multiple times.
            //[Obsolete("This property is obsolete and will be removed in a future version. The recommended alternative is Cookie.Name.")]
            //options.CookieName


            // Summary:
            //     This property is obsolete and will be removed in a future version. The recommended
            //     alternative is Microsoft.AspNetCore.Http.CookieBuilder.Path on Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions.Cookie.
            //     Determines the path used to create the cookie. The default value is "/" for highest
            //     browser compatibility.
            //[Obsolete("This property is obsolete and will be removed in a future version. The recommended alternative is Cookie.Path.")]

                //options.CookiePath

                        // Summary:
                        //     This property is obsolete and will be removed in a future version. The recommended
                        //     alternative is Microsoft.AspNetCore.Http.CookieBuilder.SecurePolicy on Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions.Cookie.
                        //     Determines if the cookie should only be transmitted on HTTPS request. The default
                        //     is to limit the cookie to HTTPS requests if the page which is doing the SignIn
                        //     is also HTTPS. If you have an HTTPS sign in page and portions of your site are
                        //     HTTP you may need to change this value.
                //options.CookieSecure

                        // Summary:
                        //     If set this will be used by the CookieAuthenticationHandler for data protection.

                //options.DataProtectionProvider

                        // Summary:
                        //     Controls how much time the authentication ticket stored in the cookie will remain
                        //     valid from the point it is created The expiration information is stored in the
                        //     protected cookie ticket. Because of that an expired cookie will be ignored even
                        //     if it is passed to the server after the browser should have purged it.
                        //     This is separate from the value of Microsoft.AspNetCore.Http.CookieOptions.Expires,
                        //     which specifies how long the browser will keep the cookie.
                //options.ExpireTimeSpan

                        // Summary:
                        //     If set, this specifies the target scheme that this scheme should forward AuthenticateAsync
                        //     calls to. For example Context.AuthenticateAsync("ThisScheme") => Context.AuthenticateAsync("ForwardAuthenticateValue");
                        //     Set the target to the current scheme to disable forwarding and allow normal processing.

                //options.ForwardAuthenticate

                        //
                        // Summary:
                        //     If set, this specifies the target scheme that this scheme should forward ChallengeAsync
                        //     calls to. For example Context.ChallengeAsync("ThisScheme") => Context.ChallengeAsync("ForwardChallengeValue");
                        //     Set the target to the current scheme to disable forwarding and allow normal processing.

                //options.ForwardChallenge

                        // Summary:
                        //     If set, this specifies a default scheme that authentication handlers should forward
                        //     all authentication operations to by default. The default forwarding logic will
                        //     check the most specific ForwardAuthenticate/Challenge/Forbid/SignIn/SignOut setting
                        //     first, followed by checking the ForwardDefaultSelector, followed by ForwardDefault.
                        //     The first non null result will be used as the target scheme to forward to.

                //options.ForwardDefault

                        // Summary:
                        //     Used to select a default scheme for the current request that authentication handlers
                        //     should forward all authentication operations to by default. The default forwarding
                        //     logic will check the most specific ForwardAuthenticate/Challenge/Forbid/SignIn/SignOut
                        //     setting first, followed by checking the ForwardDefaultSelector, followed by ForwardDefault.
                        //     The first non null result will be used as the target scheme to forward to.

                //options.ForwardDefaultSelector

                        // Summary:
                        //     If set, this specifies the target scheme that this scheme should forward ForbidAsync
                        //     calls to. For example Context.ForbidAsync("ThisScheme") => Context.ForbidAsync("ForwardForbidValue");
                        //     Set the target to the current scheme to disable forwarding and allow normal processing.

                //options.ForwardForbid

                        // Summary:
                        //     If set, this specifies the target scheme that this scheme should forward SignInAsync
                        //     calls to. For example Context.SignInAsync("ThisScheme") => Context.SignInAsync("ForwardSignInValue");
                        //     Set the target to the current scheme to disable forwarding and allow normal processing.

                //options.ForwardSignIn

                        // Summary:
                        //     If set, this specifies the target scheme that this scheme should forward SignOutAsync
                        //     calls to. For example Context.SignOutAsync("ThisScheme") => Context.SignInAsync("ForwardSignOutValue");
                        //     Set the target to the current scheme to disable forwarding and allow normal processing.

                //options.ForwardSignOut

                        // Summary:
                        //     The LoginPath property is used by the handler for the redirection target when
                        //     handling ChallengeAsync. The current url which is added to the LoginPath as a
                        //     query string parameter named by the ReturnUrlParameter. Once a request to the
                        //     LoginPath grants a new SignIn identity, the ReturnUrlParameter value is used
                        //     to redirect the browser back to the original url.

                //options.LoginPath

                        // Summary:
                        //     If the LogoutPath is provided the handler then a request to that path will redirect
                        //     based on the ReturnUrlParameter.

                //options.LogoutPath

                        // Summary:
                        //     The ReturnUrlParameter determines the name of the query string parameter which
                        //     is appended by the handler when during a Challenge. This is also the query string
                        //     parameter looked for when a request arrives on the login path or logout path,
                        //     in order to return to the original url after the action is performed.

                //options.ReturnUrlParameter

                        // Summary:
                        //     An optional container in which to store the identity across requests. When used,
                        //     only a session identifier is sent to the client. This can be used to mitigate
                        //     potential problems with very large identities.

                //options.SessionStore

                        // Summary:
                        //     The SlidingExpiration is set to true to instruct the handler to re-issue a new
                        //     cookie with a new expiration time any time it processes a request which is more
                        //     than halfway through the expiration window.

                //options.SlidingExpiration

                        // Summary:
                        //     The TicketDataFormat is used to protect and unprotect the identity and other
                        //     properties which are stored in the cookie value. If not provided one will be
                        //     created using Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions.DataProtectionProvider.

                //options.TicketDataFormat


            });

        }


    }
}
