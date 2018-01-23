using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.DtoEntities
{
    public class UserDto
    {
        public UserDto()
        {
        }
        public string UserName { get; set; }
        public string Id { get; set; }
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<RoleDto> Roles { get; set; }
    }
}
