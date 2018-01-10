using NetMastery.Lab05.FileManager.UI.ViewModels.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class DirectorySearchVIewModel : DirectoryViewModel
    {
        public IList<string> Data { get; set; }

        private string pattern;

        public string Pattern
        {
            get { return pattern; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    AddError(nameof(Pattern), "Pattern shouldn't be null or empty");
                }
                pattern = value;
            }
        }
        public DirectorySearchVIewModel(string currentPath, string path, string pat) : base(currentPath, path)
        {
            Pattern = pat;
        }

        public override void RenderViewModel()
        {
            base.RenderViewModel();
            Console.WriteLine("Search result: ");
            foreach (var item in Data)
            {
                Console.WriteLine(item);
            }
            
        }
    }
}
