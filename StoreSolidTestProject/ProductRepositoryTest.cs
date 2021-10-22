using NUnit.Framework;
using StoreSolidConsoleApp.Models;
using StoreSolidConsoleApp.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using StoreSolidConsoleApp.Context;

namespace TestProject1
{
    [TestFixture]
    public class ProductRepositoryTest
    {
        [Test]
        public void ProductRepository_GetAll_ReturnsEqual()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionProductRepository(arrangedContext);

            // Act
            var actualResult = repo.GetProducts();

            //Assert
            CollectionAssert.AreEquivalent(arrangedContext.Products, actualResult);
        }

        [Test]
        public void ProductRepository_GetByID_ReturnsEquals()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var expectedProduct = arrangedContext.Products.ElementAt(0);
            var repo = new CollectionProductRepository(arrangedContext);

            // Act
            var actualResult = repo.GetProductByID(expectedProduct.ID.ToString());

            //Assert
            Assert.AreEqual(expectedProduct, actualResult, "Products are not the same");
        }

        [Test]
        public void ProductRepository_GetByName_ReturnsEquals()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var expectedProduct = arrangedContext.Products.ElementAt(0);
            var repo = new CollectionProductRepository(arrangedContext);

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
            var arrangedContext = new StoreContext();
            var repo = new CollectionProductRepository(arrangedContext);
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
            var arrangedContext = new StoreContext();
            var repo = new CollectionProductRepository(arrangedContext);
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
            var arrangedContext = new StoreContext();
            var repo = new CollectionProductRepository(arrangedContext);
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
            var arrangedContext = new StoreContext();
            var repo = new CollectionProductRepository(arrangedContext);
            var arrangedProduct = new Product("product11", "category6", "description11", 45.2F);
            var expectedLength = arrangedContext.Products.ToList().Count + 1;

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
            var arrangedContext = new StoreContext();
            var repo = new CollectionProductRepository(arrangedContext);
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
            var arrangedContext = new StoreContext();
            var repo = new CollectionProductRepository(arrangedContext);
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
            var arrangedContext = new StoreContext();
            var repo = new CollectionProductRepository(arrangedContext);
            var id = arrangedContext.Products.ElementAt(0).ID.ToString();
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
            var arrangedContext = new StoreContext();
            var repo = new CollectionProductRepository(arrangedContext);
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
            var arrangedContext = new StoreContext();
            var repo = new CollectionProductRepository(arrangedContext);
            var arrangedProduct = new Product(name, category, description, cost);
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.UpdateProduct(arrangedProduct); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }
    }
}