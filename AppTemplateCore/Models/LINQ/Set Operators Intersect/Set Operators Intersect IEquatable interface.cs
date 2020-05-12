using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Intersect.IEquatable
{

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> s1 = new List<Student>()
            {
                new Student {ID = 101, Name = "Preety" },
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 105, Name = "Hina"},
                new Student {ID = 106, Name = "Anurag"},
            };
            List<Student> s2 = new List<Student>()
            {
                new Student {ID = 105, Name = "Hina"},
                new Student {ID = 106, Name = "Anurag"},
                new Student {ID = 107, Name = "Pranaya"},
                new Student {ID = 108, Name = "Santosh"},
            };

           
            var MS = s1.Intersect(s2).ToList();

            var QS = (from std in s1 select std).Intersect(s2).ToList();

        }
    }

    public class Student : IEquatable<Student>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public bool Equals(Student other)
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
