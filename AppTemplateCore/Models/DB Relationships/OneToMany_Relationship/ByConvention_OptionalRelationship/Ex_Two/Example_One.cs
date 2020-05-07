using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationshipex22ddd
{

    //Convention Approach to Create Automatic One-to-Many Relationship

    // ONE TO MANY OPTIONAL RELATIONSHIP, WITH CASCADE DELETE, 
    // FOR MORE CONTROL OVER RELATIONSHIP USE FLUENT APIS
    // Example One: ADD NULLABLE NAVIGATION PROPERTY IN THE PRINCIPAL ENTITY
    // Example Two: ADD NULLABLE NAVIGATION PROPERTY IN THE DEPENDENT ENTITY
    // Example Three: ADD NULLABLE NAVIGATION PROPERTIES IN BOTH
    // Example Four: ADD NULLABLE NAVIGATION PROPERTIES IN THE BOTH & NULLABLE F.KEY IS ALSO MENTIONED
    // EXACT SAME RELATIONSHIP IS BUILT WITH 1,2,3,4 
    // THE CHOICE OF NAVIGATION PROPERTY DEPENDS UPON QUERYING REQUIREMENTS OF DOMAIN LOGIC

    // ONE GRADE IS RELATED TO MANY STUDENTS, ONE STUDENT HAS ONE FINAL GRADE
    // GRADE PRINCIPAL ENTITY, STUDENT DEPENDENT ENTITY

    //Because there is no foreign key property in the dependant entity, 
    //EF Core will configure a nullable foreign key column in the dependant table
    //with a referential constraint action of Restrict, which will translate to NO ACTION in SQL Server.

    public class Grade
    {
        public int GradeId { get; set; }

        public string GradeName { get; set; }

        public string Section { get; set; }

        // nullable navigationo property in principal entity
        // nullable for optional rel
        public ICollection<Student> Students { get; set; }
    }


    public class Student
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }
    }









}
