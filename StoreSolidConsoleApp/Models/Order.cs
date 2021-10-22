using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.Models
{
    public enum OrderStatus : byte
    {
        New,
        PaymentReceived,
        Sent,
        Received,
        Completed,
        CanceledByAdmin,
        CanceledByUser
    }

    public partial class Order : BaseEntity
    {
        public IEnumerable<OrderItem> OrderItems { get; internal set; }
        public DateTime DateOfOpening { get; internal set; }
        public float Total { get; private set; }
        public User User { get; internal set; }
        public OrderStatus OrderStatus { get; internal set; }

        public Order(IEnumerable<OrderItem> orderItems, User user)
        {
            OrderItems = orderItems;
            DateOfOpening = DateTime.Now;
            User = user;
            OrderStatus = OrderStatus.New;
        }

        public Order(string id, IEnumerable<OrderItem> orderItems, User user)
        {
            ID = Guid.Parse(id);
            OrderItems = orderItems;
            DateOfOpening = DateTime.Now;
            User = user;
            OrderStatus = OrderStatus.New;
        }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   ID == order.ID &&
                   EqualityComparer<IEnumerable<OrderItem>>.Default.Equals(OrderItems, order.OrderItems) &&
                   DateOfOpening == order.DateOfOpening &&
                   Total == order.Total &&
                   EqualityComparer<User>.Default.Equals(User, order.User) &&
                   OrderStatus == order.OrderStatus;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, OrderItems, DateOfOpening, Total, User, OrderStatus);
        }
    }
}
