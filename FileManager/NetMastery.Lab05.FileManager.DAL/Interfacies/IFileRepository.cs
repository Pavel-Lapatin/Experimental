using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IFileRepository : IFSRepository
    {
        FileInfo GetFileInfo(string path);
        void Copy(string destination, string source);
    }
}
