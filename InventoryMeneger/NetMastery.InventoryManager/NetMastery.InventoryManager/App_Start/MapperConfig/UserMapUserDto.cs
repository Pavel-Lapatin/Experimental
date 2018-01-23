using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using NetMastery.InventoryManager.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class UserMapUserDto : Profile
    {
        public UserMapUserDto()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.UserName, y => y.MapFrom(t=>t.UserName))
                .ForMember(x => x.Id, y => y.MapFrom(t => t.Id))
                .ForMember(x => x.AccountId, y => y.MapFrom(t => t.AccountId))
                .ForMember(x => x.Email, y => y.MapFrom(t => t.Email))
                .ForMember(x => x.PhoneNumber, y => y.MapFrom(t => t.PhoneNumber))
                .ForMember(x => x.Roles, y => y.MapFrom(t => t.Roles))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }

    public class RegisterViewModelMapUserDto : Profile
    {
        public RegisterViewModelMapUserDto()
        {
            CreateMap<RegisterViewModel, UserDto>()
                .ForMember(x => x.AccountId, y => y.Ignore());
            CreateMap<UserDto, RegisterViewModel>()
                .ForMember(x => x.ConfirmPassword, y => y.Ignore());
        }
    }
}