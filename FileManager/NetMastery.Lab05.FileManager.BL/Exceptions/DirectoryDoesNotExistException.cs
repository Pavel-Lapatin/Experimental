using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Bl.Exceptions
{
    class DirectoryDoesNotExistException : FileManagerBlArgumentException
    {
        public DirectoryDoesNotExistException() : base("Directory dosn't exist")
        {

        }
    }
}
