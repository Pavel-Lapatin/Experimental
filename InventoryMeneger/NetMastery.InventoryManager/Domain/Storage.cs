using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
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
