using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class PersonInChargeMapPersonDto : Profile
    {
        public PersonInChargeMapPersonDto()
        {
            CreateMap<PersonInCharge, PersonDto>();

            CreateMap<PersonDto, PersonInCharge>()
                .ForMember(x => x.Subdivision, y => y.Ignore())
                .ForMember(x => x.Storages, y => y.Ignore());
        }
    }
}