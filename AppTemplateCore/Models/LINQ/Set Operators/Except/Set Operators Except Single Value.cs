using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Except.SingleValue
{

    //The LINQ Except Method in C# is used to return the elements which are present in the 
    //first data source but not in the 
    //second data source.

    class Programeewwqq
    {
        static void Main(string[] args)
        {
            List<int> dataSource1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            List<int> dataSource2 = new List<int>() { 1, 3, 5, 8, 9, 10 };

            var MS = dataSource1.Except(dataSource2).ToList();
            var QS = (from num in dataSource1 select num).Except(dataSource2).ToList();

            string[] dataSource11 = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] dataSource21 = { "India", "uk", "Canada", "France", "Japan" };

            var MS1 = dataSource11.Except(dataSource21).ToList();
            var QS1 = (from country in dataSource11 select country).Except(dataSource21).ToList();

            //In spite of having the country UK in the second collection, it still shows in the output. 
            //This is because the default comparer that is being used to filter the data 
            //is case-insensitive.
            string[] dataSource13 = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] dataSource23 = { "India", "uk", "Canada", "France", "Japan" };

            //So if you want to ignore the case-sensitive then you need to use the other overloaded version of the 
            //Except() method which takes IEqualityComparer as an argument.
            //StringComparer as an argument and this StringComparer class already implements 
            //the IEqualityComparer interface.
            var MS3 = dataSource13.Except(dataSource23, StringComparer.OrdinalIgnoreCase).ToList();
            var QS3 = (from country in dataSource13 select country).Except(dataSource23, StringComparer.OrdinalIgnoreCase).ToList();


            //LINQ Except() Method with Complex Type:

            List<Student555> AllStudents = new List<Student555>()
            {
                new Student555 {ID = 101, Name = "Preety" },
                new Student555 {ID = 102, Name = "Sambit" },
                new Student555 {ID = 103, Name = "Hina"},
                new Student555 {ID = 104, Name = "Anurag"},
                new Student555 {ID = 105, Name = "Pranaya"},
                new Student555 {ID = 106, Name = "Santosh"},
            };

            List<Student555> Class6Students = new List<Student555>()
            {
                new Student555 {ID = 102, Name = "Sambit" },
                new Student555 {ID = 104, Name = "Anurag"},
                new Student555 {ID = 105, Name = "Pranaya"},
            };

            var MSbbbb = AllStudents.Select(x => x.Name).Except(Class6Students.Select(y => y.Name)).ToList();

            var QSbbbb = (from std in AllStudents select std.Name)
                                                .Except(Class6Students.Select(y => y.Name)).ToList();


            var MSbbbb1 = AllStudents.Select(x => x.Name).Except(Class6Students.Select(y => y.Name), 
                StringComparer.OrdinalIgnoreCase).ToList();

            var QSbbbb1 = (from std in AllStudents select std.Name).Except(Class6Students.Select(y => y.Name), 
                StringComparer.OrdinalIgnoreCase).ToList();


            //Now we need to select all the information of all the students from the first data source 
            //which are not present in the second data source. 
            //Let us modify the program class as shown below.

            var MS777 = AllStudents.Except(Class6Students).ToList();
            var QS777 = (from std in AllStudents select std).Except(Class6Students).ToList();

            //As you can see, it displays all the student’s data from the first data source. 
            //This is because the default comparer which is used for comparison is only checking 
            //whether two object references are equal and not the individual property values 
            //of the complex object.



        }
    }


    //The LINQ Except() Method in C# works slightly different manner when working with complex types 
    //such as Employee, Product, Student, etc. 

    public class Student555
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }

}
