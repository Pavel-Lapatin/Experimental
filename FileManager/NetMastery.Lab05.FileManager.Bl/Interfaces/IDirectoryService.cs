﻿using NetMastery.Lab05.FileManager.Dto;
using System.Collections.Generic;


namespace NetMastery.Lab05.FileManager.Bl.Interfaces
{
    public interface IDirectoryService
    {
        void Add(string path, string name);
        void Move(string pathFrom, string pathTo);
        void Remove(string path);
        IEnumerable<string> Search(string path, string pattern);
        IEnumerable<string> ShowContent(string path);
        DirectoryStructureDto GetInfoByPath(string path);
    }
}