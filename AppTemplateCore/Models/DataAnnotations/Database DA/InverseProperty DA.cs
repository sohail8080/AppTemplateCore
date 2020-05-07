using System;
using System.Collections.Generic;
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


}
