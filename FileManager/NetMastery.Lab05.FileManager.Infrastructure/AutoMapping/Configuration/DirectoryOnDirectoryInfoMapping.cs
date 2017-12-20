using AutoMapper;
using NetMastery.Lab05.FileManager.BL.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Infrastructure.AutoMapping.Configuration
{
    class DirectoryOnDirectoryInfoMapping : Profile
    {
        public DirectoryOnDirectoryInfoMapping()
        {
            CreateMap<DirectoryInfo, DirectoryStructureDto>()
                .ForMember(m => m.Name, cfg => cfg.MapFrom(x => x.Name))
                .ForMember(m => m.ModificationDate, cfg => cfg.MapFrom(x => x.LastWriteTime))
                .ForMember(m => m.CreationDate, cfg => cfg.MapFrom(x => x.CreationTime));  
        }
    }
}
