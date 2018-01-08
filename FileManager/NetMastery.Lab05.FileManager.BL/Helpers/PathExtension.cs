using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Bl.Helpers
{
    public static class PathExtension
    {
        public static string TransformToVirtualPath(this String str, string currentPath)
        {
            return str.Replace(currentPath, "~");
        }
        public static string TransformToFulllPatht(this String str, string currentPath)
        {
            return str.Replace("~", currentPath);
        }
        public static string GetFileName(this String str)
        {
            return str.Substring(str.LastIndexOf('\\') + 1);
        }
        public static string GetDirectoryPath(this String str)
        {
            return str.Substring(0, str.LastIndexOf('\\'));
        }

    }
}
