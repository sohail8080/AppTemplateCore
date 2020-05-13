using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.QuantifierOperation.All
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

    //When we want to check whether all the employees are present on a specific day or not

    //All: This specifies whether all the elements of a data source satisfy a given condition or not.
    // ALL ELEMENTS IN COLLECTION SATISFY THE CONDITION.
    //The Linq All Operator in C# is used to check whether all the elements of a data source satisfy a 
    //given condition or not. If all the elements satisfy the condition, 
    //then it returns true else return false. 

    //Note: All of the above three methods return Boolean true or false depending on 
    //whether all or some of the elements in a data source satisfy a condition.


    class Program
    {
        static void Main(string[] args)
        {
            var students = Student.GetAllStudnets();

            int[] IntArray = { 11, 22, 33, 44, 55 };
            var Result = IntArray.All(x => x > 10);

            string[] stringArray = { "James", "Sachin", "Sourav", "Pam", "Sara" };
            var Result2 = stringArray.All(name => name.Length > 5);

            //Check whether all the students having total marks greater than 250. As you can see the student 
            //James total mark is 240 which is less than 250. 
            //So here it will give you the output as false.
            bool MSResult = students.All(std => std.TotalMarks > 250);           
            bool QSResult = (from std in students select std).All(std => std.TotalMarks > 250);

            //Now we need to fetch all the student details whose mark on each subject is greater than 80.
            var MSResult2 = students.Where(std => std.Subjects.All(x => x.Marks > 80)).ToList();            
            var QSResult2 = (from std in students where std.Subjects.All(x => x.Marks > 80) select std).ToList();
       
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
