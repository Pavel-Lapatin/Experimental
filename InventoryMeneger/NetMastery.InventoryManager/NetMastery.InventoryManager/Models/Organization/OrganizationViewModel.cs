using System.ComponentModel.DataAnnotations;

namespace NetMastery.InventoryManager.Models
{
    public class OrganizationViewModel
    {
        public int OrganizationId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        public byte[] Image { get; set; }
        public string MimeType { get; set; }
        public bool IsSelected { get; set; }
    }
}