using AutoMapper;
using NetMastery.Lab05.FileManager.BL.Dto;
using NetMastery.Lab05.FileManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Infrastructure.AutoMapping.Configuration
{
    public class FileMapping : Profile
    {
        CreateMap<FileStructure, FileStructureDto>();
    }
}
