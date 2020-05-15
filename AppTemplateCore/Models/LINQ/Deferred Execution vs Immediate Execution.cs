using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Defferred
{
    //Deferred Execution vs Immediate Execution in LINQ

    //The LINQ queries are executed in two different ways as follows.

    //Deferred execution
    //Immediate execution

    //Based on the above two types of execution, the LINQ operators are divided into 2 categories.
    //They are as follows:

    //Deferred Operators or Lazy Operators:  
    //These query operators are used for deferred execution.
    //For example – select, SelectMany, where, Take, Skip, etc.
    //are belongs to Deferred or Lazy Operators category.

    //Immediate Operators or Greedy Operators: 
    //These query operators are used for immediate execution. 
    //For Example – count, average, min, max, First, Last, ToArray, ToList, etc.
    //are belongs to the Immediate or Greedy Operators category.

    //LINQ Deferred Execution:
    //In this case, the LINQ Query is not executed at the point of its declaration. 
    //That means, when we write a LINQ query, it doesn’t execute by itself. 
    //It executes only when we access the query results.
    //So, here the execution of the query is deferred until the query variable is iterated over 
    //using for each loop.

    //Advantages of Deferred Execution:

    //It avoids unnecessary query execution which improves the performance of the application.
    //The Query creation and the Query execution are decoupled which provide us the flexibility 
    //to create the query in several steps.
    //A Linq deferred execution query is always re-evaluated when we re-enumerate.
    //As a result, we always get the updated data.

    class Program22
    {
        public static void Main22()
        {

            List<Employee> listEmployees = new List<Employee>
            {
                new Employee { ID= 1001, Name = "Priyanka", Salary = 80000 },
                new Employee { ID= 1002, Name = "Anurag", Salary = 90000 },
                new Employee { ID= 1003, Name = "Preety", Salary = 80000 }
            };

            // In the below statement the LINQ Query is only defined and not executed
            // If the query is executed here, then the result should not display Santosh
            IEnumerable<Employee> result = from emp in listEmployees
                                           where emp.Salary == 80000
                                           select emp;

            // Adding a new employee with Salary = 80000 to the collection listEmployees
            listEmployees.Add(new Employee { ID = 1004, Name = "Santosh", Salary = 80000 });

            // The LINQ query is actually executed when we iterate thru using a for each loop
            // This is proved because Santosh is also included in the result
            foreach (Employee emp in result)
            {
                Console.WriteLine($" {emp.ID} {emp.Name} {emp.Salary}");
            }



            //Immediate Execution
            //In the case of Immediate Execution, the LINQ query is executed at the point of its declaration.
            //So, it forces the query to execute and gets the result immediately.

            // In the following statement, the LINQ Query is executed immediately as we are
            // Using the ToList() method which is a greedy operator which forces the query 
            // to be executed immediately

            IEnumerable<Employee> result2 = (from emp in listEmployees
                                            where emp.Salary == 80000
                                            select emp).ToList();

            // Adding a new employee with Salary = 80000 to the collection listEmployees
            // will not have any effect on the result as the query is already executed
            listEmployees.Add(new Employee { ID = 1004, Name = "Santosh", Salary = 80000 });

            // The above LINQ query is executed at the time of its creation.
            // This is proved because Santosh is not included in the result
            foreach (Employee emp in result2)
            {
                Console.WriteLine($" {emp.ID} {emp.Name} {emp.Salary}");
            }



            // In the following statement, the LINQ Query is executed immediately as we are
            // Using the Count() method which is a greedy operator which forces the query 
            // to be executed immediately
            var result3 = (from emp in listEmployees where emp.Salary == 80000 select emp).Count();
            // Adding a new employee with Salary = 80000 to the collection listEmployees
            // will not have any effect on the result as the query is already executed
            listEmployees.Add(new Employee { ID = 1004, Name = "Santosh", Salary = 80000 });




        }
    }



    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
    }



}
