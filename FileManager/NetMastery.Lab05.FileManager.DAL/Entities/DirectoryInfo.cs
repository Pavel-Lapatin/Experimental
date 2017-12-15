using System;
using System.Collections;
using System.Collections.Generic;


namespace NetMastery.Lab05.FileManager.DAL.Entities
{
    public class DirectoryInfo
    {
        public int DirectoryId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }


        public virtual Account Account { get; set; }

        public virtual int? ParentDirectoryId { get; set; }
        public DirectoryInfo ParentDirectory { get; set; }

        public virtual ICollection<DirectoryInfo> ChildrenDirectories { get; set; }
        public virtual ICollection<FileInfo> Files { get; set; }
    }


}
