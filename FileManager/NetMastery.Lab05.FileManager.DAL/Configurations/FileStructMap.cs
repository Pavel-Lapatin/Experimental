using NetMastery.Lab05.FileManager.Domain;
using System.Data.Entity.ModelConfiguration;


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
