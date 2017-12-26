using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public LoginViewModel(string login, string password)
        {
            Login = login;
            Password = password;
            if(string.IsNullOrEmpty(Login))
            {
                Errors.Add("Login mustn't be the empty string");
            }
            if(password.Length<3)
            {
                Errors.Add("Password must be at least three characters");
            }    
        }
        public string Registre(string currentPath)
        {
            Console.Write(currentPath + "--> login");
            string var = Console.ReadLine();
            return ("login " + var);
        }
    }
}
