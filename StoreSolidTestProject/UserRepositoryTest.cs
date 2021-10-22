using NUnit.Framework;
using StoreSolidConsoleApp.Models;
using StoreSolidConsoleApp.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using StoreSolidConsoleApp.Context;
using Moq;

namespace TestProject1
{
    [TestFixture]
    class UserRepositoryTest
    {
        private List<User> users;

        [SetUp]
        public void SetUp()
        {
            users = new List<User>()
            {
                new User("user1", "pa$$w0rd", "User1", "Userov1", "0981005060"),
                new User("user2", "pa$$w0rd", "User2", "Userov2", "0972004070"),
                new User("user3", "pa$$w0rd", "User3", "Userov3", "0963003080"),
                new User("admin", "pa$$w0rd", "Admin", "Adminov", "0911001010", UserRole.Administrator)
            };
        }

        [Test]
        public void UserRepository_GetAll_ReturnsEqual()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);

            // Act
            var actualResult = repo.GetUsers();

            //Assert
            CollectionAssert.AreEquivalent(mockContext.Object.Users, actualResult);
        }

        [Test]
        public void UserRepository_GetByID_ReturnsEquals()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
            var expectedProduct = mockContext.Object.Users[0];

            // Act
            var actualResult = repo.GetUserByID(expectedProduct.ID.ToString());

            //Assert
            Assert.AreEqual(expectedProduct, actualResult, "Users are not the same");
        }

        [Test]
        public void UserRepository_GetByLogin_ReturnsEquals()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var expectedProduct = mockContext.Object.Users[0];
            var repo = new CollectionUserRepository(mockContext.Object);

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
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
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
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
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
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
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
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
            var arrangedUser = new User("user_test", "pa$$w0rd", "Tester", "Testerov", "0671545574");
            var expectedLength = mockContext.Object.Users.Count + 1;

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
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
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
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
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
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
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
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
            var id = mockContext.Object.Users[1].ID.ToString();
            var login = mockContext.Object.Users[1].Login;
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
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
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
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
            var arrangedUser = new User(login, password, name, surname, phone);
            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { repo.UpdateUser(arrangedUser); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }
    }
}
