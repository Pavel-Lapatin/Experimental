﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.DtoEntities
{
    public class RoleDto
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] image { get; set; }
        public string MimeType { get; set; }
    }
}
