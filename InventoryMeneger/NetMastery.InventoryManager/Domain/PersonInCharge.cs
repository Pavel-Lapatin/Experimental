using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetMastery.InventoryManager.Domain
{
    public class PersonInCharge
    {
        public int PersonId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public virtual int SubdivisionId { get; set; }
        public virtual Subdivision Subdivision { get; set; }

        public virtual ICollection<Storage> Storages { get; set; }
    }
}
