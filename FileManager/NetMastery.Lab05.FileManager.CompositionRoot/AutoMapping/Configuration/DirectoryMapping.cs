﻿using AutoMapper;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Dto;

namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
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