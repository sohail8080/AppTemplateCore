using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA66666666666666666666
{
    //Data Annotations Complex Type Attribute in EF 6 & EF Core

    //The complex type attribute denotes the class as a complex type.
    //The complex type is a class, which is similar to an entity, but with few differences.
    //The Complex Types are not available in EF Core.

    //What is Complex Type
    //Consider the entity model class below. Here the ContactInfo is a Complex Type, 
    //which is shared by both Customer and Employee entity.
    //All you need to do is to decorate the class with ComplexType Attribute.
    //This attribute resides in the System.ComponentModel.DataAnnotations.Schema

    //Complex types do not have primary keys, hence they do not exist independently.
    //They can exist as the properties of other entities or other complex types
    //We cannot define foreign key relationships on complex types

    //Complex Type Attribue
    //When you add the ComplexType Attribute the EF does not generate the table for the class. 
    //But it creates the columns in each and every entity which refers to the Complex Type as shown 
    //in the image below.It creates the Column with the name as <PropertyName>_<ComplexTypePropertyName>

    //Complex Type in EF Core
    //EF Core does not support ComplexType attribute as of now



    public class Customeraaaaaaa
    {

        public int CustomerID { get; set; }

        public string Name { get; set; }

        public ContactInfo Contact { get; set; }
    }

    public class Employee
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; }

        public ContactInfo EmployeeContact { get; set; }
    }

    [ComplexType]
    public class ContactInfo
    {
        public string Email { get; set; }

        public string Phone { get; set; }
    }

}
