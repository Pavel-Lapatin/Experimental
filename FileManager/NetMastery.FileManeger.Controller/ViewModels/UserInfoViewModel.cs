using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.FileManeger.Bl.ViewModels
{
    public class UserInfoViewModel
    {
        public string Login { get; set; }
        public DateTime CreationDate { get; set; }
        public long StorageSize { get; set; }
        public long MaxStorageSize { get; set; }
        public long CurentStorageSize { get; set; }

    }
}
