using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.Servicies.Implementations
{
    public class OrganizationService : BusinessService, IOrganizationService
    {
        public OrganizationService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public IEnumerable<OrganizationDto> GetAll(int accountId)
        {
            var y = _unitOfWork.OrganizationRepository
                .FindByPredicate(x => x.AccountId == accountId)
                .Select(x => _mapper.Map<OrganizationDto>(x));
            return y;

        }
    }
}
