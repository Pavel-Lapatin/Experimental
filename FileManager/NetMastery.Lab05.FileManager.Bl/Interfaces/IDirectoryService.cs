using NetMastery.Lab05.FileManager.Dto;
using System.Collections.Generic;


namespace NetMastery.FileManeger.Bl.Interfaces
{
    public interface IDirectoryService
    {
        void Add(string path, string name);
        void Move(string pathFrom, string pathTo);
        void Remove(string path);
        IEnumerable<string> Search(string pattern, string path);
        string ChangeWorkDirectory(string path);

        DirectoryStructureDto GetInfoByPath(string path);

    }
}