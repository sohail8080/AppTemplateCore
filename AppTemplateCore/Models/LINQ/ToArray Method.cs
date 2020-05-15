using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.ToArray
{

    //ToArray Method in Linq:
    //The ToArray Method is used to copies the elements of System.Collections.Generic.List<T> 
    //to a new array.
    //This method causes the query to be executed immediately.

    //Example: Convert List<int> to integer array. 


    class Program22
    {
        public static void Main22()
        {
            //Create a List
            List<int> numbersList = new List<int>()
            {
                10, 22, 30, 40, 50, 60
            };

            //Converting List to Array
            int[] numbersArray = numbersList.ToArray();


            foreach (var num in numbersArray)
            {
                Console.WriteLine(num);
            }
            
        }
    }




}
