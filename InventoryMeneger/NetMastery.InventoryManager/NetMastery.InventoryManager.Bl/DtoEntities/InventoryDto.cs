using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.DtoEntities
{
    public class InventoryDto
    {
        public InventoryDto()
        {
            Quantity = 1;
        }
        public int InventoryId { get; set; }
        [Required]
        public string InventoryNumber { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public string PassportNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public string CreationTime { get; set; }
        public string Model { get; set; }
        //1 - in stock, 2 -on stream, 3 - written off
        [Required]
        public int Status { get; set; }
        public int Quantity { get; set; }
        public virtual int InventoryTypeId { get; set; }
        public virtual int? ManufactureId { get; set; }
        public virtual int CardId { get; set; }
    }
}
