using System.Linq;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using AutoMapper;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected bool disposed = false;

        #region Constructors

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        #endregion

        public AccountDto GetInfoByLogin(string login)
        {
            if (login == null)
            {
                throw new ArgumentNullException();
            }    
            return _mapper.Map<AccountDto>(_unitOfWork
                .Get<IAccountRepository>().Find(x => x.Login == login).FirstOrDefault());
        }
    }
}
