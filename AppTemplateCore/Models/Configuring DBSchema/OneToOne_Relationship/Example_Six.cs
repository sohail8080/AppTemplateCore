using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToOne_Relationship
{

    //EF will choose one of the entities to be the dependent based on its ability to detect a foreign key
    //property.If the wrong entity is chosen as the dependent, you can use the Fluent API to correct this.

    public class Blog
    {
        public int BlogId { get; set; }

        public string Url { get; set; }

        //NP
        public BlogImage BlogImage { get; set; }
    }

    public class BlogImage
    {
        public int BlogImageId { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }

        // FKey
        public int BlogId { get; set; }

        //NP
        public Blog Blog { get; set; }
    }
}
