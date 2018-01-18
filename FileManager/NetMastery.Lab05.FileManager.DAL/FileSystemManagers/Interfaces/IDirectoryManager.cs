﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public interface IDirectoryManager : IFileSystemManager
    {
        void AddFolder(string path, string name);
        string GetCurrentPath();
    }
}