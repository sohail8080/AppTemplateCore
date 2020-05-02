using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByFluentAPIs__OptionalRelationshipbgrd
{

    // For Optional Relationship 
    // 1. PKey (StudentId) & FKey (StudId) names should be different
    // If FKeyName == PKeyName Then by Convention F.Key defined &
    // relationship will become Required.
    // 2. HasForeignKey should not be used in Model Builder configurations


    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // FKeyName != PKeyName && Do not use the HasForeignKey in config
        // That is why Relationship is Optinal
        public int CurrentGradeId { get; set; }

        // navigation property
        public Grade CurrentGrade { get; set; }
    }

    public class Grade
    {
        public int GradeId { get; set; }

        public string GradeName { get; set; }

        public string Section { get; set; }

        // navigation property
        public ICollection<Student> Students { get; set; }
    }




    // By Model Builder extension method
    public static class ModelBuilderExtensions
    {
        public static void ConfigureStudentEntity(this ModelBuilder modelBuilder)
        {
            //Student has one Grade entiy.
            // Grade is related to many Student entities.
            // foreign key is CurrentGradeId in this relationship.

            // configures one-to-many relationship
            // Method One, Same result
            modelBuilder.Entity<Student>()
                .HasOne<Grade>(s => s.CurrentGrade)// One Student has One Final Grade
                .WithMany(g => g.Students) // One Grade is related to many Students
                                           //.HasForeignKey(s => s.CurrentGradeId) // Foreign key in Dependent/Student table
                 .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Method Two, Same result
            modelBuilder.Entity<Grade>()
                .HasMany<Student>(g => g.Students) // One Grade is related to Many Students
                .WithOne(s => s.CurrentGrade) // One Student has One Final Grade
                                              //.HasForeignKey(s => s.CurrentGradeId) // FKey in Dependent/Student table
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
            // foreign key is StudentId in this relationship.

            builder
                  .HasOne<Grade>(s => s.CurrentGrade)
                  .WithMany(g => g.Students)
                  .HasForeignKey(s => s.CurrentGradeId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }




    // By Entity Configuration Class
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            // builder object is of type EntityTypeBuilder<Student>
            //Student is related to many Evaluation entities.
            // Evaluation is related to one Student entity.
            // foreign key is StudentId in this relationship.

            // Method Two, Same result
            builder
                .HasMany<Student>(g => g.Students) // One Grade is related to Many Students
                .WithOne(s => s.CurrentGrade) // One Student has One Final Grade
                                              //.HasForeignKey(s => s.CurrentGradeId) // FKey in Dependent/Student table
                 .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }






}
