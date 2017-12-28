using AutoMapper;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.ViewModels;

namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
{
    public class InfoUserVMOnAccountDtoMapping : Profile
    {
        public InfoUserVMOnAccountDtoMapping()
        {
            CreateMap<AccountDto, InfoUserViewModel>()
                .ForMember(x=>x.Messages, cfg=>cfg.Ignore());
        }
    }
}
