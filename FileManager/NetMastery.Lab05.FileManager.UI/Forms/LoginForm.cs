using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Forms
{
    public class LoginForm : Form
    {
        private string login;

        public string Login
        {
            get { return login; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    AddError("Login", "Login mustn't be empty string");
                }
                else
                {
                    RemoveError("Login");
                }
                login = value;
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                if (value == null || value.Length < 3)
                {
                    AddError("Password", "Password mustn't be more than 3 characters");
                }
                else
                {
                    RemoveError("Password");
                }
                password = value;
            }
        }

        public LoginForm(string currentPath) : base(currentPath)
        {
            
            Login = null;
            Password = null;
        }

        public LoginForm(string currentPath, string login, string password) : base(currentPath)
        {
            Login = login;
            Password = password;
        }

        public override void RenderForm()
        {
            Console.WriteLine("Please, signin in the system");
            Console.WriteLine("Command: login -l <userName> -p <password>");
            Console.WriteLine();
            Console.Write($"{_currentPath}--> login -l ");
        }
    }
}
