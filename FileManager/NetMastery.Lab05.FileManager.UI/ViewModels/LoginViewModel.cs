using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class LoginViewModel : ViewModel
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

        public LoginViewModel()
        {
            Login = null;
            Password = null;
        }

        public LoginViewModel(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public void RenderGet()
        {
            Console.WriteLine();
            Console.WriteLine("Please, signin in the system");
            Console.WriteLine("Command: login -l <userName> -p <password>");
            Console.WriteLine();

            Console.Write("login -l ");
        }
        public void RenderSignoff()
        {
            Console.WriteLine("Goodbye!!!");
            Console.WriteLine();
            Console.WriteLine("Press any button for continue");
            Console.ReadKey();
        }

        public string Registre(string currentPath)
        {
            Console.Write(currentPath + "--> login");
            var arguments = Console.ReadLine();
            return ("login -l"  + arguments);
        }
    }
}
