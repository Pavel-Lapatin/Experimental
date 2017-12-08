using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace QueueDemonstrate
{
    public class CustomQueue<T> : IQueue<T> where T : class
    {
        private QueueElement<T> firstElemetn;
        private QueueElement<T> lastElement;

      
        public void Enqueue(T element)
        {
            QueueElement<T> currentElement = new QueueElement<T>();
            currentElement.Current = element;
            currentElement.Parent = null;
            if (firstElemetn == null)
            {
                firstElemetn = currentElement;
                lastElement = currentElement;
            }
            else
            {
                firstElemetn.Parent = currentElement;
                firstElemetn = currentElement;
            }
        }

        public T Dequeue()
        {
            QueueElement<T> currentElement = lastElement;
            lastElement = currentElement.Parent;
            return currentElement.Current;
        }

        public T Peek()
        {
            return lastElement.Current;
        }
    }
}
