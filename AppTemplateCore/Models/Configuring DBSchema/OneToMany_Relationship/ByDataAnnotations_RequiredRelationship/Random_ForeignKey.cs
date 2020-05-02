using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByDataAnnotations_RequiredRelationship222244
{
    // You can use the Data Annotations to configure which property should be used as the foreign key property for a given relationship. This is typically done when the foreign key property is not discovered by convention:

    // The [ForeignKey] annotation can be placed on either navigation property in the relationship. It does not need to go on the navigation property in the dependent entity class.

    // The property specified using [ForeignKey] on a navigation property doesn't need to exist the the dependent type. In this case the specified name will be used to create a shadow foreign key.

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogForeignKey { get; set; }

        [ForeignKey("BlogForeignKey")]
        public Blog Blog { get; set; }
    }
}
