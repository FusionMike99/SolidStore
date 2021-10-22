using System;
using System.Collections.Generic;
using System.Text;
using StoreSolidConsoleApp.Roles;
using StoreSolidConsoleApp.Data;
using StoreSolidConsoleApp.Models;
using static System.Console;
using StoreSolidConsoleApp.UoW;

namespace StoreSolidConsoleApp.UI
{
    public partial class GuestMenuControl : RoleMenuControl
    {
        private readonly Guest guest;

        public GuestMenuControl()
        {
            guest = new Guest();
            User = null;
            IsRunning = true;
        }

        public override void ControlMenu()
        {
            var menuElements = new string[]
            {
                "List of products", "Find product by name",
                "Registration", "Authorization", "Exit"
            };
            DisplayMenu(menuElements);
            if (byte.TryParse(ReadLine(), out byte result))
            {
                try
                {
                    Operation operation = result switch
                    {
                        1 => DisplayProducts,
                        2 => DisplayProductByName,
                        3 => Registration,
                        4 => Authorization,
                        5 => ExitFromApp,
                        _ => DisplayUnknwonOperation
                    };
                    operation.Invoke();
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }
            }
            else
            {
                WriteLine("Inputting is not a number. Please, input number");
            }
        }

        private void DisplayProducts()
        {
            var products = guest.ListOfProducts();
            foreach (var item in products)
            {
                WriteLine("{0} {1} {2} {3}", item.Name, item.Category, item.Description, item.Cost);
            }
        }

        private void DisplayProductByName()
        {
            string input = GetInput("Input name of product");
            var product = guest.SearchProductByName(input);
            WriteLine("{0} {1} {2} {3}", product.Name, product.Category, product.Description, product.Cost);
        }

        private void Registration()
        {
            string login = GetInput("Input login");
            string password = GetInput("Input password");
            string name = GetInput("Input name");
            string surname = GetInput("Input surname");
            string phoneNumber = GetInput("Input phone number");
            guest.RegisterUser(new User(login, password, name, surname, phoneNumber));
        }

        private void Authorization()
        {
            string login = GetInput("Input login");
            string password = GetInput("Input password");
            User = guest.AuthorizeUser(login, password);
        }
    }
}
