using NetMastery.Lab05.FileManager.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Forms
{
    public class OnePathForm : Form
    {
        public string destinationPath;

        public string Test { get; set; }

        public string DestinationPath
        {
            get { return destinationPath; }
            set
            {
                RemoveError(nameof(DestinationPath));
                if(string.IsNullOrEmpty(value))
                {
                    AddError(nameof(DestinationPath), "Destination path shouldn't be null or empty");
                }
                else
                {
                    value = CreatePath(value, _currentPath, "Destination path");
                }
                destinationPath = value;
            }
        }

        public OnePathForm(string currentPath, string destinationPath) : base(currentPath)
        {
            DestinationPath = destinationPath;
        }

        public string CreatePath(string newPath, string currentPath, string errorKey)
        {
            var path = new StringBuilder();
            var disk = newPath.Trim().Split(':');
            if (disk.Length > 2)
            {
                AddError(errorKey, "There is could be only one \":\" in the path");
                return null;
            }
            else if (disk.Length == 2)
            {
                path.Append(disk[0]);
                path.Append(":");
                newPath = disk[1];
            }
            var pathParts = newPath.Trim('\\').Split('\\');

            if (pathParts[0] == "~")
            {
                path.Append("~");
                pathParts = pathParts.Skip(1).ToArray();
            }
            foreach (var partName in pathParts.Where(x => !string.IsNullOrEmpty(x)))
            {
                switch (partName)
                {
                    case "..":
                        if (path.Length == 0) path.Append(currentPath);
                        var index = path.ToString().LastIndexOf("\\");
                        path = path.Remove(index, path.Length - index);
                        break;
                    case ".":
                        if (path.Length == 0) path.Append(currentPath);
                        break;
                    default:
                        if(CheckValidCharactersInName(partName))
                        {
                            AddError(errorKey, "The characters: /,|,:,*,<,>,\\,~\" are not allowed");
                            return null;
                        }
                        if (!string.IsNullOrEmpty(partName))
                        {
                            path.Append("\\");
                            path.Append(partName);
                        }
                        break;
                }
            }
            var virtualPath = path.ToString();
            if (virtualPath[0] != '~')
            {
                var curDir = Directory.GetCurrentDirectory();
                if (virtualPath.Contains(curDir))
                {
                    virtualPath = virtualPath.Replace(curDir, "~");
                }
                else
                {
                    return virtualPath;
                }
            }
            var newRootFolder = virtualPath.Trim().Split('\\');
            var currentRootFolder = currentPath.Split('\\');
            if (newRootFolder.Length >= 2 && newRootFolder[1] == currentRootFolder[1])
            {
                return path.ToString();
            }
            else
            {
                AddError(errorKey, "Access is denied");
                return null;
            }

        }

        public bool CheckValidCharactersInName(string name)
        {
            if (name.Any(x => x == '/'
                        || x == ':'
                        || x == '*'
                        || x == '?'
                        || x == '<'
                        || x == '>'
                        || x == '\"'
                        || x == '|'
                        || x == '~'))
            {
                return false;
            }
            {
                return true;
            }
        }
    }
}
