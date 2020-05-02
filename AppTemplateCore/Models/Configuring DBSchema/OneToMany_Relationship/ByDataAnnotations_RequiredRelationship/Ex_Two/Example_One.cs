using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByDataAnnotations_RequiredRelationship
{

    //Data Annotation + Conventions Approach to Create One-to-Many Relationship

    // ONE TO MANY REQUIRED RELATIONSHIP, WITH CASCADE DELETE
    // FOR MORE CONTROL OVER RELATIONSHIP USE FLUENT APIS
    // CASE 1: FK ATTRIBUTE ON THE FKEY IN DEPENDENTY ENTITY
    // CASE 2: FK ATTRIBUTE ON THE NAVIGATION PROPERTY IN THE DEPENDENT ENTITY
    // CASE 3: FK ATTRIBUTE ON THE NAVIGATION PROPERTY IN THE PRINCIPAL ENTITY
    // EXACT SAME RELATIONSHIP IS BUILT WITH 1,2,3
    // THE CHOICE OF NAVIGATION PROPERTY DEPENDS UPON QUERYING REQUIREMENTS OF DOMAIN LOGIC

    // ONE STUDENT HAS ONE STANDARD & ONE STANDARD IS RELATED TO MANY STUDENTS


    //PRINCIPAL ENTITY
    public class Standard
    {
        public int StandardId { get; set; }

        public string StandardName { get; set; }

        // navigation property
        public ICollection<Student> Students { get; set; }
    }

    //DEPENDENT ENTITY
    public class Student
    {
        public int StudentID { get; set; }

        public string StudentName { get; set; }

        //Fkey name is different, so convension will not work
        [ForeignKey("Standard")]
        public int StandardRefId { get; set; }

        // navigation property
        public Standard Standard { get; set; }
    }


}
