using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.AccessControl.Models
{
    [Table("AspNetRoles")]
    public class ApplicationRole : IdentityRole
    {


    }

    // Model Classes do not specify COMPLETE DB Schema details
    // Conventions do not specify COMPLETE DB Schema details
    // Data Annotations do not specify COMPLETE DB Schema details
    // We need something more, and that more is Fluent API for 
    // Configuring the MODEL that matches the DB Schema.
    // These Configurations when applied they generated the 
    // required Database Schema by Code First Schema.
    // Secondly when the DATA is loaded in Memory, and different 
    // Database Operations are performed, the Operations are executed
    // as desired. Big examples includes the Configuration of RELATIONSHIPS
    // in the MODE and BEHAVIOUR of those RELATIONSHIPS during Database
    // operations.

    // This Class is used to Add Database Schema Stuff to the Domain Model Class
    // This Class extend the Domain Model class and add the DB schema Stuff
    // Without poluting the Domain Model Class
    // After adding DB Schema Stuff here, Create & Apply the Migration 
    // This Class is hooked to OnModelCreating() of the UOW, and this 
    // method is executed when Migrations are run. 
    // THIS METHOD IS NOT EXECUTED WHEN THE APPLICATION IS RUN.
    // THIS METHOD IS EXECUTED WHEN MIGRATIONS ARE RUN.
    // THIS METHOD SHOULD BE USED FOR ENTITY CONFIGURATION FOR DB SCHEMA
    // THIS METHOD SHOULD CAN ASLO BE USED FOR ADDING REFERENCE DATA TO DB.

    //     Allows configuration for an entity type to be placed into a separate class,
    //     rather than in-line in OnModelCreating().
    //     Implement this interface, applying configuration for entity in the IEntityTypeConfiguration 
    //     in Configure() method, and then apply the configuration to the model using ApplyConfiguration()
    //     in DbContext.OnModelCreating().
    //     The entity type to be configured.

    // This class is called by ApplicationDbContext.OnModelCreating()

    public class ApplicationRoleConfigurations : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {

            builder.HasData(
                new ApplicationRole
                {                    
                    Name = "AdminUser",                    
                },
                new ApplicationRole
                {
                    Name = "DeptFooUser",
                }
            );

        }
    }


    // This class is called on Application Start Up to add Ref Data
    // This class is called by Program.cs Class 
    public static class RoleTableSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Get the DBConext object by getting the DbContextOptions object
            // from the DI Container. DbContextOptions is registered as Service
            using (var context = new ApplicationDbContext(

            // Get service of type T from the System.IServiceProvider.
            // Get the DbContextOptions related to RazorPagesMovieContext
            // so that we can add
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Roles.Any())
                {
                    return; // DB has been seeded
                }


                context.Roles.AddRange(
                new ApplicationRole
                {
                    Name = "AdminUser",
                    NormalizedName = "ADMINUSER",
                },
                new ApplicationRole
                {
                    Name = "FinanceUser",
                    NormalizedName = "FINANCEUSER",
                },
                new ApplicationRole
                {
                    Name = "HrUser",
                    NormalizedName = "HRUSER",
                },
                new ApplicationRole
                {
                    Name = "FinanceUser",
                    NormalizedName = "FINANCEUSER",
                }
                );

                context.SaveChanges();
            }
        }
    }



}
