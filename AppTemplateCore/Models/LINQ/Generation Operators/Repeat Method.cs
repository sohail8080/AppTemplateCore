using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Generation_Operators.Repeat
{

    //Linq Repeat Method in C#:
    //The LINQ Repeat Method is used to generate a sequence or collection that with a specified number 
    //of elements and each element contains the same value.

    //Note: When the count value is less than 0 then it will through ArgumentOutOfRangeException.


    class Program22
    {
        static void Main22(string[] args)
        {
            // Repeat method to generate string for 10 times and store in array
            IEnumerable<string> repeatStrings = Enumerable.Repeat("Welcome to DOT NET Tutorials", 10);

            // The LINQ Repeat method is implemented using the deferred execution.
            //So, the immediate return value is an object which stores all the required information to 
            //perform an action. 
            //The query represented by this method is not executed until the object is enumerated either 
            //by calling its GetEnumerator method directly or by using a for each loop.


        }
    }



}
