using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.Identity.Models
{
    public class ApplicationRole : IdentityRole
    {


    }

    // This Class is used to Add Database Schema Stuff to the Domain Model Class
    // This Class extend the Domain Model class and add the DB schema Stuff
    // Without poluting the Domain Model Class
    // After adding DB Schema Stuff here, Create & Apply the Migration 
    // This Class is hooked to OnModelCreating() of the UOW, and this 
    // method is executed when Migrations are run. This method is not 
    // executed when Application is run.
    public class ApplicationRoleConfigurations : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {

            builder.HasData(
                new IdentityRole
                {                    
                    Name = "AdminUser",                    
                },
                new IdentityRole
                {
                    Name = "DeptFooUser",
                }
            );

        }
    }

}
