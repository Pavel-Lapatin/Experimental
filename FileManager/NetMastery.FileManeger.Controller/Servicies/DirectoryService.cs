using NetMastery.Lab05.FileManager.BL;
using System;
using System.Text;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.BL.Dto;

namespace NetMastery.Lab05.FileManager.BL.Servicies
{
    public class DirectoryService
    {
        public string FullPath { get; set; }
        public DirectoryInfoDto CurrentDirectory { get; set; }

        private readonly IDirectoryRepository _storageRepository;

        public DirectoryService(IDirectoryRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public void CalculateFullSize(string login)
        {
            
        }

        //public void MoveToTheDirrectory(string path)
        //{
        //    var directories = PathParser(path);
        //    if (directories.Length == 0) throw  new NullReferenceException();
        //    StringBuilder newPath = new StringBuilder();
        //    foreach (var directory in directories)
        //    {
        //        switch (directory)
        //        {
        //            case ".":
        //            {
        //                newPath.Clear();
        //                newPath.Append(FullPath);
        //                break;    
        //            }     
        //            case "..":
        //            {
        //                newPath.Clear();
        //                    newPath.Append(FullPath.Substring(0, FullPath.LastIndexOf('\\')) + path);
        //                break;    
        //            }
        //            default:
        //            {
        //                newPath.Append('\\');
        //                newPath.Append(directory);
        //                break;
        //            }
        //        }
        //        FullPath = newPath.ToString();
        //    }
        //}

        //private static string[] PathParser(string path)
        //{
        //    var separators = new [] { '\\' };
        //    return path.Split(separators);
        //}
    }
}
