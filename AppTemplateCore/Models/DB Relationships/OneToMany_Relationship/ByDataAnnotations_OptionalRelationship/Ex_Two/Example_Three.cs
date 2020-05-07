using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByDataAnnotations_RequiredRelationshipvfvfvvvv
{

    //Data Annotation + Conventions Approach to Create One-to-Many Relationship

    // ONE TO MANY REQUIRED RELATIONSHIP, WITH CASCADE DELETE
    // FOR MORE CONTROL OVER RELATIONSHIP USE FLUENT APIS
    // CASE 1: FK ATTRIBUTE ON THE FKEY IN DEPENDENTY ENTITY
    // CASE 2: FK ATTRIBUTE ON THE NAVIGATION PROPERTY IN THE DEPENDENT ENTITY
    // CASE 3: FK ATTRIBUTE ON THE NAVIGATION PROPERTY IN THE PRINCIPAL ENTITY
    // EXACT SAME RELATIONSHIP IS BUILT WITH 1,2,3
    // THE CHOICE OF NAVIGATION PROPERTY DEPENDS UPON QUERYING REQUIREMENTS OF DOMAIN LOGIC


    public class Standard
    {
        public int StandardId { get; set; }

        public string StandardName { get; set; }

        [ForeignKey("StandardRefId")]
        public ICollection<Student> Students { get; set; }
    }


    public class Student
    {
        public int StudentID { get; set; }

        public string StudentName { get; set; }

        //Fkey name is diff than convention
        // FKey is nullable, that means it is optional
        public int? StandardRefId { get; set; }

        public Standard Standard { get; set; }
    }


}
