using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Repository
{
    public class DbFileRepository : DbRepository<FileStructure>, IDbFileRepository
    {
        public DbFileRepository(FileManagerDbContext context) : base(context)
        {
        }
    }
}
