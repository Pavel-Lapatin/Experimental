using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Bl.Exceptions
{
    public class ServiceArgumentException : ArgumentException
    {
        public ServiceArgumentException() : base()
        {

        }
        public ServiceArgumentException(string message) : base(message)
        {

        }
    }
}
