using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToOne_Relationshipex2
{

    //Student and StudentAddress have a One-to-One relationship(zero or one). 
    //A student can have only one or zero addresses.

    //use data annotation attributes to configure a one-to-zero-or-one relationship
    //between two entities.

    public class Student
    {
        // by convention primary key
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public virtual StudentAddress Address { get; set; }
    }

    public class StudentAddress
    {
        // we need to configure the StudentAddressId as PK & FK both. 
        // by convention primary key

        // apply [ForeignKey("Student")] on the StudentAddressId property which will 
        // make it a foreign key for the Student entity,

        [ForeignKey("Student")]
        public int StudentAddressId { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual Student Student { get; set; }
    }

}
