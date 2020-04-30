using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class DatabaseDA
    {



    }


    public class DrivingLicense
    {
        [Key, Column(Order = 1)]
        public int LicenseNumber { get; set; }

        [Key, Column(Order = 2)]
        public string IssuingCountry { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expires { get; set; }
    }

    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        [Timestamp]
        public byte[] TStamp { get; set; }

        public virtual ICollection<Object> Enrollments { get; set; }
    }


    public class Course3
    {
        public int CourseID { get; set; }

        [ConcurrencyCheck]
        public string Title { get; set; }
        public int Credits { get; set; }

        [Timestamp, DataType("timestamp")]
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<Object> Enrollments { get; set; }
    }


    public class Course2
    {
        public int CourseID { get; set; }

        [ConcurrencyCheck]
        public string Title { get; set; }
        public int Credits { get; set; }

        [Timestamp, DataType("timestamp")]
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<Object> Enrollments { get; set; }
    }



    public class Course4
    {
        public int CourseID { get; set; }

        [ConcurrencyCheck]
        [MaxLength(24)]
        public string Title { get; set; }

        public int Credits { get; set; }

        public virtual ICollection<Object> Enrollments { get; set; }

    }


    public class Course555
    {
        public int CourseID { get; set; }
        [StringLength(24)]
        public string Title { get; set; }
        public int Credits { get; set; }

        public virtual ICollection<Object> Enrollments { get; set; }
    }

    [Table("StudentsInfo")]
    public class Student
    {
        [Key]
        public int StdntID { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Object> Enrollments { get; set; }
    }



    [Table("StudentsInfo", Schema = "Admin")]
    public class Student4433
    {
        [Key]
        public int StdntID { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Object> Enrollments { get; set; }
    }



    public class Student32322
    {
        public int ID { get; set; }
        public string LastName { get; set; }

        [Column("FirstName")]
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Object> Enrollments { get; set; }
    }


    public class Cours
    {
        public int CourseID { get; set; }
        public string Title { get; set; }

        //[Index]
        public int Credits { get; set; }

            
        public virtual ICollection<Object> Enrollments { get; set; }
    }


    ////////////////////////////////////////////////

        // fOREIGN KEY

    public class Enrollment444
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Object Grade { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }

    public class Student444
    {
        [Key]
        public int StdntID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Object> Enrollments { get; set; }

    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Object Grade { get; set; }
        public virtual Course Course { get; set; }

        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
    }


    ////////////////////////////////////////////////

    public class StudentCCCC
    {
        [Key]
        public int StdntID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        [NotMapped]
        public int FatherName { get; set; }

        public virtual ICollection<Object> Enrollments { get; set; }
    }


}