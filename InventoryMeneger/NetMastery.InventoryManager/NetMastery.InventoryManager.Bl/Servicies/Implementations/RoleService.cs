using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using AutoMapper;

namespace NetMastery.InventoryManager.Bl.Servicies.Implementations
{
    public class RoleService : BusinessService, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {        
        }
        public async Task<IdentityResult> CreateAsync(RoleDto role)
        {
            return await _unitOfWork.RoleManager.CreateAsync(_mapper.Map<Role>(role));
        }
        public async Task<IdentityResult> DeleteAsync(RoleDto role)
        {
            return await _unitOfWork.RoleManager.DeleteAsync(_mapper.Map<Role>(role));
        }
        public async Task<RoleDto> FindByIdAsync(string Id)
        {
            return  _mapper.Map<RoleDto>(await _unitOfWork.RoleManager.FindByIdAsync(Id));
        }
        public IEnumerable<RoleDto> GetAll()
        {
            return _unitOfWork.RoleManager.GetAll().Select(item => _mapper.Map<RoleDto>(item));
        }
        public async Task<IdentityResult> UpdateAsync(RoleDto role)
        {
            return await _unitOfWork.RoleManager.UpdateAsync(_mapper.Map<Role>(role));
        }       
    }
}
