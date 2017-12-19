using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.BL.Dto
{
    public class DirectoryStructureDto
    {
        public int DirectoryId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public long DirectorySize { get; set; }
        public string FullPath { get; set; }

        public DirectoryStructureDto ParentDirectory { get; set; }

        public ICollection<DirectoryStructureDto> ChildrenDirectories { get; set; }
        public ICollection<FileStructureDto> Files { get; set; }

    }
}
