using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Exceptions
{
    public class DbRepositoryArgumentException : FileManagerDALArgumentException
    {
        public DbRepositoryArgumentException() : base()
        {

        }
        public DbRepositoryArgumentException(string message) : base(message)
        {

        }
    }
}
