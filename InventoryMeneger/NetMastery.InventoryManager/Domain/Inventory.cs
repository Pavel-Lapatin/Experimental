using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace NetMastery.InventoryManager.Domain
{
    public class Inventory
    {
        public Inventory()
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
        public virtual InventoryType InventoryType { get; set; }
        public virtual int? ManufactureId { get; set; }
        public virtual Manufacture Manufacture { get; set; }
        public virtual int CardId { get; set; }
        public virtual Card Card { get; set; }
    }
}
