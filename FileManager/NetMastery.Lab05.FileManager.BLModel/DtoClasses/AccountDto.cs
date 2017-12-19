using NetMastery.Lab05.FileManager.BLModel.DtoClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.BL.Dto
{
    public class AccountDto : UserInfo
    {
        public int AccountId { get; set; }
        public string Password { get; set; }
    }
}
