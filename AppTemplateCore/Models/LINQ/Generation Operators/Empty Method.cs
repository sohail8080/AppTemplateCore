using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Generation_Operators.Empty
{
    class Program22
    {


        static void Main2(string[] args)
        {

            //Linq Empty Method in C#: 
            //The Empty Method belongs to the Generation Operator category.
            //It is a static method included in the static Enumerable class. 
            //The LINQ Empty Method is used to return an empty collection(i.e.IEnumerable<T>) 
            //of a specified type.

            var emptyCollection1 = Enumerable.Empty<string>();
            var emptyCollection2 = Enumerable.Empty<Student>();


            //Why do we need the LINQ Empty Method?
            //Consider one scenario, where our application calls a method which returns an IEnumerable<int>. 
            //There might be a situation where the method returns null. 
            // when using the following list will cause the NULL reference exception.
            IEnumerable<int> integerSequence = GetData();

            //Solution1:
            // We need to check before using the collection
            IEnumerable<int> integerSequence222 = GetData();
            if (integerSequence222 != null)
            {
                // use collection
            }

            //Solution2:
            //solve the problem by using the LINQ Empty Method 
            //using the NULL-COALESCING operator

            IEnumerable<int> integerSequence333 = GetData() ?? Enumerable.Empty<int>(); ;
            // use the collection

        }

        private static IEnumerable<int> GetData()
        {
            return null;
        }

    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }



}
