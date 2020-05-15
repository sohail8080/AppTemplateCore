using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.CastOperator
{

    //Cast Operator in C#:
    //The Cast Operator in C# is used to casts all the elements of a collection (System.Collections.IEnumerable) 
    //to a specified type and then return a new System.Collections.Generic.IEnumerable<T> collection 
    //which contains all the elements of the source sequence cast to the specified type. 
    //This method uses deferred execution.


    //Exceptions: It will throw System.ArgumentNullException when the source is null.  
    //It also throws System.InvalidCastException when an element in the source sequence cannot be cast to 
    //the specified type TResult.

    //the source sequence contains 3 elements and all these three elements are of type integer. 
    //So, all these elements are cast to integers.

    class Program22
    {
        public static void Main22()
        {
            ArrayList list = new ArrayList { 10, 20, 30 };

            IEnumerable<int> result = list.Cast<int>();
            //Output: 10 20 30


            //In the following example, the line “list.Add(“40”);” will throw an exception. 
            //This is because it is a string value and we are trying to convert it into an int. 
            //So, it will throw System.InvalidCastException.


            ArrayList list2 = new ArrayList
            { 10, 20, 30, };

            //The following statement throws System.InvalidCastException
            list2.Add("40");
            IEnumerable<int> result2 = list2.Cast<int>();


            //In the following example, the source sequence is null. 
            //So, when we run the application, it will throw System.ArgumentNullException.

            ArrayList list3 = null;
            //Throws System.ArgumentNullException
            IEnumerable<int> result3 = list3.Cast<int>();

        }
    }

}
