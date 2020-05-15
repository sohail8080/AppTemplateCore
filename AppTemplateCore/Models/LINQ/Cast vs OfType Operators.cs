using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.CastVsType
{

    class Program22
    {
        public static void Main22()
        {

            //Difference between Cast and OfType Operators

            //Difference between Cast and OfType operators:
            //The Cast operator in C# will try to cast all the elements present in the source sequence into a 
            //specified type. 
            //If any of the elements in the source sequence cannot be cast to the specified type then 
            //it will throw InvalidCastException.

            //On the other hand, the OfType operator in C# returns 
            //only the elements of the specified type and 
            //the rest of the elements in the source sequence will be ignored 
            //as well as excluded from the result. 

            //Cast Operator Example:
            //In the following example, the Cast operator throws InvalidCastException 
            //as it is unable the Cast the value “50” (it is in the form of a string) to an integer.

            ArrayList list = new ArrayList
            { 10, 20, 30, "50" };

            IEnumerable<int> result = list.Cast<int>();
            //Cast operator throws InvalidCastException 

            //OfType Operator Example:
            //Let us rewrite the same example using the OfType operator as shown below.

            ArrayList list2 = new ArrayList
            { 10, 20, 30, "50"};

            IEnumerable<int> result2 = list.OfType<int>();
            //Output: 10 20 30
            //you will not get any exception. The value “50” is ignored as well as excluded from the result.

            //When to use Cast over OfType and vice versa?

            //We need to use the Cast Operator
            //When we want to cast all the elements in the collection
            //When we know for sure the collection contains only elements of the specified type

            //We need to use the OfType operator 
            //when we want to filter the elements and return only the specified type of elements.


        }
    }



}
