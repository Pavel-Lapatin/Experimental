using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Bl.Exceptions
{
    public class ServiceUnathorizedtAccessException : UnauthorizedAccessException
    {
        public ServiceUnathorizedtAccessException(): base("Access to the storage path is denied")
        {

        }
        public ServiceUnathorizedtAccessException(string message) : base(message)
        {

        }
    }
}
