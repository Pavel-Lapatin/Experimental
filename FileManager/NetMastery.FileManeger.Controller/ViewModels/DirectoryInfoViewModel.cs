using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.FileManeger.Bl.ViewModels
{
    public class DirectoryInfoViewModel
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public long DirectorySize { get; set; }
        public string Login { get; set; }
    }
}
