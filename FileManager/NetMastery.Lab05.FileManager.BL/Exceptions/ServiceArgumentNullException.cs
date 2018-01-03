using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Bl.Exceptions
{
    public class ServiceArgumentNullException : ServiceArgumentException
    {
        public ServiceArgumentNullException() : base("Input argument is null")
        {

        }
        public ServiceArgumentNullException(string message) : base(message)
        {

        }
    }
}
