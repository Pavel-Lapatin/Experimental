using System;
using System.Collections;
using System.Collections.Generic;


namespace NetMastery.Lab05.FileManager.DAL
{
    public class Storage
    {
        public int StorageId { get; set; }
        public string path { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public int
        public virtual ICollection ChildrenStoragies { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }


}
