using Microsoft.AspNet.Identity.EntityFramework;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.DAL
{
    public interface IDbContextManager
    {
        IdentityDbContext<User> Create();
    }
}
