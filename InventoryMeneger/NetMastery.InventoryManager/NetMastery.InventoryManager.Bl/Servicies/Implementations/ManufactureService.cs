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
    public class ManufactureService : BusinessService, IManufactureService
    {
        public ManufactureService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public void Add(ManufactureDto item)
        {
            try
            {
                _unitOfWork.ManufactureRepository.Create(_mapper.Map<Manufacture>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public void Delete(ManufactureDto item)
        {
            try
            {
                _unitOfWork.ManufactureRepository.Remove(_mapper.Map<Manufacture>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public void DeleteRange(IEnumerable<ManufactureDto> items)
        {
            try
            {
                _unitOfWork.ManufactureRepository.RemoveRange(items.Select(item => _mapper.Map<Manufacture>(item)));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public void Update(ManufactureDto item)
        {
            try
            {
                _unitOfWork.ManufactureRepository.Update(_mapper.Map<Manufacture>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
    }
}
