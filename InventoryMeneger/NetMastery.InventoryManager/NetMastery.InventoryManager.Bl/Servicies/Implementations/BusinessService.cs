using AutoMapper;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using System;

namespace NetMastery.InventoryManager.Bl.Servicies.Implementations
{
    public abstract class BusinessService
    {
        private bool disposed = false;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BusinessService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 
    }
}
