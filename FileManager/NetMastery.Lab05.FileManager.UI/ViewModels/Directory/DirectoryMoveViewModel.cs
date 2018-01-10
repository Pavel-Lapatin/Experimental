using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels.Directory
{
    public class DirectoryMoveViewModel : DirectoryViewModel
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

        public DirectoryMoveViewModel(string currentPath, string destinationPath, string sourcePath) : base(currentPath, destinationPath)
        {
            SourcePath = sourcePath;
        }
        public override void RenderViewModel()
        {
            base.RenderViewModel();
            Console.WriteLine("Directory moved seccessfully");
        }
    }
}
