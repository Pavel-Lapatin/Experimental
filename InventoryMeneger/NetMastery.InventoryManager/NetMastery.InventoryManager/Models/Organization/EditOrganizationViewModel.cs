using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.Models
{
    public class EditOrganizationViewModel
    {
        public EditOrganizationViewModel()
        {
            Organization = new OrganizationViewModel();
            CurrentSubdivision = new SubdivisionViewModel();
            Subdivisions = new  List<SubdivisionViewModel>();
        }
        public OrganizationViewModel Organization { get; set; }
        public SubdivisionViewModel CurrentSubdivision { get; set; } 
        public IEnumerable<SubdivisionViewModel> Subdivisions { get; set; } 
    }
}