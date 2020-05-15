using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.ToList
{

    //ToList Method in LINQ:
    //The ToList Method is used to create a System.Collections.Generic.List<T> collection
    //from a System.Collections.Generic.IEnumerable<T>.
    //This method causes the query to be executed immediately.

    //This method throws System.ArgumentNullException when the source sequence is null.
    //Convert int array to List<int> using the ToList method

    class Program22
    {
        public static void Main22()
        {
            //Creating Integer Array
            int[] numbersArray = { 10, 22, 30, 40, 50, 60 };
            //Converting Integer Array to List using ToList method
            List<int> numbersList = numbersArray.ToList();

            foreach (var num in numbersList)
            {
                Console.WriteLine(num);
            }
            
        }
    }


}
