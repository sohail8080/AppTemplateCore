using System;
using System.Collections.Generic;
using System.Text;
using AppTemplateCore.Areas.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppTemplateCore.Data
{
    // In Visual Studio, you can use the Package Manager Console to apply pending migrations to the database:
    // PM> Update-Database
    // Alternatively, you can apply pending migrations from a command prompt at your project directory:
    // CommandPrompt> dotnet ef database update

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Connection String and Provider will be provided inside ConfigureService()
        // UOW is Connection String Free, It be hooked to any Database Type
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



    }
}
