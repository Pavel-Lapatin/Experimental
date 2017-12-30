using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IFSRepository
    {
        void Move(string destination, string source);
        void Remove(string destination);
        void MoveRollback(string destination, string source);
        bool IsExist(string path);
    }
}
