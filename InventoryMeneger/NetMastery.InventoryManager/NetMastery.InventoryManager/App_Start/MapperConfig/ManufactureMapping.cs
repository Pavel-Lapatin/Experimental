using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class ManufactureMapManufaturedto : Profile
    {
        public ManufactureMapManufaturedto()
        {
            CreateMap<Manufacture, ManufactureDto>();

            CreateMap<ManufactureDto, Manufacture>()
                .ForMember(x => x.Inventories, y => y.Ignore());
        }
    }
}