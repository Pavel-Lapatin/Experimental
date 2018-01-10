using AutoMapper;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Domain;
using System.IO;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class FileManager : IFileManager
    {
        IMapper _mapper;
        public FileManager(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Copy(string destination, string source)
        {
            File.Copy(source, destination); 

        }

        public FileStructure GetFileInfo(string path)
        {
            return _mapper.Map<FileStructure>(new FileInfo(path));
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
