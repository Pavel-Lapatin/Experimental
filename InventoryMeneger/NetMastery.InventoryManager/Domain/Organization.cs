using System.Collections.Generic;

namespace NetMastery.InventoryManager.Domain
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Subdivision> Subdivisions { get; set; }
    }
}
