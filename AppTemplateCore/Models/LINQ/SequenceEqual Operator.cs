using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.SequenceEqual
{

    //SequenceEqual Operator in LINQ

    //The SequenceEqual Operator in Linq is used to check whether two sequences are equal or not. 
    //If two sequences are equal then it returns true else it returns false.

    //Two sequences are considered to be equal when both the sequences have the 
    //same number of elements as well as the 
    //same values should be present in the same order.

    //There are two overloaded versions available for this SequenceEqual method 
    //second overloaded versions take an extra IEqualityComparer parameter.


    class Program22
    {
        static void Main22(string[] args)
        {
            List<string> cityList1 = new List<string> { "Delhi", "Mumbai", "Hyderabad" };
            List<string> cityList2 = new List<string> { "Delhi", "Mumbai", "Hyderabad" };

            bool IsEqual = cityList1.SequenceEqual(cityList2);//Output: True

            //The default comparer to the SequencesEqual method uses is case sensitive. 
            //So, in the below example, it returns false as the values are in case-sensitive.

            List<string> cityList11 = new List<string> { "DELHI", "mumbai", "Hyderabad" };
            List<string> cityList21 = new List<string> { "delhi", "MUMBAI", "Hyderabad" };

            bool IsEqual1 = cityList11.SequenceEqual(cityList21);//Output: False

            //If we want the comparison to be case-insensitive, then you need to use the other 
            //overloaded version of the SequenceEqual() method which takes IEqualityComparer 
            //as a parameter as shown in the below example.

            List<string> cityList12 = new List<string> { "DELHI", "mumbai", "Hyderabad" };
            List<string> cityList22 = new List<string> { "delhi", "MUMBAI", "Hyderabad" };

            bool IsEqual2 = cityList12.SequenceEqual(cityList22, StringComparer.OrdinalIgnoreCase);
            //Output: True


            //In the following example, the SequenceEqual method returns false. 
            //This is because the data are not present in the same order in both the sequences.

            List<string> cityList13 = new List<string> { "Delhi", "Mumbai", "Hyderabad" };
            List<string> cityList23 = new List<string> { "Delhi", "Hyderabad", "Mumbai" };

            bool IsEqual3 = cityList13.SequenceEqual(cityList23, StringComparer.OrdinalIgnoreCase);
            //Output: False

            //If you want to solve the problem of the previous example, 
            //then first you need to sort the data and then apply the SequenceEqual method 
            //as shown in the below example.

            List<string> cityList14 = new List<string> { "Delhi", "Mumbai", "Hyderabad" };
            List<string> cityList24 = new List<string> { "Delhi", "Hyderabad", "Mumbai" };
            bool IsEqual4 = cityList14.OrderBy(city => city)
                    .SequenceEqual(cityList2.OrderBy(city => city), StringComparer.OrdinalIgnoreCase);
            //Output: True


            //Working with Complex Type:
            //GetStudents1 and GetStudents2. Further, if you notice these two methods are going to return the same data.

            List<Student> StudentList1 = Student.GetStudents1();
            List<Student> StudentList2 = Student.GetStudents2();
            bool IsEqual5 = StudentList1.SequenceEqual(StudentList2);

            //Output: False
            //Both the sequence contains the same data but here we are getting the output as False.
            //This is because when comparing the complex types, the default comparer which is used by 
            //the SequenceEqual method will only check if the object references are equal or not.

            //How to solve the above problem?
            //There are many ways we can solve the above problem as follows.

            //1-We need to use the other overloaded version of the SequenceEqual method to which we can pass a 
            //custom class that implements the IEqualityComparer interface.
            //2-Project the properties into a new anonymous type, 
            //which overrides Equals() and GetHashCode() methods.
            //3-In the Student class override the Equals() and GetHashCode() methods.


            List<Student> StudentList12 = Student.GetStudents1();
            List<Student> StudentList22 = Student.GetStudents2();
            StudentComparer studentComparer = new StudentComparer();
            bool IsEqual6 = StudentList12.SequenceEqual(StudentList22, studentComparer);
            //t should return True.

            //Using Anonymous Type:
            List<Student> StudentList13 = Student.GetStudents1();
            List<Student> StudentList23 = Student.GetStudents2();
            var IsEqual7 = StudentList13.Select(std => new { std.ID, std.Name })
                    .SequenceEqual(StudentList23.Select(std => new { std.ID, std.Name }));
            //Output: True



            //Overriding the Equals and GetHashCode method in Student Class:

            List<Student> StudentList15 = Student.GetStudents1();
            List<Student> StudentList25 = Student.GetStudents2();
            var IsEqual8 = StudentList15.SequenceEqual(StudentList25);
            //Output: True

        }
    }


    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public static List<Student> GetStudents1()
        {
            List<Student> listStudents = new List<Student>()
            {
                new Student{ID= 101,Name = "Preety"},
                new Student{ID= 102,Name = "Priyanka"}
            };
            return listStudents;
        }
        public static List<Student> GetStudents2()
        {
            List<Student> listStudents = new List<Student>()
            {
                new Student{ID= 101,Name = "Preety"},
                new Student{ID= 102,Name = "Priyanka"}
            };
            return listStudents;
        }


        public override bool Equals(object x)
        {
            return this.ID == ((Student)x).ID && this.Name == ((Student)x).Name;
        }
        public override int GetHashCode()
        {
            return this.ID.GetHashCode() ^ this.Name.GetHashCode();
        }

    }


    public class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return x.ID == y.ID && x.Name == y.Name;
        }
        public int GetHashCode(Student obj)
        {
            return obj.ID.GetHashCode() ^ obj.Name.GetHashCode();
        }
    }

}
