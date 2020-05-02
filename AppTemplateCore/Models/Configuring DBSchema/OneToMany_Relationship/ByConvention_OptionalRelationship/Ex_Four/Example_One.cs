using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship5555
{

    //Adding an Author navigation property will create a one-to-many relationship between 
    //the Authors and Books tables in the database by adding a foreign key Author_AuthorId 
    //to Books table.

    public class Author
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }
    }

    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }
      
        public Author Author { get; set; }
    }

}
