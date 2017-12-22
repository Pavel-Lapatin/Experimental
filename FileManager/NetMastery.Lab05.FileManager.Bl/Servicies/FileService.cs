//using AutoMapper;
//using NetMastery.Lab05.FileManager.BL.Dto;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NetMastery.FileManeger.Bl.Servicies
//{
//    public class FileService
//    {

//        #region FileServiceAPI

//        public FileStructureDto GetFileInfo(string path, int rootDirectoryId)
//        {
//            var directoryPath = path.Substring(0, path.LastIndexOf("\\"));
//            var fileName = path.Substring(path.LastIndexOf("\\") + 1, path.Length - 1);
//            var currentDirectory = GetDirectoryByPath(directoryPath, rootDirectoryId);
//            if (currentDirectory != null)
//            {
//                var file = currentDirectory.Files.FirstOrDefault(x => x.Name == fileName);
//                if (file == null) throw new ArgumentException("Such file doesn't exist");
//                return Mapper.Instance.Map<FileStructureDto>(file);
//            }
//            else throw new ArgumentException(rm.GetString("DirectoryNotExistArgumenException"));
//        }

//        public void UploadFile(string uploadFilePath, string newFilePath, int rootDirectoryId)
//        {
//            var newDirectory = GetDirectoryByPath(newFilePath, rootDirectoryId);
//            if (newDirectory != null)
//            {
//                var oldFile = new FileInfo(uploadFilePath);
//                var fileInfo = new FileStructureDto
//                {
//                    Name = oldFile.Name,
//                    CreationTime = oldFile.CreationTime,
//                    ModificationDate = oldFile.LastWriteTime,
//                    DownloadsNumber = 0,
//                    FileSize = oldFile.Length,
//                    Extension = oldFile.Extension,
//                    Directory = newDirectory
//                };
//                File.Copy(uploadFilePath, Path.Combine(newFilePath, fileInfo.Name));

//                unitOfWork.Files.Add(Mapper.Map<FileStructure>(fileInfo));
//                unitOfWork.Complete();
//            }
//            else
//                throw new ArgumentException(rm.GetString("DirectoryNotExistArgumenException"));
//        }

//        public void DownLoadFile(string fromPath, string toPath, int rootDirectoryId)
//        {
//            if (!Directory.Exists(toPath)) throw new ArgumentException("Destination directory doesn't Exists");
//            var directoryPath = fromPath.Substring(0, fromPath.LastIndexOf("\\"));
//            var fileName = fromPath.Substring(fromPath.LastIndexOf("\\") + 1, fromPath.Length - 1);
//            var directory = GetDirectoryByPath(directoryPath, rootDirectoryId);
//            if (directory != null)
//            {

//            }
//        }
//        #endregion
//    }
//}
