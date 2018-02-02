using NetMastery.InventoryManager.Bl.DtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.Models
{
    public class OrganizationListViewModel
    {
        public OrganizationListViewModel()
        {
            Organizations = new List<OrganizationViewModel>();
            PagingInfo = new PagingInfo { CurrentPage = 1 };
        }
        public IList<OrganizationViewModel> Organizations { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string Pattern { get; set; }
    }
}