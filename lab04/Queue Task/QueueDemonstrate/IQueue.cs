using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDemonstrate
{
    public interface IQueue<T> where T : class
    {
        void Enqueue(T element);
        T Dequeue();
        T Peek();
    }
}
