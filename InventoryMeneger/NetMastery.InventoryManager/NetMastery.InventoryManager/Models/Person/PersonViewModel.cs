using System.ComponentModel.DataAnnotations;

namespace NetMastery.InventoryManager.Models
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
    }
}