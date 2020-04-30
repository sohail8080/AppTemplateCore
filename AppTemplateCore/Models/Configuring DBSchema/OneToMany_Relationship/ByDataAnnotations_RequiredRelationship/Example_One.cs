using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship7
{
    //Principal entity, main entity in a relationship, contains a primary key
    //Dependent entity.refers primary key as foreign key, entity that holds the foreign key
    // entity classes will contain Navigational properties(single class or a collection), 
    //used to link to other entity classes.
    // Required Relationship: where a foreign key cannot be null, principal entity must exis
    // Optional relationship: where a foreign key could be null, principal entity can be missing


    //Data Annotation Approach to Create One-to-Many Relationship
    //foreign key is a non nullable field. Required Relationship

    //Data Annotation + Conventions which automatically configure the 
    // one-to-many relationship between 
    //the Student and Evaluation classes

    // DbSet Property in DbContext
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

        //navigation property in the principal entity
        // I am not sure about following property, should it be there or not
        public ICollection<Evaluation> Evaluations { get; set; }
    }

    // No DbSet Property in DbContext, but table is created
    // X DbSet<Evaluation> Evaluations
    public class Evaluation
    {
        [Column("EvaluationId")]
        public Guid Id { get; set; }
        [Required]
        public int Grade { get; set; }
        public string AdditionalExplanation { get; set; }

        //We have applied the[ForeignKey] attribute on top of the StudentId 
        // property(which is a foreign key in this class) giving it a name of the 
        //navigational property Student.

        //navigation property in the dependent entity
        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }// For Required Relationship
        public Student Student { get; set; }
    }

    // No DbSet Property in DbContext is required, but table is created
    // Have DbSet<Evaluation> Evaluations property, if DB Operations are required
    public class StudentDetails
    {
        [Column("StudentDetailsId")]
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string AdditionalInformation { get; set; }


        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }





}
