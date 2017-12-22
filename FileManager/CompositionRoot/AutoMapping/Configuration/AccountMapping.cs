using AutoMapper;
using NetMastery.Lab05.FileManager.BL.Dto;
using NetMastery.Lab05.FileManager.DAL.Entities;


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
