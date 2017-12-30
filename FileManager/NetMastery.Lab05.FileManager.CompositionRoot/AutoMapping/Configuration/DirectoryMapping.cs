using AutoMapper;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Dto;

namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration
{
    public class DirectoryMapping : Profile
    {
        public DirectoryMapping()
        {
            CreateMap<DirectoryStructureDto, DirectoryStructure>()
                .ForMember(c=>c.ParentDirectory, c=>c.Ignore())
                .ForMember(c=>c.Files, c=>c.Ignore())
                .ForMember(c=>c.ChildrenDirectories, c=>c.Ignore());
        }
    }
}
