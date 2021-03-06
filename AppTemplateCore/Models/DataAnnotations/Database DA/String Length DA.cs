﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{
    //Data Annotations StringLength Attribute EF 6 & EF Core

    //The StringLength Attribute Allows us to specify the size of the column.
    //This attribute is available in the System.ComponentModel.DataAnnotations.
    //The attribute works both in entity framework & Entity Framework core.

    //StringLength Attribute
    //The StringLength Attribute Specifies both the Min and Max length of characters that 
    //we can use in a field.

    //StringLength is somewhat similar to MaxLength and MinLength attribute but 
    //operates only on string type properties.

    //Code-First will create the column with the MaxLength size set in this attribute.ASP.Net MVC uses 
    //this attribute to validate the input.

    //Max Length
    //We can set the Max Length as shown below.
    //This attribute affects the schema as it creates the database column with max length.

    public class EmployeeVVVVVVVVVVVVMMMMMMMMMM
    {
        public int EmployeeID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }

    //Min Length
    //We can use the StringLength Attribute to set both Max & Min Length of the string as 
    //shown below.The Address property cannot be greater than 50 and cannot be less 
    //than 10. While Max length affects the schema of the database Min length applies only 
    //for validation purpose.

    //[StringLength(50, MinimumLength = 10)]
    //public string Address { get; set; }

    //Validation Message
    //We can customize the validation messages using the error message parameter as shown 
    //below.

    //[StringLength(50, MinimumLength = 10, ErrorMessage = 
    //"Remark must have min length of 10 and max Length of 50")]
    //public string Remarks { get; set; }

    //The entity framework throws a validation error(EntityValidationError), 
    //if the string exceeds Max Length or size is less than Min length specified in 
    //this attribute.

    //StringLength Attribute

    //StringLength is used to specify the maximum length of the string. This attribute is applied only to string type properties.We can also specify a minimum length of characters that are allowed in the data field.This attribute will also participate in database creation(by setting the length of the property).

    //Example

    //[StringLength(100, MinimumLength = 5)]
    //public string Name { get; set; }




    //    StringLength
    //StringLength also allows you to specify additional property validations like MaxLength.The only difference is that StringLength attribute can only be applied to a string type property of Domain classes.

    //public class Course
    //    {

    //        public int CourseID { get; set; }
    //        [StringLength(24)]
    //        public string Title { get; set; }
    //        public int Credits { get; set; }

    //        public virtual ICollection<Enrollment> Enrollments { get; set; }
    //    }
    //    Entity Framework also validates the value of a property for StringLength attribute.If the user sets the Title which contains more than 24 characters, then EF will throw EntityValidationError.





}
