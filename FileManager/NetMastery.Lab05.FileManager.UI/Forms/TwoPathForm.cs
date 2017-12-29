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
                RemoveError(nameof(SourcePath));
                if (string.IsNullOrEmpty(value))
                {
                    AddError(nameof(SourcePath), "Source path shouldn't be null or empty");
                }
                else
                {
                    value = CreatePath(value, _currentPath, nameof(SourcePath));
                }
                sourcePath = value;

            }
        }

        public TwoPathForm(string currentPath, string destinationPath, string sourcePath) : base(currentPath, destinationPath)
        {
            SourcePath = sourcePath;
        }
    }
}
