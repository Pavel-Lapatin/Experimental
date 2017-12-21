using AutoMapper;
using NetMastery.Lab05.FileManager.BL.Dto;
using NetMastery.Lab05.FileManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
{
    public class AccountMapping : Profile
    {
        public AccountMapping()
        {
            CreateMap<Account, AccountDto>();
        }
    }
}
