using NetMastery.Lab05.FileManager.BLModel.DtoClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.FileManeger.Bl.Interfaces
{
    public interface IAuthenticationService
    {
        UserInfo Signin(string login, string password);
        UserInfo Singup(string login, string password);
    }
}
