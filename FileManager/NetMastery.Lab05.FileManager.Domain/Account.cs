using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Domain
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public long MaxStorageSize { get; set; }
        public long CurentStorageSize { get; set; }

        public virtual DirectoryStructure RootDirectory { get; set; }
    }
}
