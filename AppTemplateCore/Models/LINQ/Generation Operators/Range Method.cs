using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Generation_Operators
{

    //Range()
    //Repeat<T>()
    //Empty<T>()
    //These methods allow us to create some specific type of array, sequence, or collection 
    //using a single expression instead of creating them manually and populating them 
    //using some kind of loops.
    //That means these methods return a new sequence or collection that implements the IEnumerable<T> interface.

    //Linq Range Method in C#:
    //This method is used to Generates a sequence of integer within a specified range.

    class Program22
    {
        static void Main2(string[] args)
        {
            IEnumerable<int> numberSequence = Enumerable.Range(1, 10);
            //values from 1 to 10 as expected.

            // Range method with Filter.
            //all the even numbers which are present between 10 and 30. 
            IEnumerable<int> EvenNumbers = Enumerable.Range(10, 30).Where(x => x%2 == 0);

            IEnumerable<int> Numbers = Enumerable.Range(1, 5).Select(x => x * x);
            //Output: 1 4 9 16 25

            IEnumerable<string> NumbersWithOrdianl = Enumerable.Range(1, 5)
                            .Select(x => (x * x) + " " + CustomLogic(x)).ToArray();

            //This method is implemented by using the deferred execution. 
            //The immediate return value is an object that stores all the information that is required to 
            //perform the action. 
            //The query represented by this method is not executed until the object is enumerated 
            //either by calling its GetEnumerator method directly or by using a for each loop.


        }


        private static string CustomLogic(int x)
        {
            string result = string.Empty;
            switch (x)
            {
                case 1:
                    result = "1st";
                    break;
                case 2:
                    result = "2nd";
                    break;
                case 3:
                    result = "3rd";
                    break;
                case 4:
                    result = "4th";
                    break;
                case 5:
                    result = "5th";
                    break;
            }
            return result;
        }
    }



}


   



