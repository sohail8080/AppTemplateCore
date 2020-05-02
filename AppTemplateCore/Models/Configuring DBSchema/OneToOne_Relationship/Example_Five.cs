using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToOne_Relationship7665552233
{

    public class Author
    {
        public int AuthorId { get; set; }// PK
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AuthorBiography AuthorBiography { get; set; }
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

    //Configuring a relationship that uses Table Splitting link
    //Table splitting is a technique that enables you to use a single table 
    //to represent both entities in a one-to-one relationship.Using this feature, 
    //both entities in the one-to-one relationship illustrated above will be stored in a 
    //database table together:
    //Both entities must share the same primary key, which is used as a foreign key 
    //in the dependent end of the relationship:

    public static class ModelBuilderExtensions
    {
        public static void ConfigureStudentEntity(this ModelBuilder modelBuilder)
        {
            // One table will be created
            modelBuilder.Entity<Author>()
                        .HasOne(a => a.AuthorBiography)
                        .WithOne(b => b.Author)
                        .HasForeignKey<AuthorBiography>(e => e.AuthorRef);

            modelBuilder.Entity<Author>().ToTable("Authors");

            modelBuilder.Entity<AuthorBiography>().ToTable("Authors");
        }
    }



    // By Entity Configuration Class
    public class StudentConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            // One table will be created
            builder
                    .HasOne(a => a.AuthorBiography)
                    .WithOne(b => b.Author)
                    .HasForeignKey<AuthorBiography>(e => e.AuthorRef);

            builder.ToTable("Authors");

            builder.ToTable("Authors");
        }
    }




}
