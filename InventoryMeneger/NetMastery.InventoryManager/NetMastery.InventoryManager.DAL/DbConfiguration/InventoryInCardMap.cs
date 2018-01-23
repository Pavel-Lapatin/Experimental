using NetMastery.InventoryManager.Domain;
using System.Data.Entity.ModelConfiguration;

namespace NetMastery.InventoryManager.DAL.DbConfiguration
{
    public class InventoryInCardMap : EntityTypeConfiguration<InventoryInCard>
    {
        public InventoryInCardMap()
        {
            HasKey(x => new { x.CardId, x.InventoryId });
        }
    }
}
