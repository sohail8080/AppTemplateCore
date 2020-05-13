using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ
{
    public class Employee22
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public List<string> Technology { get; set; }

        public static List<Employee22> GetEmployees()
        {
            List<Employee22> employees = new List<Employee22>()
            {
                new Employee22 {ID = 101, Name = "Preety", Gender = "Female", Salary = 60000,
                              Technology = new List<string>() {"C#", "Jave", "C++"} },
                new Employee22 {ID = 102, Name = "Priyanka", Gender = "Female", Salary = 50000,
                              Technology =new List<string>() { "WCF", "SQL Server", "C#" } },
                new Employee22 {ID = 103, Name = "Hina", Gender = "Female", Salary = 40000,
                              Technology =new List<string>() { "MVC", "Jave", "LINQ"}},
                new Employee22 {ID = 104, Name = "Anurag", Gender = "Male", Salary = 450000},
                new Employee22 {ID = 105, Name = "Sambit", Gender = "Male", Salary = 550000},
                new Employee22 {ID = 106, Name = "Sushanta", Gender = "Male", Salary = 700000,
                             Technology =new List<string>() { "ADO.NET", "C#", "LINQ" }}
            };
            return employees;
        }
    }


    class Program333
    {
        static void Main(string[] args)
        {
            List<int> intList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Method Syntax
            IEnumerable<int> filteredData = intList.Where(num => num > 5);
            //Query Syntax
            IEnumerable<int> filteredResult = from num in intList where num > 5 select num;

            IEnumerable<int> filteredDatavvv = intList.Where(num => CheckNumber(num));
            IEnumerable<int> filteredDatavsvv = intList.Where(predicate);

            var OddNumbersWithIndexPosition = intList.Select((num, index) => new
            {
                Numbers = num,
                IndexPosition = index
            }).Where(x => x.Numbers % 2 != 0)
                                                .Select(data => new
                                                {
                                                    Number = data.Numbers,
                                                    IndexPosition = data.IndexPosition
                                                });

            //Query Syntax
            var OddNumbersWithIndexPositiono = from number in intList.Select((num, index) => new { Numbers = num, IndexPosition = index })
                                               where number.Numbers % 2 != 0
                                               select new
                                               {
                                                   Number = number.Numbers,
                                                   IndexPosition = number.IndexPosition
                                               };

            var employees = Employee22.GetEmployees();


            var QuerySyntax = from employee in employees where employee.Salary > 50000 select employee;
            var MethodSyntax = employees.Where(emp => emp.Salary > 50000);


            // Using multiple conditions
            // We need to fetch all the employee whose gender is Male and Salary is greater than 500000.
            var QuerySyntax22 = from employee in employees
                                where employee.Salary > 500000 && employee.Gender == "Male"
                                select employee;
            var MethodSyntax22 = employees.Where(emp => emp.Salary > 500000 && emp.Gender == "Male").ToList();



            //Multiple conditions with the custom operation and projecting the data to an anonymous type:
            // All the employees whose salary is greater than or equal to 50000 and technology should not be null.
            var QuerySyntax33 = (from employee in employees
                                 where employee.Salary >= 50000 && employee.Technology != null
                                 select new
                                 {
                                     EmployeeName = employee.Name,
                                     Gender = employee.Gender,
                                     MonthlySalary = employee.Salary / 12
                                 }).ToList();
            var MethodSyntax33 = employees.Where(emp => emp.Salary >= 50000 && emp.Technology != null)
                               .Select(emp => new
                               {
                                   EmployeeName = emp.Name,
                                   Gender = emp.Gender,
                                   MonthlySalary = emp.Salary / 12
                               }).ToList();


            // Fetching elements along with the Index position
            //Here we need to fetch all the employees whose Gender is Male and Salary is greater than 
            //500000 along with their index position to an anonymous type.

            var QuerySyntax55 = (from data in employees.Select((Data, index) => new { employee = Data, Index = index })
                                 where data.employee.Salary >= 500000 && data.employee.Gender == "Male"
                                 select new
                                 {
                                     EmployeeName = data.employee.Name,
                                     Gender = data.employee.Gender,
                                     Salary = data.employee.Salary,
                                     IndexPosition = data.Index
                                 }).ToList();

            var MethodSyntax55 = employees.Select((Data, index) => new { employee = Data, Index = index })
                               .Where(emp => emp.employee.Salary >= 500000 && emp.employee.Gender == "Male")
                               .Select(emp => new
                               {
                                   EmployeeName = emp.employee.Name,
                                   Gender = emp.employee.Gender,
                                   Salary = emp.employee.Salary,
                                   IndexPosition = emp.Index
                               })
                               .ToList();



        }

        static Func<int, bool> predicate = i => i > 5;

        public static bool CheckNumber(int number)
        {
            if (number > 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }



}
