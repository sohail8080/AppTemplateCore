using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Join_Operators.LeftOuterJoin
{
    //What is Left Outer Join in Linq?
    //The left join or left outer join is a join in which 
    //each data from the first data source is going to be returned 
    //irrespective of whether it has any correlated data present in the second data source or not.


    //Left Outer Join in Linq

    //Left Outer Join is going to return all the matching data from both the data sources 
    //as well as all the non-matching data from the left data source.
    //In such cases, for the non-matching data, it will take null values for the second data source.

    //In order to implement the Left Outer Join in Linq, it’s mandatory to use the “INTO” keyword 
    //along with the “DefaultIfEmpty()” method.


    //Left Outer Join using Query Syntax:
    //In order to perform the left outer join using query syntax, 
    //you need to call the DefaultIfEmpty() method on the results of a group join.

    //The first step to implement a left outer join is to perform an inner join by using a group join.

    //In the second step, we need to include each element of the first (i.e. left) data source in the 
    //result set irrespective of whether that element has no matches in the second 
    //(i.e. right) data source. 
    //In order to do this, we need to call the DefaultIfEmpty() method on each sequence of matching elements 
    //from the group join.

    //In our example, we need to call the DefaultIfEmpty() method on each sequence of matching Address objects. 
    //The DefaultIfEmpty() method returns a collection that contains a single, 
    //default value if the sequence of matching Address object is empty for any Employee object 
    //which will ensure that each Employee object is represented in the result collection. 
    //The following code exactly does the same thing.

    //Note: The default value for a reference type is null. So, you need to check for the null reference before accessing each element of Address collection.


    class Program
    {
        static void Main(string[] args)
        {
            var employees = Employee.GetAllEmployees();
            var addresses = Address.GetAddress();

            var QSOuterJoin =
                from emp in employees
                join add in addresses on emp.AddressId equals add.ID into EmployeeAddressGroup
                from address in EmployeeAddressGroup.DefaultIfEmpty()
                select new { emp, address };

            foreach (var item in QSOuterJoin)
            {
                Console.WriteLine($"Name : {item.emp.Name}, Address : {item.address?.AddressLine} ");
            }

            // I feel it always better to use Query Syntax over Method Syntax to perform left outer join in Linq as it is 
            //simple and easy to understand.

            var MSOuterJOIN =
                employees.GroupJoin(addresses, emp => emp.AddressId, add => add.ID, (emp, add) => new { emp, add })
                         .SelectMany(x => x.add.DefaultIfEmpty(), (employee, address) => new { employee, address });


            //Anonymous type with user-defined properties in the ResultSet:
            //Let us see how to return an anonymous type with user-defined properties.

            var MSOuterJOIN22 =
                   employees.GroupJoin(addresses, emp => emp.AddressId, add => add.ID, (emp, add) => new { emp, add })
                  .SelectMany(x => x.add.DefaultIfEmpty(), (employee, address) => new
                  {
                      EmployeeName = employee.emp.Name,
                      AddressLine = address == null ? "NA" : address.AddressLine
                  });


            var QSOuterJoin22 =
                from emp in employees
                join add in addresses on emp.AddressId equals add.ID into EmployeeAddressGroup
                from address in EmployeeAddressGroup.DefaultIfEmpty()
                select new
                {
                    EmployeeName = emp.Name,
                    AddressLine = address == null ? "NA" : address.AddressLine
                };






            foreach (var item in MSOuterJOIN)
            {
                Console.WriteLine($"Name : {item.employee.emp.Name}, Address : {item.address?.AddressLine} ");
            }





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
                new Employee { ID = 1, Name = "Preety", AddressId = 1},
                new Employee { ID = 2, Name = "Priyanka", AddressId =2},
                new Employee { ID = 3, Name = "Anurag", AddressId = 0},
                new Employee { ID = 4, Name = "Pranaya", AddressId = 0},
                new Employee { ID = 5, Name = "Hina", AddressId = 5},
                new Employee { ID = 6, Name = "Sambit", AddressId = 6}
            };
        }
    }
    public class Address
    {
        public int ID { get; set; }
        public string AddressLine { get; set; }
        public static List<Address> GetAddress()
        {
            return new List<Address>()
            {
                new Address { ID = 1, AddressLine = "AddressLine1"},
                new Address { ID = 2, AddressLine = "AddressLine2"},
                new Address { ID = 5, AddressLine = "AddressLine5"},
                new Address { ID = 6, AddressLine = "AddressLine6"},
            };
        }
    }



}
