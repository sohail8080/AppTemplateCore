using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship5
{

    //Convention Approach to Create Automatic One-to-Many Relationship, with cascade delete

    // ONE TO MANY REQUIRED RELATIONSHIP, WITH CASCADE DELETE
    // FOR MORE CONTROL OVER RELATIONSHIP USE FLUENT APIS
    // Example One: ADD NOT-NULLABLE FKEY IN DEPENDENT ENTITY & ADD NULLABLE NAVIGATION PROPERTY IN THE PRINCIPAL ENTITY
    // Example Two: ADD NOT-NULLABLE FKEY IN DEPENDENT ENTITY & ADD NULLABLE NAVIGATION PROPERTY IN THE DEPENDENT ENTITY
    // Example Three: ADD NOT-NULLABLE FKEY IN DEPENDENT ENTITY & ADD NULLABLE NAVIGATION PROPERTIES IN BOTH
    // EXACT SAME RELATIONSHIP IS BUILT WITH 1,2,3. 
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

      
    }

    // DbSet Property in DbContext
    // DbSet<Evaluation> Evaluations
    public class Evaluation
    {
        [Column("EvaluationId")]
        public Guid Id { get; set; }

        [Required]
        public int Grade { get; set; }

        public string AdditionalExplanation { get; set; }

        //Foreign key is a non nullable field for required relationship
        [Required]
        public Guid StudentId { get; set; }

        public Student Student { get; set; }
    }







}
