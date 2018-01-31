using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.DtoEntities
{
    public class ManufactureDto
    {
        public int ManufactureId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
