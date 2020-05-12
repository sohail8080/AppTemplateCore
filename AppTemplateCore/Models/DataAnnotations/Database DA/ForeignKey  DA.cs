using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{
    //Data Annotations - ForeignKey Attribute in EF 6 & EF Core

    //The ForeignKey attribute is used to configure a foreign key in the relationship between 
    //two entities in EF 6 and EF Core.

    //It overrides the default conventions.As per the default convention, 
    //EF makes a property as foreign key property when 
    //its name matches with the primary key property of a related entity.

    //ForeignKey Signature: [ForeignKey(name string)]

    //name: Name of the associated NAVIATION PROPERTY or the name of the associated FORIEIGN KEYS.
    //Consider the following example of a one-to-many relationship among entities.

    using System.ComponentModel.DataAnnotations.Schema;

    public class StudentQQQQQQ
    {
        public int StudentID { get; set; }

        public string StudentName { get; set; }

        //Foreign key for Standard BY CONVENTION
        public int StandardId { get; set; }

        public Standard Standard { get; set; }
    }

    public class Standard
    {
        public int StandardId { get; set; }

        public string StandardName { get; set; }

        public ICollection<Student> Students { get; set; }
    }


    //A property name StandardId in Student entity matches with the primary key property of

    //Standard entity, so StandardId in Student entity will automatically become a foreign key property

    //and the corresponding column in the db table will also be a foreign key column,

    //The[ForeignKey] attribute overrides the default convention for a foreign key 

    //It allows us to specify the foreign key property in the dependent entity whose name 

    //does not match with the primary key property of the principal entity.

    //The[ForeignKey(name)] attribute can be applied in three ways:

    //[ForeignKey(NavigationPropertyName)] on the foreign key SCALAR PROPERTY in the DEPENDENT ENTITY

    // DEPENDENT ENTITY
    public class StudentSDSDS
    {
        public int StudentID { get; set; }

        public string StudentName { get; set; }

        [ForeignKey("Standard")]
        public int StandardRefId { get; set; }// SCALAR PROPERTRY

        public Standard Standard { get; set; }
    }


    // PRINCIPAL ENTITY
    public class StandardBBBBB
    {
        public int StandardId { get; set; }

        public string StandardName { get; set; }

        public ICollection<Student> Students { get; set; }
    }



    //[ForeignKey(ForeignKeyPropertyName)] on the related REFERENCE NAVIGATION PROPERTY in the 
    //DEPENDENT ENTITY

    // DEPENDENT ENTITY
    public class StudentOOOOO
    {
        public int StudentID { get; set; }

        public string StudentName { get; set; }

        public int StandardRefId { get; set; }

        // REFERENCE ENTITY
        [ForeignKey("StandardRefId")]
        public Standard Standard { get; set; }
    }

    // PRINCIPAL ENTITY
    public class StandardOOOOO
    {
        public int StandardId { get; set; }

        public string StandardName { get; set; }

        public ICollection<Student> Students { get; set; }
    }


    //[ForeignKey(ForeignKeyPropertyName)] on the NAVIGATION PROPERTY in the PRINCIPAL ENTITY

    // DEPENDENT ENTITY
    public class StudentMMMMMMMMMMMMM
    {
        public int StudentID { get; set; }

        public string StudentName { get; set; }

        public int StandardRefId { get; set; }

        public Standard Standard { get; set; }
    }

    // PRINCIPAL ENTITY
    public class StandardMMMMMMMMMMMM
    {
        public int StandardId { get; set; }

        public string StandardName { get; set; }

        // COLLECTION NAVIGATION PROPERTY
        [ForeignKey("StandardRefId")]
        public ICollection<Student> Students { get; set; }
    }


    //ForeignKey Attribute in EF & EF Core
    //In this tutorial learn to use the ForeignKey Attribute Entity Framework & Entity Framework Core 
    //to configure the Foreign Key Property.We use the Foreign Key to define the relationship 
    //between tables in the database. For Example, the Employee working in a Department is a relationship. 
    //We express this relationship by creating a DepartmentID field in the Employee table and 
    //marking it as Foreign Key. In this example, the Department is the Principal entity & Employee 
    //is a Dependent entity.

    //Foreign Key Default Convention
    //In the following model, the Entity Employee has a Department navigational property 
    //that links it to the Department entity.
    //We do not have any Property representing the Foreign Key field in Employee entity. 
    //The Entity framework automatically creates the Foreign Key field in the database for us.

    //In EF 6, if we did not include the Foreign key field, the Entity Framework conventions will use the 
    //format NavigationpropertyName_PrimaryKeyOftheNavigationPropertyType.While in the case of 
    //EF Core Conventions, it will use the Primary key field of the Principal class. 

    public class EmployeeVVVVVVVVVVVVVVVVVVV
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        //NO FKEY, ONLY Navigation property
        public Department Department { get; set; }
    }

    public class Department
    {
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        //NO FKEY, ONLY Navigation property
        public ICollection<Employee> Employeees { get; set; }
    }

    //But, if you add the DepartmentID property in the Employee entity, Entity Framework conventions 
    //will use that field.

    public class Employee0333
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        // FKEY
        public int DepartmentID { get; set; }

        //Navigation property
        public Department Department { get; set; }
    }

    public class Department6333
    {
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        //COLLECTION Navigation property
        public ICollection<Employee> Employeees { get; set; }
    }


    //ForeignKey Attribute
    //What if we wish to change the DepartmentID Property to DeptID in the employee table.
    //The Default Convention of both EF & EF Core will not recognize the DeptID Property and 
    //will create Department_DepartmentID(or DepartmentID in EF Core) column in the database.

    //We can override this behavior using the Foreign key attribute on the navigational property.
    //The following is the syntax of the ForeignKey Attribute.


    //ForeignKey(string name)

    //Where
    //Name:  The name of the associated navigation property or of the associated foreign keys

    //There are three ways you can apply this attribute

    //ForeignKey property of the dependent class
    //Navigational Property of  the dependent class
    //Navigational Property of the Principal class
    //In the example above example, Employee is the dependent class as it depends on the Department.
    //The Department is the Principal class.

    //Foreign Key property of the dependent class
    //The following example, we apply the ForeignKey attribute on the DeptID Property of 
    //the Employee class. 
    //In that case, the name must point to the navigational property

    // PRINCIPAL ENTITY
    public class Department1222
    {
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        //Navigation property
        public ICollection<Employee> Employeees { get; set; }
    }

    // DEPENDENT ENTITY
    public class EmployeeCVCCCCCCCCCCC
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        // RELATING FKEY TO THE NAVIGATION PROPERTY 
        [ForeignKey("Department")]
        public int DeptID { get; set; }

        //Navigation property
        public Department Department { get; set; }
    }



    //Navigational Property of the dependent class
    //We can also place the ForeignKey Attribute Navigation property.
    //When placed on navigation property, 
    //it should specify the associated foreign key

    // PRINCIPAL ENTITY
    public class DepartmentHJHDDDDDDDD
    {
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        //Navigation property
        public ICollection<Employee> Employeees { get; set; }
    }

    // DEPDENDENT ENITY
    public class EmployeeCVCSSSSSS
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public int DeptID { get; set; }

        //RELATING Navigation property TO FKEY
        [ForeignKey("DeptID")]
        public Department Department { get; set; }
    }


    //Navigational Property of the Principal class
    //We can also place the Foreign key attribute on the Navigational property of the Principal class. 
    //The name argument must point to the Foreign Key of the dependent class.

    // PRINCIPAL ENITY
    public class Department7666
    {
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        //RELATING COLL. Navigation property TO F.KEY
        [ForeignKey("DeptID")]
        public ICollection<Employee> Employeees { get; set; }
    }


    // DEPENDENT ENTITY
    public class Employee4333
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public int DeptID { get; set; }

        //Navigation property
        public Department Department { get; set; }
    }




    //ForeignKey Attribute

    //This attribute specifies the foreign key for the Navigation property.

    //Example

    [Table("Employee", Schema = "dbo")]
    public class Employeeffffffffffffffffffff
    {
        [Column("ID", Order = 1)]
        public int EmployeeId { get; set; }

        [Column("Name", Order = 2, TypeName = "Varchar(100)")]
        public string EmployeeName { get; set; }

        [ForeignKey("Department ")]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public DepartmentMaster Department { get; set; }
    }


    //    ForeignKey
    //Code First convention will take care of the most common relationships in your model, but there are some cases where it needs help.For example, by changing the name of the key property in the Student class created a problem with its relationship to Enrollment class.

    //public class Enrollment
    //    {
    //        public int EnrollmentID { get; set; }
    //        public int CourseID { get; set; }
    //        public int StudentID { get; set; }
    //        public Grade? Grade { get; set; }

    //        public virtual Course Course { get; set; }
    //        public virtual Student Student { get; set; }
    //    }

    //    public class Student
    //    {
    //        [Key]
    //        public int StdntID { get; set; }
    //        public string LastName { get; set; }
    //        public string FirstMidName { get; set; }
    //        public DateTime EnrollmentDate { get; set; }

    //        public virtual ICollection<Enrollment> Enrollments { get; set; }
    //    }
    //    While generating the database, Code First sees the StudentID property in the Enrollment class and recognizes it, by the convention that it matches a class name plus “ID”, as a foreign key to the Student class. However, there is no StudentID property in the Student class, but it is StdntID property is Student class.

    //The solution for this is to create a navigation property in the Enrollment and use the ForeignKey DataAnnotation to help Code First understand how to build the relationship between the two classes as shown in the following code.

    //public class Enrollment
    //    {
    //        public int EnrollmentID { get; set; }
    //        public int CourseID { get; set; }
    //        public int StudentID { get; set; }

    //        public Grade? Grade { get; set; }
    //        public virtual Course Course { get; set; }
    //        [ForeignKey("StudentID")]

    //        public virtual Student Student { get; set; }
    //    }
    //    You can see now that the ForeignKey attribute is applied to the navigation property.

}
