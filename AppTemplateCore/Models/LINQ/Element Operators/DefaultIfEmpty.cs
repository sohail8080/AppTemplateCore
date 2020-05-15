using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.ElementOperators.DefaultIfEmpty
{

    //What are Element Operators in Linq?
    //The Element Operators in Linq are used to return a single element from a data source 
    //using the element index or based on a predicate i.e.a condition.
    //These Element Operators can be used with a single data source 
    //or on a query of multiple data sources.

    //Why do we need to use the Element Operators in Linq?
    //If you want to perform the following operations, then you need to use the Element Operators.

    //Select the First record from a data source.
    //Fetch a specific record from the data source.
    //Select the last record from a data source.

    //What methods are available in the Element Operators category?
    //The following methods are provided by Linq to perform element operations.

    //ElementAt and ElementAtOrDefault
    //First and FirstOrDefault
    //Last and LastOrDefault
    //Single and SingleOrDefault
    //DefaultIfEmpty

    class Program22
    {
        static void Main22(string[] args)
        {
 
            //Linq DefaultIfEmpty Method:
            //If the data source on which the DefaultIfEmpty method is called is not empty, 
            //then the values of the original data source are going to be returned.

            //On the other hand, if the sequence or data source is empty, 
            //then it returns a sequence with the default values based on the data type.

            List<int> numbersddd = new List<int>() { 10, 20, 30 };
            //return a copy of the original values
            IEnumerable<int> resultddd = numbersddd.DefaultIfEmpty();

            List<int> numberseee = new List<int>() { };
            // return “0” as the default value. This is because 0 is the default value for the 
            //integer data type.
            IEnumerable<int> resulteee = numberseee.DefaultIfEmpty();


            List<int> numbersfff = new List<int>() { };
            // the sequence is empty but here we have supplied a default value (i.e. 5) to
            //Output: 5
            IEnumerable<int> resultfff = numbersfff.DefaultIfEmpty(5);

            List<int> numbersggg = new List<int>() { 10, 20, 30 };
            // elements which are present in the sequence are going to be returned.
            IEnumerable<int> resultggg = numbersggg.DefaultIfEmpty(5);
            
        }
    }



}
