using StoreSolidConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.Data
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetUserOrders(string login);
        Order GetOrderByID(string orderID);
        void InsertOrder(Order order);
        void UpdateOrderStatus(string orderID, OrderStatus orderStatus);
    }
}
