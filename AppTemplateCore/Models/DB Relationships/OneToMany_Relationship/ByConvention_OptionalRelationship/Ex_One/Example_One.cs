using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship
{

    //Convention Approach to Create Automatic One-to-Many Relationship, with cascade delete

    // ONE TO MANY OPTIONAL RELATIONSHIP, WITH CASCADE DELETE, 
    // FOR MORE CONTROL OVER RELATIONSHIP USE FLUENT APIS
    // Example One: ADD NULLABLE NAVIGATION PROPERTY IN THE PRINCIPAL ENTITY
    // Example Two: ADD NULLABLE NAVIGATION PROPERTY IN THE DEPENDENT ENTITY
    // Example Three: ADD NULLABLE NAVIGATION PROPERTIES IN BOTH
    // Example Four: ADD NULLABLE NAVIGATION PROPERTIES IN THE BOTH & NULLABLE F.KEY IS ALSO MENTIONED
    // EXACT SAME RELATIONSHIP IS BUILT WITH 1,2,3,4 
    // THE CHOICE OF NAVIGATION PROPERTY DEPENDS UPON QUERYING REQUIREMENTS OF DOMAIN LOGIC

    // ONE STUDENT HAS MANY EVALUATIONS AND ONE EVALUATION BELONGS TO ONE STUDENT
    // STUDENT PRINCIPAL ENTITY, EVALUATION DEPENDENT ENTITY

    //Because there is no foreign key property in the dependant entity Evaluation, 
    //EF Core will configure a nullable StudentId foreign key column in the Evaluation table
    //with a referential constraint action of Restrict, which will translate to NO ACTION in SQL Server.


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
      
        //nullable navigation property in the principal entity, for Optional Requirement
        public ICollection<Evaluation> Evaluations { get; set; }
    }

    // No DbSet<Evaluation> Evaluations Property in DbContext, but table is created
    // Have this property if DB Operations are required on Evaluations
    public class Evaluation
    {
        [Column("EvaluationId")]
        public Guid Id { get; set; }

        [Required]
        public int Grade { get; set; }

        public string AdditionalExplanation { get; set; }
    }







}
