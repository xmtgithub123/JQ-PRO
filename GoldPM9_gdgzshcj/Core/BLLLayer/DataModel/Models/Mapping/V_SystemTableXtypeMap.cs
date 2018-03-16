using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataModel.Models.Mapping
{
    public class V_SystemTableXtypeMap : EntityTypeConfiguration<V_SystemTableXtype>
    {
        public V_SystemTableXtypeMap()
        {
            // Primary Key
            this.HasKey(t => t.XName);

            // Properties
            this.Property(t => t.TableName)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.XName)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.XType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("V_SystemTableXtype");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.XName).HasColumnName("XName");
            this.Property(t => t.XType).HasColumnName("XType");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
        }
    }
}
