using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.QuantifierOperation.Any
{
    //What are the LINQ Quantifier Operations?
    //We need to use the LINQ Quantifier Operators on a data source when we want to 
    //check if some or all  of the elements of that data source satisfy a condition or not.

    //That means, here we have a data source and also we have a condition.
    //Then we need to check whether 
    //all or some of the elements of that data source satisfied the condition or not.

    //All the methods in quantifier operations are always going to return a Boolean value.
    //That means if the all or some of the elements in the data source satisfy the given condition 
    //then it is going to return true else it is going to return false.

    //Note: The condition that we specify may be for some or all of the elements.

    //Examples:
    //We need to use the quantifier operations on the below scenarios.

    
    //In a specific class of students, we need to check if any of the students having marks more than 90%

    //Any: This specifies whether at least one of the elements of a data source satisfies the condition or not.
    // ANY ONE ELEMENT IN COLLECTION SPECIFY THE CONDITION
    //The Linq Any Operator in C# is used to check whether at least one of the elements of a data source 
    //satisfies a given condition or not. If any of the elements satisfy the given condition, 
    //then it returns true else return false.
    //It is also used to check whether a collection contains some data or not. That means it checks the length of the collection also. 
    //If it contains any data then it returns true else return false. 

    //Note: All of the above three methods return Boolean true or false depending on 
    //whether all or some of the elements in a data source satisfy a condition.


    class Program22
    {
        static void Main22(string[] args)
        {
            var students = Student.GetAllStudnets();

            int[] IntArray = { 11, 22, 33, 44, 55 };           
            string[] stringArray = { "James", "Sachin", "Sourav", "Pam", "Sara" };
            

            //The following example returns true as the collection contains at least one element.
            var ResultMS = IntArray.Any();            
            var ResultQS = (from num in IntArray select num).Any();

            //The following program returns false as there is no element that is less than 10.
            var Result3 = IntArray.Any(x => x < 10);

            //The following example returns true as some of the names are greater than 5 characters.
            var ResultMS3 = stringArray.Any(name => name.Length > 5);           
            var ResultQS3 = (from name in stringArray select name).Any(name => name.Length > 5);

            //Check whether any students having total marks greater than 250. As you can see excepts James 
            //all the students having a total mark greater than 250. 
            //So here it will give you the output as true.
            bool MSResult4 = students.Any(std => std.TotalMarks > 250);
            bool QSResult4 = (from std in students select std).Any(std => std.TotalMarks > 250);

            //Now we need to fetch all the student details whose mark on any subject is greater than 80.
            var MSResult5 = students.Where(std => std.Subjects.Any(x => x.Marks > 90)).ToList();           
            var QSResult5 = (from std in students where std.Subjects.Any(x => x.Marks > 90) select std)
                                .ToList();
        }
    }




    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TotalMarks { get; set; }
        public List<Subject> Subjects { get; set; }
        public static List<Student> GetAllStudnets()
        {
            List<Student> listStudents = new List<Student>()
            {
                new Student{ID= 101,Name = "Preety", TotalMarks = 265,
                    Subjects = new List<Subject>()
                    {
                        new Subject(){SubjectName = "Math", Marks = 80},
                        new Subject(){SubjectName = "Science", Marks = 90},
                        new Subject(){SubjectName = "English", Marks = 95}
                    }},
                new Student{ID= 102,Name = "Priyanka", TotalMarks = 278,
                    Subjects = new List<Subject>()
                    {
                        new Subject(){SubjectName = "Math", Marks = 90},
                        new Subject(){SubjectName = "Science", Marks = 95},
                        new Subject(){SubjectName = "English", Marks = 93}
                    }},
                new Student{ID= 103,Name = "James", TotalMarks = 240,
                    Subjects = new List<Subject>()
                    {
                        new Subject(){SubjectName = "Math", Marks = 70},
                        new Subject(){SubjectName = "Science", Marks = 80},
                        new Subject(){SubjectName = "English", Marks = 90}
                    }},
                new Student{ID= 104,Name = "Hina", TotalMarks = 275,
                    Subjects = new List<Subject>()
                    {
                        new Subject(){SubjectName = "Math", Marks = 90},
                        new Subject(){SubjectName = "Science", Marks = 90},
                        new Subject(){SubjectName = "English", Marks = 95}
                    }},
                new Student{ID= 105,Name = "Anurag", TotalMarks = 255,
                    Subjects = new List<Subject>()
                    {
                        new Subject(){SubjectName = "Math", Marks = 80},
                        new Subject(){SubjectName = "Science", Marks = 90},
                        new Subject(){SubjectName = "English", Marks = 85}
                    }
                },
            };
            return listStudents;
        }
    }


    public class Subject
    {
        public string SubjectName { get; set; }
        public int Marks { get; set; }
    }

       


}
