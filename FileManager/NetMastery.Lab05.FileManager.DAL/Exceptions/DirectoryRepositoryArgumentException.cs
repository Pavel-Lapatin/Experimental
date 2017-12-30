using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Exceptions
{
    public class FSRepositoryArgumentException : FileManagerDALArgumentException
    {
        public FSRepositoryArgumentException() : base()
        {

        }

        public FSRepositoryArgumentException(string message) : base(message)
        {

        }
    }
}
