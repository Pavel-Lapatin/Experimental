using AutoMapper;
using NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping.Configuration;
using System.Linq;
using System.Reflection;

namespace NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping
{
    public static class AutoMapperInitializer
    {
        public static void Initialize()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Profile)));
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(types);
            });
        }
    }
}
