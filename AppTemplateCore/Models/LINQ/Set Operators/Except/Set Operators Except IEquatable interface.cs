using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Except.IEquatable
{

    class Program2
    {
        static void Main2(string[] args)
        {

            List<Student> AllStudents = new List<Student>()
            {
                new Student {ID = 101, Name = "Preety" },
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 103, Name = "Hina"},
                new Student {ID = 104, Name = "Anurag"},
                new Student {ID = 105, Name = "Pranaya"},
                new Student {ID = 106, Name = "Santosh"},
            };

            List<Student> Class6Students = new List<Student>()
            {
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 104, Name = "Anurag"},
                new Student {ID = 105, Name = "Pranaya"},
            };


            var MS = AllStudents.Except(Class6Students).ToList();

            var QS = (from std in AllStudents select std).Except(Class6Students).ToList();

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
