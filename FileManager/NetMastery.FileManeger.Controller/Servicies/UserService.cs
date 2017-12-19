using NetMastery.FileManeger.Bl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.BLModel.DtoClasses;

namespace NetMastery.FileManeger.Bl.Servicies
{
    public class UserInfoConsoleService : IUserInfoWriter
    {
        public void WriteUserInfo(string login)
        {
            if (userInfo == null) throw new ArgumentException("There is no user info");
            ConsoleWriteline
        }
    }
}
