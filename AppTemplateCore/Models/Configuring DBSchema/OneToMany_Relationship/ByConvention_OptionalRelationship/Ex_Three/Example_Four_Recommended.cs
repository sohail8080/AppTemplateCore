using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByConvention_OptionalRelationship.Ex_Three
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


    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }

        //nullable foreign key for optional rel
        public int? StandardId { get; set; }

        public Standard Standard { get; set; }
    }

    public class Standard
    {
        public int StandardId { get; set; }

        public string StandardName { get; set; }

        public ICollection<Student> Students { get; set; }
    }



}
