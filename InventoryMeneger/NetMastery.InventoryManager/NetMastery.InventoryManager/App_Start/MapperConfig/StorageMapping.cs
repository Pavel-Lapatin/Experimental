using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class StorageMapStorageId : Profile
    {
        public StorageMapStorageId()
        {
            CreateMap<Storage, StorageDto>();

            CreateMap<StorageDto, Storage>()
                .ForMember(x => x.Inventory, y => y.Ignore())
                .ForMember(x => x.PersonInCharge, y => y.Ignore());
        }
    }
}