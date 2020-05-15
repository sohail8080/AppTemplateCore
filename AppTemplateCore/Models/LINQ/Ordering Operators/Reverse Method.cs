using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.ReverseOperator
{
    //The LINQ Reverse method is used to reverse the data stored in a data source. 
    //That means this method will not change the data rather it simple reverse the 
    //data stored in the data source. So, as a result, we will get the output in reverse order.

    //Reverse method (System.Linq) is an extension method on IEnumerable<TSource> interface and more importantly 
    //this method also returns an IEnumerable<TSource> type.

    //Reverse method  which belongs to the System.Collections.Generic namespace 
    //is not returning any value as the return type is void.

    //Example1: System.Linq namespace Reverse method

    class Program2
    {
        static void Main22(string[] args)
        {
            int[] intArray = new int[] { 10, 30, 50, 40, 60, 20, 70, 100 };

            IEnumerable<int> ArrayReversedData = intArray.Reverse();

            IEnumerable<int> ArrayReversedData1 = (from num in intArray select num).Reverse();

            // System.Collections.Generic namespace Reverse method

            List<string> stringList = new List<string>() { "Preety", "Tiwary", "Agrawal", "Priyanka", "Dewangan" };
            stringList.Reverse();

            //How to apply the Linq Reverse method on a collection of List<T> type?
            //If you want to apply the Reverse method which belongs to System.Linq namespace on a collection 
            //of type List<T>, then first you need to convert to the List<T> collection to as 
            //IEnumerable or IQueryable collection by using the AsEnumerable() or AsQueryable() method on 
            //the data source.

            //If you use the AsEnumerable() method then it will convert the collection to IEnumerable 
            //whereas if you use AsQueryable() method then it will convert the collection to IQueryable.

            IEnumerable<string> ReverseData1 = stringList.AsEnumerable().Reverse();
            IQueryable<string> ReverseData2 = stringList.AsQueryable().Reverse();

            //The LINQ Reverse Method in C# will return the data as IEnumerable<TSource> or IQuereable<TSource> 
            //based on how we use the LINQ method.

        }
    }
}
