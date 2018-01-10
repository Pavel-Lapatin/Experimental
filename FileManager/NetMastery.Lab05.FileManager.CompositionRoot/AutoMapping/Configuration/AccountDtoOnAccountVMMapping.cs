using AutoMapper;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.ViewModels;

namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
{
    class AccountDtoOnAccountVMMapping : Profile
    {
        public AccountDtoOnAccountVMMapping()
        {
            CreateMap<AccountDto, AccountViewModel>()
                .modelember(m => m.Login, cfg => cfg.MapFrom(x => x.Login))
                .modelember(m => m.RootDirectory, cfg => cfg.MapFrom(x => x.RootDirectory))
                .modelember(m => m.Messages, cfg => cfg.Ignore());
        }
    }
}