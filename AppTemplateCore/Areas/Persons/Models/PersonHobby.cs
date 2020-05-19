using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.Persons.Models
{
    public class PersonHobby
    {
        public int PersonID { get; set; }
        public Person Person { get; set; }

        public int HobbyID { get; set; }
        public Hobby Hobby { get; set; }

    }


    public class PostTagConfiguration : IEntityTypeConfiguration<PersonHobby>
    {
        public void Configure(EntityTypeBuilder<PersonHobby> builder)
        {
            // composite key sfor surrogate table
            builder.HasKey(s => new { s.PersonID, s.HobbyID });

            // Configure one end of relationship
            builder.HasOne(ss => ss.Person)
                .WithMany(s => s.PersonHobbies)
                .HasForeignKey(ss => ss.PersonID);

            // Configure second end of relationship
            builder.HasOne(ss => ss.Hobby)
                .WithMany(s => s.PersonHobbies)
                .HasForeignKey(ss => ss.HobbyID);
        }
    }


}
