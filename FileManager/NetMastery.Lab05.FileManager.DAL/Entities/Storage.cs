using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;


namespace NetMastery.Lab05.FileManager.DAL
{
    public class Storage
    {
        public int StorageId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public virtual Storage Parent { get; set; }
        public virtual ICollection<File> Files { get; set;}
    }


}
