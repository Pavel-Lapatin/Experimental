using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Bl.Exceptions
{
    public class FileManagerBlArgumentException : ArgumentException
    {
        public FileManagerBlArgumentException() : base()
        {

        }
        public FileManagerBlArgumentException(string message) : base(message)
        {

        }
    }
}
