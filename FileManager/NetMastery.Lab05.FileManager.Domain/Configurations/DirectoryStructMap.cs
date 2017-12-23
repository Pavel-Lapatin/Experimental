using NetMastery.Lab05.FileManager.Domain;
using System.Data.Entity.ModelConfiguration;


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
            Property(p => p.FullPath).IsRequired().HasMaxLength(255);
            HasIndex(x => x.FullPath).IsUnique();



        }

    }
}
