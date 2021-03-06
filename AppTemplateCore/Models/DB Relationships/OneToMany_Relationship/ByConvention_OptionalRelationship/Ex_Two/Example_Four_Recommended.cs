﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByConvention_OptionalRelationship2121ddd
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

        // nullable foreign key in dependent entity for optional relationship
        public int? GradeId { get; set; }// Nullable<int>

        // navigationo property in dependent entity
        public Grade Grade { get; set; }
    }


}
