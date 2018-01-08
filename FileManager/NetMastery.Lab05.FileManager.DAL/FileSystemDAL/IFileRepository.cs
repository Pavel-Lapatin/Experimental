using NetMastery.Lab05.FileManager.Domain;
using System.IO;


namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IFSFileManager : IFSManager
    {
        FileStructure GetFileInfo(string path);
        void Copy(string destination, string source);
    }
}
