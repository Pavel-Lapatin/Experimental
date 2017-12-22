using NetMastery.FileManeger.Bl.Interfaces;
using System.Linq;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using AutoMapper;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.Domain;

namespace NetMastery.FileManeger.Bl.Servicies
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

        public UserInfo GetInfoByLogin(string login)
        {
            return Mapper.Instance.Map<AccountDto>(_unitOfWork
                .Repository<Account>().Find(x => x.Login == login).FirstOrDefault());
        }
    }
}
