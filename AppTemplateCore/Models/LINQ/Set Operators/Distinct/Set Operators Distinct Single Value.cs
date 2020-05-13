using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Distinch.SigleValue
{

    //Distinct: We need to use the Distinct() method when we want to remove the duplicate data or records 
    //from a data source. This method operates on a single data source.

    //If we need to select the distinct records from a data source(No Duplicate Records) then we need to 
    //use Set Operators.

    class Program56565
    {
        static void Main(string[] args)
        {
            List<int> intCollection = new List<int>() { 1, 2, 3, 2, 3, 4, 4, 5, 6, 3, 4, 5 };

            //Using Method Syntax
            var MS = intCollection.Distinct();

            //Using Query Syntax
            var QS = (from num in intCollection select num).Distinct();


            string[] namesArray = { "Priyanka", "HINA", "hina", "Anurag", "Anurag", "ABC", "abc" };
            var distinctNames = namesArray.Distinct();


            string[] namesArray44 = { "Priyanka", "HINA", "hina", "Anurag", "Anurag", "ABC", "abc" };
            var distinctNames44 = namesArray.Distinct(StringComparer.OrdinalIgnoreCase);


            var students = Studentppp.GetStudents();

            //Here we need to fetch all the distinct names from the student’s collection. 
            // SELECTING distinct name string, done fine. 
            var MSbbb = students.Select(std => std.Name).Distinct().ToList();
            var QSbbb = (from std in students select std.Name).Distinct().ToList();


            //Now we need to select distinct students from the collection. 
            //As you can see in our collection three students are identical and in our 
            //result set, they should appear only once.
            var MS1 = students.Distinct().ToList();
            var QS1 = (from std in students select std).Distinct().ToList();

            //As you can see, it will not select distinct students rather it select all 
            //the students.This is because the default comparer which is used for comparison 
            //is only checked whether two object references are equal and not the individual 
            //property values of the complex object.

            // DEFAULT DISTINCT OPERATOR FAILED FOR COMPARING WHOLE OBJECTS

            //We can solve the above problem in four different ways.
            //They are as follows

            //IST APPROACH: IEqualityComparer
            //We need to use the other overloaded version of Distinct() method which takes 
            //IEqualityComparer interface as an argument.
            //So here we will create a class that implements IEqualityComparer interface and then 
            //we need to pass that compare instance to the Distinct() method.

            //2ND APPROACH: Override Equals() GetHashCode()
            //In the second approach, we need to override the Equals() and GetHashCode() methods within the 
            //Student class itself.           


            // 3RD APPROACH: new anonymous type
            //In the third approach, we need to project the required properties into a new anonymous type, 
            //which already overrides the Equals() and GetHashCode() methods 
            //Here we need to project the properties of Student class into a new anonymous type, 
            //which already overrides the Equals() and GetHashCode() methods.
            //In the above example, we project the ID and Name properties to IEnumeable<’a> means to anonymous type which 
            //already overrides the Equals and GetHashCode method. 
            //Now run the application and you will see the output as expected.


            // 4TH APPROACH: IEquatable<T> interface.
            //By Implementing IEquatable<T> interface.           

            //Difference between IEqualityComparer<T> and IEquatable<T>:
            //The IEqualityComparer<T> is an interface for an object that performs the comparison on 
            //two objects of the type T whereas the IEquatable<T> is also an interface for an object of 
            //type T so that it can compare itself to another.



        }
    }


    public class Studentppp
    {
        public int ID { get; set; }

        public string Name { get; set; }


        public static List<Studentppp> GetStudents()
        {
            List<Studentppp> students = new List<Studentppp>()
            {
                new Studentppp {ID = 101, Name = "Preety" },
                new Studentppp {ID = 102, Name = "Sambit" },
                new Studentppp {ID = 103, Name = "Hina"},
                new Studentppp {ID = 104, Name = "Anurag"},
                new Studentppp {ID = 102, Name = "Sambit"},
                new Studentppp {ID = 103, Name = "Hina"},
                new Studentppp {ID = 101, Name = "Preety" },
            };
            return students;
        }
    }

         

}
