using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class PathViewModel : ViewModel
    {
        private string path;
        public string Path
        {
            get { return path; }
            set
            {
                ValidateInputPath(value, nameof(Path));
                path = value;
            }
        }

        public PathViewModel(string path)
        {
            Path = path;
        }

        protected void ValidateInputPath(string path, string errorKey)
        {
            if (string.IsNullOrEmpty(path))
            {
                AddError(errorKey, "The path must not be null or empty string");
                return;
            }
            var names = SplitPath(path, errorKey);
            if(!IsValid)
            {
                return;
            }
            Validate(names, errorKey);
        }

        private List<string> SplitPath(string path, string errorKey)
        {
            var names = new List<string>();
            var pathParts = path.Trim().Split(':');
            if (pathParts.Length > 2)
            {
                AddError(errorKey, "The path must contain only one \":\" character");
                return null;
            }
            else if (pathParts.Length == 2)
            {
                names.Add(pathParts[0]);
            }
            names.AddRange(pathParts[pathParts.Length - 1].Trim().Split('\\'));
            return names;
        }

        private void Validate(List<string> names, string errorKey)
        {
            if (names[0] == "~")
            {
                names = names.Skip(1).ToList();
            }
            foreach (var name in names.Where(x => !string.IsNullOrEmpty(x)))
            {
                if (!IsNameValid(name))
                {
                    AddError(errorKey, "The characters: /,|,*,<,>,\\,~\" are not allowed");
                    return;
                }
            }
        }

        protected bool IsNameValid(string name)
        {
            var pattern = new Regex("^([a-zA-Z0-9.][^*/><?\"|:~]*)$");
            if (pattern.IsMatch(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //private bool IsAccessIsAllowed(string virtualPath, string currentPath, string errorKey)
        //{
        //    if (virtualPath[0] != '~')
        //    {
        //        var curDir = System.IO.Directory.GetCurrentDirectory();
        //        if (virtualPath.Contains(curDir))
        //        {
        //            virtualPath = virtualPath.Replace(curDir, "~");
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    var newRootFolder = virtualPath.Trim().Split('\\');
        //    var currentRootFolder = currentPath.Split('\\');
        //    if (newRootFolder.Length >= 2 && newRootFolder[1] == currentRootFolder[1])
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        AddError(errorKey, "Access is denied");
        //        return false;
        //    }
        //}   
    }
}
