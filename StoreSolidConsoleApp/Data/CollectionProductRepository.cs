using StoreSolidConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using StoreSolidConsoleApp.Context;

namespace StoreSolidConsoleApp.Data
{
    public partial class CollectionProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        public CollectionProductRepository(StoreContext context)
        {
            this.context = context;
        }

        public Product GetProductByID(string productID)
        {
            var foundProduct = context.Products.Find(item => item.ID.ToString() == productID);
            if (foundProduct == null)
                throw new ArgumentException("Not found product with that ID");
            return foundProduct;
        }

        public Product GetProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name", "Empty argument name");
            var foundProduct = context.Products.Find(item => item.Name == name);
            if (foundProduct == null)
                throw new ArgumentException("Not found product with that name");
            return foundProduct;
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.Products;
        }

        public void InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product", " is null");
            if (!ValidateProduct(product))
                throw new ArgumentException("Some arguments of product are not valid");
            context.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product", " is null");
            if (!ValidateProduct(product))
                throw new ArgumentException("Some arguments of product are not valid");
            var updatedProduct = GetProductByID(product.ID.ToString());
            updatedProduct.Name = product.Name;
            updatedProduct.Category = product.Category;
            updatedProduct.Description = product.Description;
            updatedProduct.Cost = product.Cost;
        }

        private bool ValidateProduct(Product product)
        {
            if (!string.IsNullOrEmpty(product.Name) && !string.IsNullOrEmpty(product.Category)
                && !string.IsNullOrEmpty(product.Description) && product.Cost > 0)
                return true;
            else
                return false;
        }
    }
}
