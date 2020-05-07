using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotationForDBSchema
{

    //We should always start with the configuration by Convention
    // Use Data Annotations for the validation configuration
    // For Validation always prefer Data Annotations over Fluent API approach
    // easy to see which validation rule is related to which property 
    // we should use Fluent API approach for everything else.
    // use this approach for the configuration that we can’t do otherwise
    // we want to hide the configuration setup from the model class
    // indexes, composite keys, relationships

    public static class ModelBuilderExtensions
    {
        public static void ConfigureStudentEntity(this ModelBuilder modelBuilder)
        {

            // Map Entity to Table
            modelBuilder.Entity<Student>()
                .ToTable("Student");

            // Map Id Property to Column
            modelBuilder.Entity<Student>()
                .Property(s => s.Id)
                .HasColumnName("StudentId");

            // Configure Name Property
            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Configure Age Property
            modelBuilder.Entity<Student>()
                .Property(s => s.Age)
                .IsRequired(false);

            // Exclude LocalCalculation Property
            modelBuilder.Entity<Student>()
                .Ignore(s => s.LocalCalculation);

            //if the name of the property doesn’t fit the naming convention
            modelBuilder.Entity<Student8>()
                .HasKey(s => s.StudId);

            //For the composite key, we have to use only the Fluent API approach  
            modelBuilder.Entity<Student9>()
                .HasKey(s => new { s.StudId, s.AnotherKeyProperty });

            // add an index to the property
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Name);

            // add an index to the two property
            modelBuilder.Entity<Student>()
                .HasIndex(s => new { s.Name, s.Age });

            // use a named index:
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Name)
                 .HasName("Student_Index"); ;

            // unique constraint will ensure that a column has only unique values
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Name)
                .HasName("index_name")
                .IsUnique();

            //configure its default value via the Fluent API:
            modelBuilder.Entity<Student>()
                .Property(s => s.IsRegularStudent)
                .HasDefaultValue(true);


            // Ignore class in model generation
            modelBuilder.Ignore<NotMappedClass>();
        }
    }





}
