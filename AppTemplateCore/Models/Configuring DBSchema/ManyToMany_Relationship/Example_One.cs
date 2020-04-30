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
    //Principal entity, main entity in a relationship, contains a primary key
    //Dependent entity.refers primary key as foreign key, entity that holds the foreign key
    //entity classes will contain Navigational properties(single class or a collection), 
    //used to link to other entity classes.
    //Required Relationship: where a foreign key cannot be null, principal entity must exis
    //Optional relationship: where a foreign key could be null, principal entity can be missing

    public class Student
    {
        [Column("StudentId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        public int? Age { get; set; }
        public bool IsRegularStudent { get; set; }

        public StudentDetails StudentDetails { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }

        public ICollection<StudentSubject> StudentSubjects { get; set; }
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
        // F.Key name is same for Required Relationship
        public Guid StudentId { get; set; }

        //Following is the navigation property in dependent entity 
        //FluentAPI make relationships in presence of Navigation Properties
        public Student Student { get; set; }// Navigation Property
    }


    public class Subject
    {
        [Column("SubjectId")]
        public Guid Id { get; set; }
        public string SubjectName { get; set; }

        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }


    public class StudentSubject
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
    }


    // No DbSet Property in DbContext, but table is created
    public class StudentDetails
    {
        [Column("StudentDetailsId")]
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string AdditionalInformation { get; set; }


        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }


    // Many-to-Many relationship is just two One-To-Many relationships.
    // primary key for the StudentSubject table which is, in this case, a composite key.    

    //Restrict – can’t delete principal entity if it has related dependent entity.

    //SetNull – principal entity is deleted & dependent entity is not deleted and its 
    //fkey is set to null, optional relationship

    //ClientSetNull – If EF Core tracks dependent entity,  when principal entity is deleted, 
    // its foreign key is set to null and dependent entity is not deleted
    // If EF Core does not tracks dependent entity, when principal entity is deleted,
    // then database rules apply to decide what to do with dependent entity

    //Cascade – The dependent entity is deleted with the principal entity.



    // By Model Builder extension method
    public static class ModelBuilderExtensions
    {
        public static void ConfigureStudentEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubject>().HasKey(s => new { s.StudentId, s.SubjectId });

            modelBuilder.Entity<StudentSubject>().HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);

            modelBuilder.Entity<StudentSubject>().HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);
        }
    }




    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(s => new { s.StudentId, s.SubjectId });

            builder.HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);

            builder.HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);
        }
    }



    // Full Configuration Class for Student Entity, nothing ot witih 
    // above example
    public class StudentConfiguration2 : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.Property(s => s.Age)
                .IsRequired(false);
            builder.Property(s => s.IsRegularStudent)
                .HasDefaultValue(true);

            builder.HasData
            (
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    Age = 30
                },
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "Jane Doe",
                    Age = 25
                },
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "Mike Miles",
                    Age = 28
                }
            );

            builder.HasMany(e => e.Evaluations)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }



}
