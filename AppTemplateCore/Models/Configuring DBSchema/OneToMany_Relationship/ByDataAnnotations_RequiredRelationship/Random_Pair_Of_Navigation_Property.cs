using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByDataAnnotations_RequiredRelationship
{
    //You can use the Data Annotations to configure how navigation properties on the dependent and principal entities pair up. This is typically done when there is more than one pair of navigation properties between two entity types.

    // Principle
    public class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [InverseProperty("Author")]
        public List<Post> AuthoredPosts { get; set; }

        [InverseProperty("Contributor")]
        public List<Post> ContributedToPosts { get; set; }
    }

    //You can only use [Required] on properties on the dependent entity to impact the requiredness of the relationship. [Required] on the navigation from the principal entity is usually ignored, but it may cause the entity to become the dependent one.

    // Dependent
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [Required]
        public int AuthorUserId { get; set; }
        [Required]
        public User Author { get; set; }

        [Required]
        public int ContributorUserId { get; set; }
        [Required]
        public User Contributor { get; set; }
    }


}
