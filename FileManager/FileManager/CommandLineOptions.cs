using System;
using System.Resources;
using System.Security.Authentication;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.BL.Servicies;
using NetMastery.Lab05.FileManager.BL;
using NetMastery.Lab05.FileManager.BL.Dto;

namespace NetMastery.Lab05.FileManager
{
    public static class CommandLineOptions
    {
        

        public static void WriteUserInfo(AccountDto account, ResourceManager rm)
        {
            
        }

        public static void WriteDirectoryInfo(DirectoryStructureDto directory, FileManagerModel model, ResourceManager rm)
        {
            Console.WriteLine("name: " + directory.Name);
            Console.WriteLine("path " + directory.FullPath);
            Console.WriteLine("creation date: " + directory.CreationDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("modification date " + directory.ModificationDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("size " + directory.DirectorySize + " kB");
            Console.WriteLine("login " + model.LoginName);
            Console.WriteLine();
        }

        private static void RecursiveWriter(DirectoryStructureDto rootDirectory)
        {
            while (rootDirectory.ChildrenDirectories != null || rootDirectory.ChildrenDirectories.Count != 0)
            {
                Console.WriteLine(rootDirectory.Name);
                if (rootDirectory.Files != null)
                {
                    foreach (var file in rootDirectory.Files)
                    {
                        Console.WriteLine(file.Name);
                    }
                }
                foreach (var child in rootDirectory.ChildrenDirectories)
                {
                    RecursiveWriter(child);
                }
            }
            
        }
    }
}
