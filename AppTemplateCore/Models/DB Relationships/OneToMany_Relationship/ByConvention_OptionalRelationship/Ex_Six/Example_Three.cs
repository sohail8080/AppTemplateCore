using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByConvention_OptionalRelationship.Ex_Six
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    //it is recommended to have a foreign key property defined in the dependent entity class, 
    //it is not required.

    //If no foreign key property is found, a shadow foreign key property will be introduced 
    //with the name<navigation property name><principal key property name> or
    //if no navigation key is present on the dependent type then 
    //fkey with name <principal entity name><principal key property name> is introduced

    //In this example the SHADOW FOREIGN KEY is BlogId 
    //because prepending the navigation name would be redundant. BlogBlogId

    //If a property with the same name already exists then 
    //the shadow property name will be suffixed with a number.

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Blog Blog { get; set; }
    }
}
