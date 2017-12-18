using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.BL.Dto
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public long MaxStorageSize { get; set; }
        public long CurentStorageSize { get; set; }

        public DirectoryStructureDto RootDirectory { get; set; }

    }
}
