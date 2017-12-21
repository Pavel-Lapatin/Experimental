using NetMastery.Lab05.FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot
{
    public static class DirectoryInitializer
    {
        public static CommandLineViewModel SetCurrentDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var directoryPath = Path.GetDirectoryName(path);
            var workDirectory = Path.Combine(directoryPath, ConfigurationManager.AppSettings["StoragePath"]);
            if (!Directory.Exists(workDirectory))
            {
                Directory.CreateDirectory(workDirectory);
            }
            Directory.SetCurrentDirectory(workDirectory);
            return new CommandLineViewModel
            {
                CurrentPath = "~\\";
            }
        }
    }

}
