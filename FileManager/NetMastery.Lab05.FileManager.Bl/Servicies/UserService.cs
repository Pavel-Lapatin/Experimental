using NetMastery.FileManeger.Bl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.BLModel.DtoClasses;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using AutoMapper;
using NetMastery.Lab05.FileManager.BL.Dto;
using NetMastery.Lab05.FileManager.DAL.Entities;

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
