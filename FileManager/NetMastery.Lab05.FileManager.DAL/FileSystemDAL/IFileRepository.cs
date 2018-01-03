using System.IO;


namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IFSFileManager : IFSManager
    {
        FileInfo GetFileInfo(string path);
        void Copy(string destination, string source);
    }
}
