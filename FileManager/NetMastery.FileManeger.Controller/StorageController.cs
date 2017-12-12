using NetMastery.Lab05.FileManager.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.DAL.Interfacies;

namespace NetMastery.FileManeger.Controller
{
    public class StorageController
    {
        public string FullPath { get; set; }
        public StorageDto CurrentDirectory { get; set; }

        private readonly IStorageRepository _storageRepository;

        public StorageController(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public MoveToTheDirrectory(string path)
        {
            var directories = PathParser(path);
            if (directories.Length == 0) throw  new NullReferenceException();
            StringBuilder newPath = new StringBuilder();
            foreach (var directory in directories)
            {
                switch (directory)
                {
                    case ".":
                    {
                        newPath.Clear();
                        newPath.Append(FullPath);
                        break;    
                    }     
                    case "..":
                    {
                        newPath.Clear();
                            newPath.Append(FullPath.Substring(0, FullPath.LastIndexOf('\\')) + path);
                        break;    
                    }
                    default:
                    {
                        newPath.Append('\\');
                        newPath.Append(directory);
                        break;
                    }
                }

                FullPath = newPath.ToString();
            }
        }

        private static string[] PathParser(string path)
        {
            var separators = new char[] { '\\' };
            return path.Split(separators);
        }
    }
}
