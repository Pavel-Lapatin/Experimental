using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Forms
{
    public class SearchDirectoryForm : OnePathForm
    {
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
        public SearchDirectoryForm(string currentPath, string path, string pat) : base(currentPath, path)
        {
            Pattern = pat;
        }
    }
}
