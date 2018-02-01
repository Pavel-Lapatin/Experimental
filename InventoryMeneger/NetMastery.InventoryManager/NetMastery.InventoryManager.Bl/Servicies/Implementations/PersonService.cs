using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using AutoMapper;
using NetMastery.InventoryManager.Bl.Exceptions;
using NetMastery.InventoryManager.Domain;

namespace NetMastery.InventoryManager.Bl.Servicies.Implementations
{
    class PersonService : BusinessService, IPersonService
    {
        public PersonService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public void Add(PersonDto item)
        {
            try
            {
                _unitOfWork.PersonInChargeRepository.Create(_mapper.Map<PersonInCharge>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public void Delete(PersonDto item)
        {
            try
            {
                _unitOfWork.PersonInChargeRepository.Remove(_mapper.Map<PersonInCharge>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public void DeleteRange(IEnumerable<PersonDto> items)
        {
            try
            {
                _unitOfWork.PersonInChargeRepository.RemoveRange(items.Select(item => _mapper.Map<PersonInCharge>(item)));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
        public void Update(PersonDto item)
        {
            try
            {
                _unitOfWork.PersonInChargeRepository.Update(_mapper.Map<PersonInCharge>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public IEnumerable<PersonDto> GetAll(int subdivisionId)
        {
            return _unitOfWork.PersonInChargeRepository
                              .FindByPredicate(x => x.SubdivisionId == subdivisionId)
                              .Select(item => _mapper.Map<PersonDto>(item)).ToArray();
        }
    }
}
