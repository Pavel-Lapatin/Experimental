using AutoMapper;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public abstract class BusinessService : IDisposable
    {
        protected IUnitOfWork _unitOfWork;
        protected IMapper _mapper;
        protected bool disposed = false;
        protected readonly IUserContext _userContext;

        protected BusinessService(IUnitOfWork unitOfWork, IMapper mapper, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContext = userContext;
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                _unitOfWork.Dispose();
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
