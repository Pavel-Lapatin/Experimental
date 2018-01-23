using System.Collections.Generic;

namespace NetMastery.InventoryManager.Domain
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] image { get; set; }
        public string MimeType { get; set; }
        public virtual ICollection<Subdivision> Subdivisions { get; set; }
    }
}
