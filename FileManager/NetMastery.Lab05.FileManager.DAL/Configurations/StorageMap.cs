using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.DAL.Entities;

namespace NetMastery.Lab05.FileManager.DAL.Configurations
{
    internal class StorageMap : EntityTypeConfiguration<DirectoryInfo>
    {
        public StorageMap()
        {
            HasKey(p => p.StorageId);

            HasOptional(x => x.ParentStorage)
            .WithMany(x => x.ChildrenStoragies)
            .HasForeignKey(x=>x.ParentStorageId);

            Property(p => p.CreationDate).IsRequired();
            Property(p => p.ModificationDate).IsRequired();
            Property(p => p.Name).IsRequired().HasMaxLength(255);
        }

    }
}
