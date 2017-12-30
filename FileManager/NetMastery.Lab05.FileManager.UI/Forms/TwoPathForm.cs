using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Forms
{
    public class TwoPathForm : OnePathForm
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

        public TwoPathForm(string currentPath, string destinationPath, string sourcePath) : base(currentPath, destinationPath)
        {
            SourcePath = sourcePath;
        }
    }
}
