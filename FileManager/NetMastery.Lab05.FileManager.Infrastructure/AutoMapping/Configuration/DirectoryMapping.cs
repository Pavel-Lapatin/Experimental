using AutoMapper;
using NetMastery.Lab05.FileManager.BL.Dto;
using NetMastery.Lab05.FileManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Infrastructure.AutoMapping.Configuration
{
    public class DirectoryMapping : Profile
    {
        public DirectoryMapping()
        {
            CreateMap<DirectoryStructure, DirectoryStructureDto>()
                .ForMember(m => m.ChildrenDirectories, cfg => cfg.MapFrom(x => x.ChildrenDirectories))
                .ForMember(m => m.ParentDirectory, cfg => cfg.MapFrom(x => x.ParentDirectory))
                .ForMember(m => m.Files, cfg => cfg.MapFrom(x => x.Files));
        }
    }
}
