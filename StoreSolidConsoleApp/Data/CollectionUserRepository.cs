using StoreSolidConsoleApp.Context;
using StoreSolidConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSolidConsoleApp.Data
{
    public partial class CollectionUserRepository : IUserRepository
    {
        private readonly StoreContext context;

        public CollectionUserRepository(StoreContext context)
        {
            this.context = context;
        }

        public User AuthorizeUser(string login, string password)
        {
            var foundUser = GetUserByLogin(login);
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Empty argument password");
            if (foundUser.Password != password)
                throw new ArgumentException("Wrong password");
            return foundUser;
        }

        public User GetUserByID(string userID)
        {
            var foundUser = context.Users.Find(item => item.ID.ToString() == userID);
            if (foundUser == null)
                throw new ArgumentException("Not found user with that ID");
            return foundUser;
        }

        public User GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException("login", "Empty argument login");
            var foundUser = context.Users.Find(item => item.Login == login);
            if (foundUser == null)
                throw new ArgumentException("Not found user with that login");
            return foundUser;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users;
        }

        public void RegisterUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", " is null");
            if (!ValidateUser(user))
                throw new ArgumentException("Some arguments of user are not valid");
            if (context.Users.Exists(item => item.Login == user.Login))
                throw new ArgumentException("User with same login is already exist");
            context.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", " is null");
            if (!ValidateUser(user))
                throw new ArgumentException("Some arguments of user are not valid");
            var updatedProduct = GetUserByID(user.ID.ToString());
            updatedProduct.Name = user.Name;
            updatedProduct.Surname = user.Surname;
            updatedProduct.PhoneNumber = user.PhoneNumber;
            updatedProduct.Password = user.Password;
        }

        private bool ValidateUser(User user)
        {
            if (!string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password)
                && !string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Surname)
                && !string.IsNullOrEmpty(user.PhoneNumber))
                return true;
            else
                return false;
        }
    }
}
