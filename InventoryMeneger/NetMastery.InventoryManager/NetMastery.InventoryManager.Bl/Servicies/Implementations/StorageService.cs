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
    public class StorageService : BusinessService, IStorageService
    {
        public StorageService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public void Add(StorageDto item)
        {
            try
            {
                _unitOfWork.StorageRepository.Create(_mapper.Map<Storage>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public void Delete(StorageDto item)
        {
            try
            {
                _unitOfWork.StorageRepository.Remove(_mapper.Map<Storage>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public void DeleteRange(IEnumerable<StorageDto> items)
        {
            try
            {
                _unitOfWork.StorageRepository.RemoveRange(items.Select(item => _mapper.Map<Storage>(item)));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public void Update(StorageDto item)
        {
            try
            {
                _unitOfWork.StorageRepository.Update(_mapper.Map<Storage>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
    }
}
