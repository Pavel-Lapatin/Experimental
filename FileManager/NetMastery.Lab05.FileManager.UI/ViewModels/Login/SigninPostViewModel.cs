using NetMastery.Lab05.FileManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class SigninPostViewModel : ViewModel
    {
        public AccountDto Account { get; set; }
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
                password = value;
            }
        }

        public SigninPostViewModel(string login, string password) 
        {
            Login = login;
            Password = password;
        }

        public override void RenderViewModel()
        {
            base.RenderViewModel();
            if(Account != null)
            {
                Console.WriteLine($"Hi, {Account.Login}");
            }
           
        }
    }
}
