using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByDataAnnotations_RequiredRelationship.Ex_Onevcvcvcvvv
{
    //Data Annotation + Conventions Approach to Create One-to-Many Relationship

    // ONE TO MANY REQUIRED RELATIONSHIP, WITH CASCADE DELETE
    // FOR MORE CONTROL OVER RELATIONSHIP USE FLUENT APIS
    // EXACT SAME RELATIONSHIP IS BUILT WITH 1,2. 
    // THE CHOICE OF NAVIGATION PROPERTY DEPENDS UPON QUERYING REQUIREMENTS OF DOMAIN LOGIC

    // ONE STUDENT HAS MANY EVALUATIONS AND ONE EVALUATION BELONGS TO ONE STUDENT
    // STUDENT PRINCIPAL ENTITY, EVALUATION DEPENDENT ENTITY

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

        //navigation property in the principal entity
        [ForeignKey("StudentRefId")]
        public ICollection<Evaluation> Evaluations { get; set; }
    }



    // No DbSet<Evaluation> Evaluations property in DbContext, but table is created
    // If you need database operations, have that property
    public class Evaluation
    {
        [Column("EvaluationId")]
        public Guid Id { get; set; }

        [Required]
        public int Grade { get; set; }

        public string AdditionalExplanation { get; set; }

        //Foreign key is a nullable field for Optional Relationship   
        // FKey is nullable, that means it is optional
        public Guid? StudentRefId { get; set; }

        //navigation property in the dependent entity       
        public Student Student { get; set; }
    }




}
