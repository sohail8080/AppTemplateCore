using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToOne_Relationship
{
    // ONE TO ONE RELATION SHIP WITH DEPENDENT ENTITY HAVING ITS OWN PRIMARY KEY
    // AND SEPERATE FORIENKEY

    //Principal entity, main entity in a relationship, contains a primary key
    //Dependent entity.refers primary key as foreign key, entity that holds the foreign key
    // entity classes will contain Navigational properties(single class or a collection), 
    //used to link to other entity classes.
    // Required Relationship: where a foreign key cannot be null, principal entity must exis
    // Optional relationship: where a foreign key could be null, principal entity can be missing

    // one-to-one relationship means that a row in one table can only relate to 
    // one row in another table in a relationship
    // not that common relationship because it is usually handled as “all the data in one table”
    //  but sometimes (when we want to separate our entities) it is useful to divide data into two tables.


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

        //add a reference navigation property at both sides
        public StudentDetails StudentDetails { get; set; } // Navigation Property
    }



    // No DbSet Property in DbContext, but table is created
    public class StudentDetails
    {
        [Column("StudentDetailsId")]
        public Guid Id { get; set; }// Primary key
        public string Address { get; set; }
        public string AdditionalInformation { get; set; }

        //add a reference navigation property at both sides
        public Guid StudentId { get; set; }// F.Key
        public Student Student { get; set; } // Navigation Property
    }


    // create a DbSet for the Student and created in db as migration is done
    // No DbSet for StudentDetails, but still created in db. How?
    // Create Tables for all DbSet<Entity> properties in DbContext
    // Create Colums for all public properties of Entity 
    // Create Tables & Columns for all navigational properties in Entity class 


}
