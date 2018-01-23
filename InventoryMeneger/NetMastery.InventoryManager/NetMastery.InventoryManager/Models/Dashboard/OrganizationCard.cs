using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.Models.Dashboard
{
    public class OrganizationCard
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] Image { get; set; }
        public string MimeType { get; set; }
        public bool IsSelected { get; set; }
    }
}