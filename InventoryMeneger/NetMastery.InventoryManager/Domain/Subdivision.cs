using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetMastery.InventoryManager.Domain
{
    public class Subdivision
    {
        public int SubdivisionId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<PersonInCharge> Persons { get; set; }
        public virtual int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
