using AutoMapper;
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
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
        }
    }
}
