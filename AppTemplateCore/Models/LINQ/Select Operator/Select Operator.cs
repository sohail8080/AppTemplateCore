using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }


        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee {ID = 101, FirstName = "Preety", LastName = "Tiwary", Salary = 60000 },
                new Employee {ID = 102, FirstName = "Priyanka", LastName = "Dewangan", Salary = 70000 },
                new Employee {ID = 103, FirstName = "Hina", LastName = "Sharma", Salary = 80000 },
                new Employee {ID = 104, FirstName = "Anurag", LastName = "Mohanty", Salary = 90000 },
                new Employee {ID = 105, FirstName = "Sambit", LastName = "Satapathy", Salary = 100000 },
                new Employee {ID = 106, FirstName = "Sushanta", LastName = "Jena", Salary = 160000 }
            };
            return employees;
        }
    }


    public class EmployeeBasicInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
    }



    class Program2
    {
        static void Main2(string[] args)
        {

            var employees = Employee.GetEmployees();

            // full list of full objects is returned
            //Select Operator:
            IEnumerable<Employee> basicMethod = employees;
            IEnumerable<Employee> basicQuery = from emp in employees select emp;
            List<Employee> basicQueryList = (from emp in employees select emp).ToList();
            List<Employee> basicMethodList = employees.ToList();

            // full list of one property is returned
            //How to select a single property using Select Operator?
            IEnumerable<int> basicPropQuery = (from emp in employees select emp.ID);
            IEnumerable<int> basicPropMethod = employees.Select(emp => emp.ID);
            List<int> basicPropQueryList = (from emp in employees select emp.ID).ToList();
            List<int> basicPropMethodList = employees.Select(emp => emp.ID).ToList();


            // full list of filtered properties of same object is returned
            //Our requirement is to select only the Employee First Name, Last Name, and Salary properties
            List<Employee> selectQueryList =
                                (from emp in employees
                                 select new Employee()
                                 {
                                     FirstName = emp.FirstName,
                                     LastName = emp.LastName,
                                     Salary = emp.Salary
                                 }).ToList();

            List<Employee> selectMethodList = employees.Select(emp => new Employee()
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Salary = emp.Salary
            }).ToList();


            ////////////////////////////////////////////////////////////////////////////

            // full list of filtered properties of different object is returned
            //How to Select Data to another class using LINQ Projection Operator?
            List<EmployeeBasicInfo> selQurEmpInfo =
                              (from emp in employees
                               select new EmployeeBasicInfo()
                               {
                                   FirstName = emp.FirstName,
                                   LastName = emp.LastName,
                                   Salary = emp.Salary
                               }).ToList();

            List<EmployeeBasicInfo> selMethEmpInfo = employees.
                                         Select(emp => new EmployeeBasicInfo()
                                         {
                                             FirstName = emp.FirstName,
                                             LastName = emp.LastName,
                                             Salary = emp.Salary
                                         }).ToList();

            ////////////////////////////////////////////////////////////////////////////////

            // full list of filtered properties of ananymouse object is returned
            //How to Select Data to Anonymous Type using LINQ Select Operator?
            var selectQueryAnn = (from emp in employees
                                  select new
                                  {
                                      FirstName = emp.FirstName,
                                      LastName = emp.LastName,
                                      Salary = emp.Salary
                                  });

            var selectMethodAnn = employees.Select(emp => new
                                    {
                                        FirstName = emp.FirstName,
                                        LastName = emp.LastName,
                                        Salary = emp.Salary
                                    }).ToList();

            //How to perform calculations on data selected using the LINQ Select Operator?
            // full list of filtered & calculated properties of ananymouse object is returned
            var selectQuery = (from emp in employees select new
                                            {
                                                EmployeeId = emp.ID,
                                                FullName = emp.FirstName + " " + emp.LastName,
                                                AnnualSalary = emp.Salary * 12
                                            });

            var selectMethod = employees.Select(emp => new
                                            {
                                                EmployeeId = emp.ID,
                                                FullName = emp.FirstName + " " + emp.LastName,
                                                AnnualSalary = emp.Salary * 12
                                             }).ToList();


        }
    }







}
