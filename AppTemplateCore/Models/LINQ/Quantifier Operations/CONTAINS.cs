using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.QuantifierOperation.Contains
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

    //When we need to check if any of the employees having the names James is present or not

    //Contains: This method is used to check whether the data source contains a specified element or not.
    // COLLECTION CONTAINS SPECIFIC ELMENT OR NOT.
    //The Linq Contains Method in C# is used to check whether a sequence or collection (i.e. data source) 
    //contains a specified element or not. If the data source contains the specified element, 
    //then it returns true else return false. 
    //There Contains method in C# is implemented in two different namespaces 
    //The Contains method belongs to System.Collections.Generic namespace takes one element as an 
    //input parameter and if that element present in the data source then it returns true else false.
    //There are two overloaded versions available for the Contains method that belongs to 
    //System.Linq namespace and of the method take IEqualityComparer.
    //Note: This method works in a different manner when working with complex type objects.
    //For complex type objects, it only checks the reference, not the values.
    //In order to work with values, we need to use IEqualityComparer.
    

    //Note: All of the above three methods return Boolean true or false depending on 
    //whether all or some of the elements in a data source satisfy a condition.


    class Program
    {
        static void Main(string[] args)
        {
            var students = Student.GetAllStudnets();

            int[] IntArray = { 11, 22, 33, 44, 55 };
            
            string[] stringArray = { "James", "Sachin", "Sourav", "Pam", "Sara" };


            var IsExistsMS = IntArray.Contains(33);           
            var IsExistsQS = (from num in IntArray select num).Contains(33);

            ////This method belongs to System.Collections.Generic namespace
            var IsExistsMS1 = stringArray.Contains("Anurag");

            // //This method belongs to System.Linq namespace
            var IsExistsMS2 = stringArray.AsEnumerable().Contains("Anurag");            
            var IsExistsQS2 = (from num in stringArray select num).Contains("Anurag");
            
            
            var student1 = new Student() { ID = 101, Name = "Priyanka", TotalMarks = 275 };
            var student2 = new Student() { ID = 102, Name = "Preety", TotalMarks = 375 };
            //The following example returns True as the object that we pass to the Contains method is 
            //exists in the data source.
            //Note: While working with complex type, the Contains method checks the object reference, not the values 
            //of the object. In the above example, the object reference of the object we passed is available 
            //in the data source, so it returns true.
            //Linq Contains Method in C# does not check the values rather it checks the object reference
            //The following example returns false even though the values that we passed is available in the data source. 
            //This is because the Linq Contains Method in C# does not check the values rather it checks the 
            //object reference and in this case, the object references are different.
            var IsExistsMS55 = students.Contains(student1);            
            var IsExistsQS55 = (from num in students select num).Contains(student1);

            var IsExistsMS555 = students.Contains(new Student() { ID = 101, Name = "Priyanka", TotalMarks = 275 });
            var student1555 = new Student() { ID = 101, Name = "Priyanka", TotalMarks = 275 };
            
            var IsExistsQS555 = (from num in students
                              select num).Contains(student1555);


            //If you want to check the values rather than the reference then you need to create a class and 
            //need to implement the IEqualityComparere interface. 
            //Then you need to use the overloaded version of the Contains method which takes IEqualityComparere 
            //as a parameter.


            //Createing Student Comparer Instance
            StudentComparer studentComparer = new StudentComparer();
            Student studenttoCheck = new Student() { ID = 101, Name = "Priyanka", TotalMarks = 275 };
           
            var IsExistsMS1212 = students.Contains(studenttoCheck, studentComparer);

            var studenttoCheck2 = new Student() { ID = 101, Name = "Priyanka", TotalMarks = 275 };
            
            var IsExistsQS232 = (from num in students select num).Contains(studenttoCheck2, studentComparer);



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



    public class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            //If both object refernces are equal then return true
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            //If one of the object refernce is null then return false
            if (x is null || y is null)
            {
                return false;
            }
            return x.ID == y.ID && x.Name == y.Name && x.TotalMarks == y.TotalMarks;
        }
        public int GetHashCode(Student obj)
        {
            //If obj is null then return 0
            if (obj is null)
            {
                return 0;
            }
            int IDHashCode = obj.ID.GetHashCode();
            int NameHashCode = obj.Name == null ? 0 : obj.Name.GetHashCode();
            int TotalMarksHashCode = obj.TotalMarks.GetHashCode();
            return IDHashCode ^ NameHashCode ^ TotalMarksHashCode;
        }
    }




}
