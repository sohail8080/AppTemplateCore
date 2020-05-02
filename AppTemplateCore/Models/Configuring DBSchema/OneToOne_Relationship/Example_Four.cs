using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToOne_Relationship766555
{

    public class Author
    {
        public int AuthorId { get; set; }// PK
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AuthorBiography Biography { get; set; }
    }

    public class AuthorBiography
    {
        public int AuthorBiographyId { get; set; }// PK
        public string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Nationality { get; set; }
        public int AuthorRef { get; set; }// Fkey
        public Author Author { get; set; }
    }



    public static class ModelBuilderExtensions
    {
        public static void ConfigureStudentEntity(this ModelBuilder modelBuilder)
        {
            // Two different tables will be created
            modelBuilder.Entity<Author>()
                        .HasOne(a => a.Biography)
                        .WithOne(b => b.Author)
                        .HasForeignKey<AuthorBiography>(b => b.AuthorRef);
        }
    }



    // By Entity Configuration Class
    public class StudentConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            // Two different tables will be created
            builder
                .HasOne(a => a.Biography)
                .WithOne(b => b.Author)
                .HasForeignKey<AuthorBiography>(b => b.AuthorRef);
        }
    }




}
