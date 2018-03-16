using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataModel.Models.Mapping
{
    public class V_SystemTableStructureMap : EntityTypeConfiguration<V_SystemTableStructure>
    {
        public V_SystemTableStructureMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TableName, t.ColIndex });

            // Properties
            this.Property(t => t.TableName)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.TableNote)
                .HasMaxLength(500);

            this.Property(t => t.ColIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ColName)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.IsIdentity)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.IsPK)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.ColType)
                .HasMaxLength(128);

            this.Property(t => t.ColLength)
                .HasMaxLength(30);

            this.Property(t => t.ColLen)
                .HasMaxLength(30);

            this.Property(t => t.ColScale)
                .HasMaxLength(30);

            this.Property(t => t.IsToNull)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.DefaultText)
                .IsRequired()
                .HasMaxLength(4000);

            this.Property(t => t.ColNote)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("V_SystemTableStructure");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.TableNote).HasColumnName("TableNote");
            this.Property(t => t.ColIndex).HasColumnName("ColIndex");
            this.Property(t => t.ColName).HasColumnName("ColName");
            this.Property(t => t.IsIdentity).HasColumnName("IsIdentity");
            this.Property(t => t.IsPK).HasColumnName("IsPK");
            this.Property(t => t.ColType).HasColumnName("ColType");
            this.Property(t => t.ColLength).HasColumnName("ColLength");
            this.Property(t => t.ColLen).HasColumnName("ColLen");
            this.Property(t => t.ColScale).HasColumnName("ColScale");
            this.Property(t => t.IsToNull).HasColumnName("IsToNull");
            this.Property(t => t.DefaultText).HasColumnName("DefaultText");
            this.Property(t => t.ColNote).HasColumnName("ColNote");
        }
    }
}
