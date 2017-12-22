using NetMastery.Lab05.FileManager.Dto;

namespace NetMastery.FileManeger.Bl.Interfaces
{
    public interface IFileService
    {
        void Upload(string path, string name);
        void Download(string pathFrom, string pathTo);
        void Move(string pathFrom, string pathTo);
        void Remove(string path, string currentPath);
        FileStructureDto GetInfoByCurrentPath(string path);
    }
}
