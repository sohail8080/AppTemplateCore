using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship7
{

    //Data Annotation + Conventions Approach to Create One-to-Many Relationship

    // ONE TO MANY REQUIRED RELATIONSHIP, WITH CASCADE DELETE
    // FOR MORE CONTROL OVER RELATIONSHIP USE FLUENT APIS
    // CASE 1: FK ATTRIBUTE ON THE FKEY IN DEPENDENTY ENTITY
    // CASE 2: FK ATTRIBUTE ON THE NAVIGATION PROPERTY IN THE DEPENDENT ENTITY
    // CASE 3: FK ATTRIBUTE ON THE NAVIGATION PROPERTY IN THE PRINCIPAL ENTITY
    // EXACT SAME RELATIONSHIP IS BUILT WITH 1,2,3
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

        //Foreign key is a non nullable field for Required Relationship
        [ForeignKey(nameof(Student))]
        [Required]
        public Guid StudentId { get; set; }

        //navigation property in the dependent entity
        public Student Student { get; set; }
    }



}
