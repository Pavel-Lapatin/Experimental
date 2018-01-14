using AutoMapper;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Domain;
using System.IO;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class FileManager : IFileManager
    {

        public void Copy(string destination, string source)
        {
            File.Copy(source, destination); 

        }

        public FileStructure GetFileInfo(string path)
        {
            var result = new FileInfo(path);
            return new FileStructure
            {
                 CreationTime = result.CreationTime,
                 Extension = result.Extension,
                 FileSize = result.Length,
                 Name = result.Name,
                 DownloadsNumber = 0,
                 ModificationDate = result.LastWriteTime,
                 FileId = 0,
                 Directory = null
            };
        }

        public bool IsExist(string path)
        {
            return File.Exists(path);   
        }

        public void Move(string destination, string source)
        {
            File.Move(source, destination);
        }
        public void Remove(string destination)
        {
            File.Delete(destination);
        }
    }
}
