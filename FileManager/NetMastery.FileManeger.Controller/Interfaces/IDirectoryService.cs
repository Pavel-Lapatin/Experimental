using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.FileManeger.Bl.Interfaces
{
    interface IDirectoryService
    {
        void AddDirectory(string path, string name, string currentPath);
        void Move(string pathFrom, string pathTo);
        void Remove(string path, string currentPath);
        IEnumerable<string> Search(string currentPAth, string pattern);
        void ChangeWorkDirectory(string path, string currentPath);
    }
}
