using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDemonstrate
{
    class Program
    {
        public static void Main(string[] args)
        {
            CustomQueue<String> strQueue = new CustomQueue<String>();
            strQueue.Enqueue("1");
            strQueue.Enqueue("2");
            strQueue.Enqueue("3");
            Console.WriteLine(strQueue.Dequeue());
            Console.WriteLine(strQueue.Dequeue());
            Console.WriteLine(strQueue.Peek());
            Console.WriteLine(strQueue.Dequeue());
            Console.ReadKey();
        }
    }
}
