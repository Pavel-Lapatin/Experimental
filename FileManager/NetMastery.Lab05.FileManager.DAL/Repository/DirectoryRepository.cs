//using System.Collections.Generic;
//using NetMastery.Lab05.FileManager.DAL.Interfacies;
//using System.Linq;
//using System.Data.Entity;
//using NetMastery.Lab05.FileManager.Domain;

//namespace NetMastery.Lab05.FileManager.DAL.Repository
//{
//    public class DirectoryRepository : Repository<DirectoryStructure>
//    {
//        public DirectoryRepository(FileManagerDbContext context) : base(context)
//        {

//        }

//        DirectoryStructure GetAllEagerLoading(string fullPath)
//        {
//            return ((FileManagerDbContext)Context).Directories.Where(x=> x.FullPath == fullPath).Include(x=> x.ChildrenDirectories).FirstOrDefault();

//        }


//    }
//}
