using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels.Directory
{
    public class FileMoveViewModel : DirectoryViewModel
    {
        private string sourcePath;

        public string SourcePath
        {
            get { return sourcePath; }
            set
            {
                sourcePath = CreatePath(value, _currentPath, nameof(SourcePath));
            }
        }

        public FileMoveViewModel(string currentPath, string path, string name) : base(currentPath, path)
        {
            SourcePath = sourcePath;
        }

        public override void RenderViewModel()
        {
            base.RenderViewModel();
            Console.WriteLine("File uploaded seccessfully");
        }
    }
}
