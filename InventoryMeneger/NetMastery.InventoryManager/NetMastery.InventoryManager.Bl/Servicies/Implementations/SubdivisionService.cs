using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Bl.Exceptions;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using AutoMapper;
using NetMastery.InventoryManager.Domain;

namespace NetMastery.InventoryManager.Bl.Servicies.Implementations
{
    class SubdivisionService : BusinessService, ISubdivisionService
    {
        public SubdivisionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public void Add(SubdivisionDto item)
        {
            try
            {
                _unitOfWork.SubdivisionRepository.Create(_mapper.Map<Subdivision>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public void Delete(SubdivisionDto item)
        {
            try
            {
                _unitOfWork.SubdivisionRepository.Remove(_mapper.Map<Subdivision>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public void DeleteRange(IEnumerable<SubdivisionDto> items)
        {
            try
            {
                _unitOfWork.SubdivisionRepository.RemoveRange(items.Select(item => _mapper.Map<Subdivision>(item)));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public void Update(SubdivisionDto item)
        {
            try
            {
                _unitOfWork.SubdivisionRepository.Update(_mapper.Map<Subdivision>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
    }
}
