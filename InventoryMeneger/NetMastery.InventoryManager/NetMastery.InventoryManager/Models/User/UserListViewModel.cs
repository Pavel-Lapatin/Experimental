using System.Collections.Generic;

namespace NetMastery.InventoryManager.Models
{
    public class UserListViewModel
    {
        public IList<UserViewModel> Users { get; set; }
        public UserSearchViewModel SearchParametrs { get; set; }
        public PagingInfo PagingInfo { get; internal set; }
    }
}