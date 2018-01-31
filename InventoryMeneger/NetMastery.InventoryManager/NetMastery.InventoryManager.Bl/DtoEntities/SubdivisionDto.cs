using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.DtoEntities
{
    public class SubdivisionDto
    {
        public int SubdivisionId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual int OrganizationId { get; set; }
    }
}
