using System;
using System.ComponentModel.DataAnnotations;

namespace NetMastery.InventoryManager.Domain
{
    public class Storage
    {
        public int StorageId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual int PersonInChargeId { get; set; }
        public virtual PersonInCharge PersonInCharge { get; set; }

    }
}
