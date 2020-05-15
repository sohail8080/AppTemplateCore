using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Join_Operators
{
    //What is Inner Join in Linq?
    //“An inner join produces a result set in which each element of the first collection appears 
    //one time for every matching element in the second collection.

    //If an element in the first collection does not have any matching element in the 
    //second collection, then it does not appear in the result set”.

    //Linq Inner join is used to return only the 
    //matching elements from both the data sources while the 
    //non-matching elements are removed from the result set.

    //So, if you have two data sources, and when you perform the LINQ inner join, 
    //then only the matching elements which exist in both the data sources are included 
    //in the result set.

    //Note: While performing the Linq inner join then there should exist a 
    //common property in both the data sources.

    //there are two overloaded versions available in Linq to perform inner join operations.
    //The second overloaded version takes a comparer as an extra parameter.

    //So, while working with Linq Join you need to understand the following five things.

    //Outer data source
    //Inner data source
    //Outer Key selector (common key in the outer data source)
    //Inner Key selector(Common key in the inner data source)
    //Result selector(project the data into a result set)

    //Our requirement is to fetch the employee name and their address into an anonymous type. 
    //But here we need to fetch only the elements which are present in both the data sources.


    class Program22
    {
        static void Main22(string[] args)
        {
            var employees = Employee.GetAllEmployees();
            var addresses = Address.GetAllAddresses();

            //Outer Data Source: employees
            //Inner Data Source: addresses
            //Inner Key Selector: employee.AddressId
            //Outer Key selector: address.ID
            //Projecting the data into a result set: (employee, address) => new
            //it only fetches the matching records from both the data sources.
            //how to perform the inner join in LINQ with two data sources using both Method and Query syntax.

            var JoinUsingMS = employees.Join(addresses, employee => employee.AddressId, address => address.ID, 
                                (employee, address) => new 
                                {
                                    EmployeeName = employee.Name,
                                    AddressLine = address.AddressLine
                                }).ToList();

            var JoinUsingQS = (from emp in employees join address in addresses
                               on emp.AddressId equals address.ID
                               select new
                               {
                                   EmployeeName = emp.Name,
                                   AddressLine = address.AddressLine
                               }).ToList();


            //Using Linq Query Syntax to Perform Inner Join:
            //The Linq provides the join operator to perform the joins using Query syntax. Performing 
            //the join using query syntax is very much similar to performing the join in SQL.

        }
    }



    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public static List<Employee> GetAllEmployees()
        {
            return new List<Employee>()
            {
                new Employee { ID = 1, Name = "Preety", AddressId = 1 },
                new Employee { ID = 2, Name = "Priyanka", AddressId = 2 },
                new Employee { ID = 3, Name = "Anurag", AddressId = 3 },
                new Employee { ID = 4, Name = "Pranaya", AddressId = 4 },
                new Employee { ID = 5, Name = "Hina", AddressId = 5 },
                new Employee { ID = 6, Name = "Sambit", AddressId = 6 },
                new Employee { ID = 7, Name = "Happy", AddressId = 7},
                new Employee { ID = 8, Name = "Tarun", AddressId = 8 },
                new Employee { ID = 9, Name = "Santosh", AddressId = 9 },
                new Employee { ID = 10, Name = "Raja", AddressId = 10},
                new Employee { ID = 11, Name = "Sudhanshu", AddressId = 11}
            };
        }
    }
    public class Address
    {
        public int ID { get; set; }
        public string AddressLine { get; set; }
        public static List<Address> GetAllAddresses()
        {
            return new List<Address>()
            {
                new Address { ID = 1, AddressLine = "AddressLine1"},
                new Address { ID = 2, AddressLine = "AddressLine2"},
                new Address { ID = 3, AddressLine = "AddressLine3"},
                new Address { ID = 4, AddressLine = "AddressLine4"},
                new Address { ID = 5, AddressLine = "AddressLine5"},
                new Address { ID = 9, AddressLine = "AddressLine9"},
                new Address { ID = 10, AddressLine = "AddressLine10"},
                new Address { ID = 11, AddressLine = "AddressLine11"},
            };
        }
    }



}
