using NetMastery.FileManeger.Bl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.BLModel.DtoClasses;

namespace NetMastery.Lab05.FileManager.BlExtensions
{
    public class UserInfoConsoleWriter : IUserInfoWriter
    {
        public void WriteUserInfo(UserInfo userInfo)
        {
            Console.WriteLine("login: " + userInfo.Login);
            Console.WriteLine("creation date: " + userInfo.CreationDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("used storage space: " + userInfo.CurentStorageSize + " kB");
            Console.WriteLine("max storage size: " + userInfo.MaxStorageSize + " kB");
            Console.WriteLine();
        }
    }
}
