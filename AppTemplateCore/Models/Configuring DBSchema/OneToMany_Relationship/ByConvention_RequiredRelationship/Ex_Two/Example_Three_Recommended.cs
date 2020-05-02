using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship68787
{
    //Convention Approach to Create Automatic One-to-Many Relationship, with cascade delete

    // ONE TO MANY REQUIRED RELATIONSHIP, WITH CASCADE DELETE
    // FOR MORE CONTROL OVER RELATIONSHIP USE FLUENT APIS
    // Example One: ADD NOT-NULLABLE FKEY IN DEPENDENT ENTITY & ADD NULLABLE NAVIGATION PROPERTY IN THE PRINCIPAL ENTITY
    // Example Two: ADD NOT-NULLABLE FKEY IN DEPENDENT ENTITY & ADD NULLABLE NAVIGATION PROPERTY IN THE DEPENDENT ENTITY
    // Example Three: ADD NOT-NULLABLE FKEY IN DEPENDENT ENTITY & ADD NULLABLE NAVIGATION PROPERTIES IN BOTH
    // EXACT SAME RELATIONSHIP IS BUILT WITH 1,2,3. 
    // THE CHOICE OF NAVIGATION PROPERTY DEPENDS UPON QUERYING REQUIREMENTS OF DOMAIN LOGIC

    // ONE STUDENT HAS ONE FINAL GRADE & ONE GRADE IS RELATED TO MANY STUDENTS
    // GRADE PRINCIPAL ENTITY, STUDENT DEPENDENT ENTITY

    public class Grade
    {
        public int GradeId { get; set; }

        public string GradeName { get; set; }

        // navigationo property in principle entity
        public ICollection<Student> Student { get; set; }
    }


    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // not nullable foreign key in dependent entity for required rel
        public int GradeId { get; set; }

        // navigationo property in dependent entity
        public Grade Grade { get; set; }

    }



}
