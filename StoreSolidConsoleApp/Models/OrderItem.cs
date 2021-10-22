using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.Models
{
    public partial class OrderItem
    {
        public Product Product { get; internal set; }
        public int Amount { get; internal set; }
        public float Cost { get; private set; }

        public OrderItem(Product product, int amount)
        {
            Product = product;
            Amount = amount;
            Cost = Product.Cost * Amount;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderItem item &&
                   EqualityComparer<Product>.Default.Equals(Product, item.Product) &&
                   Amount == item.Amount &&
                   Cost == item.Cost;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product, Amount, Cost);
        }
    }
}
