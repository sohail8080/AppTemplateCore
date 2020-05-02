using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByConvention_OptionalRelationship21215551111
{


    public class Author
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        // navigation property
        public virtual ICollection<Book> Books { get; set; }
    }



    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        // not nullable fk, for required relationship
        public int AuthorId { get; set; }

        // navigation property
        public Author Author { get; set; }
    }


}
