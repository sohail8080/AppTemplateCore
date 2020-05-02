using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByConvention_RequiredRelationship.Ex_Four
{
    //conventional behaviour will be to cascade delete operations to dependent entity
    //because the relationship is required(because of non-nullable 
    //foreign key property (PropertyId) in the dependent Tenant entity


    public class Property
    {
        public int PropertyId { get; set; }

        public string Address { get; set; }

        public ICollection<Tenant> Tenants { get; set; }
    }


    public class Tenant
    {
        public int TenantId { get; set; }

        public string Name { get; set; }

        // not null property, required relationship
        public int PropertyId { get; set; }

        public Property Property { get; set; }
    }





}
