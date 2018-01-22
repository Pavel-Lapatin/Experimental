using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public  class InventoryType
    {
        public int InventoryTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        public int AssetDepricationLevel { get; set; }

    }
}
