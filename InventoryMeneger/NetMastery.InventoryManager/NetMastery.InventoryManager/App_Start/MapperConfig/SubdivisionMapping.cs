using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class SubdivisionMapSubdivisionDto : Profile
    {
        public SubdivisionMapSubdivisionDto()
        {
            CreateMap<Subdivision, SubdivisionDto>();

            CreateMap<SubdivisionDto, Subdivision>()
                .ForMember(x => x.Organization, y => y.Ignore())
                .ForMember(x => x.Persons, y => y.Ignore());
        }
    }
}