﻿using NetMastery.Lab05.FileManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class DirectoryRepository : Repository<DirectoryStructure>, IDirectoryRepository
    {
        public DirectoryRepository(FileManagerDbContext context) : base(context)
        {
            Find(x => x.FullPath == "~\\adminRoot").FirstOrDefault();
        }

        public DirectoryStructure FindByPath(string path)
        {
            return Find(x=>x.FullPath == path).FirstOrDefault();
        }
        public DirectoryStructure FindByPathEagerLoadingParentDirectory(string path)
        {
            return EagerFind(x => x.FullPath == path, y=>y.ParentDirectory).FirstOrDefault();
        }
        public DirectoryStructure FindByPathEagerLoadingFiles(string path)
        {
            return EagerFind(x => x.FullPath == path, y => y.Files).FirstOrDefault();
        }
        public DirectoryStructure FindByPathEagerLoadingChildrenDirectories(string path)
        {
            return EagerFind(x => x.FullPath == path, y => y.ChildrenDirectories).FirstOrDefault();
        }
        public DirectoryStructure FindByPathEagerLoadingFull(string path)
        {
            return EagerFind(x => x.FullPath == path, y => y.ChildrenDirectories, t=>t.ChildrenDirectories).FirstOrDefault();
        }
        public IEnumerable<DirectoryStructure> FindDirectoriesWhichContainPath(string path)
        {
            return Find(x => x.FullPath.Contains(path));
        }

    }
}