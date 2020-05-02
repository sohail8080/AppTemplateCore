using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByFluentAPIs__RequiredRelationship9
{

    // For Optional Relationship 
    // 1. PKey (StudentId) & FKey (StudId) names should be different
    // If FKeyName == PKeyName Then by Convention F.Key defined &
    // relationship will become Required.
    // 2. HasForeignKey should not be used in Model Builder configurations


    // DbSet Property in DbContext
    public class Student
    {
        // PKey & FKey names should be different
        [Column("StudentId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        public int? Age { get; set; }

        public bool IsRegularStudent { get; set; }

        //navigation property in the principal entity is must
        public ICollection<Evaluation> Evaluations { get; set; }
    }



    // No DbSet<Evaluation> Evaluations Property in DbContext is required, but table is created
    // Have DbSet<Evaluation> Evaluations property, if DB Operations are required
    public class Evaluation
    {
        [Column("EvaluationId")]
        public Guid Id { get; set; }

        [Required]
        public int Grade { get; set; }

        public string AdditionalExplanation { get; set; }

        // PKey & FKey names should be different
        public Guid StudId { get; set; }

        //navigation property in dependent entity is a must
        public Student Student { get; set; }

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
                .HasMany<Evaluation>(e => e.Evaluations) // Student has many evaluations
                .WithOne(s => s.Student) // Evaluation is related to One Student
                                         //.HasForeignKey(s => s.StudentId);// Remove this for Optional Relationship
                 .OnDelete(DeleteBehavior.Restrict);

            // Or Use Method Two: Same Relationship
            modelBuilder.Entity<Evaluation>()
                .HasOne<Student>(s => s.Student) // Evaluation is related to One Student
                .WithMany(e => e.Evaluations) // Student has many Evaluations
                                              //.HasForeignKey(s => s.StudentId);// Remove this for Optional Relationship
                .IsRequired(false)
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
            // foreign key is StudentIdin this relationship.

            builder.HasMany(e => e.Evaluations) // From Student table
                .WithOne(s => s.Student) // From Evaluations table
                                         //.HasForeignKey(s => s.StudentId) // Remove this for Optional Relationship
                .IsRequired(false)
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
            // Or Use Method Two: Same Relationship
            builder
                 .HasOne<Student>(s => s.Student) // Evaluation is related to One Student
                 .WithMany(e => e.Evaluations) // Student has many Evaluations
                                               //.HasForeignKey(s => s.StudentId);// Remove this for Optional Relationship
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }






}
