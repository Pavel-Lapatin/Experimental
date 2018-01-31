using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetMastery.InventoryManager.Domain
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<Subdivision> Subdivisions { get; set; }
        public virtual int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
