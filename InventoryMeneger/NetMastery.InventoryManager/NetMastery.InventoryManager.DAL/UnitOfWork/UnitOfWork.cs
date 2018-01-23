using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using NetMastery.InventoryManager.DAL.Repository;
using NetMastery.InventoryManager.Domain;
using System.Data.Entity;

namespace NetMastery.InventoryManager.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private bool disposed = false;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        public IRepository<Inventory> InventoryRepository => 
            InventoryRepository ?? new Repository<Inventory>(_context);

        public IRepository<InventoryType> InventoryTypeRepository =>
            InventoryTypeRepository ?? new Repository<InventoryType>(_context);

        public IRepository<Card> CardRepository =>
            CardRepository ?? new Repository<Card>(_context);

        public IRepository<Organization> OrganizationRepository =>
            OrganizationRepository ?? new Repository<Organization>(_context);

        public IRepository<PersonInCharge> PersonInChargeRepository =>
            PersonInChargeRepository ?? new Repository<PersonInCharge>(_context);

        public IRepository<Storage> StorageRepository =>
            StorageRepository ?? new Repository<Storage>(_context);

        public IRepository<Manufacture> ManufactureRepository =>
            ManufactureRepository ?? new Repository<Manufacture>(_context);

        public IRepository<Subdivision> SubdivisionRepository =>
            SubdivisionRepository ?? new Repository<Subdivision>(_context);

        public IRepository<IdentityRole> RoleRepository =>
            RoleRepository ?? new Repository<IdentityRole>(_context);

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
