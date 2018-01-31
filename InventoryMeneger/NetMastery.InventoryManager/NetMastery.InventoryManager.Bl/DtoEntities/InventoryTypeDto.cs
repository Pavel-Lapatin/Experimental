using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.DtoEntities
{
    public class InventoryTypeDto
    {
        public int InventoryTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        public int AssetDepricationLevel { get; set; }

    }
}
