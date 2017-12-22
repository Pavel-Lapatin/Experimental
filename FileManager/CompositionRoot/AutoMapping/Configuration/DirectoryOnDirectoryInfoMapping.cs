using AutoMapper;
using NetMastery.Lab05.FileManager.BL.Dto;
using System.IO;


namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
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
