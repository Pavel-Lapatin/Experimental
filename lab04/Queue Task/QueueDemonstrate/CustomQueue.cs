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
        private QueueElement<T> FirstElemetn;
        private QueueElement<T> LastElement;
        private QueueElement<T> CurrentElement { get; set; }

      
        public void Enqueue(T element)
        {
            QueueElement<T> currentElement = new QueueElement<T>();
            currentElement.Current = element;
            currentElement.Parent = null;
            if (FirstElemetn == null)
            {
                FirstElemetn = currentElement;
                LastElement = currentElement;
            }
            else
            {
                FirstElemetn.Parent = currentElement;
                currentElement.Previous = FirstElemetn;
                FirstElemetn = currentElement;
            }
        }

        public T Dequeue()
        {
            QueueElement<T> currentElement = LastElement;
            LastElement = currentElement.Parent;
            return currentElement.Current;
        }

        public T Peek()
        {
            return LastElement.Current;
        }
    }
}
