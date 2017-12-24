using AutoMapper;
using NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration;


namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping
{
    public static class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AccountMapping());
                cfg.AddProfile(new DirectoryMapping());
                cfg.AddProfile(new FileMapping());
                cfg.AddProfile(new DirectoryOnDirectoryInfoMapping());
                cfg.AddProfile(new FileStructureOnFileInfoMapping());
            });
        }
    }
}
