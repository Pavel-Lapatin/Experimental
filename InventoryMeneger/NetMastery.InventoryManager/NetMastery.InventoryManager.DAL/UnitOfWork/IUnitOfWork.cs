using Microsoft.AspNet.Identity.EntityFramework;
using NetMastery.InventoryManager.DAL.IdentityManagers;
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
        InventoryRoleManager RoleManager { get; }
        InventoryUserManager UserManager { get;  }
        InventorySignInManager SignInManager { get; }
        IRepository<Inventory> InventoryRepository { get; set; }
        IRepository<InventoryType> InventoryTypeRepository { get; set; }
        IRepository<Card> CardRepository { get; set; }
        IRepository<Organization> OrganizationRepository { get; set; }
        IRepository<PersonInCharge> PersonInChargeRepository { get; set; }
        IRepository<Storage> StorageRepository { get; set; }
        IRepository<Manufacture> ManufactureRepository { get; set; }
        IRepository<Subdivision> SubdivisionRepository { get; set; }
        IRepository<Account> AccountRepository { get; set; }
        void Save();
    }
}
