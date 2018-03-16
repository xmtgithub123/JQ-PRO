﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ModelVolCatalogMap : EntityTypeConfiguration<ModelVolCatalog>
    {
        public ModelVolCatalogMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.PhaseID).IsRequired();
	  
			this.Property(t => t.SpecialID).IsRequired();
	  
			this.Property(t => t.ModelVolGroup).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ModelVolNumber).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ModelVolName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ModelVolNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.ModelVolTypeID).IsRequired();
	  
			this.Property(t => t.ModelVolParentID).IsRequired();
	  
			this.Property(t => t.ModelDays).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.ModelCheckDays).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.ModelAuditDays).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.ModelPlanDays).IsRequired();
	  
			this.Property(t => t.ModelTypeID).IsRequired();


			 // Table & Column Mappings
			this.ToTable("ModelVolCatalog");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.PhaseID).HasColumnName("PhaseID");
			this.Property(t => t.SpecialID).HasColumnName("SpecialID");
			this.Property(t => t.ModelVolGroup).HasColumnName("ModelVolGroup");
			this.Property(t => t.ModelVolNumber).HasColumnName("ModelVolNumber");
			this.Property(t => t.ModelVolName).HasColumnName("ModelVolName");
			this.Property(t => t.ModelVolNote).HasColumnName("ModelVolNote");
			this.Property(t => t.ModelVolTypeID).HasColumnName("ModelVolTypeID");
			this.Property(t => t.ModelVolParentID).HasColumnName("ModelVolParentID");
			this.Property(t => t.ModelDays).HasColumnName("ModelDays");
			this.Property(t => t.ModelCheckDays).HasColumnName("ModelCheckDays");
			this.Property(t => t.ModelAuditDays).HasColumnName("ModelAuditDays");
			this.Property(t => t.ModelPlanDays).HasColumnName("ModelPlanDays");
			this.Property(t => t.ModelTypeID).HasColumnName("ModelTypeID");
			           
			#region Relationships
			this.HasRequired(t => t.FK_ModelVolCatalog_PhaseID).WithMany(t => t.FK_ModelVolCatalog_PhaseID).HasForeignKey(d => d.PhaseID);
			this.HasRequired(t => t.FK_ModelVolCatalog_SpecialID).WithMany(t => t.FK_ModelVolCatalog_SpecialID).HasForeignKey(d => d.SpecialID);
			#endregion
        }
    }
}  
