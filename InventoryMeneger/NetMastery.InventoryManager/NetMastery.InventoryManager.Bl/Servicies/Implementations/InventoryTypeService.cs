using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using AutoMapper;
using NetMastery.InventoryManager.Domain;
using NetMastery.InventoryManager.Bl.Exceptions;

namespace NetMastery.InventoryManager.Bl.Servicies.Implementations
{
    public class InventoryTypeService : BusinessService, IInventoryTypeService
    {
        public InventoryTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public void Add(InventoryTypeDto item)
        {
            try
            {
                _unitOfWork.InventoryTypeRepository.Create(_mapper.Map<InventoryType>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public void Delete(InventoryTypeDto item)
        {
            try
            {
                _unitOfWork.InventoryTypeRepository.Remove(_mapper.Map<InventoryType>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public void DeleteRange(IEnumerable<InventoryTypeDto> items)
        {
            try
            {
                _unitOfWork.InventoryTypeRepository.RemoveRange(items.Select(item => _mapper.Map<InventoryType>(item)));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public IEnumerable<InventoryTypeDto> GetAll(int accountId)
        {
            throw new NotImplementedException();
        }

        public void Update(InventoryTypeDto item)
        {
            try
            {
                _unitOfWork.InventoryTypeRepository.Update(_mapper.Map<InventoryType>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
    }
}
