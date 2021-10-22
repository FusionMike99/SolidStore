using StoreSolidConsoleApp.Data;
using StoreSolidConsoleApp.Context;
using StoreSolidConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSolidConsoleApp.Data
{
    public partial class CollectionOrderRepository : IOrderRepository
    {
        private readonly StoreContext context;

        public CollectionOrderRepository(StoreContext context)
        {
            this.context = context;
        }

        public Order GetOrderByID(string orderID)
        {
            var foundOrder = context.Orders.Find(item => item.ID.ToString() == orderID);
            if (foundOrder == null)
                throw new ArgumentException("Not found order with that ID");
            return foundOrder;
        }

        public IEnumerable<Order> GetOrders()
        {
            return context.Orders.ToList();
        }

        public IEnumerable<Order> GetUserOrders(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException("login", "argument login is empty");
            return context.Orders.Where(item => item.User.Login == login);
        }

        public void InsertOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order", " is null");
            if (!ValidateOrder(order))
                throw new ArgumentException("Some arguments of order are not valid");
            context.Orders.Add(order);
        }

        public void UpdateOrderStatus(string orderID, OrderStatus orderStatus)
        {
            var updatedOrder = GetOrderByID(orderID);
            updatedOrder.OrderStatus = orderStatus;
        }

        private bool ValidateOrder(Order order)
        {
            if (order.OrderItems != null && order.User != null)
                return true;
            else
                return false;
        }
    }
}
