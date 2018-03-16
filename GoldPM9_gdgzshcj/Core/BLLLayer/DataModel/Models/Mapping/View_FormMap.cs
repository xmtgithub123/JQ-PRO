using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataModel.Models.Mapping
{
    public class View_FormMap : EntityTypeConfiguration<View_Form>
    {
        public View_FormMap()
        {
            // Primary Key
            this.HasKey(t => t.ok);

            // Properties
            this.Property(t => t.ok)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("View_Form");
            this.Property(t => t.ok).HasColumnName("ok");
        }
    }
}
