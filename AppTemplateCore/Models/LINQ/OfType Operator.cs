using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ
{
    //The OfType Operator in C# belongs to the filtering category of LINQ operators
    // filter specific type data from a data source based on the data type we passed to this 
    //operator. For example, if we have a collection that stores both integer and string 
    //values and if we need to fetch either only the integer values or only the string values 
    //from that collection then we need to use the OfType operator.

    class Programjkkk
    {
        static void Main(string[] args)
        {
            List<object> dataSource = new List<object>()
            {
                "Tom", "Mary", 50, "Prince", "Jack", 10, 20, 30, 40, "James"
            };

            List<int> intData = dataSource.OfType<int>().ToList();

            var stringData = (from name in dataSource where name is string select name).ToList();

            var intDataa = dataSource.OfType<int>().Where(num => num > 30).ToList();

            var stringDataa = (from name in dataSource where name is string && name.ToString().Length > 3
                              select name).ToList();
        }
    }


}
