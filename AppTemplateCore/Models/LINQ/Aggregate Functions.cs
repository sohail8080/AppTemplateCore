using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.AggregateFunction.XXX
{
    //What are Linq Aggregate Functions in C#?
    //The Linq aggregate functions are used to group together the values of multiple rows.
    //It takes mulitiple rows as the input and then return the output as a single value.
    //So, simple word, we can say that the aggregate function in C# is always going to 
    //return a single value. 

    //When to use the Aggregate Functions in C#?
    //Whenever you want to perform some mathematical operations such as 
    //Sum, Count, Max, Min, Average, and Aggregate on the numeric property of a collection 
    //then you need to use the Linq Aggregate Functions.

    //What are the Aggregate Methods Provided by Linq?
    //The following are the aggregate methods provided by Linq to perform mathematical operations 
    //on a collection.

    //Sum(): This method is used to calculate the total(sum) value of the collection.
    //Max(): This method is used to find the largest value in the collection
    //Min(): This method is used to find the smallest value in the collection
    //Average(): This method is used to calculate the average value of the numeric type of the collection.
    //Count(): This method is used to count the number of elements present in the collection.
    //Aggregate(): This method is used to Performs a custom aggregation operation on the values 
    //of a collection.

    //The Linq Sum() Method belongs to the category of Aggregate Functions. The Linq Sum method in C# is used to calculates the total or sum of numeric values in the collection. 

    class Program2
    {
        static void Main2(string[] args)
        {
            int[] intNumbers = new int[] { 10, 30, 50, 40, 60, 20, 70, 90, 80, 100 };

            // SUM.............................................

            int MSTotal = intNumbers.Sum();
            int QSTotal = (from num in intNumbers select num).Sum();

            //Sum Method with filter
            int MSTotal2 = intNumbers.Where(num => num > 50).Sum();
            int QSTotal2 = (from num in intNumbers where num > 50 select num).Sum();

            //Sum Method with Predicate
            int MSTotal3 = intNumbers.Sum(num =>
            {
                if (num > 50)
                    return num;
                else
                    return 0;
            });

            //Working with Complex Type:

            var employees = Employee.GetAllEmployees();

            var TotalSalaryMS = employees.Sum(emp => emp.Salary);

            var TotalSalaryQS = (from emp in employees select emp).Sum(e => e.Salary);

            var TotalSalaryMS2 = employees.Where(emp => emp.Department == "IT").Sum(emp => emp.Salary);

            var TotalSalaryQS2 = (from emp in employees where emp.Department == "IT" select emp)
                                    .Sum(e => e.Salary);


            var TotalSalaryMS3 = employees.Sum(emp =>
            {
                if (emp.Department == "IT")
                    return emp.Salary;
                else
                    return 0;
            });

            // MAX .............................................

            int MSLergestNumber = intNumbers.Max();

            int QSLergestNumber = (from num in intNumbers select num).Max();

            int MSLergestNumber2 = intNumbers.Where(num => num < 50).Max();

            int QSLergestNumber2 = (from num in intNumbers where num < 50 select num).Max();

            int MSLergestNumber3 = intNumbers.Max(num =>
            {
                if (num < 50)
                    return num;
                else
                    return 0;
            });


            var MSHighestSalary = Employee.GetAllEmployees().Max(emp => emp.Salary);

            var QSHighestSalary = (from emp in Employee.GetAllEmployees() select emp)
                                    .Max(e => e.Salary);


            var MSHighestSalary2 = employees
                              .Where(emp => emp.Department == "IT")
                              .Max(emp => emp.Salary);

            var QSHighestSalary2 = (from emp in employees
                                    where emp.Department == "IT"
                                    select emp).Max(e => e.Salary);

            var MSHighestSalary3 = employees
                             .Max(emp =>
                             {
                                 if (emp.Department == "IT")
                                     return emp.Salary;
                                 else
                                     return 0;
                             });

            //MIN.............................................

            int MSLowestNumber = intNumbers.Min();

            int QSLowestNumber = (from num in intNumbers select num).Min();

            int MSLowestNumber2 = intNumbers.Where(num => num > 50).Min();

            int QSLowestNumber2 = (from num in intNumbers where num > 50 select num)
                                    .Min();

            int MSLowestNumber3 = intNumbers.Where(num => num > 50).Min(num =>
            {
                if (num > 50)
                    return num;
                else
                    return 0;
            });


            var MSLowestSalary = employees.Min(emp => emp.Salary);

            var QSLowestSalary = (from emp in employees select emp).Min(e => e.Salary);

            var MSLowestSalary2 = employees.Where(emp => emp.Department == "IT")
                                 .Min(emp => emp.Salary);

            var QSLowestSalary2 = (from emp in employees where emp.Department == "IT" select emp)
                                    .Min(e => e.Salary);

            //The Linq Average method is used to calculate the average of numeric values from the 
            //collection on which it is applied.This Average method can return nullable or non-nullable
            //decimal, float or double value. 

            var MSAverageValue = intNumbers.Average();

            var QSAverageValue = (from num in intNumbers select num).Average();

            var MSAverageValue1 = intNumbers.Where(num => num > 50).Average();

            var QSAverageValue1 = (from num in intNumbers
                                   where num > 50
                                   select num).Average();

            var MSAverageSalary = employees
                                 .Average(emp => emp.Salary);

            var QSAverageSalary = (from emp in employees
                                   select emp).Average(e => e.Salary);

            var MSAverageSalary1 = employees
                                 .Where(emp => emp.Department == "IT")
                                 .Average(emp => emp.Salary);

            var QSAverageSalary1 = (from emp in employees
                                    where emp.Department == "IT"
                                    select emp).Average(e => e.Salary);


            //COUNT.........................
            //The Linq Count Method used to return the number of elements present in the collection or 
            //the number of elements that have satisfied a given condition.


            int MSCount = intNumbers.Count();

            var QSCount = (from num in intNumbers select num).Count();

            int MSCount1 = intNumbers.Where(num => num > 40).Count();

            var QSCount1 = (from num in intNumbers where num > 40 select num)
                            .Count();


            var MSCount2 = employees.Count();

            var QSCount2 = (from emp in employees select emp).Count();

            var MSCount3 = employees.Where(emp => emp.Department == "IT").Count();

            var QSCount4 = (from emp in employees where emp.Department == "IT" select emp).Count();


            // AGGREGATE...................................

            //Comma-separated string.
            string[] skills = { "C#.NET", "MVC", "WCF", "SQL", "LINQ", "ASP.NET" };
            string result = skills.Aggregate((s1, s2) => s1 + ", " + s2);

            //string result = string.Empty;
            //foreach (string skill in skills)
            //{
            //    result = result + skill + ", ";
            //}

            // Product of integer numbers
            int[] intNumbers1 = { 3, 5, 7, 9 };
            int result1 = intNumbers1.Aggregate((n1, n2) => n1 * n2);

            //int result = 1;
            //foreach (int num in intNumbers)
            //{
            //    result = result * num;
            //}

            //Step1: First it multiplies(3X5) to produce the result as 15
            //Step2: Result of Step 1 i.e. 15 is then multiplied with 7 to produce the result as 105
            //Step3: Result of Step 2 i.e. 105 is then multiplied with 9 to produce the final result as 945.

            //The following example uses the Aggregate method to add the salary of all the employees.
            //Please note here we passed the seed value as 0. 

            int Salary = employees.Aggregate<Employee, int>
                (0, (TotalSalary, emp) => TotalSalary += emp.Salary);

            //In the following example, we pass a string as the seed value to the Aggregate extension method. 
            //Here the seed value is “Employee Names”.

            string CommaSeparatedEmployeeNames = employees.Aggregate<Employee, string>(
                                        "Employee Names : ",  // seed value
                                        (employeeNames, employee) => employeeNames = employeeNames + employee.Name + ", ");
            int LastIndex = CommaSeparatedEmployeeNames.LastIndexOf(",");
            CommaSeparatedEmployeeNames = CommaSeparatedEmployeeNames.Remove(LastIndex);

            //Employee Names : Preety, Priyanka, James, Hina, Anurag

            //Aggregate Method with Result Selector
            //The third overload version requires the third parameter of the Func delegate expression 
            //for the result selector so that we can formulate the result.In our previous example, 
            //once we get the comma - separated string then we remove the last comma using some 
            //additional logic.

            string CommaSeparatedEmployeeNames1 = employees.Aggregate<Employee, string, string>(
                                       "Employee Names : ",  // seed value
                                       (employeeNames, employee) => employeeNames = employeeNames + employee.Name + ",",
                                       employeeNames => employeeNames.Substring(0, employeeNames.Length - 1));

            //Employee Names : Preety,Priyanka,James,Hina,Anurag
        }
    }




    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Department { get; set; }
        public static List<Employee> GetAllEmployees()
        {
            List<Employee> listStudents = new List<Employee>()
            {
                new Employee{ID= 101,Name = "Preety", Salary = 10000, Department = "IT"},
                new Employee{ID= 102,Name = "Priyanka", Salary = 15000, Department = "Sales"},
                new Employee{ID= 103,Name = "James", Salary = 50000, Department = "Sales"},
                new Employee{ID= 104,Name = "Hina", Salary = 20000, Department = "IT"},
                new Employee{ID= 105,Name = "Anurag", Salary = 30000, Department = "IT"},
                new Employee{ID= 106,Name = "Sara", Salary = 25000, Department = "IT"},
                new Employee{ID= 107,Name = "Pranaya", Salary = 35000, Department = "IT"},
                new Employee{ID= 108,Name = "Manoj", Salary = 11000, Department = "Sales"},
                new Employee{ID= 109,Name = "Sam", Salary = 45000, Department = "Sales"},
                new Employee{ID= 110,Name = "Saurav", Salary = 25000, Department = "Sales"}
            };
            return listStudents;
        }
    }




}
