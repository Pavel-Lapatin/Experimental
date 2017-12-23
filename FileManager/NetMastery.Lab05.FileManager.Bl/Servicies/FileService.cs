using AutoMapper;
using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Dto;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using NetMastery.Lab05.FileManager.Domain;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class FileService : IFileService
    {
        IUnitOfWork _unitOfWork;
        #region Constructors

        public FileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region FileServiceAPI
  
        public void Upload(string pathToFile, string pathToStorage)
        {
            if (File.Exists(pathToFile))
            {

                //var fileInfo = Directory.
                //var directory = _unitOfWork
                //    .Repository<DirectoryStructure>()
                //    .Find(x => x.FullPath == pathToStorage).FirstOrDefault();
                //if (directory == null) throw new NullReferenceException("Directory doesnt exist");
                //directory.Files.
                    throw new NotImplementedException();
            }
        }

        public void Download(string pathFrom, string pathTo)
        {
            throw new NotImplementedException();
        }

        public void Move(string pathFrom, string pathTo)
        {
            throw new NotImplementedException();
        }

        public void Remove(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> Search(string pattern, string path)
        {
            throw new NotImplementedException();
        }

        public FileStructureDto GetFileByPath(string path)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
