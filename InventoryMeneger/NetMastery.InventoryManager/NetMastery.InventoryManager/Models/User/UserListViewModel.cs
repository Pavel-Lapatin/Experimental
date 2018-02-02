using System.Collections.Generic;

namespace NetMastery.InventoryManager.Models
{
    public class UserListViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public SearchViewModel SearchParametrs { get; set; }
    }
}