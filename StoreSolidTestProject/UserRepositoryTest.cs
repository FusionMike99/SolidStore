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
    class UserRepositoryTest
    {
        [Test]
        public void UserRepository_GetAll_ReturnsEqual()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionUserRepository(arrangedContext);

            // Act
            var actualResult = repo.GetUsers();

            //Assert
            CollectionAssert.AreEquivalent(arrangedContext.Users, actualResult);
        }

        [Test]
        public void UserRepository_GetByID_ReturnsEquals()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var expectedProduct = arrangedContext.Users.ElementAt(0);
            var repo = new CollectionUserRepository(arrangedContext);

            // Act
            var actualResult = repo.GetUserByID(expectedProduct.ID.ToString());

            //Assert
            Assert.AreEqual(expectedProduct, actualResult, "Users are not the same");
        }

        [Test]
        public void UserRepository_GetByLogin_ReturnsEquals()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var expectedProduct = arrangedContext.Users.ElementAt(0);
            var repo = new CollectionUserRepository(arrangedContext);

            // Act
            var actualResult = repo.GetUserByLogin(expectedProduct.Login);

            //Assert
            Assert.AreEqual(expectedProduct, actualResult, "Users are not the same");
        }

        [TestCase("vsxdvsvdsv")]
        [TestCase("bdfbbdsfb")]
        [TestCase("bsdbfbbefe")]
        public void UserRepository_GetByID_ShouldThrowArgumentException(string id)
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionUserRepository(arrangedContext);
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.GetUserByID(id); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("")]
        [TestCase(null)]
        public void UserRepository_GetByLogin_ShouldThrowArgumentNullException(string login)
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionUserRepository(arrangedContext);
            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { repo.GetUserByLogin(login); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("vsvdvfs")]
        [TestCase("seffesgerg")]
        [TestCase("product11")]
        public void UserRepository_GetByLogin_ShouldThrowArgumentException(string login)
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionUserRepository(arrangedContext);
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.GetUserByLogin(login); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [Test]
        public void UserRepository_AfterRegisterUser_LengthOfCollectionsEquivalents()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionUserRepository(arrangedContext);
            var arrangedUser = new User("user_test", "pa$$w0rd", "Tester", "Testerov", "0671545574");
            var expectedLength = arrangedContext.Users.ToList().Count + 1;

            // Act
            repo.RegisterUser(arrangedUser);
            var actualResult = repo.GetUsers().Count();

            //Assert
            Assert.AreEqual(expectedLength, actualResult);
        }

        [Test]
        public void UserRepository_RegisterNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionUserRepository(arrangedContext);
            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { repo.RegisterUser(null); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("", "afsfa", "fafwdfw", "fsefesfe", "esfesff")]
        [TestCase("vdsvvdsv", "", "fafwdfw", "fsefesfe", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "", "fsefesfe", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "fafwdfw", "", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "fafwdfw", "fsefesfe", "")]
        public void UserRepository_RegiserUserWithInvalidData_ShouldThrowArgumentException(string login, string password,
            string name, string surname, string phone)
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionUserRepository(arrangedContext);
            var arrangedUser = new User(login, password, name, surname, phone);
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.RegisterUser(arrangedUser); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("user1")]
        [TestCase("user2")]
        [TestCase("user3")]
        [TestCase("admin")]
        public void UserRepository_RegisterUserWithExistsLogin_ShouldThrowArgumentException(string login)
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionUserRepository(arrangedContext);
            var arrangedUser = new User(login, "pa$$w0rd", "Tester", "Testerov", "0671254545");
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.RegisterUser(arrangedUser); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [Test]
        public void UserRepository_AfterUpdateUser_ObjectsAreEqual()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionUserRepository(arrangedContext);
            var id = arrangedContext.Users.ElementAt(1).ID.ToString();
            var login = arrangedContext.Users.ElementAt(1).Login;
            var expectedUser = new User(id, login, "pa$$w0rd", "Tester", "Testerov", "0985655665");

            // Act
            repo.UpdateUser(expectedUser);
            var actualResult = repo.GetUserByID(id);

            //Assert
            Assert.AreEqual(expectedUser, actualResult);
        }

        [Test]
        public void UserRepository_UpdateUserNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionUserRepository(arrangedContext);
            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { repo.UpdateUser(null); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("vdsvvdsv", "", "fafwdfw", "fsefesfe", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "", "fsefesfe", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "fafwdfw", "", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "fafwdfw", "fsefesfe", "")]
        public void UserRepository_UpdateUserWithInvalidData_ShouldThrowArgumentException(string login, string password,
            string name, string surname, string phone)
        {
            // Arrange
            var arrangedContext = new StoreContext();
            var repo = new CollectionUserRepository(arrangedContext);
            var arrangedUser = new User(login, password, name, surname, phone);
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.UpdateUser(arrangedUser); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }
    }
}
