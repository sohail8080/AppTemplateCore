using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{
    // TWO ENTITIES HAVIN MORE THAN ONE RELATIONSHIP
    // IN ORDER TO HAVE CLARITY AMONNG MULTIPLE RELATIONSHIPS
    // TWO ONE TO MANY RELATIONSHIPS IN ONE DIRECTION
    // LINK THE RELATED REFERENCE & COLLECTION NAVIGATION PROPERTIES IN MULTIPLE RELATIONSHIPS

    //The InverseProperty attribute is used when two entities have more than one 
    //relationship.

    //To understand the InverseProperty attribute, 
    //consider the following example of Course and Teacher entities.

    //In the above example, the Course and Teacher entities have 
    //two one-to-many relationships.

    //Here, EF API cannot determine the other end of the relationship.
    //It will throw the following exception

    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string Description { get; set; }

        // REF NAV PROPERTY 
        public Teacher OnlineTeacher { get; set; }// Confusion

        // REF NAV PROPERTY
        public Teacher ClassRoomTeacher { get; set; }// Confusion
    }

    public class Teacher
    {
        public int TeacherId { get; set; }

        public string Name { get; set; }

        // COLLECTION NAV PROPERTY
        public ICollection<Course> OnlineCourses { get; set; }// Confusion

        // COLLECTION NAV PROPERTY
        public ICollection<Course> ClassRoomCourses { get; set; }// Confusion
    }

    //To solve this issue, use the [InverseProperty] attribute in the above example 
    //to configure the other end of the relationship as shown below.

    public class Courseqqqqq
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string Description { get; set; }

        // related to OnlineCourses
        public Teacher OnlineTeacher { get; set; }

        // related to ClassRoomCourses
        public Teacher ClassRoomTeacher { get; set; }
    }

    public class Teacherqqqqqq
    {
        public int TeacherId { get; set; }

        public string Name { get; set; }

        // related to OnlineTeacher
        [InverseProperty("OnlineTeacher")]
        public ICollection<Course> OnlineCourses { get; set; }

        // related to ClassRoomTeacher
        [InverseProperty("ClassRoomTeacher")]
        public ICollection<Course> ClassRoomCourses { get; set; }
    }


    //Data Annotations InverseProperty Attribute EF 6 & EF Core
    //The InverserProperty informs the EF, which navigational property it relates to 
    //on the other end of the relationship.we use it when there are MULTIPLE RELATIONSHIPS
    //between two entities.The DEFAULT CONVENTIONS fails to detect MULTIPLE RELATIONSHIPS.

    //InverseProperty
    //A relationship in the Entity Framework always has TWO END POINTS.
    //Each end must return a NAVIGATION PROPERTY that maps to the other end of the relationship.
    //you can read about it from the tutorial relationships in entity framework or 
    //Relationships in Entity Framework Core.
    //The Entity Framework BY CONVENTION detects this relationship and creates the appropriate 
    //FOREIGN KEY COLUMN.

    //Consider the following model

    public class Employee555555555555
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; }

        public int DepartmentID { get; set; }

        public Department Department { get; set; }
    }


    public class Departmentttttttttttt
    {
        public int DepartmentID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }


    public class Employeegggggggggg
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; }

        public int DepartmentID { get; set; }// F.KEY

        public Department Department { get; set; }// NP
    }


    public class Departmentnnnnnnnnnnn
    {
        public int DepartmentID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }//NP
    }

    //In the above example, we have employee belonging to a particular department.
    //The DEFAULT CONVENTION automatically detects the relationship and creates the 
    //DepartmentID foreign key column in the Employee table.

    //Multiple Relations
    //The employee and department is a single relationship.
    //What if the employee belongs to multiple departments ?. 
    //Let us take the example of flight & airports.
    //Flight departing from one airport and arrives at another.
    //So the flight has multiple relationships with the Airport

    public class Flight
    {
        public int FlightID { get; set; }

        public string Name { get; set; }

        public Airport DepartureAirport { get; set; }// REL ONE NP

        public Airport ArrivalAirport { get; set; }// REL TWO NP
    }


    public class Airport
    {
        public int AirportID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Flight> DepartingFlights { get; set; }// COLL NAP

        public virtual ICollection<Flight> ArrivingFlights { get; set; }// COLL NP
    }


    //The DepartngFlights property must map to DepartureAirport property in the airport model and 
    //ArrivingFlights property must map to ArrivalAirport property.

    //The default Convention fails to detect multiple relationships.
    //There is no way EF can deduce which navigational property maps to which property on the other end. 
    //It will assume that these are four different relationships and creates the four columns 
    //in the Flight table as shown in the image below.

    //The above works only in EF 6. The EF Core generates an error

    //InverseProperty Attribute
    //This problem can be solved by using the Data annotations InverserProperty attribute 
    //on any one side of the relationships.

    //Apply InverseProperty attribute on a property and specify the corresponding navigational property 
    //of the other end of the relationship.

    //The Airport class after applying the InverseProperty is as shown below.

    // FOLLOWING IS MAY BE EF 6 SOLUTTION

    public class Airportuuuuuuuuuuuuuu
    {
        public int AirportID { get; set; }

        public string Name { get; set; }

        // SAME STRING DepartureAirport IS USED TO RELATE NAV PROPERTIES
        [InverseProperty("DepartureAirport")]
        public ICollection<Flight> DepartingFlights { get; set; }

        // SAME STRING ArrivalAirport IS USED TO RELATE NAV PROPERTIES
        [InverseProperty("ArrivalAirport")]
        public ICollection<Flight> ArrivingFlights { get; set; }
    }


    public class Airportppppppppppppp
    {
        public int AirportID { get; set; }

        public string Name { get; set; }

        // SAME STRING DepartureAirport IS USED TO RELATE NAV PROPERTIES
        [InverseProperty("DepartureAirport")]
        public ICollection<Flight> DepartingFlights { get; set; }

        // SAME STRING ArrivalAirport IS USED TO RELATE NAV PROPERTIES
        [InverseProperty("ArrivalAirport")]
        public ICollection<Flight> ArrivingFlights { get; set; }
    }





    //Now the EF matches the relationship correctly and creates only two fields as shown in the image below.
    //Note that EF 6 & EF Core a slightly different way of naming the Foreign Key.

    //You can apply InverseProperty on the Flight instead of the airport as shown below.
    //Both will result in the same output

    // FOLLOWING IS MAY BE EF CORE SOLUTTION

    public class Flightvvvvvvvvvvvvvvvv
    {
        public int FlightID { get; set; }

        public string Name { get; set; }

        [InverseProperty("DepartingFlights")]
        public Airport DepartureAirport { get; set; }

        [InverseProperty("ArrivingFlights")]
        public Airport ArrivalAirport { get; set; }
    }



    //InverseProperty Attribute

    //A relationship in the Entity Framework always has two ends, a navigation property on each side and an Entity Framework that maps them together automatically by convention.If there are multiple relationships between the two entities, Entity Framework cannot handle the relationships.This is because Entity Framework doesn't know which navigation property map with which properties on other side. For example, Employee is bound with two departments one is the primary and the other is the secondary. In this scenario, Entity Framework does not know which navigation properties on the opposite side should be returned.


    //Using an InverseProperty attribute, we can specify which navigation property should be returned.

    //Example

