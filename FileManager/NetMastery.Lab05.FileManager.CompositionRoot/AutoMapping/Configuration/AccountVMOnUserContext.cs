using AutoMapper;
using NetMastery.Lab05.FileManager.UI;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
{
    class AccountVMOnUserContext : Profile
    {
        public AccountVMOnUserContext()
        {
            CreateMap<IUserContext, AccountViewModel>()
                .ForMember(m => m.Login, cfg => cfg.MapFrom(x => x.Login))
                .ForMember(m => m.RootDirectory, cfg => cfg.MapFrom(x => x.CurrentPath))
                .ForMember(m => m.Messages, cfg => cfg.Ignore())
                .ForMember(m => m.RootDirectory, cfg => cfg.MapFrom(x => x.RootDirectory));
        }
    }
}
