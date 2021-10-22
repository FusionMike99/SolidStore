using System;
using System.Collections.Generic;
using System.Text;
using StoreSolidConsoleApp.Roles;
using StoreSolidConsoleApp.Data;
using StoreSolidConsoleApp.Models;
using static System.Console;
using StoreSolidConsoleApp.UoW;
using System.Linq;

namespace StoreSolidConsoleApp.UI
{
    class RegisteredUserMenuControl : RoleMenuControl
    {
        private readonly RegisteredUser registered;

        public RegisteredUserMenuControl(User user)
        {
            registered = new RegisteredUser(user);
            IsRunning = true;
            User = user;
        }

        public override void ControlMenu()
        {
            var menuElements = new string[]
            {
                "List of products", "Find product by name",
                "Create order", "Cancel order", "View history",
                "Receive order", "Update profile", "Sign out", "Exit"
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
                        4 => CancelingOrder,
                        5 => ViewHistoryOfOrders,
                        6 => ReceiveOrder,
                        7 => UpdatingProfile,
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
            var products = registered.ListOfProducts();
            foreach (var item in products)
            {
                WriteLine("{0} {1} {2} {3}", item.Name, item.Category, item.Description, item.Cost);
            }
        }

        private void DisplayProductByName()
        {
            string input = GetInput("Input name of product");
            var product = registered.SearchProductByName(input);
            WriteLine("{0} {1} {2} {3}", product.Name, product.Category, product.Description, product.Cost);
        }

        private void CreatingOrder()
        {
            var products = registered.ListOfProducts().ToList();
            for (int i = 0; i < products.Count; i++)
            {
                WriteLine("{0} {1} {2} {3}", products[i].Name, products[i].Category,
                    products[i].Description, products[i].Cost);
            }

            List<OrderItem> items = new List<OrderItem>();
            while(true)
            {
                string input = GetInput("Input name of product or input \"finish\"");
                if (input == "finish")
                    break;
                Product product = null;
                try
                {
                    product = registered.SearchProductByName(input);
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }

                input = GetInput("Input count of products");
                if(!int.TryParse(input, out int countOfProduct))
                {
                    WriteLine("Invalid input, you should input number");
                    continue;
                }
                items.Add(new OrderItem(product, countOfProduct));
            }

            Order order = new Order(items, null);
            registered.CreateNewOrder(order);
        }

        private void CancelingOrder()
        {
            var orders = registered.ReviewOrderHistory().ToList();
            var collectionOfIds = new List<Guid>();
            for (int i = 0; i < orders.Count; i++)
            {
                Order order = orders[i];
                WriteLine($"{i + 1}. Date: {order.DateOfOpening}; Status: {order.OrderStatus}");
                collectionOfIds.Add(order.ID);
            }
            string input = GetInput("Input number of order");
            if(!int.TryParse(input, out int numberOfId))
            {
                WriteLine("Invalid number");
                return;
            }
            try
            {
                registered.CancelOrder(collectionOfIds[numberOfId - 1].ToString());
            }
            catch (IndexOutOfRangeException)
            {
                WriteLine("Input wrong number");
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        private void ViewHistoryOfOrders()
        {
            var orders = registered.ReviewOrderHistory().ToList();
            for (int i = 0; i < orders.Count; i++)
            {
                Order order = orders[i];
                WriteLine($"{i + 1}. Date: {order.DateOfOpening}; Status: {order.OrderStatus}:");
                foreach (var item in order.OrderItems)
                {
                    WriteLine($"\t{item.Product.Name} - {item.Amount} - {item.Cost}");
                }
            }
        }

        private void ReceiveOrder()
        {
            var orders = registered.ReviewOrderHistory().ToList();
            var collectionOfIds = new List<Guid>();
            for (int i = 0; i < orders.Count; i++)
            {
                Order order = orders[i];
                WriteLine($"{i + 1}. Date: {order.DateOfOpening}; Status: {order.OrderStatus}");
                collectionOfIds.Add(order.ID);
            }
            string input = GetInput("Input number of order");
            if (!int.TryParse(input, out int numberOfId))
            {
                WriteLine("Invalid number");
                return;
            }
            try
            {
                registered.ReceiveOrder(collectionOfIds[numberOfId - 1].ToString());
            }
            catch (IndexOutOfRangeException)
            {
                WriteLine("Input wrong number");
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        private void UpdatingProfile()
        {
            string password = GetInput("Input password");
            string name = GetInput("Input name");
            string surname = GetInput("Input surname");
            string phoneNumber = GetInput("Input phone number");
            registered.UpdateProfile(name, surname, password, phoneNumber);
        }
    }
}
