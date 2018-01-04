using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Exceptions
{
    public class fsDirectoryManagerArgumentException : FileManagerDALArgumentException
    {
        public fsDirectoryManagerArgumentException() : base()
        {

        }

        public fsDirectoryManagerArgumentException(string message) : base(message)
        {

        }
    }
}
