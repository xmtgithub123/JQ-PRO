﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class OaEquipStockMap : EntityTypeConfiguration<OaEquipStock>
    {
        public OaEquipStockMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.EquipID).IsRequired();
	  
			this.Property(t => t.EquipActionID).IsRequired();
	  
			this.Property(t => t.EquipCount).IsRequired();
	  
			this.Property(t => t.EquipDateTime).IsRequired();
	  
			this.Property(t => t.EquipNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.EquipFormID);
	  
			this.Property(t => t.EquipFormType).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();


			 // Table & Column Mappings
			this.ToTable("OaEquipStock");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.EquipID).HasColumnName("EquipID");
			this.Property(t => t.EquipActionID).HasColumnName("EquipActionID");
			this.Property(t => t.EquipCount).HasColumnName("EquipCount");
			this.Property(t => t.EquipDateTime).HasColumnName("EquipDateTime");
			this.Property(t => t.EquipNote).HasColumnName("EquipNote");
			this.Property(t => t.EquipFormID).HasColumnName("EquipFormID");
			this.Property(t => t.EquipFormType).HasColumnName("EquipFormType");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			           
			#region Relationships
			this.HasRequired(t => t.FK_OaEquipStock_EquipID).WithMany(t => t.FK_OaEquipStock_EquipID).HasForeignKey(d => d.EquipID);
			#endregion
        }
    }
}  
