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
            foreach (var item in strs.Where(c => !string.IsNullOrEmpty(c.Trim())))
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
    }
}
