using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMastery.InventoryManager.App_Start.MapperConfig
{
    public class RoleMapRoleDto : Profile
    {
        public RoleMapRoleDto()
        {
            CreateMap<Role, RoleDto>();
                
            CreateMap<RoleDto, Role>()
                .ForMember(x=>x.Users, y=>y.Ignore());

            CreateMap<SelectListItem, RoleDto>()
                .ForMember(x => x.Name, y => y.MapFrom(t => t.Value))
                .ForMember(x => x.Id, y => y.Ignore())
                .AfterMap((src, dest) =>
                {
                    src.Group.Name = "Roles";
                    src.Group.Disabled = false;
                });

            CreateMap<RoleDto, SelectListItem>()
                .ForMember(x => x.Value, y => y.MapFrom(t => t.Name))
                .ForMember(x => x.Text, y => y.MapFrom(t => t.Name))
                .ForMember(x => x.Disabled, y => y.MapFrom(t => false))
                //.AfterMap((src, dest) =>
                //{
                //    dest.Group = new SelectListGroup();
                //    dest.Group.Name = "Roles";
                //    dest.Group.Disabled = false;
                //})
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}