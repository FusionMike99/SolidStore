using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.UI
{
    class MenuContext
    {
        public RoleMenuControl RoleMenu { get; private set; }

        public MenuContext(RoleMenuControl roleMenu)
        {
            RoleMenu = roleMenu;
        }

        public void SetStrategy(RoleMenuControl roleMenu)
        {
            RoleMenu = roleMenu;
        }

        public void Execute()
        {
            RoleMenu.ControlMenu();
        }
    }
}
