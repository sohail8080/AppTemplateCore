using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToOne_Relationshipex3
{
    // ONE TO ONE RELATION SHIP WITH DEPENDENT ENTITY HAVING ONE COLUMN AS  BOTH
    // PRIMARY KEY ANDE FOREIGN KEY ALSO

    //Configure One-to-Zero-or-One Relationship using Data Annotation Attributes
    // ONE TO ZEOR OR ONE TO ONE : THIS IS IMPORTANT

    //Student and StudentAddress have a One-to-One relationship(zero or one). 
    //A student can have only one or zero addresses.

    //configure a one-to-Zero-or-one relationship between two entities where the 
    //Student entity can be saved without attaching the StudentAddress object to 
    //it but the StudentAddress entity cannot be saved without attaching an object 
    //of the Student entity.This makes one end required.

    //With the one-to-zero-or-one relationship, a Student can be saved without 
    //StudentAddress but the StudentAddress entity cannot be saved without the 
    //Student entity.EF will throw an exception if you try to save the StudentAddress 
    //entity without the Student entity.

    //use data annotation attributes to configure a one-to-zero-or-one relationship
    //between two entities.

    public class Student
    {
        // by convention primary key
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        // As Address Navigation Property is nullable, hence optional
        // i think virtual is about lazy loading
        public virtual StudentAddress Address { get; set; }
    }

    public class StudentAddress
    {
        // we need to configure the StudentAddressId as PK & FK both. 
        // by convention primary key

        // apply [ForeignKey("Student")] on the StudentAddressId property which will 
        // make it a foreign key for the Student entity,
        // by convention primary key       
        public int StudentAddressId { get; set; }// PK + FK

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual Student Student { get; set; }
    }



    public static class ModelBuilderExtensions
    {
        public static void ConfigureStudentEntity(this ModelBuilder modelBuilder)
        {
            // Configure Student & StudentAddress entity
            // Check it out.
            // As Address Navigation Property is nullable, hence optional
            modelBuilder.Entity<Student>()
                        .HasOne(s => s.Address)
                        .WithOne(ad => ad.Student)
                        .HasForeignKey<StudentAddress>(ad => ad.StudentAddressId);


        }
    }


    // By Entity Configuration Class
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
             .HasOne(s => s.Address)
            .WithOne(ad => ad.Student)
            .HasForeignKey<StudentAddress>(ad => ad.StudentAddressId);
        }
    }

        }
