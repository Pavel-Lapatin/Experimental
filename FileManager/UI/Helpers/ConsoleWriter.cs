using NetMastery.FileManeger.Bl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Helpers
{
    public class ConsoleWriter<T> : IInfoWriter<T> where T : class
    {
        public void WriteInfo(T obj)
        {
            var properties = typeof(T).GetProperties(System.Reflection.
                BindingFlags.Public).
                Where(x => x.GetType().IsValueType ||
                   x.GetType().Name == "string");

            foreach (var property in properties)
            {
                Console.WriteLine($"{property.Name}: {property.GetValue(obj).ToString()}");
            }
        }
    }
}
