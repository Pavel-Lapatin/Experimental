using NetMastery.Lab05.FileManager.DAL.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Bl.Helpers
{
    public static class PathExtension
    {
        public static string TransformToVirtualPath(this string str, string currentPath)
        {
            return str.Replace(currentPath, "~");
        }
        public static string TransformToFullPath(this string str, string currentPath)
        {
            return str.Replace("~", currentPath);
        }
        public static string GetFileName(this string str)
        {
            return str.Substring(str.LastIndexOf('\\') + 1);
        }
        public static string GetDirectoryPath(this string str)
        {
            return str.Substring(0, str.LastIndexOf('\\'));
        }
        public static string GetRootDirectoryName(this string str)
        {
            return str.Trim(new[] { '~','\\'}).Substring(0, str.LastIndexOf('\\'));
        }

        public static bool HasAccessToVirtualStorage(this String str, string rootDirectory)
        {
            str = str.Replace("\\", "");
            rootDirectory = rootDirectory.Replace("\\", "");
            return new Regex('^' + rootDirectory).IsMatch(str);
        }

        public static bool IsInTheVirtualStorage(this string str, string currentPath)
        {
            return str.TransformToFullPath(currentPath).Contains(currentPath);
        }

        //public static bool IsPathInTheStorage(this String str, string rootDirectory)
        //{
        //    return str.Contains(Directory.GetCurrentDirectory());

        //}
    }
}
