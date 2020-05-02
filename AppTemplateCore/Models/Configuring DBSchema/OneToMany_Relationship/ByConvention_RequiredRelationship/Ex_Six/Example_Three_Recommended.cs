using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByConvention_RequiredRelationship.Ex_Six
{
    // principal entity
    public class Blog
    {
        public int BlogId { get; set; }

        public string Url { get; set; }

        // navigation property
        public List<Post> Posts { get; set; }
    }


    // dependent entity
    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        //<principal entity name>Id 
        //<navigation property name>Id
        // Not nullable, that is why required relation
        public int BlogId { get; set; }

        //<principal entity name><principal key property name> 
        //<navigation property name><principal key property name>
        //public int BlogBlogIdId { get; set; }
        // navigation property
        public Blog Blog { get; set; }
    }
}
