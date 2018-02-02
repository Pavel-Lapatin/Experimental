using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Bl.Exceptions;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.Servicies.Implementations
{
    public class OrganizationService : BusinessService, IOrganizationService
    {
        public OrganizationService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public void Add(OrganizationDto item)
        {
            try
            {
                _unitOfWork.OrganizationRepository.Create(_mapper.Map<Organization>(item));
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public void Delete(OrganizationDto item)
        {
            try
            {
                _unitOfWork.OrganizationRepository.Remove(_mapper.Map<Organization>(item));
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public void DeleteRange(IEnumerable<OrganizationDto> items)
        {
            try
            {
                _unitOfWork.OrganizationRepository.RemoveRange( items.Select(item => _mapper.Map<Organization>(item)));
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public void Update(OrganizationDto item)
        {
            try
            {
                _unitOfWork.OrganizationRepository.Update(_mapper.Map<Organization>(item));
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public IEnumerable<OrganizationDto> GetAll(int accountId)
        {
            return _unitOfWork.OrganizationRepository
                .FindByPredicate(x => x.AccountId == accountId)
                .Select(x => _mapper.Map<OrganizationDto>(x)).ToArray();
        }
        public IEnumerable<OrganizationDto> Search(int accountId, string pattern)
        {
            return _unitOfWork.OrganizationRepository
                .FindByPredicate(x => x.AccountId == accountId && x.Name.Contains(pattern))
                .Select(x => _mapper.Map<OrganizationDto>(x)).ToArray();
        }
    }
}
