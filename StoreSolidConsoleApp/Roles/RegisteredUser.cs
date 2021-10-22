using StoreSolidConsoleApp.Models;
using StoreSolidConsoleApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using StoreSolidConsoleApp.UoW;

namespace StoreSolidConsoleApp.Roles
{
    class RegisteredUser
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly User user;

        public RegisteredUser(User user)
        {
            unitOfWork = new UnitOfWork();
            this.user = user;
        }

        public RegisteredUser(IUnitOfWork unitOfWork, User user)
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

        public void CancelOrder(string orderID)
        {
            var order = unitOfWork.OrderRepository.GetOrderByID(orderID);
            if (((byte)order.OrderStatus) >= 3)
                throw new ArgumentException("Order has been already received/competed/canceled");
            unitOfWork.OrderRepository.UpdateOrderStatus(orderID, OrderStatus.CanceledByUser);
        }

        public IEnumerable<Order> ReviewOrderHistory() => unitOfWork.OrderRepository.GetUserOrders(user.Login);

        public void ReceiveOrder(string orderID)
        {
            var order = unitOfWork.OrderRepository.GetOrderByID(orderID);
            if (((byte)order.OrderStatus) >= 3)
                throw new ArgumentException("Order has been already received/competed/canceled");
            unitOfWork.OrderRepository.UpdateOrderStatus(orderID, OrderStatus.Received);
        }

        public void UpdateProfile(string name, string surname, string password, string phoneNumber) 
        {
            User updatableUser = new User(user.ID.ToString(), user.Login, password,
                name, surname, phoneNumber);
            unitOfWork.UserRepository.UpdateUser(updatableUser);
        }
    }
}
