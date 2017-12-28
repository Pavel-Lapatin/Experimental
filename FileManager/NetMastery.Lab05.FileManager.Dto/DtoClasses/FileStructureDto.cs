using System;

namespace NetMastery.Lab05.FileManager.Dto
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
        public string DirectoryPath { get; set; }
    }
}