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

        #region Constructors

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
