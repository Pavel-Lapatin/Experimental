using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Exceptions
{
    public class FSDirectoryManagerArgumentException : FileManagerDALArgumentException
    {
        public FSDirectoryManagerArgumentException() : base()
        {

        }

        public FSDirectoryManagerArgumentException(string message) : base(message)
        {

        }
    }
}
