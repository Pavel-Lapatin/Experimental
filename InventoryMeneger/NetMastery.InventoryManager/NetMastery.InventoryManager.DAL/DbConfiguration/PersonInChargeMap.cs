using NetMastery.InventoryManager.Domain;
using System.Data.Entity.ModelConfiguration;

namespace NetMastery.InventoryManager.DAL.DbConfiguration
{
    class PersonInChargeMap : EntityTypeConfiguration<PersonInCharge>
    {
        public PersonInChargeMap()
        {
            HasKey(x => x.PersonId);
        }
    }
}
