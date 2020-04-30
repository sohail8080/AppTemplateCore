using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;// property validation
using System.ComponentModel.DataAnnotations.Schema;// database configuration
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotationForDBSchema
{

    public class Student
    {
        public Guid Id { get; set; }// PK
        public string Name { get; set; } // nullable
        public int Age { get; set; } // not nullable
        public int LocalCalculation { get; set; }
        public bool IsRegularStudent { get; set; }
    }


    public class Student2
    {
        public Guid StudentId { get; set; } //PK
        public string Name { get; set; }
        public int Age { get; set; } 
    }


    public class Student3
    {
        public Guid StudentId { get; set; } 
        public string Name { get; set; } 
        public int? Age { get; set; } // nullable
    }


    public class Student4
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Age { get; set; } // nullable
    }


    public class Student5
    {
        public Guid StudentId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }// not nullable, max length 50

        public int Age { get; set; }
    }



    // Class Name becomes irrelevant
    // >>Property name in ApplicationDBContext becomes irrelevant
    // Now class will be mapped to Student table
    [Table("Student")]
    public class Student6
    {
        // Column name in class becomes irrelevant, mapped to StudentId column
        [Column("StudentId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        public int? Age { get; set; }
    }



    public class Student7
    {
        [Column("StudentId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        public int? Age { get; set; }

        [NotMapped]
        public int LocalCalculation { get; set; }// No column generated for this
    }

    //PrimaryKey Configuration with Data Annotations
    public class Student8
    {
        //if the name of the property doesn’t fit the naming convention
        [Key]
        [Column("StudentId")]
        public Guid StudId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        public int? Age { get; set; }

        [NotMapped]
        public int LocalCalculation { get; set; }
    }


    //PrimaryKey Configuration with Fluent API
    public class Student9
    {
        //For the composite key, we have to use only the Fluent API approach               
        public Guid StudId { get; set; }
        public Guid AnotherKeyProperty { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string Name { get; set; }

        public int? Age { get; set; }

    }



    [NotMapped]
    public class NotMappedClass // Class not mappted to DBTable
    {
        //properties
    }


}
