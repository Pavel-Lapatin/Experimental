using System;

namespace NetMastery.Lab05.FileManager.DAL.Interfacies
{
    public abstract class AbstructUnitOfWork : IDisposable
    {
        private readonly FileManagerDbContext _context;


        public AbstructUnitOfWork(FileManagerDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}

