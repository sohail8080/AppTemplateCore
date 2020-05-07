using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToOne_Relationshiplllllllllll
{
    public class SampleContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasOne(a => a.Biography)
                .WithOne(b => b.Author);
        }
    }
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AuthorBiography Biography { get; set; }
    }
    public class AuthorBiography
    {
        public int AuthorBiographyId { get; set; }
        public string Biography { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
