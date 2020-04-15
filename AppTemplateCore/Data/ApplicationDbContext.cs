using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Areas.Movies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppTemplateCore.Data
{
    // In Visual Studio, you can use the Package Manager Console to apply pending migrations to the database:
    // PM> Update-Database
    // Alternatively, you can apply pending migrations from a command prompt at your project directory:
    // CommandPrompt> dotnet ef database update

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        // Connection String and Provider will be provided inside ConfigureService()
        // UOW is Connection String Free, It be hooked to any Database Type
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configures the schema needed for the identity framework tables.
            base.OnModelCreating(modelBuilder);

            //modelBuilder.SeedRoleData();

            // Applies configuration that is defined in an IEntityTypeConfiguration
            // instance.
            modelBuilder.ApplyConfiguration(new ApplicationRoleConfigurations());

            //foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
            //    .SelectMany(e => e.GetForeignKeys()))
            //{
            //    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            //}

        }

    }





    public static class ModelBuilderExtensions
    {
        public static void SeedRoleData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRole>().HasData(

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

        }
    }




}
