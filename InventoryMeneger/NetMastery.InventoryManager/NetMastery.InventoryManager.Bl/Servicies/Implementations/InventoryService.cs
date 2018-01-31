using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using AutoMapper;
using NetMastery.InventoryManager.Domain;
using NetMastery.InventoryManager.Bl.Exceptions;

namespace NetMastery.InventoryManager.Bl.Servicies.Implementations
{
    class InventoryService : BusinessService, IInventoryService
    {
        public InventoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public void Add(InventoryDto item)
        {
            try
            {
                _unitOfWork.InventoryRepository.Create(_mapper.Map<Inventory>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public void Delete(InventoryDto item)
        {
            try
            {
                _unitOfWork.InventoryRepository.Remove(_mapper.Map<Inventory>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public void DeleteRange(IEnumerable<InventoryDto> items)
        {
            try
            {
                _unitOfWork.InventoryRepository.RemoveRange(items.Select(item => _mapper.Map<Inventory>(item)));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public void Update(InventoryDto item)
        {
            try
            {
                _unitOfWork.InventoryRepository.Update(_mapper.Map<Inventory>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
    }
}
