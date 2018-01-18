using NetMastery.Lab05.FileManager.Dto;
using System;
using System.Collections.Generic;

namespace NetMastery.Lab05.FileManager.Bl.Interfaces
{
    public interface IFileService : IDisposable
    {
        void Upload(string pathToFile, string pathToStorage);
        void Download(string pathFromStorage, string pathToFile);
        void Move(string pathFromStorage, string pathToStorage);
        void Remove(string path);
        FileStructureDto GetFileByPath(string path);
    }
}
