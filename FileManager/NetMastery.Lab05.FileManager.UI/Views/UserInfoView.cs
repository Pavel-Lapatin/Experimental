using NetMastery.Lab05.FileManager.Dto;
using System;

namespace NetMastery.Lab05.FileManager.UI.Views
{
    public class UserInfoView : View
    {
        private readonly AccountDto _account;
        public UserInfoView(AccountDto account)
        {
            _account = account;
        }

        public override void RenderView()
        {
            if (_account != null)
            {
                Console.WriteLine();
                Console.WriteLine("User info:");
                Console.WriteLine($"Login: { _account.Login}");
                Console.WriteLine($"Creation date: {_account.CreationDate.ToString("yy-MM-dd")}");
                Console.WriteLine($"Current Storage Size: { _account.CurentStorageSize}");
                Console.WriteLine($"Max Storage Size: { _account.MaxStorageSize }");
                Console.WriteLine($"Root directory: { _account.RootDirectory}");
                Console.WriteLine();
            }
        }
    }
}
