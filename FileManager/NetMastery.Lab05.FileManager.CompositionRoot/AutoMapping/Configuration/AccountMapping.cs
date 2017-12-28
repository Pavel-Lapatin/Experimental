using AutoMapper;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Dto;

namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
{
    public class AccountMapping : Profile
    {
        public AccountMapping()
        {
            CreateMap<Account, AccountDto>()
                .ForMember(m => m.RootDirectory, cfg => cfg.MapFrom(x => x.RootDirectory.FullPath));
        }
    }
}
