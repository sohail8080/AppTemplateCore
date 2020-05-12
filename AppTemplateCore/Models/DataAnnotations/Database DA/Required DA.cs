using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{
    //Data Annotations - Required Attribute in EF 6 & EF Core
    //The Required attribute can be applied to one or more properties in an entity class. 
    //EF will create a NOT NULL column in a database table for a property on which the 
    //Required attribute is applied.

    using System.ComponentModel.DataAnnotations;

    public class Studentcccddd
    {
        public int StudentID { get; set; }

        [Required]
        public string StudentName { get; set; }
    }


    //Data Annotations Required Attribute EF 6 & EF Core

    //The Required attribute is used for generating NOT NULL columns.
    //EF 6 & EF Core will create a NOT NULL column if it detects a property having the 
    //Requiredattribute.

    public class Employeebbbbbbbbbbbbbbvvvvvvvvvvvvvv
    {
        public int EmployeeID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }
    }


    //In the above example, the Name property is decorated with Required Attribute.
    //This will create the Name column as Not Null in the database. 
    //Without the Required attribute, all string properties are mapped to 
    //NULLABLE columns (Example Address in the above model)

    //ASP.NET MVC also uses this attribute to Validate the model in the User interface


    //Required Attribute

    //The Required attribute tells the Entity Framework that this property must have a value and this attribute will force the Entity Framework to ensure that this has data in it.This attribute will also participate in database creation (by marking this column as “not nullable”).

    //Example

    //[Required]
    // public string Code { get; set; }



    //    Required Annotation
    //The Required annotation tells EF that a particular property is required.Let’s take a look at the following Student class in which Required id is added to the FirstMidName property.Required attribute will force EF to ensure that the property has data in it.

    //public class Student
    //    {

    //        [Key]
    //        public int StdntID { get; set; }

    //        [Required]
    //        public string LastName { get; set; }

    //        [Required]
    //        public string FirstMidName { get; set; }
    //        public DateTime EnrollmentDate { get; set; }

    //        public virtual ICollection<Enrollment> Enrollments { get; set; }
    //    }
    //    As seen in the above example, Required attribute is applied to FirstMidName and LastName.So, Code First will create a NOT NULL FirstMidName and LastName columns in the Students table as shown in the following image.




}
