using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Concatmethod
{

    //The Linq Concat Method in C# is used to concatenate two sequences into one sequence. 

    class Program
    {
        static void Main(string[] args)
        {
            List<int> sequence1 = new List<int> { 1, 2, 3, 4 };
            List<int> sequence2 = new List<int> { 2, 4, 6, 8 };
            var result = sequence1.Concat(sequence2);

            //duplicate elements are not removed.Now let us concatenate the above 
            //two sequences using the Union operator and observe what happened.

            var result2 = sequence1.Union(sequence2);

            //duplicate elements are removed from the result set.

            //What is the difference between Concat and Union operators in Linq?
            //The Concat operator is used to concatenate two sequences into one sequence without 
            //removing the duplicate elements.That means it simply returns the elements from the 
            //first sequence followed by the elements from the second sequence.
            //On the other hand, the Linq Union operator is also used to concatenate two sequences into
            //one sequence by removing the duplicate elements. 
            //Note: While working with the Concat operator if any of the sequences is null then it will
            //throw an exception.



        }
    }



}
