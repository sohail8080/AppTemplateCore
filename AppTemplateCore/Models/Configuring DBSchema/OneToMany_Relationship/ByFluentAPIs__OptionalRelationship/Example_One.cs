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
    //We should always start with the configuration by Convention
    // Use Data Annotations for the validation configuration
    // For Validation always prefer Data Annotations over Fluent API approach
    // easy to see which validation rule is related to which property 
    // we should use Fluent API approach for everything else.
    // use this approach for the configuration that we can’t do otherwise
    // we want to hide the configuration setup from the model class
    // indexes, composite keys, relationships

    // For Optional Relationship
    // PKey (StudentId) & FKey (StudId) names should be different
    // HasForeignKey should not be used in Model Builder configurations


    // For Optiona Relationshiop, we have foreign key with a different name, StudId 
    // for example, and HasForeignKey method should not be used
    // thus we will create an optional relationship between Evaluation and Student classes.


    // DbSet Property in DbContext
    public class Student
    {
        // P.Key & F.Key names are now diff for Optional Relationship
        [Column("StudentId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        public int? Age { get; set; }
        public bool IsRegularStudent { get; set; }

        public StudentDetails StudentDetails { get; set; }

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


        // Followoing F.Key may be deleted for having Optional Relationship
        // F.Key name is different for Optional Relationship
        public Guid StudId { get; set; }

        //Following is the navigation property in dependent entity 
        // FluentAPI make relationships in presence of Navigation Properties
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

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Evaluations) // From Student table
                .WithOne(s => s.Student) // From Evaluations table
                                         //.HasForeignKey(s => s.StudentId);// Remove this for Optional Relationship
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
                .OnDelete(DeleteBehavior.Restrict);
        }
    }



}
