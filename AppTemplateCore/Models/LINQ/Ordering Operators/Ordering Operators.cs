using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.OrderByOperators
{
    //What Are Ordering Operators?
    //a process to manage the data in a particular order.
    //It is not changing the data or output rather 
    //this operation arranges the data in a particular order 
    //i.e.either ascending order or descending order.

    //The order may be integer-based or any other data type based.For example

    //Name of Cities of a particular state in alphabetical order.
    //Students order by Roll Number in a class.
    //It is also possible to order based on multiple columns like Employee 
    //First and Last Name in ascending order while the Salary is on descending order.

    //What are the Methods available in Linq for Sorting the data?
    //There are five methods provided by LINQ to sort the data.They are as follows

    //OrderBy
    //OrderByDescending
    //ThenBy
    //ThenByDescending
    //Reverse


    class Program2
    {
        static void Main22(string[] args)
        {
            //You can use the OrderBy method on any data type i.e. 
            //you can use character, string, decimal, integer, etc.

            //Example1: Working with integer data

            List<int> intList = new List<int>() { 10, 45, 35, 29, 100, 69, 58, 50 };

            var MS = intList.OrderBy(num => num);

            var QS = (from num in intList orderby num select num).ToList();

            //Exampe2: Working with string data.

            List<string> stringList = new List<string>() { "Preety", "Tiwary", "Agrawal", "Priyanka", "Dewangan", "Hina", "Kumar", "Manoj", "Rout", "James" };

            var MS2 = stringList.OrderBy(name => name);

            var QS2 = (from name in stringList orderby name ascending select name).ToList();

            //Using LINQ OrderBy Method with Complex type:

            var students = Student.GetAllStudents();


            var MS3 = students.OrderBy(x => x.Branch).ToList();

            var QS3 = (from std in students orderby std.Branch select std);

            //Sorting with Filtering.

            var MS4 = students.Where(std => std.Branch.ToUpper() == "CSE").OrderBy(x => x.FirstName).ToList();

            var QS4 = (from std in students where std.Branch.ToUpper() == "CSE" orderby std.FirstName select std);

            //OrderByDescending method on any data type such as string, character, float, integer,

            //Working with integer data

            var MS5 = intList.OrderByDescending(num => num);

            var QS5 = (from num in intList orderby num descending select num).ToList();

            var MS6 = stringList.OrderByDescending(name => name);

            var QS6 = (from name in stringList orderby name descending select name).ToList();

            //LINQ OrderByDescending Method with Complex type in C#:


            var MS7 = students.OrderByDescending(x => x.Branch).ToList();

            var QS7 = (from std in students orderby std.Branch descending select std);

            //Linq OrderByDescending with Filtering Operator.

            var MS8 = students.Where(std => std.Branch.ToUpper() == "ETC")
                            .OrderByDescending(x => x.FirstName).ToList();

            var QS8 = (from std in students
                       where std.Branch.ToUpper() == "ETC"
                       orderby std.FirstName descending
                       select std);

            //Linq ThenBy and ThenByDescending

            //Why we need the LINQ ThenBy and ThenByDescending Method in C#?
            //The LINQ OrderBy or OrderByDescending method works fine when you want to sort the data based
            //on a single value or a single expression.But if you want to sort the data based on 
            //multiple values or multiple expressions then you need to use the LINQ 
            //ThenBy and ThenByDescending Method along with OrderBy or OrderByDescending Method.

            var MS9 = students.OrderBy(x => x.FirstName).ThenBy(y => y.LastName).ToList();

            var QS9 = (from std in students orderby std.FirstName, std.LastName select std);

            //First sort the data in ascending order based on Branch. Then sort the data in descending 
            //order based on First Name. Finally, sort the data on the ascending order based on the 
            //Last Name values.

            var MS10 = students.OrderBy(x => x.Branch)
                      .ThenByDescending(y => y.FirstName)
                      .ThenBy(z => z.LastName)
                      .ToList();
           
            var QS10 = (from std in students orderby std.Branch ascending,
                              std.FirstName descending,
                              std.LastName
                      select std).ToList();

            //Using ThenBy and ThenByDescending along with the Where Method:


            var MS11 = students
                     .Where(std => std.Branch == "CSE")
                     .OrderBy(x => x.FirstName)
                     .ThenByDescending(y => y.LastName)
                     .ToList();
            
            var QS11 = (from std in students
                        where std.Branch == "CSE"
                      orderby std.FirstName,
                              std.LastName descending
                      select std).ToList();


        }
    }


    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Branch { get; set; }

        public static List<Student> GetAllStudents()
        {
            List<Student> listStudents = new List<Student>()
            {
                new Student{ID= 101,FirstName = "Preety",LastName = "Tiwary",Branch = "CSE"},
                new Student{ID= 102,FirstName = "Preety",LastName = "Agrawal",Branch = "ETC"},
                new Student{ID= 103,FirstName = "Priyanka",LastName = "Dewangan",Branch = "ETC"},
                new Student{ID= 104,FirstName = "Hina",LastName = "Sharma",Branch = "ETC"},
                new Student{ID= 105,FirstName = "Anugrag",LastName = "Mohanty",Branch = "CSE"},
                new Student{ID= 106,FirstName = "Anurag",LastName = "Sharma",Branch = "CSE"},
                new Student{ID= 107,FirstName = "Pranaya",LastName = "Kumar",Branch = "CSE"},
                new Student{ID= 108,FirstName = "Manoj",LastName = "Kumar",Branch = "ETC"},
                new Student{ID= 109,FirstName = "Pranaya",LastName = "Rout",Branch = "ETC"},
                new Student{ID= 110,FirstName = "Saurav",LastName = "Rout",Branch = "CSE"}
            };
            return listStudents;
        }
    }


}
