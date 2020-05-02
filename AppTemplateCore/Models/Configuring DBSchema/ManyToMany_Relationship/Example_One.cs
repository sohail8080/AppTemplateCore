using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.ManyToMany_Relationship
{
    // MANY TO MANY REL B/W STUDENTS & SUBJECTS HAVING SURROGATE TABLE STUDENTSUBJECTS

    public class Student
    {
        [Column("StudentId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        public int? Age { get; set; }

        public bool IsRegularStudent { get; set; }

        // navigation propery
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }


    public class Subject
    {
        [Column("SubjectId")]
        public Guid Id { get; set; }

        public string SubjectName { get; set; }

        // navigation property
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }


    public class StudentSubject
    {
        public Guid StudentId { get; set; }

        public Student Student { get; set; }


        public Guid SubjectId { get; set; }

        public Subject Subject { get; set; }
    }

    // Many-to-Many relationship is just two One-To-Many relationships.
    // primary key for the StudentSubject table which is, in this case, a composite key.    

    // By Model Builder extension method
    public static class ModelBuilderExtensions
    {
        public static void ConfigureStudentEntity(this ModelBuilder modelBuilder)
        {
            // compsite key for surrogate table
            modelBuilder.Entity<StudentSubject>().HasKey(s => new { s.StudentId, s.SubjectId });

            // Configure one end of relationship
            modelBuilder.Entity<StudentSubject>().HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);

            // Configure second end of relationship
            modelBuilder.Entity<StudentSubject>().HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);
        }
    }




    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            // composite key sfor surrogate table
            builder.HasKey(s => new { s.StudentId, s.SubjectId });

            // Configure one end of relationship
            builder.HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);

            // Configure second end of relationship
            builder.HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);
        }
    }


}
