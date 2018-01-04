using NetMastery.Lab05.FileManager.DAL.Exceptions;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using Serilog;
using System;
using System.IO;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class FSDirectoryManager : IFSDirectoryManager
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
                throw new fsDirectoryManagerArgumentException("Directory add operation failed");
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
                throw new fsDirectoryManagerArgumentException("Get current path operation failed");
            }
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
            if (Directory.Exists(fullPathDestination))
            {
                throw new fsDirectoryManagerArgumentException("There is already folder with the exact name as source folder");
            }
            try
            {
                Directory.Move(fullPathSource, fullPathDestination);
            }
            catch (Exception e)
            {
                Log.Logger.Debug(e.Message);
                throw new fsDirectoryManagerArgumentException("Directory move operation failed");
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
                throw new fsDirectoryManagerArgumentException("Directory Remove operation failed");
            }
        }
    }
}
