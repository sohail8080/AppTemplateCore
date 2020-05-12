using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Distinct.IEquatableinterface
{

    //Distinct: We need to use the Distinct() method when we want to remove the duplicate data 
    //or records from a data source. This method operates on a single data source.
    //If we need to select the distinct records from a data source(No Duplicate Records) then 
    //we need to use Set Operators.

    // 4TH APPROACH: IEquatable<T> interface.
    //By Implementing IEquatable<T> interface.
    //Difference between IEqualityComparer<T> and IEquatable<T>:
    //The IEqualityComparer<T> is an interface for an object that performs the comparison on 
    //two objects of the type T whereas the IEquatable<T> is also an interface for an object of 
    //type T so that it can compare itself to another.


    class Program4445
    {
        static void Main(string[] args)
        {
            var studentsq = StudentQQQ.GetStudents();

            var MS2222 = studentsq.Distinct().ToList();
            var QS2222 = (from std in studentsq select std).Distinct().ToList();

        }
    }


    public class StudentQQQ : IEquatable<StudentQQQ>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public static List<StudentQQQ> GetStudents()
        {
            List<StudentQQQ> students = new List<StudentQQQ>()
            {
                new StudentQQQ {ID = 101, Name = "Preety" },
                new StudentQQQ {ID = 102, Name = "Sambit" },
                new StudentQQQ {ID = 103, Name = "Hina"},
                new StudentQQQ {ID = 104, Name = "Anurag"},
                new StudentQQQ {ID = 102, Name = "Sambit"},
                new StudentQQQ {ID = 103, Name = "Hina"},
                new StudentQQQ {ID = 101, Name = "Preety" },
            };
            return students;
        }

        public bool Equals(StudentQQQ other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            return this.ID.Equals(other.ID) && this.Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            int IDHashCode = this.ID.GetHashCode();
            int NameHashCode = this.Name == null ? 0 : this.Name.GetHashCode();
            return IDHashCode ^ NameHashCode;
        }

    }

 }
