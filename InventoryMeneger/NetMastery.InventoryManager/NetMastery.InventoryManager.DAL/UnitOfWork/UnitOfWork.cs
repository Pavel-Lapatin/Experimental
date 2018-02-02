using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using NetMastery.InventoryManager.DAL.Repository;
using NetMastery.InventoryManager.Domain;
using System.Data.Entity;
using NetMastery.InventoryManager.DAL.IdentityManagers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace NetMastery.InventoryManager.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _inventoryContext;
        private bool disposed = false;
        private readonly IOwinContext _owinContext;

        private IRepository<Inventory> _inventoryRepository;
        private IRepository<InventoryType> _inventoryTypeRepository;
        private IRepository<Card> _cardRepository;
        private IRepository<Organization> _organizationRepository;
        private IRepository<PersonInCharge> _personInChargeRepository;
        private IRepository<Storage> _storageRepository;
        private IRepository<Manufacture> _manufactureRepository;
        private IRepository<Subdivision> _subdivisionRepository;
        private IRepository<Account> _accountRepository;

        public UnitOfWork(IOwinContext owinContext)
        {
            _inventoryContext = new InventoryDbContext();
            _owinContext = owinContext;
        }
 
        public IRepository<Inventory> InventoryRepository {
            get
            {
                if (_inventoryRepository == null)
                {
                    _inventoryRepository = new Repository<Inventory>(_inventoryContext);
                }
                return _inventoryRepository;
            }
            set { _inventoryRepository = value; }
        }
        public IRepository<InventoryType> InventoryTypeRepository
        {
            get
            {
                if (_inventoryTypeRepository == null)
                {
                    _inventoryTypeRepository = new Repository<InventoryType>(_inventoryContext);
                }
                return _inventoryTypeRepository;
            }
            set { _inventoryTypeRepository = value; }
        }
        public IRepository<Card> CardRepository
        {
            get
            {
                if (_cardRepository == null)
                {
                    _cardRepository = new Repository<Card>(_inventoryContext);
                }
                return _cardRepository;
            }
           set { _cardRepository = value; }
        }
        public IRepository<Organization> OrganizationRepository
        {
            get
            {
                if (_organizationRepository == null)
                {
                    _organizationRepository = new Repository<Organization>(_inventoryContext);
                }
                return _organizationRepository;
            }
            set { _organizationRepository = value; }
        }
        
        public IRepository<PersonInCharge> PersonInChargeRepository
        {
            get
            {
                if (_personInChargeRepository == null)
                {
                    _personInChargeRepository = new Repository<PersonInCharge>(_inventoryContext);
                }
                return _personInChargeRepository;
            }
            set { _personInChargeRepository = value; }
        }
        public IRepository<Storage> StorageRepository
        {
            get
            {
                if (_storageRepository == null)
                {
                    _storageRepository = new Repository<Storage>(_inventoryContext);
                }
                return _storageRepository;
            }
            set { _storageRepository = value; }
        }
        public IRepository<Manufacture> ManufactureRepository
        {
            get
            {
                if (_manufactureRepository == null)
                {
                    _manufactureRepository = new Repository<Manufacture>(_inventoryContext);
                }
                return _manufactureRepository;
            }
            set { _manufactureRepository = value; }
        }
        public IRepository<Subdivision> SubdivisionRepository
        {
            get
            {
                if (_subdivisionRepository == null)
                {
                    _subdivisionRepository = new Repository<Subdivision>(_inventoryContext);
                }
                return _subdivisionRepository;
            }
            set { _subdivisionRepository = value; }
        }
        public IRepository<Account> AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new Repository<Account>(_inventoryContext);
                }
                return _accountRepository;
            }
            set { _accountRepository = value; }
        }
        public InventoryUserManager UserManager => _owinContext.GetUserManager<InventoryUserManager>();
        public InventorySignInManager SignInManager => _owinContext.Get<InventorySignInManager>();
        public InventoryRoleManager RoleManager => _owinContext.Get<InventoryRoleManager>();

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _inventoryContext.Dispose();
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
            try
            {
                _inventoryContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
