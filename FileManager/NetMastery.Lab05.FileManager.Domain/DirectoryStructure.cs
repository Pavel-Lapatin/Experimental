using System;
using System.Collections;
using System.Collections.Generic;


namespace NetMastery.Lab05.FileManager.Domain
{
    public class DirectoryStructure
    {
        public int DirectoryId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public long DirectorySize { get; set; }
        public string FullPath { get; set; }
        public virtual DirectoryStructure ParentDirectory { get; set; }
        public virtual ICollection<DirectoryStructure> ChildrenDirectories { get; set; }
        public virtual ICollection<FileStructure> Files { get; set; }
    }
}
