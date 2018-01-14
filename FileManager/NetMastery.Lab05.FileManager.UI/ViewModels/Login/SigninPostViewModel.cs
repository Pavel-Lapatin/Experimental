using NetMastery.Lab05.FileManager.Dto;
using System;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class SigninPostViewModel : ViewModel
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
    }
}
