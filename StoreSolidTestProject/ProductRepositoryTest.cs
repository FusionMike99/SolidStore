using NUnit.Framework;
using Moq;
using StoreSolidConsoleApp.Models;
using StoreSolidConsoleApp.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using StoreSolidConsoleApp.Context;
using StoreSolidConsoleApp.UoW;

namespace TestProject1
{
    [TestFixture]
    public class ProductRepositoryTest
    {
        private List<Product> products;

        [SetUp]
        public void SetUp()
        {
            products = new List<Product>()
            {
                new Product("product1", "category1", "description1", 45.5F),
                new Product("product2", "category1", "description2", 3F),
                new Product("product3", "category2", "description3", 41F),
                new Product("product4", "category2", "description4", 89.6F),
                new Product("product5", "category3", "description5", 53F)
            };
        }

        [Test]
        public void ProductRepository_GetAll_ReturnsEqualLength()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);

            // Act
            var actualResult = repo.GetProducts().ToList();

            //Assert
            CollectionAssert.AreEquivalent(mockContext.Object.Products, actualResult);
        }

        [Test]
        public void ProductRepository_GetByID_ReturnsEquals()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var expectedProduct = mockContext.Object.Products[0];
            var repo = new CollectionProductRepository(mockContext.Object);

            // Act
            var actualResult = repo.GetProductByID(expectedProduct.ID.ToString());

            //Assert
            Assert.AreEqual(expectedProduct, actualResult, "Products are not the same");
        }

        [Test]
        public void ProductRepository_GetByName_ReturnsEquals()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var expectedProduct = mockContext.Object.Products[0];
            var repo = new CollectionProductRepository(mockContext.Object);

            // Act
            var actualResult = repo.GetProductByName(expectedProduct.Name);

            //Assert
            Assert.AreEqual(expectedProduct, actualResult, "Products are not the same");
        }

        [TestCase("vsxdvsvdsv")]
        [TestCase("bdfbbdsfb")]
        [TestCase("bsdbfbbefe")]
        public void ProductRepository_GetByID_ShouldThrowArgumentException(string id)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);
            var expectedEx = typeof(ArgumentException);
            
            //Act
            var actualEx = Assert.Catch(() => { repo.GetProductByID(id); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("")]
        [TestCase(null)]
        public void ProductRepository_GetByName_ShouldThrowArgumentNullException(string name)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);
            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { repo.GetProductByName(name); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("vsvdvfs")]
        [TestCase("seffesgerg")]
        [TestCase("product11")]
        public void ProductRepository_GetByName_ShouldThrowArgumentException(string name)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.GetProductByName(name); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [Test]
        public void ProductRepository_AfterInsert_LengthOfCollectionsEquivalents()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);
            var arrangedProduct = new Product("product11", "category6", "description11", 45.2F);
            var expectedLength = mockContext.Object.Products.Count + 1;

            // Act
            repo.InsertProduct(arrangedProduct);
            var actualResult = repo.GetProducts().Count();

            //Assert
            Assert.AreEqual(expectedLength, actualResult);
        }

        [Test]
        public void ProductRepository_InsertNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);
            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { repo.InsertProduct(null); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("", "afsfa", "fafwdfw", 56F)]
        [TestCase("awfda", "", "fafwdfw", 56F)]
        [TestCase("asffvdg", "afsfa", "", 56F)]
        [TestCase("sdvsdgs", "afsfa", "fafwdfw", 0F)]
        public void ProductRepository_InsertInvalidData_ShouldThrowArgumentException(string name, string category,
            string description, float cost)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);
            var arrangedProduct = new Product(name, category, description, cost);
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.InsertProduct(arrangedProduct); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [Test]
        public void ProductRepository_AfterUpdate_ObjectsAreEqual()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);
            var id = mockContext.Object.Products[0].ID.ToString();
            var expectedProduct = new Product(id, "product11", "category6", "description11", 45.2F);

            // Act
            repo.UpdateProduct(expectedProduct);
            var actualResult = repo.GetProductByID(id);

            //Assert
            Assert.AreEqual(expectedProduct, actualResult);
        }

        [Test]
        public void ProductRepository_UpdateNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);
            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { repo.UpdateProduct(null); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("", "afsfa", "fafwdfw", 56F)]
        [TestCase("awfda", "", "fafwdfw", 56F)]
        [TestCase("asffvdg", "afsfa", "", 56F)]
        [TestCase("sdvsdgs", "afsfa", "fafwdfw", 0F)]
        public void ProductRepository_UpdateInvalidData_ShouldThrowArgumentException(string name, string category,
            string description, float cost)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);
            var arrangedProduct = new Product(name, category, description, cost);
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.UpdateProduct(arrangedProduct); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }
    }
}