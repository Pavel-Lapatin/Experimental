using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Bl.Exceptions
{
    public class DirectoryExistsException : ServiceArgumentException
    {
        public DirectoryExistsException() : base("Directory already exists")
        {

        }
    }
}
