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
                RemoveError(nameof(Pattern));
                if (string.IsNullOrEmpty(value))
                {
                    AddError(nameof(Pattern), "Pattern shouldn't be null or empty");
                }
                value = pattern;
            }
        }
        public SearchDirectoryForm(string currentPath, string path, string pattern) : base(currentPath, path)
        {
            Pattern = pattern;
        }
    }
}
