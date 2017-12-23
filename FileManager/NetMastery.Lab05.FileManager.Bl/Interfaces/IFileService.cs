using NetMastery.Lab05.FileManager.Dto;
using System.Collections.Generic;

namespace NetMastery.FileManager.Bl.Interfaces
{
    public interface IFileService
    {
        void Upload(string path, string name);
        void Download(string pathFrom, string pathTo);
        void Move(string pathFrom, string pathTo);
        void Remove(string path);
        IEnumerable<string> Search(string pattern, string path);

        FileStructureDto GetFileByPath(string path);
    }
}
