using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataModel.Models.Mapping
{
    public class V_SystemTableFKeyMap : EntityTypeConfiguration<V_SystemTableFKey>
    {
        public V_SystemTableFKeyMap()
        {
            // Primary Key
            this.HasKey(t => t.FKName);

            // Properties
            this.Property(t => t.FKName)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.TableName)
                .HasMaxLength(128);

            this.Property(t => t.ColName)
                .HasMaxLength(128);

            this.Property(t => t.RefTableName)
                .HasMaxLength(128);

            this.Property(t => t.RefColName)
                .HasMaxLength(128);

            this.Property(t => t.CnstIsNotRepl)
                .IsRequired()
                .HasMaxLength(19);

            this.Property(t => t.CnstIsUpdateCascade)
                .IsRequired()
                .HasMaxLength(17);

            this.Property(t => t.CnstIsDeleteCascade)
                .IsRequired()
                .HasMaxLength(17);

            // Table & Column Mappings
            this.ToTable("V_SystemTableFKey");
            this.Property(t => t.FKName).HasColumnName("FKName");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.ColName).HasColumnName("ColName");
            this.Property(t => t.RefTableName).HasColumnName("RefTableName");
            this.Property(t => t.RefColName).HasColumnName("RefColName");
            this.Property(t => t.CnstIsNotRepl).HasColumnName("CnstIsNotRepl");
            this.Property(t => t.CnstIsUpdateCascade).HasColumnName("CnstIsUpdateCascade");
            this.Property(t => t.CnstIsDeleteCascade).HasColumnName("CnstIsDeleteCascade");
        }
    }
}
