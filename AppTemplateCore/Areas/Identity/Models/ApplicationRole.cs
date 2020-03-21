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
