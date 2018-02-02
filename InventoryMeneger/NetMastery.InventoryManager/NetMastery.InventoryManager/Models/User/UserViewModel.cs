using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        public string Id { get; set; }
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Image { get; set; }
        public string RoleName { get; set; }
    }
}