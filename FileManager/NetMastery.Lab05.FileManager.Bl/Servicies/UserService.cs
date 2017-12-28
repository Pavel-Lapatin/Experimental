using System.Linq;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using AutoMapper;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Bl.Interfaces;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        #region Constructors

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public AccountDto GetInfoByLogin(string login)
        {
            return Mapper.Instance.Map<AccountDto>(_unitOfWork
                .Repository<Account>().Find(x => x.Login == login).FirstOrDefault());
        }
    }
}
