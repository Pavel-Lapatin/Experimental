using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.DAL.Entities;

namespace NetMastery.Lab05.FileManager.DAL.Configurations
{
    public class FileStructMap : EntityTypeConfiguration<FileStructure>
    {
        public FileStructMap()
        {
            HasKey(x => x.FileId);
            Property(p => p.Name).HasMaxLength(50).IsRequired();
            Property(p => p.FileSize).IsRequired();
            Property(p => p.CreationTime).IsRequired();
            Property(p => p.ModificationDate).IsRequired();

            HasRequired(p => p.Directory)
                .WithMany(c => c.Files)
                .Map(x => x.MapKey("Directory"));
        }
    }
}
