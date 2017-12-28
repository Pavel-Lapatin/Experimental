using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Domain

{
    public class FileStructure
    {
        public int FileId { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationDate { get; set; }
        public long FileSize { get; set; }
        public int DownloadsNumber { get; set; }
        public string Extension { get; set; }
        public virtual DirectoryStructure Directory { get; set; }
    }
}
