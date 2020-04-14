using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.AccessControl.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Required]
        [StringLength(15)]
        public string FirstName { get; set; }

        // Personal Data Atrributs are included while 
        // Profile Data is Downloaded.
        [PersonalData]
        [Required]
        [StringLength(15)]
        public string LastName { get; set; }

    }


    // This class is called on Application Start Up to add Ref Data
    // This class is called by Program.cs Class 
    public static class UserTableSeedData
    {
        // This application is ApplicationDBContext based
        public static void InitializeAsync(IServiceProvider serviceProvider)
        {
            var um = serviceProvider.GetService<UserManager<ApplicationUser>>();

            // Get the DBConext object by getting the DbContextOptions object
            // from the DI Container. DbContextOptions is registered as Service
            using (var context = new ApplicationDbContext(

            // Get service of type T from the System.IServiceProvider.
            // Get the DbContextOptions related to RazorPagesMovieContext
            // so that we can add
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {


                if (context.Users.Any(user => user.UserName == "admin@abc.com"))
                {
                    return;
                }

                context.Users.AddRange(

                new ApplicationUser
                {
                    UserName = "admin@abc.com",
                    Email = "admin@abc.com",
                    FirstName = "Site",
                    LastName = "Admin"
                }
                //new ApplicationRole
                //{
                //    Name = "XXX",
                //    NormalizedName = "XXXX",
                //},

                );

                try
                { context.SaveChanges(); }
                catch (Exception ex)
                { throw ex; }

            }
        }


        // This application is UserManager & RoleManager based
        public static async Task InitializeIdentityAsync(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetService<ILogger<Program>>();

            try
            {

                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();
                

                // If Admin User's Role does not exist, create it
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    var appRole = new ApplicationRole
                    {
                        Name = "Admin"
                    };

                    var result = await roleManager.CreateAsync(appRole);

                    if (!result.Succeeded)
                    {
                        logger.LogError("Error occurred  while seeding Admin Role to the Database");
                    }
                }


                // If Admin User does not exist, create it
                if (!userManager.Users.Any(user => user.UserName == "admin@abc.com"))
                {
                    var appUser = new ApplicationUser
                    {
                        UserName = "admin@abc.com",
                        Email = "admin@abc.com",
                        FirstName = "Site",
                        LastName = "Admin"
                    };

                    var result = await userManager.CreateAsync(appUser, "Abc@123");

                    if (!result.Succeeded)
                    {
                        logger.LogError("Error occurred  while seeding Admin User to the Database");
                    }
                }



                // If Admin User is not added in Admin Role, add him
                var applicationUser = await userManager.FindByNameAsync("admin@abc.com");

                if (!await userManager.IsInRoleAsync(applicationUser, "Admin"))
                {
                    var result = await userManager.AddToRoleAsync(applicationUser, "Admin");

                    if (!result.Succeeded)
                    {
                        logger.LogError("Error occurred  while seeding Admin User Role to the Database");
                    }
                }

                // If Admin User is not added in Edit Claim, add him
                var userClaims = await userManager.GetClaimsAsync(applicationUser);
                if (! (userClaims.Any(claim => claim.Type == "Edit Role" && claim.Value == "Edit Role")))
                {
                    var result = await userManager.AddClaimAsync(applicationUser, ClaimsStore.AllClaims[1]);

                    if (!result.Succeeded)
                    {
                        logger.LogError("Error occurred  while seeding Admin User Claim to the Database");
                    }
                }



            }
            catch (Exception ex)
            {
                logger.LogError("Error occurred  while seeding Admin User data to the Database");
                throw ex;
            }


        }


    }


}
