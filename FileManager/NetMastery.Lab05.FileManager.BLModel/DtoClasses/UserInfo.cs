using NetMastery.Lab05.FileManager.BL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.BLModel.DtoClasses
{
    public class UserInfo
    {
        public string Login { get; set; }
        public DateTime CreationDate { get; set; }
        public long MaxStorageSize { get; set; }
        public long CurentStorageSize { get; set; }
        public DirectoryStructureDto RootDirectory { get; set; }
    }
}
