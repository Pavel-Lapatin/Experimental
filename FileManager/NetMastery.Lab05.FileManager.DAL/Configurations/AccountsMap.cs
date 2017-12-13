
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
            HasKey(c => c.AccoountId);
            Property(p => p.Login)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UQ_Account_Name")
                        {
                            IsUnique = true
                        }));

            Property(p => p.Password).IsRequired();
            Property(p => p.CreationDate).IsRequired();

            HasRequired(x => x.Storage)
                .WithRequiredDependent(x=>x.Account)
                .Map(x=>x.MapKey("RootDirectory"));
                
        }
    }
}
