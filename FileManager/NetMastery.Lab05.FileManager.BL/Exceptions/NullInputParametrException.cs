using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Bl.Exceptions
{
    public class NullInputParametrException : FileManagerBlArgumentException
    {
        public NullInputParametrException() : base("Input argument is null")
        {

        }
        public NullInputParametrException(string message) : base(message)
        {

        }
    }
}
