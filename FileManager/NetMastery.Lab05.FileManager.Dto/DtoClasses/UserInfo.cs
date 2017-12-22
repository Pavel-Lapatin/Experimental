using System;

namespace NetMastery.Lab05.FileManager.Dto
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
