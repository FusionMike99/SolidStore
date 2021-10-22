using StoreSolidConsoleApp.Models;
using StoreSolidConsoleApp.Data;
using StoreSolidConsoleApp.UoW;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.Roles
{
    public class Guest
    {
        private readonly IUnitOfWork unitOfWork;

        public Guest()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public Guest(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> ListOfProducts() => unitOfWork.ProductRepository.GetProducts();

        public Product SearchProductByName(string name) => unitOfWork.ProductRepository.GetProductByName(name);

        public void RegisterUser(User user) => unitOfWork.UserRepository.RegisterUser(user);

        public User AuthorizeUser(string login, string password) => unitOfWork.UserRepository.AuthorizeUser(login, password);
    }
}
