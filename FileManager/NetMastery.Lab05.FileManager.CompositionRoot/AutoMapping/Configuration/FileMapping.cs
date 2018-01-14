﻿using AutoMapper;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Dto;

namespace NetMastery.Lab05.FileManager.Composition.AutoMapping.Configuration
{
    class FileMapping : Profile
    {
        public FileMapping()
        {
            CreateMap<FileStructure, FileStructureDto>();
        }
        
    }
}
