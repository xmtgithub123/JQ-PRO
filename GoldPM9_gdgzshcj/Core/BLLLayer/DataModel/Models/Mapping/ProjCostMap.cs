﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-04-24 16:18:28
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ProjCostMap : EntityTypeConfiguration<ProjCost>
    {
        public ProjCostMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.XMCBID).IsRequired();
	  
			this.Property(t => t.ProjID).IsRequired();
	  
			this.Property(t => t.ProjPhaseId).IsRequired();
	  
			this.Property(t => t.DesTaskGroupId).IsRequired();
	  
			this.Property(t => t.CostId).IsRequired();
	  
			this.Property(t => t.CostName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CostType).IsRequired();
	  
			this.Property(t => t.ParentId).IsRequired();
	  
			this.Property(t => t.CostIsType).IsRequired();
	  
			this.Property(t => t.CostIsSum).IsRequired();
	  
			this.Property(t => t.CostTempFee).IsRequired().HasPrecision(18,4);
	  
			this.Property(t => t.CostFactFee).IsRequired().HasPrecision(18,4);
	  
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
	  
			this.Property(t => t.CompanyID).IsRequired();


			 // Table & Column Mappings
			this.ToTable("ProjCost");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.XMCBID).HasColumnName("XMCBID");
			this.Property(t => t.ProjID).HasColumnName("ProjID");
			this.Property(t => t.ProjPhaseId).HasColumnName("ProjPhaseId");
			this.Property(t => t.DesTaskGroupId).HasColumnName("DesTaskGroupId");
			this.Property(t => t.CostId).HasColumnName("CostId");
			this.Property(t => t.CostName).HasColumnName("CostName");
			this.Property(t => t.CostType).HasColumnName("CostType");
			this.Property(t => t.ParentId).HasColumnName("ParentId");
			this.Property(t => t.CostIsType).HasColumnName("CostIsType");
			this.Property(t => t.CostIsSum).HasColumnName("CostIsSum");
			this.Property(t => t.CostTempFee).HasColumnName("CostTempFee");
			this.Property(t => t.CostFactFee).HasColumnName("CostFactFee");
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
			this.Property(t => t.CompanyID).HasColumnName("CompanyID");
			           
			#region Relationships
			this.HasRequired(t => t.FK_ProjCost_ProjID).WithMany(t => t.FK_ProjCost_ProjID).HasForeignKey(d => d.ProjID);
			#endregion
        }
    }
}  