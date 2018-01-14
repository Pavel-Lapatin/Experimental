using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public abstract class ViewModel
    {
        public bool IsValid => Errors.Count == 0;
        public IDictionary<string, IList<string>> Errors = new Dictionary<string, IList<string>>();
        protected void AddError(string key, string error)
        {
            if (Errors.ContainsKey(key))
            {
                Errors[key].Add(error);
            }
            else
            {
                Errors.Add(key, new List<string>());
                Errors[key].Add(error);
            }
        }
        protected void RemoveError(string key)
        {
            if (Errors.ContainsKey(key))
            {
                Errors.Remove(key);
            }
        }
        protected void RemoveAll()
        {
            Errors.Clear();
        }
    }
}
