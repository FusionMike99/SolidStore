using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.Models
{
    public enum UserRole : byte
    {
        RegisteredUser,
        Administrator
    }

    public partial class User : BaseEntity
    {
        public string Login { get; private set; }
        public string Password { get; internal set; }
        public string Name { get; internal set; }
        public string Surname { get; internal set; }
        public string PhoneNumber { get; internal set; }
        public UserRole UserRole { get; internal set; }

        public User(string login, string password, string name, string surname,
            string phoneNumber, UserRole userRole = UserRole.RegisteredUser)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            UserRole = userRole;
        }

        public User(string id, string login, string password, string name, string surname,
            string phoneNumber, UserRole userRole = UserRole.RegisteredUser)
        {
            ID = Guid.Parse(id);
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            UserRole = userRole;
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   ID == user.ID &&
                   Login == user.Login &&
                   Password == user.Password &&
                   Name == user.Name &&
                   Surname == user.Surname &&
                   PhoneNumber == user.PhoneNumber &&
                   UserRole == user.UserRole;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Login, Password, Name, Surname, PhoneNumber, UserRole);
        }
    }
}
