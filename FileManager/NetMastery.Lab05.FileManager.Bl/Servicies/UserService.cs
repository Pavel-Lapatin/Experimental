using System.Linq;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using AutoMapper;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using System;
using NetMastery.Lab05.FileManager.UI;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class UserService : BusinessService, IUserService
    {
        #region Constructors

        public UserService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           IUserContext userContext) : base(unitOfWork, mapper, userContext)
        { 
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
