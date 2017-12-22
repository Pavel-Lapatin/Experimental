using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMastery.Lab05.FileManager.DAL.Entities;

namespace NetMastery.Lab05.FileManager.DAL.Configurations
{
    public class DirectoryStructMap : EntityTypeConfiguration<DirectoryStructure>
    {
        public DirectoryStructMap()
        {
            HasKey(p => p.DirectoryId);

            HasOptional(x => x.ParentDirectory)
            .WithMany(x => x.ChildrenDirectories)
            .Map(x=>x.MapKey("ParantDirectory"));

            Property(p => p.CreationDate).IsRequired();
            Property(p => p.ModificationDate).IsRequired();
            Property(p => p.Name).IsRequired().HasMaxLength(255);



        }

    }
}
