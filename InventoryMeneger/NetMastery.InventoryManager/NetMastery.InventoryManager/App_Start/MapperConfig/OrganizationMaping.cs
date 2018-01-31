using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using NetMastery.InventoryManager.Models;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class OrganizationMapOrganizationDto : Profile
    {
        public OrganizationMapOrganizationDto()
        {
            CreateMap<Organization, OrganizationDto>();

            CreateMap<OrganizationDto, Organization>()
                .ForMember(x => x.Subdivisions, y => y.Ignore())
                .ForMember(x => x.Account, y => y.Ignore());
        }
    }

    public class OrganizationDtoMapOrganizationViewModel : Profile
    {
        public OrganizationDtoMapOrganizationViewModel()
        {
            CreateMap<OrganizationDto, OrganizationViewModel>()
             .ForMember(x => x.IsSelected, y => y.Ignore());
            CreateMap<OrganizationViewModel, OrganizationDto>();
        }
    }
}