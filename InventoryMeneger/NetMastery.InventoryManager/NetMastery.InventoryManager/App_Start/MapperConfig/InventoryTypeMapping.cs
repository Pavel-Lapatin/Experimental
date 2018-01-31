using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class InventoryTypeMapInventooryTypeDto : Profile
    {
        public InventoryTypeMapInventooryTypeDto()
        {
            CreateMap<InventoryType, InventoryTypeDto>();

            CreateMap<InventoryTypeDto, InventoryType>()
                .ForMember(x => x.Inventories, y => y.Ignore());
        }
    }
}