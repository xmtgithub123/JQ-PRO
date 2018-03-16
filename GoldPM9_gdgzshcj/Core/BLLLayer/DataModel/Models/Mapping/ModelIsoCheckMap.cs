﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ModelIsoCheckMap : EntityTypeConfiguration<ModelIsoCheck>
    {
        public ModelIsoCheckMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.PhaseID).IsRequired();
	  
			this.Property(t => t.SpecialID).IsRequired();
	  
			this.Property(t => t.CheckErrTypeID).IsRequired();
	  
			this.Property(t => t.CheckType).IsRequired();
	  
			this.Property(t => t.CheckItem).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CheckNote).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.DesTaskCheckId).IsRequired();
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepID).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeleterEmpId).IsRequired();
	  
			this.Property(t => t.DeleterEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeletionTime).IsRequired();
	  
			this.Property(t => t.AgenCreatorEmpId).IsRequired();
	  
			this.Property(t => t.AgenCreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.AgenLastModifierEmpId).IsRequired();
	  
			this.Property(t => t.AgenLastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.AgenDeleterEmpId).IsRequired();
	  
			this.Property(t => t.AgenDeleterEmpName).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("ModelIsoCheck");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.PhaseID).HasColumnName("PhaseID");
			this.Property(t => t.SpecialID).HasColumnName("SpecialID");
			this.Property(t => t.CheckErrTypeID).HasColumnName("CheckErrTypeID");
			this.Property(t => t.CheckType).HasColumnName("CheckType");
			this.Property(t => t.CheckItem).HasColumnName("CheckItem");
			this.Property(t => t.CheckNote).HasColumnName("CheckNote");
			this.Property(t => t.DesTaskCheckId).HasColumnName("DesTaskCheckId");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepID).HasColumnName("CreatorDepID");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			this.Property(t => t.DeleterEmpName).HasColumnName("DeleterEmpName");
			this.Property(t => t.DeletionTime).HasColumnName("DeletionTime");
			this.Property(t => t.AgenCreatorEmpId).HasColumnName("AgenCreatorEmpId");
			this.Property(t => t.AgenCreatorEmpName).HasColumnName("AgenCreatorEmpName");
			this.Property(t => t.AgenLastModifierEmpId).HasColumnName("AgenLastModifierEmpId");
			this.Property(t => t.AgenLastModifierEmpName).HasColumnName("AgenLastModifierEmpName");
			this.Property(t => t.AgenDeleterEmpId).HasColumnName("AgenDeleterEmpId");
			this.Property(t => t.AgenDeleterEmpName).HasColumnName("AgenDeleterEmpName");
			           
			#region Relationships
			this.HasRequired(t => t.FK_ModelIsoCheck_CheckType).WithMany(t => t.FK_ModelIsoCheck_CheckType).HasForeignKey(d => d.CheckType);
			this.HasRequired(t => t.FK_ModelIsoCheck_PhaseID).WithMany(t => t.FK_ModelIsoCheck_PhaseID).HasForeignKey(d => d.PhaseID);
			this.HasRequired(t => t.FK_ModelIsoCheck_SpecialID).WithMany(t => t.FK_ModelIsoCheck_SpecialID).HasForeignKey(d => d.SpecialID);
			#endregion
        }
    }
}  
