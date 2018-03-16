using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataModel.Models.Mapping
{
    public class View_MenuPermitMap : EntityTypeConfiguration<View_MenuPermit>
    {
        public View_MenuPermitMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MenuID, t.EmpID, t.Permit });

            // Properties
            this.Property(t => t.MenuID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EmpID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Permit)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("View_MenuPermit");
            this.Property(t => t.MenuID).HasColumnName("MenuID");
            this.Property(t => t.EmpID).HasColumnName("EmpID");
            this.Property(t => t.Permit).HasColumnName("Permit");
        }
    }
}
