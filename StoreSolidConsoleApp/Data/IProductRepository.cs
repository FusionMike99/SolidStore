using StoreSolidConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.Data
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductByName(string name);
        Product GetProductByID(string productID);
        void InsertProduct(Product product);
        void UpdateProduct(Product product);
    }
}
