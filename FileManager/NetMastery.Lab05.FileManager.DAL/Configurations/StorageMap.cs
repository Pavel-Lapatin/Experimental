using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.DAL.Entities;

namespace NetMastery.Lab05.FileManager.DAL.Configurations
{
    internal class StorageMap : EntityTypeConfiguration<Storage>
    {
        public StorageMap()
        {
            HasKey(p => p.StorageId);

            HasOptional(x => x.ParentStorage)
            .WithMany()
            .HasForeignKey(x=>x.ParentStorageId);

            HasOptional(x => x.ChildrenStoragies)
                .WithMany();

            HasRequired(x => x.Account)
                .WithRequiredPrincipal(x => x.Storage);
            Property(p => p.CreationDate).IsRequired();
            Property(p => p.ModificationDate).IsRequired();
            Property(p => p.Name).IsRequired().HasMaxLength(255);
        }

    }
}
