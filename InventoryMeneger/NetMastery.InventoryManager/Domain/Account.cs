using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Domain
{
    public class Account
    {
        public int AccountId { get; set; }
        public virtual ICollection<User> Users {get; set;}
        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
