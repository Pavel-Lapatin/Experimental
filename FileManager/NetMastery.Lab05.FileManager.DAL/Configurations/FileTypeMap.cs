using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Configurations
{
    public class FileTypeMap : EntityTypeConfiguration<FileType>
    {
        public FileTypeMap()
        {

        HasKey(x => x.TypeId);
            Property(p => p.Extension)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UQ_FileType_Extension")
                        {
                            IsUnique = true
                        }));
            Property(p => p.RelatedProgram).HasMaxLength(100);

            
        }
    }
}
