using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.Models
{
    public partial class Product : BaseEntity
    {
        public string Name { get; internal set; }
        public string Category { get; internal set; }
        public string Description { get; internal set; }
        public float Cost { get; internal set; }

        public Product(string name, string category, string description, float cost)
        {
            Name = name;
            Category = category;
            Description = description;
            Cost = cost;
        }

        public Product(string id, string name, string category, string description, float cost)
        {
            ID = Guid.Parse(id);
            Name = name;
            Category = category;
            Description = description;
            Cost = cost;
        }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   ID == product.ID &&
                   Name == product.Name &&
                   Category == product.Category &&
                   Description == product.Description &&
                   Cost == product.Cost;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Name, Category, Description, Cost);
        }
    }
}
