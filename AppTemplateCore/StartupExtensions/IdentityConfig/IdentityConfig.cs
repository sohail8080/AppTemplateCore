using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{

    public static class IdentityConfig
    {
        public static void Add_Identity_Config(this IServiceCollection services)
        {

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
                .AddEntityFrameworkStores<ApplicationDbContext>()
                // Different Token Providers are used during Login, Register, 
                // Email Confirmation, Mob. Ph Confirmation & 2FA, 
                // following statement add Default Token Providers for above.
                // We can customize the Token Providers and configure here.
                .AddDefaultTokenProviders()
                // Add FirstName & Last Name to the User Claims Collection
                // Adds an UserClaimsPrincipalFactory for the UserType.
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();



        }



        public static void Add_Identity_Config2(this IServiceCollection services)
        {


            // Add Services & Configure their Settings needed by Identity Middleware
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                 {
                     options.Tokens.EmailConfirmationTokenProvider = "ApplicationEmailConfirmation";
                 })
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<ApplicationEmailConfirmationTokenProvider
                <ApplicationUser>>("ApplicationEmailConfirmation")
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();


            // Following statement override the IdentityOptions default values
            // During the RPP creation, now IdentityOption class with default values 
            // will be not used, but these new values will be used.

            services.Configure<IdentityOptions>(
                options =>
                {
                    // Summary:
                    //     Gets or sets the minimum length a password must be. Defaults to 6.
                    options.Password.RequiredLength = 6;

                    // Summary:
                    //     Gets or sets the minimum number of unique chars a password must comprised of.
                    //     Defaults to 1.
                    options.Password.RequiredUniqueChars = 1;

                    // Summary:
                    //     Gets or sets a flag indicating if passwords must contain a non-alphanumeric character.
                    //     Defaults to true.
                    options.Password.RequireNonAlphanumeric = true;

                    // Summary:
                    //     Gets or sets a flag indicating if passwords must contain a lower case ASCII character.
                    //     Defaults to true.
                    options.Password.RequireLowercase = true;

                    // Summary:
                    //     Gets or sets a flag indicating if passwords must contain a upper case ASCII character.
                    //     Defaults to true.
                    options.Password.RequireUppercase = true;

                    // Summary:
                    //     Gets or sets a flag indicating if passwords must contain a digit. Defaults to
                    //     true.
                    options.Password.RequireDigit = true;

                    // Summary:
                    //     Gets or sets the Microsoft.AspNetCore.Identity.LockoutOptions for the identity
                    //     system.
                    options.Lockout = new LockoutOptions()
                    {
                        // Summary:
                        //     Gets or sets a flag indicating whether a new user can be locked out. Defaults
                        //     to true.
                        AllowedForNewUsers = true,

                        // Summary:
                        //     Gets or sets the System.TimeSpan a user is locked out for when a lockout occurs.
                        //     Defaults to 5 minutes.
                        DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0),

                        // Summary:
                        //     Gets or sets the number of failed access attempts allowed before a user is locked
                        //     out, assuming lock out is enabled. Defaults to 5.
                        MaxFailedAccessAttempts = 5
                    };

                    // Summary:
                    //     Gets or sets a flag indicating whether a confirmed email address is required
                    //     to sign in. Defaults to false.
                    options.SignIn.RequireConfirmedEmail = true;

                    // Summary:
                    //     Gets or sets a flag indicating whether a confirmed telephone number is required
                    //     to sign in. Defaults to false.
                    options.SignIn.RequireConfirmedPhoneNumber = true;

                    // Summary:
                    //     If set to a positive number, the default OnModelCreating will use this value
                    //     as the max length for any properties used as keys, i.e. UserId, LoginProvider,
                    //     ProviderKey.
                    options.Stores.MaxLengthForKeys = 5;

                    // Summary:
                    //     If set to true, the store must protect all personally identifying data for a
                    //     user. This will be enforced by requiring the store to implement Microsoft.AspNetCore.Identity.IProtectedUserStore`1.
                    options.Stores.ProtectPersonalData = true;

                    // Summary:
                    //     Gets or sets the issuer used for the authenticator issuer.
                    options.Tokens.AuthenticatorIssuer = string.Empty;

                    // Summary:
                    //     Gets or sets the Microsoft.AspNetCore.Identity.TokenOptions.AuthenticatorTokenProvider
                    //     used to validate two factor sign ins with an authenticator.
                    options.Tokens.AuthenticatorTokenProvider = string.Empty;

                    // Summary:
                    // Gets or sets the ChangeEmailTokenProvider
                    // used to generate tokens used in email change confirmation emails.
                    options.Tokens.ChangeEmailTokenProvider = string.Empty;

                    // Summary:
                    // Gets or sets the ChangePhoneNumberTokenProvider
                    // used to generate tokens used when changing phone numbers.
                    options.Tokens.ChangePhoneNumberTokenProvider = string.Empty;

                    // Summary:
                    // Gets or sets the token provider used to generate tokens used in account confirmation
                    // emails.
                    options.Tokens.EmailConfirmationTokenProvider = string.Empty;
                    options.Tokens.EmailConfirmationTokenProvider = "ApplicationEmailConfirmation";

                    // Summary:
                    // Gets or sets the IUserTwoFactorTokenProvider`1
                    // used to generate tokens used in password reset emails.
                    options.Tokens.PasswordResetTokenProvider = string.Empty;

                    // Summary:
                    // Gets or sets the list of allowed characters in the username used to validate
                    // user names. Defaults to abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+
                    options.User.AllowedUserNameCharacters = string.Empty;

                    // Summary:
                    // Gets or sets a flag indicating whether the application requires unique emails
                    // for its users. Defaults to false.
                    options.User.RequireUniqueEmail = true;

                });


            // Following statement set the life span of all Tokens to the 5 hours
            // but this behaviour is not required.
            services.Configure<DataProtectionTokenProviderOptions>(o =>
            {
                // Summary:
                //     Gets or sets the amount of time a generated token remains valid. 
                // Defaults to
                //     1 day.
                o.TokenLifespan = TimeSpan.FromHours(5);

                // Summary:
                //     Gets or sets the name of the DataProtectorTokenProvider.
                //     Defaults to DataProtectorTokenProvider.
                o.Name = string.Empty;
            });

            // Following 2 classes are introduces to set the life span of only 
            // Email confirmation token. It set it for 3 days
            services.Configure<ApplicationEmailConfirmationTokenProviderOptions>(o =>
            {
                o.TokenLifespan = TimeSpan.FromDays(3);

            });



        }



    }
}
