using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.ToDictionary
{

    //ToDictionary Method in C#:
    //The ToDictionary method in C# is used to creates a 
    //System.Collections.Generic.Dictionary<TKey,TValue> from an System.Collections.Generic.IEnumerable<T> 
    //according to a specified key selector. 
    //This method causes the query to be executed immediately.

    //There are four overloaded versions available for this method.

    //It throws ArgumentNullException when the 
    //source or keySelector is null or 
    //the keySelector function produces a key that is null.

    //Throws ArgumentException when the keySelector produces duplicate keys for two elements.

    //Example: Converting List to a Dictionary.

    class Program22
    {
        public static void Main22()
        {

            List<Product> listProducts = new List<Product>
            {
                new Product { ID= 1001, Name = "Mobile", Price = 800 },
                new Product { ID= 1002, Name = "Laptop", Price = 900 },
                new Product { ID= 1003, Name = "Desktop", Price = 800 }
            };



            Dictionary<int, Product> productsDictionary = listProducts.ToDictionary(x => x.ID);

            foreach (KeyValuePair<int, Product> kvp in productsDictionary)
            {
                Console.WriteLine(kvp.Key + " Name : " + kvp.Value.Name + ", Price: " + kvp.Value.Price);
            }


            //we are converting List<Product> to a Dictionary. 
            //Here, the product ID is the key and the product name is its value.
            Dictionary<int, string> productsDictionary2 = listProducts.ToDictionary(x => x.ID, x => x.Name);

            foreach (KeyValuePair<int, string> kvp in productsDictionary2)
            {
                Console.WriteLine("Key : " + kvp.Key + " Value : " + kvp.Value);
            }


            // it throws a System.ArgumentException as there are two products with the same ID

            List<Product> listProducts3 = new List<Product>
            {
                new Product { ID= 1001, Name = "Mobile", Price = 800 },
                new Product { ID= 1001, Name = "Laptop", Price = 900 },
                new Product { ID= 1003, Name = "Desktop", Price = 800 }
            };
            Dictionary<int, string> productsDictionary3 = listProducts3.ToDictionary(x => x.ID, x => x.Name);
            foreach (KeyValuePair<int, string> kvp in productsDictionary3)
            {
                Console.WriteLine("Key : " + kvp.Key + " Value : " + kvp.Value);
            }



            //it will throw System.ArgumentNullException as the source (i.e listProducts) is null.

            List<Product> listProducts4 = null;

            Dictionary<int, string> productsDictionary4 = listProducts4.ToDictionary(x => x.ID, x => x.Name);

            foreach (KeyValuePair<int, string> kvp in productsDictionary4)
            {
                Console.WriteLine("Key : " + kvp.Key + " Value : " + kvp.Value);
            }


        }
    }



    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }




}
