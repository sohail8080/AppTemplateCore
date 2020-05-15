using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Partitioning_Operators.Paging
{
    //Why do we need a Paging in Real-time application?
    //What is paging?
    //Advantages and disadvantages of using Paging.
    //Example of implementing Paging using Skip and Take method in C#.NET

    //Why we need Paging in Real-time application?
    //Suppose we have a data source with lots of records and we need to display those records in a view.
    //We can display all the records in a view at once. 
    //If we do so then we will get the following disadvantages.

    //Network issue (As huge data is traveled)
    //Memory Management(Due to heavy data showing in the view, it may cause memory issue and maybe the page becomes unresponsive)
    //Performance(we will get bad performance as it takes more time to travel in the network)

    //So, in order to solve the above problems, we need to go for Paging.

    //What is paging?
    //Paging is nothing but a process in which we will divide a large data source into multiple pages.
    //At one page we need to display a certain number of records.
    //And next records can be visible with next – previous buttons or scroll or using any other techniques.

    //Advantages of using Paging:
    //We will get the following advantages

    //Faster data transfer.This is because fewer data will be traveled in the network.
    //Improve memory management. This is because we are not showing all the data in a view.
    //Better performance.
    //Drawback:

    //In a client-server architecture, the number of requests between the client and server is increased.
    //In such cases, you may get the data at once and store it locally and then implement the paging 
    //at the client-side.

    //How to implement paging?
    //We can implement the paging using the Linq Skip and Take method.
    //Here we need to understand two things one is PageNumber and the other one is the 
    //number of records per page.

    //Let say Page Number = PN and Number Of Records Per Page = NRP, 
    //then you need to use the following formula


    //Result = DataSource.Skip((PN – 1) * NRP).Take(NRP)

    class Program22
    {
        static void Main22(string[] args)
        {
            int RecordsPerPage = 4;
            int PageNumber = 0;
            do
            {
                Console.WriteLine("Enter the Page Number between 1 and 4");

                if (int.TryParse(Console.ReadLine(), out PageNumber))
                {
                    if (PageNumber > 0 && PageNumber < 5)
                    {
                        var employees = Employee.GetAllEmployees()
                                    .Skip((PageNumber - 1) * RecordsPerPage)
                                    .Take(RecordsPerPage).ToList();
                        Console.WriteLine();
                        Console.WriteLine("Page Number : " + PageNumber);
                        foreach (var emp in employees)
                        {
                            Console.WriteLine($"ID : {emp.ID}, Name : {emp.Name}, Department : {emp.Department}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please Enter a Valid Page Number");
                    }
                }
                else
                {
                    Console.WriteLine("Please Enter a Valid Page Number");
                }
            } while (true);
        }
    }



    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public static List<Employee> GetAllEmployees()
        {
            return new List<Employee>()
            {
                new Employee() {ID = 1, Name = "Pranaya", Department = "IT" },
                new Employee() {ID = 2, Name = "Priyanka", Department = "IT" },
                new Employee() {ID = 3, Name = "Preety", Department = "IT" },
                new Employee() {ID = 4, Name = "Sambit", Department = "IT" },
                new Employee() {ID = 5, Name = "Sudhanshu", Department = "HR" },
                new Employee() {ID = 6, Name = "santosh", Department = "HR" },
                new Employee() {ID = 7, Name = "Happy", Department = "HR" },
                new Employee() {ID = 8, Name = "Raja", Department = "IT" },
                new Employee() {ID = 9, Name = "Tarun", Department = "IT" },
                new Employee() {ID = 10, Name = "Bablu", Department = "IT" },
                new Employee() {ID = 11, Name = "Hina", Department = "HR" },
                new Employee() {ID = 12, Name = "Anurag", Department = "HR" },
                new Employee() {ID = 13, Name = "Dillip", Department = "HR" },
                new Employee() {ID = 14, Name = "Manoj", Department = "HR" },
                new Employee() {ID = 15, Name = "Lima", Department = "IT" },
                new Employee() {ID = 16, Name = "Sona", Department = "IT" },
            };
        }
    }


}
