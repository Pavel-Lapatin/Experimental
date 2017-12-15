using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Entities

{
    public class FileInfo
    {
        public int FileId { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationDate { get; set; }
        public int FileSize { get; set; }
        public int DownloadsNumber { get; set; }

        public virtual int FileTypeId { get; set; }
        public virtual FileType FileType  { get; set; }

        public virtual int DirectoryId { get; set; }
        public virtual DirectoryInfo Directory { get; set; }
    }
}
