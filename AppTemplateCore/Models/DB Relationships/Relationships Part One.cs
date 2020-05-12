using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DB_Relationships
{



//    Relationships & Navigation Properties in Entity Framework
//Property Mappings in Fluent APIOne to One relationships

//We looked at how to model domain classes using Conventions, Data Annotations & Fluent API.In the next few tutorials, you will learn how to set up relationships between entities in the entity framework Code first.


//Ad by Valueimpression
//In relational databases, data is divided between related tables. The relationship between these tables is defined using the foreign keys.We then use these relationships to query the database to get the meaningful data.For instance, if you are looking for an employee working in a particular department, then you need to specify the relationship between employees and the department table.

//Relationships in Entity Framework
//Relationships in Entity Framework

//Ad by Valueimpression
//Table of Content [hide]

//Relationship types
//One to One relationship
//One to Many Relationship
//Many to Many Relationship
//Configuring the Relationship in Entity Framework
//Default Code First Conventions
//Data Annotations Attributes
//Fluent API
//HasMany
//HasOptional
//HasRequired
//WithOptional
//WithRequired
//WithMany
//Conclusion
//Relationship types
//There are three kinds of relationships are possible

//One to One
//One to Many
//Many to Many
//Entity Framework uses the navigational properties to describe the relationship between two entity types.A relationship in the Entity Framework always has two endpoints.Each endpoint, which participates in the relationship must return a navigation property describing the relationship. Entity Framework maps these relationships together using the convention.

//There are two types of navigation properties that can be returned by the entity types depending on the type of relationship they participate.

//Reference Object(If the relationship is one or Zero-or-One)
//Collection(If the relationship is Many)
//Code first uses the navigational properties to navigate to the relationship.


//Ad by Valueimpression
//One to One relationship
//The Relationship between husband and wife is an example of one to one relationship.You can have only one spouse.One to one relationship usually does not happen in database design. Because you can combine them into one table easily without breaking any rule.

//For our example, consider the domain class Employee and EmployeeAddress.Let us assume that employee can have only one address.This will create a one to one relationship between employee and EmployeeAddress table.In a One to one relationship PrimaryKey of one table (employeeID of employee table) is both Primary key and Foreign key in the second table(EmployeeAddress). Hence our entity classes will be as shown below


// public class Employee
//    {
//        public int EmployeeID { get; set; }
//        public string Name { get; set; }

//        //Navigation property Returns the Employee Address
//        public virtual EmployeeAddress EmployeeAddress { get; set; }
//    }

//    public class EmployeeAddress
//    {
//        [Key, ForeignKey("Employee")]
//        public int EmployeeID { get; set; }
//        public string Address { get; set; }

//        //Navigation property Returns the Employee object
//        public virtual Employee Employee { get; set; }
//    }

//1
//2
//3
//4
//5
//6
//7
//8
//9
//10
//11
//12
//13
//14
//15
//16
//17
//18
//19
//20
 
// public class Employee
//    {
//        public int EmployeeID { get; set; }
//        public string Name { get; set; }

//        //Navigation property Returns the Employee Address
//        public virtual EmployeeAddress EmployeeAddress { get; set; }
//    }

//    public class EmployeeAddress
//    {
//        [Key, ForeignKey("Employee")]
//        public int EmployeeID { get; set; }
//        public string Address { get; set; }

//        //Navigation property Returns the Employee object
//        public virtual Employee Employee { get; set; }
//    }

//    Note that we have created the navigational property which returns the reference to the related entity.In Employee class navigational property returns the reference to the EmployeeAddress object. In EmployeeAddress class navigational property returns the reference to the Employee object.

//Configure one to one relationship in entity framework


//Ad by Valueimpression
//One to Many Relationship
//The Relationship between mother and children is an example of one to Many relationships.A Mother can have many children, but a child can have only one mother.

//The relationship between employee and the department is one to many.The employee belongs to only one department.The department can have many employees.In a One to Many relationship Primary key of the department table (DepartmentID) is defined as Foreign key in the employee’s table


//public class Employee
//    {
//        public int EmployeeID { get; set; }
//        public string Name { get; set; }
//        public virtual Department Department { get; set; }
//    }

//    public class Department
//    {
//        public int DepartmentID { get; set; }
//        public string Name { get; set; }
//        public virtual ICollection<Employee> Employees { get; set; }
//    }

//1
//2
//3
//4
//5
//6
//7
//8
//9
//10
//11
//12
//13
//14
//15
 
//public class Employee
//    {
//        public int EmployeeID { get; set; }
//        public string Name { get; set; }
//        public virtual Department Department { get; set; }
//    }

//    public class Department
//    {
//        public int DepartmentID { get; set; }
//        public string Name { get; set; }
//        public virtual ICollection<Employee> Employees { get; set; }
//    }

//    Note that navigational property in the employee class returns the reference to the department object. The navigational property in Department class returns the employee’s collection.Code first uses this information to determine which class is dependent on which.

//   Configure One to Many Relationship in Entity Framework


//Ad by Valueimpression
//Many to Many Relationship
//The Relationship between siblings is Many to Many relationships.Each of your siblings can have many siblings

//The relationship between employee and projects is many to many.The employee can be part of more than one project. The Projects can have many employees. The Many to Many relationships usually involves the creation of a join table.The join table will have a composite primary key consisting combination of the primary key of the both employees and project table.


//public class Employee
//    {
//        public int EmployeeID { get; set; }
//        public string Name { get; set; }
//        //Navigational Property
//        public virtual ICollection<Project> Projects { get; set; }
//    }
//    public class Project
//    {
//        public int ProjectID { get; set; }
//        public string Name { get; set; }
//        //Navigational Property
//        public virtual ICollection<Employee> Employees { get; set; }
//    }

//1
//2
//3
//4
//5
//6
//7
//8
//9
//10
//11
//12
//13
//14
//15
//16
 
//public class Employee
//    {
//        public int EmployeeID { get; set; }
//        public string Name { get; set; }
//        //Navigational Property
//        public virtual ICollection<Project> Projects { get; set; }
//    }
//    public class Project
//    {
//        public int ProjectID { get; set; }
//        public string Name { get; set; }
//        //Navigational Property
//        public virtual ICollection<Employee> Employees { get; set; }
//    }

//    Configure Many to Many relationships in Entity Framework

//Configuring the Relationship in Entity Framework
//The Relationships in entity framework can be done using the following methods

//Default Code First Conventions
//Data Annotations attributes
//Fluent API

//Ad by Valueimpression
//Default Code First Conventions
//We learned about Code First Conventions from the Tutorial.Code First conventions use the default rules to build a model based on your domain classes.Code First infers the relationships using the navigational properties and use the Conventions and builds the model.

//Data Annotations Attributes
//Data Annotations allow us to fine tune the model by Attributes.Key Attribute & ForeginKey Attributes are used to further configure the relationship between models

//Fluent API
//Fluent API Provides more power to configure the relationships in entity models.  DbModelbuilder returns the entitytypeconfiguration to configure the entities. entitytypeconfiguration exposes HasMany, HasOptional, HasRequired methods which used to configure relationships.The method to be used depends on the type of relationship (one or many). These methods take navigational properties as their parameter

//HasMany
//Use this when entity type participates in many relationship. This takes the collection navigational property. This method returns ManyNavigationPropertyConfiguration.

//HasOptional
//Use this when entity type participates in one relationship. This takes the reference navigation property. Use this when the relationship is optional.This method creates the nullable foreign key.This means that you can save the data to database without this relationship being created.This method returns OptionalNavigationPropertyConfiguration


//Ad by Valueimpression
//HasRequired
//This is similar to HasOptional.The difference is that the foreign key is not null, Which means that entity type and its relation must be created otherwise data will not be saved to the database. This method returns RequiredNavigationPropertyConfiguration
//The return types of the above methods are defined in the System.Data.Entity.ModelConfiguration.Configuration.These return types then used to configure the relationship at the other end of the relationship. The return types expose the following methods.

//WithOptional
//Configures an optional navigation property on the other side of the relationship.

//WithRequired
//Configures the relationship to be required on the other side of the relationship.

//WithMany
//Configures the relationship to be many on the other side of the relationship.

//Conclusion
//In the next tutorials, we will explore each of three relationships in detail.




}
