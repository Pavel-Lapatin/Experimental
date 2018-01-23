using System.ComponentModel.DataAnnotations;

namespace NetMastery.InventoryManager.Domain
{
    public class Manufacture
    {
        public int ManufactureId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

    }
}
