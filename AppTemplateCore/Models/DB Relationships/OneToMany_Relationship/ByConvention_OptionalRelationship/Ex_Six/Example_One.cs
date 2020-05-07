using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByConvention_OptionalRelationship.Ex_Six3333
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }


    //Single navigation property
    //Including just one navigation property(no inverse navigation, 
    //and no foreign key property) is enough to have a relationship defined by convention.
    //You can also have a single navigation property and a foreign key property.

    //When there are multiple navigation properties defined between two types
    //(that is, more than just one pair of navigations that point to each other) 
    //the relationships represented by the navigation properties are ambiguous.
    //You will need to manually configure them BY PROVIDING EXPLICTY FKEY to resolve the ambiguity.

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
