using NetMastery.InventoryManager.Domain;
using System.Data.Entity.ModelConfiguration;

namespace NetMastery.InventoryManager.DAL.DbConfiguration
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(x => x.Id);

            HasRequired(x => x.Account)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.AccountId);
        }
    }
}
