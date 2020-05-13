using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Join_Operators.ThreeDS
{

    class Program
    {
        static void Main(string[] args)
        {
            //Data Source1
            //Joining with Address Data Source (Data Source2)
            //Joining with Department Data Source (Data Source3)
            //Projecting the result set
            //Joining three Data Sources using Query Syntax in Linq:
            //If you want to join the fourth data source then you need to write another join within the query.

            var employees = Employee.GetAllEmployees();
            var addresses = Address.GetAllAddresses();
            var departments = Department.GetAllDepartments();

            var JoinMultipleDSUsingQS = (
                                         from emp in employees
                                         join adrs in addresses
                                         on emp.AddressId equals adrs.ID
                                         join dept in departments
                                         on emp.DepartmentId equals dept.ID
                                         select new
                                         {
                                             ID = emp.ID,
                                             EmployeeName = emp.Name,
                                             DepartmentName = dept.Name,
                                             AddressLine = adrs.AddressLine
                                         }).ToList();


            //Using Method syntax to perform Join using Multiple Data Sources:
            //Employee data Source (i.e. Data Source 1)
            //Joining with Address data Source (i.e. Data Source 2)
            //Inner Data Source 1
            //Outer Key selector
            //Inner Key selector
            //Result set
            // Joinging with Department data Source (i.e. data Source 3)
            //Inner Data Source 2
            //You cannot access the outer key selector directly
            //You can only access with the result set created in previous step
            //i.e. using empLevel1 and addLevel1
            //Outer Key selector
            //Inner Key selector
            //Result set
            //Creating the actual result set

            var JoinMultipleDSUsingMS =
                employees.Join(addresses, emp1 => emp1.AddressId, add1 => add1.ID, (emp1, add1) => new { emp1, add1 })
                         .Join(departments, empaddr => empaddr.emp1.DepartmentId, dept => dept.ID, (empaddr, dept) => new { empaddr, dept })
                         .Select(e => new
                         {
                             // Complete Address is available as e.empaddr.add1
                             // Complete Employee is available as e.empaddr.emp1
                             // Complete Department is available as e.dept
                             ID = e.empaddr.emp1.ID,
                             EmployeeName = e.empaddr.emp1.Name,
                             AddressLine = e.empaddr.add1.AddressLine,
                             DepartmentName = e.dept.Name
                         }).ToList();



        }
    }



    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public int DepartmentId { get; set; }
        public static List<Employee> GetAllEmployees()
        {
            return new List<Employee>()
            {
                new Employee { ID = 1, Name = "Preety", AddressId = 1, DepartmentId = 10    },
                new Employee { ID = 2, Name = "Priyanka", AddressId = 2, DepartmentId =20   },
                new Employee { ID = 3, Name = "Anurag", AddressId = 3, DepartmentId = 30    },
                new Employee { ID = 4, Name = "Pranaya", AddressId = 4, DepartmentId = 0    },
                new Employee { ID = 5, Name = "Hina", AddressId = 5, DepartmentId = 0       },
                new Employee { ID = 6, Name = "Sambit", AddressId = 6, DepartmentId = 0     },
                new Employee { ID = 7, Name = "Happy", AddressId = 7, DepartmentId = 0      },
                new Employee { ID = 8, Name = "Tarun", AddressId = 8, DepartmentId = 0      },
                new Employee { ID = 9, Name = "Santosh", AddressId = 9, DepartmentId = 10   },
                new Employee { ID = 10, Name = "Raja", AddressId = 10, DepartmentId = 20    },
                new Employee { ID = 11, Name = "Ramesh", AddressId = 11, DepartmentId = 30  }
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
                new Address { ID = 1, AddressLine = "AddressLine1"      },
                new Address { ID = 2, AddressLine = "AddressLine2"      },
                new Address { ID = 3, AddressLine = "AddressLine3"      },
                new Address { ID = 4, AddressLine = "AddressLine4"      },
                new Address { ID = 5, AddressLine = "AddressLine5"      },
                new Address { ID = 9, AddressLine = "AddressLine9"      },
                new Address { ID = 10, AddressLine = "AddressLine10"    },
                new Address { ID = 11, AddressLine = "AddressLine11"    },
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
                    new Department { ID = 10, Name = "IT"       },
                    new Department { ID = 20, Name = "HR"       },
                    new Department { ID = 30, Name = "Payroll"  },
                };
        }
    }



}
