using AutoMapper;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
{
    public class InfoFileVMOnFileDto : Profile
    {
        public InfoFileVMOnFileDto()
        {
            CreateMap<FileStructureDto, FileInfoVieModel>()
                .ForMember(x => x.Messages, cfg => cfg.Ignore())
                .ForMember(x => x.DirectoryPath, cfg => cfg.MapFrom(x=>x.DirectoryPath));
        }
    }
}
