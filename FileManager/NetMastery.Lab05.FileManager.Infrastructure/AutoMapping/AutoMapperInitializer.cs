using AutoMapper;
using NetMastery.Lab05.FileManager.Infrastructure.AutoMapping.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Infrastructure.AutoMapping
{
    public static class AutoMapperInitializer
    {
        public static void Initialize(params Profile[] profiles )
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AccountMapping());
                cfg.AddProfile(new DirectoryMapping());
                cfg.AddProfile(new FileMapping());
                cfg.AddProfile(new DirectoryOnDirectoryInfoMapping());
            });
        }
    }
}
