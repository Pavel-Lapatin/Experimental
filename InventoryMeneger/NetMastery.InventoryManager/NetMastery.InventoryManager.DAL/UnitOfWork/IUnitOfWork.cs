using Microsoft.AspNet.Identity.EntityFramework;
using NetMastery.InventoryManager.DAL.Repository;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Inventory> InventoryRepository { get; }
        IRepository<InventoryType> InventoryTypeRepository { get; }
        IRepository<Card> CardRepository { get; }
        IRepository<Organization> OrganizationRepository { get; }
        IRepository<PersonInCharge> PersonInChargeRepository { get; }
        IRepository<Storage> StorageRepository { get; }
        IRepository<Manufacture> ManufactureRepository { get; }
        IRepository<Subdivision> SubdivisionRepository { get; }
        IRepository<IdentityRole> RoleRepository { get; }
        void Save();
    }
}
