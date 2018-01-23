using System.ComponentModel.DataAnnotations;

namespace NetMastery.InventoryManager.Domain
{
    public  class InventoryType
    {
        public int InventoryTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        public int AssetDepricationLevel { get; set; }

    }
}
