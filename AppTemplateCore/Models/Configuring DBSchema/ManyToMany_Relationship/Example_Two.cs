using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.ManyToMany_Relationship4545
{
    // MANY TO MANY REL B/W STUDENTS & SUBJECTS HAVING SURROGATE TABLE STUDENTSUBJECTS


    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        public string StudentName { get; set; }

        // navigation property
        public ICollection<StudentCourse> StudentCourse { get; set; }
    }


    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        // navigation property
        public ICollection<StudentCourse> StudentCourse { get; set; }
    }


    public class StudentCourse
    {
        public Guid StudentId { get; set; }

        // navigation property
        public Student Student { get; set; }

        public int CourseId { get; set; }

        // navigation property
        public Course Course { get; set; }
    }


    // By Model Builder extension method
    public static class ModelBuilderExtensions
    {
        public static void ConfigureStudentEntity(this ModelBuilder modelBuilder)
        {
            // compostie key
            modelBuilder.Entity<StudentCourse>().HasKey(s => new { s.StudentId, s.CourseId });

            // configure one end
            modelBuilder.Entity<StudentCourse>().HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourse)
                .HasForeignKey(sc => sc.StudentId);

            // configure second end
            modelBuilder.Entity<StudentCourse>().HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourse)
                .HasForeignKey(ss => ss.CourseId);
        }
    }



    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            // composite key
            builder.HasKey(s => new { s.StudentId, s.CourseId });

            // configure one end
            builder.HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourse)
                .HasForeignKey(sc => sc.StudentId);

            // configure second end
            builder.HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourse)
                .HasForeignKey(ss => ss.CourseId);

        }
    }

}
