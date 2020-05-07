using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByFluentAPIs__RequiredRelationship1045
{

    // For Required Relationship 
    // 1. PKey (StudentId) & FKey (StudId) names should be same
    // If FKeyName == PKeyName Then by Convention F.Key defined &
    // relationship will become Required. HasForeignKey is optional in this case
    // 2. HasForeignKey should be used in Model Builder configurations to define FKey
    // When FKeyName != PKeyName
   
    // Use of HasForeignKey method is optional if foreign key & primary key
    // has the same type and the same name by Convention this relation will be required
    // But if we had a foreign key with a different name
    // then the HasForeignKey method would be needed


    // DbSet Property in DbContext
    public class Student
    {
        // P.Key & F.Key names are same for Required Relationship
        [Column("StudentId")] 
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        public int? Age { get; set; }
        public bool IsRegularStudent { get; set; }
     
        //navigation property in the principal entity
        // FluentAPI make relationships in the presence of 
        // Navigation Properties
        public ICollection<Evaluation> Evaluations { get; set; }
    }



    // No DbSet Property in DbContext is required, but table is created
    // X DbSet<Evaluation> Evaluations
    // Have DbSet<Evaluation> Evaluations property, if DB Operations are required
    public class Evaluation
    {
        [Column("EvaluationId")]
        public Guid Id { get; set; }
        [Required]
        public int Grade { get; set; }
        public string AdditionalExplanation { get; set; }

        // Followoing F.Key is needed for Required Relationship by Conventions
        // These Conventions may overriden by Fluent APIs
        // F.Key name is same as P.Key for Required Relationship
        public Guid StudentId { get; set; }

        //Following is the navigation property in dependent entity 
        //FluentAPI make relationships in presence of Navigation Properties
        public Student Student { get; set; }// Navigation Property
    }



    // By Model Builder extension method
    public static class ModelBuilderExtensions
    {
        public static void ConfigureStudentEntity(this ModelBuilder modelBuilder)
        {
            //Student is related to many Evaluation entities.
            // Evaluation is related to one Student entity.
            // foreign key is StudentId in this relationship.

            // Use Method One: Same Relationship
            modelBuilder.Entity<Student>()
                .HasMany(e => e.Evaluations) // From Student table
                .WithOne(s => s.Student) // From Evaluations table
                .HasForeignKey(s => s.StudentId)// From Evaluations table, needed for required relationship
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Or Use Method Two: Same Relationship
            modelBuilder.Entity<Evaluation>()
                .HasOne<Student>(s => s.Student) // Evaluation is related to One Student
                .WithMany(e => e.Evaluations) // Student has many Evaluations
                .HasForeignKey(s => s.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


        }
    }



    // By Entity Configuration Class
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // builder object is of type EntityTypeBuilder<Student>
            //Student is related to many Evaluation entities.
            // Evaluation is related to one Student entity.
            // foreign key is StudentId in this relationship.

            builder.HasMany(e => e.Evaluations) // From Student table
                .WithOne(s => s.Student) // From Evaluations table
                .HasForeignKey(s => s.StudentId) // From Evaluations table, needed for required relationship
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }



    // By Entity Configuration Class
    public class EvaluationConfiguration : IEntityTypeConfiguration<Evaluation>
    {
        public void Configure(EntityTypeBuilder<Evaluation> builder)
        {
            // builder object is of type EntityTypeBuilder<Student>
            //Student is related to many Evaluation entities.
            // Evaluation is related to one Student entity.
            // foreign key is StudentId in this relationship.

            // Or Use Method Two: Same Relationship
            builder
                .HasOne<Student>(s => s.Student) // Evaluation is related to One Student
                .WithMany(e => e.Evaluations) // Student has many Evaluations
                .HasForeignKey(s => s.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }



}
