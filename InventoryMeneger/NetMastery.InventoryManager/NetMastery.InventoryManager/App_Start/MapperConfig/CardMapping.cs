using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class CardMapCardDto : Profile
    {
        public CardMapCardDto()
        {
            CreateMap<Card, CardDto>()
                .ForMember(x => x.FullCost, y => y.Ignore());

            CreateMap<CardDto, Card>()
                .ForMember(x => x.Inventories, y => y.Ignore());       
        }
    }
}