using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Bl.Exceptions;
using NetMastery.InventoryManager.Domain;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using AutoMapper;

namespace NetMastery.InventoryManager.Bl.Servicies.Implementations
{
    public class CardService : BusinessService, ICardService
    {
        public CardService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public void Add(CardDto item)
        {
            try
            {
                _unitOfWork.CardRepository.Create(_mapper.Map<Card>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public void Delete(CardDto item)
        {
            try
            {
                _unitOfWork.CardRepository.Remove(_mapper.Map<Card>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public void DeleteRange(IEnumerable<CardDto> items)
        {
            try
            {
                _unitOfWork.CardRepository.RemoveRange(items.Select(item => _mapper.Map<Card>(item)));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }

        public IEnumerable<CardDto> GetAll(int accountId)
        {
            throw new NotImplementedException();
        }

        public void Update(CardDto item)
        {
            try
            {
                _unitOfWork.CardRepository.Update(_mapper.Map<Card>(item));
            }
            catch (Exception)
            {
                throw new InventoryServiceException();
            }
        }
    }
}
