
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using NetMastery.Lab05.FileManager.DAL.Entities;

namespace NetMastery.Lab05.FileManager.DAL.Configurations
{
    internal class AccountsMap : EntityTypeConfiguration<Account>
    {
        public AccountsMap()
        {
            HasKey(c => c.AccountId);

            Property(p => p.Login)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode();
                
            Property(p => p.Password).IsRequired();
            Property(p => p.CreationDate).IsRequired();

            HasRequired(x => x.RootDirectory)
                .WithOptional()
                .Map(x => x.MapKey("RootDirectory"));
            HasIndex(x => x.Login).IsUnique();
        }
    }
}
