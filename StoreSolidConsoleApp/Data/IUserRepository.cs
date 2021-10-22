using StoreSolidConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.Data
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserByID(string userID);
        User GetUserByLogin(string login);
        User AuthorizeUser(string login, string password);
        void RegisterUser(User user);
        void UpdateUser(User user);
    }
}
