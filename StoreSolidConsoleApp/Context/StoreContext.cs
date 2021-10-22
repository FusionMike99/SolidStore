using StoreSolidConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.Context
{
    public partial class StoreContext
    {
        public virtual List<Product> Products { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<Order> Orders { get; set; }

        public StoreContext()
        {
            InitializeProducts();
            InitializeUsers();
            InitializeOrders();
        }

        public void InitializeProducts()
        {
            Products = new List<Product>()
            {
                new Product("product1", "category1", "description1", 45.5F),
                new Product("product2", "category1", "description2", 3F),
                new Product("product3", "category2", "description3", 41F),
                new Product("product4", "category2", "description4", 89.6F),
                new Product("product5", "category3", "description5", 53F),
                new Product("product6", "category3", "description6", 215F),
                new Product("product7", "category4", "description7", 165F),
                new Product("product8", "category4", "description8", 104.5F),
                new Product("product9", "category5", "description9", 544.5F),
                new Product("product10", "category5", "description10", 256.6F)
            };
        }

        public void InitializeUsers()
        {
            Users = new List<User>()
            {
                new User("user1", "pa$$w0rd", "User1", "Userov1", "0981005060"),
                new User("user2", "pa$$w0rd", "User2", "Userov2", "0972004070"),
                new User("user3", "pa$$w0rd", "User3", "Userov3", "0963003080"),
                new User("admin", "pa$$w0rd", "Admin", "Adminov", "0911001010", UserRole.Administrator)
            };
        }

        public void InitializeOrders()
        {
            Product product1 = new Product("product1", "category1", "description1", 45.5F);
            Product product2 = new Product("product2", "category1", "description2", 3F);
            Product product3 = new Product("product3", "category2", "description3", 41F);
            Product product4 = new Product("product4", "category2", "description4", 89.6F);
            Product product5 = new Product("product5", "category3", "description5", 53F);

            User user1 = new User("user1", "pa$$w0rd", "User1", "Userov1", "0981005060");
            User user2 = new User("user2", "pa$$w0rd", "User2", "Userov2", "0972004070");

            List<OrderItem> items1 = new List<OrderItem>()
            {
                new OrderItem(product1, 2),
                new OrderItem(product2, 1)
            };
            List<OrderItem> items2 = new List<OrderItem>()
            {
                new OrderItem(product3, 4),
                new OrderItem(product4, 3)
            };
            List<OrderItem> items3 = new List<OrderItem>()
            {
                new OrderItem(product5, 6),
                new OrderItem(product1, 5)
            };
            List<OrderItem> items4 = new List<OrderItem>()
            {
                new OrderItem(product2, 8),
                new OrderItem(product3, 7)
            };

            Orders = new List<Order>()
            {
                new Order(items1, user1),
                new Order(items2, user2),
                new Order(items3, user1),
                new Order(items4, user2)
            };
        }
    }
}
