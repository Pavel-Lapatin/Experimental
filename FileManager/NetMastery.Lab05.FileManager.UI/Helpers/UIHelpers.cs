using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace NetMastery.Lab05.FileManager.Helpers
{
    public static class UIHelpers
    {
        public static string[] ParseArguemts(string arguments)
        {
            var strs = arguments.Trim('\"').Split('\"');
            List<string> args = new List<string>();
            int i = 1;
            foreach (var item in strs)
            {
                if (i % 2 != 0)
                {
                    args.AddRange(item.Trim().Split(' '));
                }
                else
                {
                    args.Add(item);
                }
                i++;
            }
            return args.ToArray();
        }

        public static string CreatePath(string newPath, string currentPath)
        {
            var path = new StringBuilder();
            var disk = newPath.Trim().Split(':');
            if (disk.Length > 2)
            {
                throw new ArgumentException("There is could be only one \":\" in the path");
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
                        if (partName.Any(x => x == '/'
                        || x == ':'
                        || x == '*'
                        || x == '?'
                        || x == '<'
                        || x == '>'
                        || x == '\"'
                        || x == '|'
                        || x == '~'))
                        {
                            throw new ArgumentException("The characters: /,|,:,*,<,>,\\,~\" are not allowed");
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
                throw new UnauthorizedAccessException("Access is denied");
            }

        }
    }
}