//    [Table("Department", Schema = "dbo")]
//    public class DepartmentMaster
//    {
//        [Key]
//        public int DepartmentId { get; set; }
//        [Required]
//        public string Code { get; set; }
//        [MinLength(5)]
//        [MaxLength(100)]
//        //[Index("IX_Name_DepartmentMaster", IsClustered = false, Order = 2)]
//        public string Name { get; set; }
//        public ICollection<Employee> PrimaryEmployees { get; set; }
//        public ICollection<Employee> SecondaryEmployees { get; set; }
//    }

//    [Table("Employee", Schema = "dbo")]
//    public class Employee666666666666666669999
//    {
//        [Column("ID", Order = 1)]
//        public int EmployeeId { get; set; }
//        [Column("Name", Order = 2, TypeName = "Varchar(100)")]
//        public string EmployeeName { get; set; }

//        [InverseProperty("PrimaryEmployees")]
//        public DepartmentMaster PrimaryDepartment { get; set; }

//        [InverseProperty("SecondaryEmployees")]
//        public DepartmentMaster SecondaryDepartment { get; set; }
//    }

//    InverseProperty
//InverseProperty is used when you have multiple relationships between classes.In the Enrollment class, you may want to keep track of who enrolled a Current Course and Previous Course.Let’s add two navigation properties for the Enrollment class.

//public class Enrollment
//    {
//        public int EnrollmentID { get; set; }
//        public int CourseID { get; set; }
//        public int StudentID { get; set; }
//        public Grade? Grade { get; set; }

//        public virtual Course CurrCourse { get; set; }
//        public virtual Course PrevCourse { get; set; }
//        public virtual Student Student { get; set; }
//    }
//    Similarly, you’ll also need to add in the Course class referenced by these properties.The Course class has navigation properties back to the Enrollment class, which contains all the current and previous enrollments.

//public class Course
//    {

//        public int CourseID { get; set; }
//        public string Title { get; set; }
//        [Index]

//        public int Credits { get; set; }
//        public virtual ICollection<Enrollment> CurrEnrollments { get; set; }
//        public virtual ICollection<Enrollment> PrevEnrollments { get; set; }
//    }
//    Code First creates {Class Name}
//_{Primary Key} foreign key column, if the foreign key property is not included in a particular class as shown in the above classes.When the database is generated, you will see the following foreign keys.

//As you can see that Code first is not able to match up the properties in the two classes on its own.The database table for Enrollments should have one foreign key for the CurrCourse and one for the PrevCourse, but Code First will create four foreign key properties, i.e.

//CurrCourse _CourseID
//PrevCourse _CourseID
//Course_CourseID, and
//Course_CourseID1
//To fix these problems, you can use the InverseProperty annotation to specify the alignment of the properties.

//public class Course
//    {

//        public int CourseID { get; set; }
//        public string Title { get; set; }
//        [Index]

//        public int Credits { get; set; }
//        [InverseProperty("CurrCourse")]

//        public virtual ICollection<Enrollment> CurrEnrollments { get; set; }
//        [InverseProperty("PrevCourse")]

//        public virtual ICollection<Enrollment> PrevEnrollments { get; set; }
//    }
//    As you can see the InverseProperty attribute is applied in the above Course class by specifying which reference property of Enrollment class it belongs to.Now, Code First will generate a database and create only two foreign key columns in Enrollments table as shown in the following image.




}
