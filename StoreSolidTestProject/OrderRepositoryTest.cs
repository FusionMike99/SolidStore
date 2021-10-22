using NUnit.Framework;
using StoreSolidConsoleApp.Models;
using StoreSolidConsoleApp.Data;
using StoreSolidConsoleApp.Context;
using System.Collections.Generic;
using System.Linq;
using System;
using Moq;

namespace TestProject1
{
    [TestFixture]
    class OrderRepositoryTest
    {
        private List<Order> orders;

        [SetUp]
        public void SetUp()
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

            orders = new List<Order>()
            {
                new Order(items1, user1),
                new Order(items2, user2),
                new Order(items3, user1),
                new Order(items4, user2)
            };
        }

        [Test]
        public void OrderRepository_GetAll_ReturnsEqual()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Orders).Returns(orders);
            var repo = new CollectionOrderRepository(mockContext.Object);

            // Act
            var actualResult = repo.GetOrders();

            //Assert
            CollectionAssert.AreEquivalent(mockContext.Object.Orders, actualResult);
        }

        [Test]
        public void OrderRepository_GetByID_ReturnsEquals()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Orders).Returns(orders);
            var expectedProduct = mockContext.Object.Orders[0];
            var repo = new CollectionOrderRepository(mockContext.Object);

            // Act
            var actualResult = repo.GetOrderByID(expectedProduct.ID.ToString());

            //Assert
            Assert.AreEqual(expectedProduct, actualResult, "Orders are not the same");
        }

        [TestCase("vsxdvsvdsv")]
        [TestCase("bdfbbdsfb")]
        [TestCase("bsdbfbbefe")]
        public void OrderRepository_GetByID_ShouldThrowArgumentException(string id)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Orders).Returns(orders);
            var repo = new CollectionOrderRepository(mockContext.Object);
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.GetOrderByID(id); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("user1")]
        [TestCase("user2")]
        public void OrderRepository_GetUserOrders_ReturnsEqual(string login)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Orders).Returns(orders);
            var repo = new CollectionOrderRepository(mockContext.Object);
            var expectedCollection = mockContext.Object.Orders.Where(i => i.User.Login == login);

            // Act
            var actualResult = repo.GetUserOrders(login);

            //Assert
            CollectionAssert.AreEquivalent(expectedCollection, actualResult);
        }

        [TestCase("")]
        [TestCase(null)]
        public void OrderRepository_GetUserOrders_ShouldThrowArgumentNullException(string login)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Orders).Returns(orders);
            var repo = new CollectionOrderRepository(mockContext.Object);
            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { repo.GetUserOrders(login); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [Test]
        public void OrderRepository_AfterInsert_LengthOfCollectionsEquivalents()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Orders).Returns(orders);
            var repo = new CollectionOrderRepository(mockContext.Object);
            Product product1 = new Product("product1", "category1", "description1", 45.5F);
            User user1 = new User("user1", "pa$$w0rd", "User1", "Userov1", "0981005060");
            List<OrderItem> items1 = new List<OrderItem>()
            {
                new OrderItem(product1, 5)
            };
            var arrangedOrder = new Order(items1, user1);
            var expectedLength = mockContext.Object.Orders.Count + 1;

            // Act
            repo.InsertOrder(arrangedOrder);
            var actualResult = repo.GetOrders().Count();

            //Assert
            Assert.AreEqual(expectedLength, actualResult);
        }

        [Test]
        public void OrderRepository_InsertNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Orders).Returns(orders);
            var repo = new CollectionOrderRepository(mockContext.Object);
            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { repo.InsertOrder(null); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [Test]
        public void OrderRepository_InsertInvalidData_ShouldThrowArgumentException()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Orders).Returns(orders);
            var repo = new CollectionOrderRepository(mockContext.Object);
            var arrangedOrder = new Order(null, null);
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.InsertOrder(arrangedOrder); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase(OrderStatus.PaymentReceived)]
        [TestCase(OrderStatus.Sent)]
        [TestCase(OrderStatus.Received)]
        [TestCase(OrderStatus.Completed)]
        [TestCase(OrderStatus.CanceledByAdmin)]
        [TestCase(OrderStatus.CanceledByUser)]
        public void OrderRepository_AfterUpdateStatus_ObjectsAreEqual(OrderStatus status)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Orders).Returns(orders);
            var repo = new CollectionOrderRepository(mockContext.Object);
            var id = mockContext.Object.Orders[0].ID.ToString();

            // Act
            repo.UpdateOrderStatus(id, status);
            var actualResult = repo.GetOrderByID(id).OrderStatus;

            //Assert
            Assert.AreEqual(status, actualResult);
        }
    }
}
