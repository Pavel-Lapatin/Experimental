using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.Models
{
    public class SubdivisionViewModel
    {
        public int SubdivisionId { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<PersonViewModel> Persons { get; set; }
    }
}