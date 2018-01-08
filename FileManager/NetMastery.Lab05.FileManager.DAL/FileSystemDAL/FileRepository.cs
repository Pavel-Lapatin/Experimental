using AutoMapper;
using NetMastery.Lab05.FileManager.DAL.Exceptions;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Domain;
using Serilog;
using System;
using System.IO;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class FSFileManager : IFSFileManager
    {
        IMapper _mapper;
        public FSFileManager(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Copy(string destination, string source)
        {
            try
            {
                File.Copy(source, destination);
            }
            catch (Exception e)
            {
                Log.Logger.Debug(e.Message);
                throw new FSDirectoryManagerArgumentException(e.Message);
            }
        }

        public FileStructure GetFileInfo(string path)
        {
            try
            {
                return _mapper.Map<FileStructure>(new FileInfo(path));
            }
            catch (Exception e)
            {
                Log.Logger.Debug(e.Message);
                throw new FSDirectoryManagerArgumentException(e.Message);
            }

        }

        public bool IsExist(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            };
        }

        public void Move(string destination, string source)
        {
            try
            {
                File.Move(source, destination);
            }
            catch (Exception e)
            {
                Log.Logger.Debug(e.Message);
                throw new FSDirectoryManagerArgumentException(e.Message);
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
                File.Delete(destination);
            }
            catch (Exception e)
            {
                Log.Logger.Debug(e.Message);
                throw new FSDirectoryManagerArgumentException(e.Message);
            }
        }
    }
}
