using System;

namespace NetMastery.Lab05.FileManager.BL.Dto
{
    public class FileStructureDto
    {
        public int FileId { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationDate { get; set; }
        public long FileSize { get; set; }
        public int DownloadsNumber { get; set; }
        public string Extension { get; set; }
        public virtual DirectoryStructureDto Directory { get; set; }

        //public int FileHash { get; set; }
    }
}