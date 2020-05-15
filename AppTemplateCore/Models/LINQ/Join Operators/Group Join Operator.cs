using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Join_Operators.GroupJoinOperator
{

    //What is Linq Group Join?
    //In Linq, we can apply the Group Join on two or more data sources based on a common key
    //(the key must exist in both the data sources) and then it produces the result set in the 
    //form of groups.

    //In simple words, we can say that Linq Group Join is used to group the result sets based on a 
    //common key.

    //So, the Group Join is basically used to produces hierarchical data structures.
    //Each item from the first data source is paired with a set of correlated items from the 
    //second data source. 

    //There are two overloaded versions of this method is available as shown below.

    //The difference between these two methods is that the second overloaded versions take an 
    //additional IEqualityComparer.
    //So, when working with Group Join we need to understand the following things.

    //Outer Data Source
    //Inner Data Source
    //Outer Key Selector
    //Inner Key Selector
    //Result Selector

    //The common property is Department ID i.e. the ID property in Department class and DepartmentID 
    //property in Employee class. 
    //Then we create two simple methods that are going to return the respective data sources. 
    //Further, if you notice the employee with ID 8 does not have a department.

    //Here we need to group the employees by department.



    class Program22
    {
        static void Main22(string[] args)
        {
            var departments = Department.GetAllDepartments();
            var employees = Employee.GetAllEmployees();

            // Using Method Syntax

            var GroupJoinMS = departments.GroupJoin(employees, dept => dept.ID, emp => emp.DepartmentId,
                                            (dept, emp) => new { dept, emp });

            // As you can see the employee with id 8 does not display here. 
            //This is because the employee with id 8 does not belong to any department.

            foreach (var item in GroupJoinMS)
            {
                Console.WriteLine("Department :" + item.dept.Name);
                
                foreach (var employee in item.emp)
                {
                    Console.WriteLine("  EmployeeID : " + employee.ID + " , Name : " + employee.Name);
                }
            }


            //Using Query Syntax

            var GroupJoinQS = 
                from dept in departments join emp in employees on dept.ID equals emp.DepartmentId
                              into EmployeeGroups
                              select new { dept, EmployeeGroups };

            
            foreach (var item in GroupJoinQS)
            {
                Console.WriteLine("Department :" + item.dept.Name);
                
                foreach (var employee in item.EmployeeGroups)
                {
                    Console.WriteLine("  EmployeeID : " + employee.ID + " , Name : " + employee.Name);
                }
            }


            //Specifying user-defined names in ResultSet

            //It is also possible to specify user - defined names as shown in the below example.


            var GroupJoinMS2 = departments.GroupJoin(employees, dept => dept.ID, emp => emp.DepartmentId,
                    //User Defined names in Result Selector
                    (dept, emp) => new {
                        Departments = dept,
                        Employees = emp
                    }
                );

            
            var GroupJoinQS2 = 
                from dept in departments join emp in employees on dept.ID equals emp.DepartmentId into EmployeeGroups
                              //User Defined names in Result Selector
                              select new
                              {
                                  Departments = dept,
                                  Employees = EmployeeGroups
                              };


        }
    }


    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public static List<Employee> GetAllEmployees()
        {
            return new List<Employee>()
            {
                new Employee { ID = 1, Name = "Preety", DepartmentId = 10},
                new Employee { ID = 2, Name = "Priyanka", DepartmentId =20},
                new Employee { ID = 3, Name = "Anurag", DepartmentId = 30},
                new Employee { ID = 4, Name = "Pranaya", DepartmentId = 30},
                new Employee { ID = 5, Name = "Hina", DepartmentId = 20},
                new Employee { ID = 6, Name = "Sambit", DepartmentId = 10},
                new Employee { ID = 7, Name = "Happy", DepartmentId = 10},
                new Employee { ID = 8, Name = "Tarun", DepartmentId = 0},
                new Employee { ID = 9, Name = "Santosh", DepartmentId = 10},
                new Employee { ID = 10, Name = "Raja", DepartmentId = 20},
                new Employee { ID = 11, Name = "Ramesh", DepartmentId = 30}
            };
        }
    }

    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public static List<Department> GetAllDepartments()
        {
            return new List<Department>()
                {
                    new Department { ID = 10, Name = "IT"},
                    new Department { ID = 20, Name = "HR"},
                    new Department { ID = 30, Name = "Sales"  },
                };
        }
    }
}
