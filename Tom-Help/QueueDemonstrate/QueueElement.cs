using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDemonstrate
{
    public class QueueElement<T> where T : class
    {
        public T Value { get; set; }
        public QueueElement<T> Parent { get; set; }
    }
}
