using NetMastery.Lab05.FileManager.DAL.Exceptions;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class DirectoryRepository : IDirectoryRepository
    {
        public void AddFolder(string path, string name)
        {
            try
            {
                Directory.CreateDirectory((path + '\\' + name).Replace("~", Directory.GetCurrentDirectory()));
            }
            catch (Exception e)
            {
                Log.Logger.Debug(e.Message);
                throw new FSRepositoryArgumentException("Directory add operation failed");
            }
           
        }

        public string GetCurrentPath()
        {
            try
            {
                return Directory.GetCurrentDirectory();
            }
            catch (Exception e)
            {
                Log.Logger.Debug(e.Message);
                throw new FSRepositoryArgumentException("Directory move operation failed");
            }
        }

        public bool IsExist(string path)
        {
            if(Directory.Exists(path))
            {
                return true;
            }   
            return false;
        }

        public void Move(string destination, string source)
        {
            var fullPathSource = source.Replace("~", Directory.GetCurrentDirectory());
            var folderName = source.Substring(source.LastIndexOf('\\')+1);
            var fullPathDestination = destination.Replace("~", Directory.GetCurrentDirectory())+'\\'+folderName;
            if (Directory.Exists(fullPathDestination))
            {
                throw new FSRepositoryArgumentException("There is already folder with the exact name as source folder");
            }
            try
            {
                Directory.Move(fullPathSource, fullPathDestination);
            }
            catch (Exception e)
            {
                Log.Logger.Debug(e.Message);
                throw new FSRepositoryArgumentException("Directory move operation failed");
            }
        }

        public void MoveRollback(string destination, string source)
        {
            Move(source, destination);
        }

        public void Remove(string destination)
        {
            try
            {
                Directory.Delete(destination.Replace("~", Directory.GetCurrentDirectory()), true);
            }
            catch (Exception e)
            {
                Log.Logger.Debug(e.Message);
                throw new FSRepositoryArgumentException("Directory Remove operation failed");
            }
        }
    }
}
