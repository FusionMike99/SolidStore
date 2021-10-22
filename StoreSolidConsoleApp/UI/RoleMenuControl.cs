using StoreSolidConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace StoreSolidConsoleApp.UI
{
    public abstract partial class RoleMenuControl
    {
        public bool IsRunning { get; protected set; }
        public User User { get; protected set; }

        public abstract void ControlMenu();
        protected delegate void Operation();

        protected void DisplayMenu(params string[] functions)
        {
            WriteLine("List of functions:");
            for (int i = 0; i < functions.Length; i++)
            {
                WriteLine($"{i+1}. {functions[i]};");
            }
            Write("Choose function: ");
        }

        protected string GetInput(string message)
        {
            Write("{0}: ", message);
            return ReadLine();
        }

        protected void ExitFromApp() => IsRunning = false;

        protected void DisplayUnknwonOperation() => WriteLine("Unknown operation");
    }
}
