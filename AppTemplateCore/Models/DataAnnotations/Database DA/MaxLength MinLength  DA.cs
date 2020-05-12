using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    //Data Annotations - MaxLength Attribute in EF 6 & EF Core
    //The MaxLength attribute specifies the maximum length of data value allowed for 
    //a property which in turn sets the size of a corresponding column in the database.
    //It can be applied to the string or byte[] properties of an entity.


    //In the above example, the MaxLength(50) attribute is applied on the StudentName 
    //property which specifies that the value of StudentName property cannot be more 
    //than 50 characters long. 
    //This will create a StudentName column with nvarchar(50) size in the SQL Server database,

    using System.ComponentModel.DataAnnotations;

    public class Studentxxxxxxxxxx
    {
        public int StudentID { get; set; }

        [MaxLength(50)]
        public string StudentName { get; set; }

    }

    //Data Annotations MaxLength/MinLength Attribute in EF 6 & EF Core

    //The MaxLength Attribute and MinLength Attribute allow us to specify the size of 
    //the column.Both these attributes are available in 
    //System.ComponentModel.DataAnnotations library.
    //These attributes are similar to StringLength Attribute. 
    //Both of these attributes are available in EF 6 & EF Core.

    //MaxLength Attribute
    //MaxLength attribute used to set the size of the database column.
    //We can apply this attribute only to the string or array type properties of an entity.
    //The following example shows how to use the MaxLength Attribute


    public class EmployeeDSSSSS
    {
        public int EmployeeID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }

    public class EmployeeOUUUU
    {
        public int EmployeeID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }

    //MinLength Attribute
    //MinLength attribute used to specify the minimum length of string or an array property.
    //It is a validation attribute as it does not change the database schema.
    //The following example shows how to use MinLength attribute

    //[MinLength(10)]
    //public string Address { get; set; }


    // MaxLength and MinLength attribute can be used together as shown below.
    //In this example, Name cannot be greater than 50 characters and cannot be less 
    //than 10 characters


    //[MaxLength(50), MinLength(10)]
    //public string Name { get; set; }


    //[MaxLength(50), MinLength(10)]
    // public string Name { get; set; }

    // You can customize the validation error message displayed to the user using the Error
    //Message parameter of both MaxLength and MinLength attribute as shown below.


    public class Customer
    {
        public int CustomerID { get; set; }

        [MaxLength(50, ErrorMessage = "Name cannot be greater than 50")]
        public string Name { get; set; }

        [MinLength(10, ErrorMessage = "Name cannot be less than 10")]
        public string Address { get; set; }
    }

    //Entity framework will throw a VALIDATION ERRO0R(EntityValidationErrorMaxLength or 
    //size is less than a Minlength attribute.

    //StringLength Attribute
    //The Entity Framework also supports StringLength attribute, 
    //which is used by the ASP.NET MVC for validating the model.
    //This attribute is covered in the next tutorial StringLength attribute.



    //    MinLength Attribute

    //This attribute is used to validate a property, whether the property has a minimum length of string.

    //Example
    //[MinLength(5)]
    //public string Name { get; set; }
    //    MaxLength Attribute

    //The MaxLength attribute allows us to specify additional property validations to set the maximum length of the string. This attribute will also participate in database creation(by setting the length of the property).

    //Example

    //[MinLength(5)]
    //[MaxLength(100)]
    //public string Name { get; set; }



    //    MaxLength
    //The MaxLength attribute allows you to specify additional property validations.It can be applied to a string or array type property of a domain class. EF Code First will set the size of a column as specified in MaxLength attribute.

    //Let’s take a look at the following Course class in which MaxLength(24) attribute is applied to Title property.

    //public class Course
    //    {

    //        public int CourseID { get; set; }
    //        [ConcurrencyCheck]
    //        [MaxLength(24)]
    //        public string Title { get; set; }
    //        public int Credits { get; set; }

    //        public virtual ICollection<Enrollment> Enrollments { get; set; }
    //    }
    //    When you run the above application, Code First will create a nvarchar(24) column Title in the CourseId table as shown in the following image.



//    When the user sets the Title which contains more than 24 characters, then EF will throw EntityValidationError.

//MinLength
//The MinLength attribute also allows you to specify additional property validations, just as you did with MaxLength.MinLength attribute can also be used with MaxLength attribute as shown in the following code.

//public class Course
//    {

//        public int CourseID { get; set; }
//        [ConcurrencyCheck]
//        [MaxLength(24), MinLength(5)]
//        public string Title { get; set; }
//        public int Credits { get; set; }

//        public virtual ICollection<Enrollment> Enrollments { get; set; }
//    }
//    EF will throw EntityValidationError, if you set a value of Title property less than the specified length in MinLength attribute or greater than specified length in MaxLength attribute.





}
