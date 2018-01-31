using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class InventoryMapInventoryDto : Profile
    {
        public InventoryMapInventoryDto()
        {

            CreateMap<Inventory, InventoryDto>();

            CreateMap<InventoryDto, Inventory>()
                .ForMember(x => x.InventoryType, y => y.Ignore())
                .ForMember(x => x.Manufacture, y => y.Ignore())
                .ForMember(x => x.Card, y => y.Ignore());
       }
    }
}