using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using NetMastery.InventoryManager.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class OrganizationMapOrganizationDto : Profile
    {
        public OrganizationMapOrganizationDto()
        {
            CreateMap<Organization, OrganizationDto>();

            CreateMap<OrganizationDto, Organization>()
                .ForMember(x => x.Subdivisions, y => y.Ignore());
        }
    }

    public class OrganizationDtoMapOrganizationCard : Profile
    {
        public OrganizationDtoMapOrganizationCard()
        {
            CreateMap<OrganizationDto, OrganizationCardViewModel>()
             .ForMember(x => x.IsSelected, y => y.Ignore());
            CreateMap<OrganizationCardViewModel, OrganizationDto>();
        }
    }
}