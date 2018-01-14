using NetMastery.Lab05.FileManager.DAL.Interfacies;
using System.IO;

namespace NetMastery.Lab05.FileManager.DAL.FileSystemManagers
{
    public class DirectoryManager : IDirectoryManager
    {
        public void AddFolder(string path, string name)
        {
            Directory.CreateDirectory((path + '\\' + name).Replace("~", Directory.GetCurrentDirectory()));
        }

        public string GetCurrentPath()
        {
            return Directory.GetCurrentDirectory();
        }

        public bool IsExist(string path)
        {
            return Directory.Exists(path);   
        }

        public void Move(string destination, string source)
        {
            var fullPathSource = source.Replace("~", Directory.GetCurrentDirectory());
            var folderName = source.Substring(source.LastIndexOf('\\')+1);
            var fullPathDestination = destination.Replace("~", Directory.GetCurrentDirectory())+'\\'+folderName;
            Directory.Move(fullPathSource, fullPathDestination);
        }

        public void Remove(string destination)
        {
            Directory.Delete(destination.Replace("~", Directory.GetCurrentDirectory()), true);
        }
    }
}
