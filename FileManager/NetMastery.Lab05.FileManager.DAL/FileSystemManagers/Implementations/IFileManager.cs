using NetMastery.Lab05.FileManager.Domain;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IFileManager : IFileSystemManager
    {
        FileStructure GetFileInfo(string path);
        void Copy(string destination, string source);
    }
}
