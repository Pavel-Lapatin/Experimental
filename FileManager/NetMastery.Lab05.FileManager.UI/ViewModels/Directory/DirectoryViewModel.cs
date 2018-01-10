using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NetMastery.Lab05.FileManager.UI.ViewModels.Directory
{
    public class DirectoryViewModel : ViewModel
    {
        private string path;
        public string Path
        {
            get { return path; }
            set
            {
                path = CreatePath(value, _currentPath, nameof(Path));
            }
        }

        protected string _currentPath;

        public DirectoryViewModel(string currentPath, string path)
        {
            _currentPath = currentPath;
            Path = path;
        }
        protected string CreatePath(string newPath, string currentPath, string errorKey)
        {
            if (!IsPathCorrect(newPath, errorKey))
            {
                return null;
            }

            var virtualPath = CreateVirtualPath(newPath, currentPath, errorKey);
            if (virtualPath == null) return null;
            if (IsAccessIsAllowed(virtualPath, currentPath, errorKey))
            {
                return virtualPath;
            }
            return null;

        }

        private string CreateVirtualPath(string path, string currentPath, string errorKey)
        {
            var virtualPath = new StringBuilder();
            path = GetDisk(path, virtualPath);
            var pathParts = path.Trim('\\').Split('\\');
            if (pathParts[0] == "~")
            {
                virtualPath.Append("~");
                pathParts = pathParts.Skip(1).ToArray();
            }
            foreach (var partName in pathParts.Where(x => !string.IsNullOrEmpty(x)))
            {
                switch (partName)
                {
                    case "..":
                        if (virtualPath.Length == 0)
                        {
                            virtualPath.Append(currentPath);
                        }
                        var index = virtualPath.ToString().LastIndexOf("\\");
                        if (index == -1) continue;
                        virtualPath = virtualPath.Remove(index, virtualPath.Length - index);
                        break;
                    case ".":
                        if (virtualPath.Length == 0)
                        {
                            virtualPath.Append(currentPath);
                        }
                        break;
                    default:
                        if (!IsFolderOrFileNameIsValid(partName))
                        {
                            AddError(errorKey, "The characters: /,|,*,<,>,\\,~\" are not allowed");
                            return null;
                        }
                        virtualPath.Append("\\");
                        virtualPath.Append(partName);
                        break;
                }
            }
            return virtualPath.ToString();
        }

        private bool IsAccessIsAllowed(string virtualPath, string currentPath, string errorKey)
        {
            if (virtualPath[0] != '~')
            {
                var curDir = System.IO.Directory.GetCurrentDirectory();
                if (virtualPath.Contains(curDir))
                {
                    virtualPath = virtualPath.Replace(curDir, "~");
                }
                else
                {
                    return true;
                }
            }
            var newRootFolder = virtualPath.Trim().Split('\\');
            var currentRootFolder = currentPath.Split('\\');
            if (newRootFolder.Length >= 2 && newRootFolder[1] == currentRootFolder[1])
            {
                return true;
            }
            else
            {
                AddError(errorKey, "Access is denied");
                return false;
            }
        }

        private bool IsPathCorrect(string path, string errorKey)
        {
            if (IsPathNull(path, errorKey))
            {
                return false;
            }

            if (new Regex(":").Match(path).Length > 2)
            {
                AddError(errorKey, "The path is not correct");
                return false;
            }
            return true;
        }

        private string GetDisk(string path, StringBuilder virtualPath)
        {
            var parts = path.Trim().Split(':');
            if (parts.Length == 2)
            {
                virtualPath.Append(parts[0]);
                virtualPath.Append(':');
                return parts[1];
            }
            return parts[0];
        }

        private bool IsPathNull(string path, string errorKey)
        {
            if (string.IsNullOrEmpty(path))
            {
                AddError(errorKey, "The path value is missing");
                return true;
            }
            return false;
        }

        protected bool IsFolderOrFileNameIsValid(string name)
        {
            var pattern = new Regex("^([a-zA-Z0-9][^*/><?\"|:~]*)$");
            if (pattern.IsMatch(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
