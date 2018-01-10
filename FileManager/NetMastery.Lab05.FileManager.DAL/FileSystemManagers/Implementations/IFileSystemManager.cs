using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IFileSystemManager
    {
        void Move(string destination, string source);
        void Remove(string destination);
        bool IsExist(string path);
    }
}
