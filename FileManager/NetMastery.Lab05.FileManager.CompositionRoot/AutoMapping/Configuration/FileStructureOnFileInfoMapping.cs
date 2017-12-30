using AutoMapper;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
{
    class FileStructureOnFileInfoMapping : Profile
    {
        public FileStructureOnFileInfoMapping()
        {
            CreateMap<FileInfo, FileStructure>()
                .ForMember(m => m.Name, cfg => cfg.MapFrom(x => x.Name))
                .ForMember(m => m.ModificationDate, cfg => cfg.MapFrom(x => x.LastWriteTime))
                .ForMember(m => m.CreationTime, cfg => cfg.MapFrom(x => x.CreationTime))
                .ForMember(m => m.FileSize, cfg => cfg.MapFrom(x => x.Length))
                .ForMember(m => m.Extension, cfg => cfg.MapFrom(x => x.Extension))
                .ForMember(m => m.FileId, cfg => cfg.Ignore())
                .ForMember(m => m.Directory, cfg => cfg.Ignore());

        }
    }
}
