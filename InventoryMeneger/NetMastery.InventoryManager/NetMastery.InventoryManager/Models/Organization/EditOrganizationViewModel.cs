using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.Models
{
    public class EditOrganizationViewModel
    {
        public OrganizationViewModel Organization { get; set; }

        public SubdivisionViewModel CurrentSubdivision { get; set; }
        public IEnumerable<SubdivisionViewModel> Subdivisions { get; set; }
    }
}