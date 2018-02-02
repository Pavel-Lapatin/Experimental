using Microsoft.AspNet.Identity;
using NetMastery.InventoryManager.Bl.DtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.Servicies.Interfaces
{
    public interface IRoleService : IDisposable
    {
        Task<IdentityResult> CreateAsync(RoleDto role);
        Task<IdentityResult> UpdateAsync(RoleDto role);
        Task<IdentityResult> DeleteAsync(RoleDto role);
        Task<RoleDto> FindByIdAsync(string Id);
        IEnumerable<RoleDto> GetAll();
    }
}
