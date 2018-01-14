using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI;
using Serilog;

namespace NetMastery.Lab05.FileManager.Helpers
{
    public static class UIHelpers
    {
        public static string[] ParseArguemts(string arguments)
        {
            var pattern = new Regex("\"");
            if (pattern.Matches(arguments).Count %2 != 0)
            {
                throw new UIException("Parametr string has unqouted arguments");
            }
            var strs = arguments.Split('\"');
            List<string> args = new List<string>();
            for (int i = 0; i < strs.Length; i++)
            {
                var str = strs[i].Trim();
                if (i % 2 == 0)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        args.AddRange(str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                    }
                }
                else
                {
                    args.Add(str);
                }
            }
            return args.ToArray();
        }

        public static string CreatePath(this string str, string currentPath)
        {
            var pattern = new Regex(@"^\.{1,2}\\");
            if(pattern.IsMatch(str))
            {
                if(currentPath == null)
                {
                    Log.Logger.Debug("UI-->CreatPath-->Current path is null");
                    throw new ArgumentNullException();
                }
                str = currentPath.Trim('\\') +'\\'+ str;
            }
            var names = str.TrimEnd('\\').Split('\\');
            var newPath = new List<string>(names.Length);
            foreach (var name in names)
            {
                switch (name)
                {
                    case "..":
                        if(newPath.Count > 1)
                        {
                            newPath.Remove(newPath.Last());
                        }
                        break;
                    case ".":
                        break;
                    default:
                        newPath.Add(name.Trim());
                        break;
                }
            }
            return string.Join("\\", newPath.ToArray());
        }

    }
}
