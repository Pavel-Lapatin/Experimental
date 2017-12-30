using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Exceptions
{
    public class DBRepositoryArgumentException : FileManagerDALArgumentException
    {
        public DBRepositoryArgumentException() : base()
        {

        }
        public DBRepositoryArgumentException(string message) : base(message)
        {

        }
    }
}
