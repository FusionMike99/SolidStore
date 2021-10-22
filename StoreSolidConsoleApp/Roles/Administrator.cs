using StoreSolidConsoleApp.Data;
using StoreSolidConsoleApp.Models;
using StoreSolidConsoleApp.UoW;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.Roles
{
    class Administrator
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly User user;

        public Administrator(User user)
        {
            unitOfWork = new UnitOfWork();
            this.user = user;
        }

        public Administrator(IUnitOfWork unitOfWork, User user)
        {
            this.unitOfWork = unitOfWork;
            this.user = user;
        }

        public IEnumerable<Product> ListOfProducts() => unitOfWork.ProductRepository.GetProducts();

        public Product SearchProductByName(string name) => unitOfWork.ProductRepository.GetProductByName(name);

        public void CreateNewOrder(Order order)
        {
            order.User = user;
            unitOfWork.OrderRepository.InsertOrder(order);
        }

        public IEnumerable<User> ListOfUsers() => unitOfWork.UserRepository.GetUsers();

        public User SearchUserByLogin(string login) => unitOfWork.UserRepository.GetUserByLogin(login);

        public void UpdateUserProfile(User user) => unitOfWork.UserRepository.UpdateUser(user);

        public void AddNewProduct(Product product) => unitOfWork.ProductRepository.InsertProduct(product);

        public void UpdateProduct(Product product) => unitOfWork.ProductRepository.UpdateProduct(product);

        public IEnumerable<Order> ListOfOrders() => unitOfWork.OrderRepository.GetOrders();

        public void UpdateStatusOrder(string orderID, OrderStatus status)
        {
            var order = unitOfWork.OrderRepository.GetOrderByID(orderID);
            if (((byte)order.OrderStatus) >= (byte)status)
                throw new ArgumentException("Order status has already a higher status");
            unitOfWork.OrderRepository.UpdateOrderStatus(orderID, status);
        }
    }
}
