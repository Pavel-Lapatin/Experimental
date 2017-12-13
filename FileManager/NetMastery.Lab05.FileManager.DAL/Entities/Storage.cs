using System;
using System.Collections;
using System.Collections.Generic;


namespace NetMastery.Lab05.FileManager.DAL.Entities
{
    public class Storage
    {
        public int StorageId { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        

        public virtual Account Account { get; set; }

        public virtual int? ParentStorageId { get; set; }
        public Storage ParentStorage { get; set; }

        public virtual ICollection<Storage> ChildrenStoragies { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }


}
