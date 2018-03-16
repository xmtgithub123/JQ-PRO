﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-03-14 14:17:54
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class HREquipmentLedgerMap : EntityTypeConfiguration<HREquipmentLedger>
    {
        public HREquipmentLedgerMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.EquipmentName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EquipmentType).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.PurchasingTime).IsRequired();
	  
			this.Property(t => t.ScrapTime).IsRequired();
	  
			this.Property(t => t.EquipmentEmpId).IsRequired();
	  
			this.Property(t => t.EquipmentEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.State);
	  
			this.Property(t => t.DeletionTime).IsRequired();
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.AgenCreatorEmpId).IsRequired();
	  
			this.Property(t => t.AgenCreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.AgenLastModifierEmpId).IsRequired();
	  
			this.Property(t => t.AgenLastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.AgenDeleterEmpId).IsRequired();
	  
			this.Property(t => t.AgenDeleterEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeleterEmpId).IsRequired();
	  
			this.Property(t => t.DeleterEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CompanyID);

            this.Property(t => t.Remark).HasMaxLength(500);


            // Table & Column Mappings
            this.ToTable("HREquipmentLedger");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.EquipmentName).HasColumnName("EquipmentName");
			this.Property(t => t.EquipmentType).HasColumnName("EquipmentType");
			this.Property(t => t.PurchasingTime).HasColumnName("PurchasingTime");
			this.Property(t => t.ScrapTime).HasColumnName("ScrapTime");
			this.Property(t => t.EquipmentEmpId).HasColumnName("EquipmentEmpId");
			this.Property(t => t.EquipmentEmpName).HasColumnName("EquipmentEmpName");
			this.Property(t => t.State).HasColumnName("State");
			this.Property(t => t.DeletionTime).HasColumnName("DeletionTime");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.AgenCreatorEmpId).HasColumnName("AgenCreatorEmpId");
			this.Property(t => t.AgenCreatorEmpName).HasColumnName("AgenCreatorEmpName");
			this.Property(t => t.AgenLastModifierEmpId).HasColumnName("AgenLastModifierEmpId");
			this.Property(t => t.AgenLastModifierEmpName).HasColumnName("AgenLastModifierEmpName");
			this.Property(t => t.AgenDeleterEmpId).HasColumnName("AgenDeleterEmpId");
			this.Property(t => t.AgenDeleterEmpName).HasColumnName("AgenDeleterEmpName");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			this.Property(t => t.DeleterEmpName).HasColumnName("DeleterEmpName");
			this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.Remark).HasColumnName("Remark");

            #region Relationships
            #endregion
        }
    }
}  
