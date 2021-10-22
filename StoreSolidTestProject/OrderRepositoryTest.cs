using NUnit.Framework;
using StoreSolidConsoleApp.Models;
using StoreSolidConsoleApp.Data;
using StoreSolidConsoleApp.Context;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TestProject1
{
    [TestFixture]
    class OrderRepositoryTest
    {
        [Test]
        public void OrderRepository_GetAll_ReturnsEqual()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionOrderRepository(arrangedContext);

            // Act
            var actualResult = repo.GetOrders();

            //Assert
            CollectionAssert.AreEquivalent(arrangedContext.Orders, actualResult);
        }

        [Test]
        public void OrderRepository_GetByID_ReturnsEquals()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var expectedProduct = arrangedContext.Orders.ElementAt(0);
            var repo = new CollectionOrderRepository(arrangedContext);

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
            var arrangedContext = new StoreContext();
            var repo = new CollectionOrderRepository(arrangedContext);
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
            var arrangedContext = new StoreContext();
            var repo = new CollectionOrderRepository(arrangedContext);
            var expectedCollection = arrangedContext.Orders.Where(i => i.User.Login == login);

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
            var arrangedContext = new StoreContext();
            var repo = new CollectionOrderRepository(arrangedContext);
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
            var arrangedContext = new StoreContext();
            var repo = new CollectionOrderRepository(arrangedContext);
            Product product1 = new Product("product1", "category1", "description1", 45.5F);
            User user1 = new User("user1", "pa$$w0rd", "User1", "Userov1", "0981005060");
            List<OrderItem> items1 = new List<OrderItem>()
            {
                new OrderItem(product1, 5)
            };
            var arrangedOrder = new Order(items1, user1);
            var expectedLength = arrangedContext.Orders.ToList().Count + 1;

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
            var arrangedContext = new StoreContext();
            var repo = new CollectionOrderRepository(arrangedContext);
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
            var arrangedContext = new StoreContext();
            var repo = new CollectionOrderRepository(arrangedContext);
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
            var arrangedContext = new StoreContext();
            var repo = new CollectionOrderRepository(arrangedContext);
            var id = arrangedContext.Orders.ElementAt(0).ID.ToString();

            // Act
            repo.UpdateOrderStatus(id, status);
            var actualResult = repo.GetOrderByID(id).OrderStatus;

            //Assert
            Assert.AreEqual(status, actualResult);
        }
    }
}
