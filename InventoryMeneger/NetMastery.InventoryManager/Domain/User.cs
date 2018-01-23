using Microsoft.AspNet.Identity.EntityFramework;

namespace NetMastery.InventoryManager.Domain
{
    public class User : IdentityUser
    {
        public virtual int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
