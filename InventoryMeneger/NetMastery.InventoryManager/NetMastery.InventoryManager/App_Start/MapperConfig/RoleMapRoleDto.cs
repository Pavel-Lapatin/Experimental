using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class RoleMapRoleDto : Profile
    {
        public RoleMapRoleDto()
        {
            CreateMap<Role, RoleDto>();
                
            CreateMap<RoleDto, Role>()
                .ForMember(x=>x.Users, y=>y.Ignore());
        }
    }
}