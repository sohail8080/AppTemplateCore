using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{

    //Data Annotations - Key Attribute in EF 6 & EF Core

    //The Key attribute can be applied to a property in an entity class 
    //to make it a key property and the corresponding column to a PrimaryKey column 
    //in the database.

    //The default convention creates a primary key column for a property whose name 
    //is Id or <Entity Class Name>Id.

    //The Key attribute overrides this default convention.


    //The Key attribute can be applied to a property of any primitive data type 
    //except unsigned integers.

    //In EF 6, the Key attribute creates a PK with an identity column 
    //when applied to a single integer type property.

    //The composite key does not create an identity column for the integer property.

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Studentnbnb
    {
        // applied to integer
        [Key]
        public int StudentKey { get; set; }

        public string StudentName { get; set; }
    }

    public class Studentmmmj
    {
        [Key]
        [Column(Order = 1)]
        public int StudentKey { get; set; }

        [Key]
        [Column(Order = 2)]
        public int AdmissionNum { get; set; }

        public string StudentName { get; set; }
    }


    //The Key Attribute or Primary key attribute maps the property as the primary key column 
    //in the database.

    //This attribute is applicable to both Entity Framework & Entity Framework Core.
    //In Entity Framework, we can use it to create Composite Primary key, while in EF Core it 
    //is not supported.

    //Default Convention
    //Entity Framework Default conventions or Entity Framework Core Convention look for the property
    //with the name id or with the name<className>ID.It then maps that property to the Primary Key.
    //In case it finds both id property and  <className>ID property, then id property is used.
    //The following model creates the table with CustomerID As the primary key.

    public class CustomerOOOOOOOOOO
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }


    //Key Attribute
    //You can override this behavior using the Data Annotation Key attribute.
    //Entity Framework Data Annotation Key attribute marks the property as Primary Key. 
    //This will override the default Primary Key. The following code creates the table with 
    //CustomerNo as the Primary key instead if CustomerID
    //Do not forget to import the following namespace


    public class CustomerMMMMMMMMMM
    {
        public int CustomerID { get; set; }

        [Key]
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
    }


    //Note that the CustomerNo column created with the Identity enabled.Key attribute, 
    //if applied on the integer column will create the column with the Identity enabled (autoincrement)


    //The unsigned data types (uint) are not allowed in EF as Primary key, while it is allowed in EF Core. 
    //But EF Core does not create an Identity column for unsigned data types

    //Key Attribute on a string property

    public class CustomerWWWWWWWWWWWWWWWWWWW
    {
        public int CustomerID { get; set; }
        [Key]
        public string CustomeNo { get; set; }
        public string CustomerName { get; set; }
    }


    public class CustomerYYYYYYYYYYYYY
    {
        public int CustomerID { get; set; }
        [Key]
        public string CustomeNo { get; set; }
        public string CustomerName { get; set; }
    }


    //Enabling / Disabling identity column
    //You can disable/enable the identity on the numeric column by using the DatabaseGenerated attribute.
    //The following code disables the identity column

    public class CustomerAAAAAAAAAAAAAA
    {
        public int CustomerID { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerNo { get; set; }

        public string CustomerName { get; set; }
    }


    //[Key]
    //public int StudentID { get; set; }

    //The following code enables the identity column


    public class CustomerCCCCCCCCCCCCCC
    {
        public int CustomerID { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerNo { get; set; }

        public string CustomerName { get; set; }
    }


    public class CustomerKKKKKKKKKKKKKK
    {
        public int CustomerID { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerNo { get; set; }

        public string CustomerName { get; set; }
    }


    //Composite Primary Key
    //Entity Framework
    //When Primary Key consists of more than on Property it is called a Composite primary key.
    //We can apply the Key attribute on all the candidate fields to denote them as Composite Key.
    //In such a case we must also specify the Column Order of the appearance of the key.

    public class CustomerXXXXXXXXXXXXXXX
    {
        [Key]
        [Column(Order = 1)]
        public int CustomerID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }

    }

    //If you do not specify the Column Order Attribute, then the entity framework will raise 
    //the error “Unable to determine composite primary key ordering for type”

    //Few important notes on Composite Primary key
    //You can use any integer values in order attribute.For example, 10 and 15 are 
    //perfectly acceptable instead of 1 and 2.

    //If we apply Composite Key on integer or GUID columns, it does not create an Identity column.
    //There is no restriction on the data type of the key except for the unsigned data types

    //EF Core
    //The EF Core does not allow creating the Composite Primary key using Key Attribute.
    //You will receive the following error

    //Entity type ‘Customer’ has the composite primary key defined with data annotations.
    //To set composite primary key, use fluent API.







}
