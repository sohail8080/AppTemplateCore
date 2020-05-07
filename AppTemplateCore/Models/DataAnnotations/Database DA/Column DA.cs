using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{
    using System.ComponentModel.DataAnnotations;
    //Data Annotations - Column Attribute in EF 6 & EF Core
    //The Column attribute can be applied to one or more properties in an entity class 
    //to configure the corresponding 
    //column name, data type and order 
    //in a database table.

    //The Column attribute overrides the default convention.
    //As per the default conventions in EF 6 and EF Core, 
    //it creates a column in a db table with the same name and order as the property names.

    //Column Attribute: [Column(string name, Properties:[Order = int],[TypeName = string])

    //name: Name of a column in a db table.
    //Order: Order of a column, starting with zero index. (Optional)
    //TypeName: Data type of a column. (Optional)
    //The following example changes the name of a column.

    using System.ComponentModel.DataAnnotations.Schema;

    public class Studentssa
    {
        public int StudentID { get; set; }

        [Column("Name")]
        public string StudentName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public byte[] Photo { get; set; }

        public decimal Height { get; set; }

        public float Weight { get; set; }
    }


    public class Studentmn
    {
        public int StudentID { get; set; }

        [Column("Name")]
        public string StudentName { get; set; }

        [Column("DoB", TypeName = "DateTime2")]
        public DateTime DateOfBirth { get; set; }

        public byte[] Photo { get; set; }

        public decimal Height { get; set; }

        public float Weight { get; set; }
    }


    public class Student1234
    {
        [Column(Order = 0)]
        public int StudentID { get; set; }

        [Column("Name", Order = 1)]
        public string StudentName { get; set; }

        [Column("DoB", Order = 5)]
        public DateTime DateOfBirth { get; set; }

        [Column(Order = 3)]
        public byte[] Photo { get; set; }

        [Column(Order = 2)]
        public decimal Height { get; set; }

        [Column(Order = 4)]
        public float Weight { get; set; }
    }


    public class Employee1
    {
        [Key]
        public int EmployeeID { get; set; }

        [Column("EmployeeName")]
        public string Name { get; set; }
    }


    //Database Data type

    // COVERSION BY CONVENSION: C# DATA TYPE <==> DATABASE DATA TYPE

    // OVERRIDDING THE CONVENTION OF DATA TYPE CONVERSION

    //Use the TypeName option to set the data type of the column IN DATABASE.
    //In the following example, 
    //EmployeeName is set as the varchar data type. Name1 is set as the nvarchar data type. 
    //TextColumn as the ntext data type and amount as money data type


    //TypeName attribute is different from the DataType data annotation attribute, 
    //which is used only for the UI Validations

    public class Employee2bbbb
    {
        [Key]
        public int EmployeeID { get; set; }

        //
        [Column("EmployeeName", TypeName = "varchar")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Name1 { get; set; }

        [Column(TypeName = "text")]
        public string TextColumn { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
    }



    //Column Order
    //Specify the order of the column using the Order option.The Order can be any integer value. 
    //The EF will create the column based on the Order specified.
    //The Unordered column appears after the ordered column as shown in the image below

    public class Employee3
    {
        [Key]
        public int EmployeeID { get; set; }

        public string Name { get; set; }

        [Column(Order = 4, TypeName = "varchar")]
        public string Column1 { get; set; }

        [Column(Order = 3, TypeName = "Varchar")]
        public string Column2 { get; set; }
    }



}
