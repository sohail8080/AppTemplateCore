using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Append
{

    //Linq Append Method in C#:
    //The LINQ Append Method is used to append a value to the end of the sequence.
    //This Append method does not modify the elements of the sequence.
    //Instead, it creates a copy of the sequence with the new element.
    //A new sequence is returned that ends with the element.
    //When the source is null, it will throw ArgumentNullException.
    //This method is support from Framework 4.7.1 or later.

    class Program2
    {
        static void Main22(string[] args)
        {
            // Creating a list of integer
            List<int> intSequence = new List<int> { 10, 20, 30, 40 };

            // Trying to append 5 at the end of the intSequence
            //It doesn't work because the original list has not been changed
            // same list is appeneded
            intSequence.Append(5);           
            Console.WriteLine(string.Join(", ", intSequence));

            // It works now because we are using a changed copy of the original sequence
            Console.WriteLine(string.Join(", ", intSequence.Append(5)));

            // Creating a new sequence explicitly
            List<int> newintSequence = intSequence.Append(5).ToList();           
            Console.WriteLine(string.Join(", ", newintSequence));
           
        }
    }




}
