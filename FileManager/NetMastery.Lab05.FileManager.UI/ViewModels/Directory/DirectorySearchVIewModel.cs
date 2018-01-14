using System;
using System.Collections.Generic;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class DirectorySearchVIewModel : PathViewModel
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
        public DirectorySearchVIewModel(string path, string pat) : base(path)
        {
            Pattern = pat;
        }
    }
}
