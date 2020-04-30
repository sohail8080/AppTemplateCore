using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByFluentAPIs__RequiredRelationship10
{
    //We should always start with the configuration by Convention
    // Use Data Annotations for the validation configuration
    // For Validation always prefer Data Annotations over Fluent API approach
    // easy to see which validation rule is related to which property 
    // we should use Fluent API approach for everything else.
    // use this approach for the configuration that we can’t do otherwise
    // we want to hide the configuration setup from the model class
    // indexes, composite keys, relationships



    // For Required Relationship
    // PKey (StudentId) & FKey (StudentId) names should be same
    // HasForeignKey should be used in Model Builder configurations

    //For the database model like we’ve defined, we don’t need to have 
    //the HasForeignKey method.That’s because the foreign key property 
    //in the Evaluation class has the same type and the same name as the primary key 
    //in the Student class. This means that by Convention this relation would still 
    //be the required one. But if we had a foreign key with a different name, 
    //StudId for example, then the HasForeignKey method would be needed


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

        // Followoing F.Key is needed for Required Relationship by Conventions
        // These Conventions may overriden by Fluent APIs
        // F.Key name is same for Required Relationship
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

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Evaluations) // From Student table
                .WithOne(s => s.Student) // From Evaluations table
                .HasForeignKey(s => s.StudentId);// From Evaluations table, needed for required relationship

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
                .OnDelete(DeleteBehavior.Restrict);
        }
    }



}
