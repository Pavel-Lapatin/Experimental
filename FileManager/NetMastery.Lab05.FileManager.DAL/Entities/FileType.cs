using NetMastery.Lab05.FileManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL
{
    public class FileType
    {
        public int TypeId { get; set; }
        public string Extension { get; set; }
        public string RelatedProgram { get; set; }

        public virtual ICollection<FileStructure> Files {get; set;}
    }
}
