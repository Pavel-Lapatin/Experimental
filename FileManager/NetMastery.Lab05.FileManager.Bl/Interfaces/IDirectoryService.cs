using NetMastery.Lab05.FileManager.Dto;
using System.Collections.Generic;


namespace NetMastery.FileManeger.Bl.Interfaces
{
    public interface IDirectoryService
    {
        void Add(string path, string name, string currentPath);
        void Move(string pathFrom, string pathTo);
        void Remove(string path, string currentPath);
        IEnumerable<string> Search(string currentPAth, string pattern);
        string ChangeWorkDirectory(string path, string currentPath);
        DirectoryStructureDto GetInfoByCurrentPath(string path);

    }
}
