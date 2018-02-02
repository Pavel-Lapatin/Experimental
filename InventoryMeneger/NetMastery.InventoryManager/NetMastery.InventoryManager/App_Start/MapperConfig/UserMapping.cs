using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using NetMastery.InventoryManager.Models;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.RoleName, y => y.Ignore());
            CreateMap<UserDto, User>()
                .ForMember(x => x.UserName, y => y.MapFrom(t=>t.UserName))
                .ForMember(x => x.Id, y => y.MapFrom(t => t.Id))
                .ForMember(x => x.AccountId, y => y.MapFrom(t => t.AccountId))
                .ForMember(x => x.Email, y => y.MapFrom(t => t.Email))
                .ForMember(x => x.PhoneNumber, y => y.MapFrom(t => t.PhoneNumber))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<UserDto, UserViewModel>()
                .ForMember(x => x.Password, y => y.Ignore());
            CreateMap<UserViewModel, UserDto>();

            CreateMap<RegisterViewModel, UserDto>()
                .ForMember(x => x.AccountId, y => y.Ignore());
            CreateMap<UserDto, RegisterViewModel>()
                .ForMember(x => x.ConfirmPassword, y => y.Ignore());

        }
    }
}