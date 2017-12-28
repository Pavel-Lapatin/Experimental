﻿using AutoMapper;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
{
    public class InfoDirectoryVMOnDirectoryStructureDto : Profile
    {
        public InfoDirectoryVMOnDirectoryStructureDto()
        {
            CreateMap<FileStructureDto, DirectoryInfoViewModel>()
                .ForMember(x => x.Messages, cfg => cfg.Ignore()); 
        }
    }
}