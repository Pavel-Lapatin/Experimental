using NetMastery.InventoryManager.Domain;
using System.Data.Entity.ModelConfiguration;


namespace NetMastery.InventoryManager.DAL.DbConfiguration
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            HasKey(c => c.AccountId);
        }
    }
}
