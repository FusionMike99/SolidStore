using StoreSolidConsoleApp.Models;
using StoreSolidConsoleApp.UI;
using System;

namespace StoreSolidConsoleApp
{
    static class Program
    {
        private static void Main()
        {
            MenuContext menuContext = new MenuContext(new GuestMenuControl());
            while (menuContext.RoleMenu.IsRunning)
            {
                if(menuContext.RoleMenu is GuestMenuControl
                    && menuContext.RoleMenu.User != null)
                {
                    User user = menuContext.RoleMenu.User;
                    if (user.UserRole == UserRole.RegisteredUser)
                        menuContext.SetStrategy(new RegisteredUserMenuControl(user));
                    else
                        menuContext.SetStrategy(new AdminMenuControl(user));
                }
                else
                {
                    if (menuContext.RoleMenu.User == null)
                        menuContext.SetStrategy(new GuestMenuControl());
                }
                menuContext.Execute();
            }
        }
    }
}
