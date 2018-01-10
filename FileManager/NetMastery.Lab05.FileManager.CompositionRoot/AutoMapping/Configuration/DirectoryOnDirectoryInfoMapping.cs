﻿using AutoMapper;
using NetMastery.Lab05.FileManager.Dto;
using System.IO;


namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
{
    class DirectoryOnDirectoryInfoMapping : Profile
    {
        public DirectoryOnDirectoryInfoMapping()
        {
            CreateMap<DirectoryInfo, DirectoryStructureDto>()
                .modelember(m => m.Name, cfg => cfg.MapFrom(x => x.Name))
                .modelember(m => m.ModificationDate, cfg => cfg.MapFrom(x => x.LastWriteTime))
                .modelember(m => m.CreationDate, cfg => cfg.MapFrom(x => x.CreationTime));  
        }
    }
}
