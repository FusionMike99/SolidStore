using StoreSolidConsoleApp.Models;
using StoreSolidConsoleApp.Roles;
using StoreSolidConsoleApp.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace StoreSolidConsoleApp.UI
{
    class AdminMenuControl : RoleMenuControl
    {
        private readonly Administrator admin;

        public AdminMenuControl(User user)
        {
            admin = new Administrator(user);
            IsRunning = true;
            User = user;
        }

        public override void ControlMenu()
        {
            var menuElements = new string[]
            {
                "List of products", "Find product by name",
                "Create order", "Update user profile", "Add product",
                "Update product", "Update order status", "Sign out", "Exit"
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
                        3 => CreatingOrder,
                        4 => UpdatingUserProfile,
                        5 => CreatingProduct,
                        6 => UpdatingProduct,
                        7 => UpdatingStatusOfOrder,
                        8 => () => User = null,
                        9 => ExitFromApp,
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
            var products = admin.ListOfProducts();
            foreach (var item in products)
            {
                WriteLine("{0} {1} {2} {3}", item.Name, item.Category, item.Description, item.Cost);
            }
        }

        private void DisplayProductByName()
        {
            string input = GetInput("Input name of product");
            var product = admin.SearchProductByName(input);
            WriteLine("{0} {1} {2} {3}", product.Name, product.Category, product.Description, product.Cost);
        }

        private void CreatingOrder()
        {
            var products = admin.ListOfProducts().ToList();
            for (int i = 0; i < products.Count; i++)
            {
                WriteLine("{0} {1} {2} {3}", products[i].Name, products[i].Category,
                    products[i].Description, products[i].Cost);
            }

            List<OrderItem> items = new List<OrderItem>();
            while (true)
            {
                string input = GetInput("Input name of product or input \"finish\"");
                if (input == "finish")
                    break;
                Product product = null;
                try
                {
                    product = admin.SearchProductByName(input);
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }

                input = GetInput("Input count of products or input");
                if (!int.TryParse(input, out int countOfProduct))
                {
                    WriteLine("Invalid input, you should input number");
                    continue;
                }
                items.Add(new OrderItem(product, countOfProduct));
            }

            Order order = new Order(items, null);
            admin.CreateNewOrder(order);
        }

        private void UpdatingUserProfile()
        {
            var users = admin.ListOfUsers().ToList();
            for (int i = 0; i < users.Count; i++)
            {
                User item = users[i];
                WriteLine($"{i + 1}. {item.Login} - {item.Name} {item.Surname}");
            }
            string login = GetInput("Input login");
            User foundUser = admin.SearchUserByLogin(login);
            string name = GetInput("Input name");
            string surname = GetInput("Input surname");
            string phoneNumber = GetInput("Input phone number");
            User updatableUser = new User(foundUser.ID.ToString(), foundUser.Login, foundUser.Password,
                name, surname, phoneNumber);
            admin.UpdateUserProfile(updatableUser);
        }

        private void CreatingProduct()
        {
            string name = GetInput("Input name");
            string category = GetInput("Input category");
            string description = GetInput("Input description");
            string costText = GetInput("Input cost");
            if(!float.TryParse(costText, out float cost))
            {
                WriteLine("Invalid float number");
                return;
            }
            admin.AddNewProduct(new Product(name, category, description, cost));
        }

        private void UpdatingProduct()
        {
            var products = admin.ListOfProducts().ToList();
            for (int i = 0; i < products.Count; i++)
            {
                WriteLine("{0} {1}", products[i].Name, products[i].Category);
            }
            string input = GetInput("Input name of product");
            Product foundProduct = admin.SearchProductByName(input);
            string name = GetInput("Input updated name");
            string category = GetInput("Input updated category");
            string description = GetInput("Input updated description");
            string costText = GetInput("Input updated cost");
            if (!float.TryParse(costText, out float cost))
            {
                WriteLine("Invalid float number");
                return;
            }
            admin.UpdateProduct(new Product(foundProduct.ID.ToString(), name,
               category, description, cost));
        }

        private void UpdatingStatusOfOrder()
        {
            var orders = admin.ListOfOrders().ToList();
            var collectionOfIds = new List<Guid>();
            for (int i = 0; i < orders.Count; i++)
            {
                Order order = orders[i];
                WriteLine($"{i + 1}. Date: {order.DateOfOpening}; Client: {order.User.Name} {order.User.Surname}; Status: {order.OrderStatus}");
                collectionOfIds.Add(order.ID);
            }
            string input = GetInput("Input number of order");
            if (!int.TryParse(input, out int numberOfId))
            {
                WriteLine("Invalid number");
                return;
            }
            WriteLine("Statuses:");
            WriteLine("\t1. Payment received");
            WriteLine("\t2. Order sent");
            WriteLine("\t4. Order completed");
            WriteLine("\t5. Order canceled");
            input = GetInput("Input number of status");
            if (!byte.TryParse(input, out byte numberOfStatus))
            {
                WriteLine("Invalid number");
                return;
            }
            try
            {
                admin.UpdateStatusOrder(collectionOfIds[numberOfId - 1].ToString(), (OrderStatus)numberOfStatus);
            }
            catch (IndexOutOfRangeException)
            {
                WriteLine("Input wrong number");
            }
        }
    }
}
