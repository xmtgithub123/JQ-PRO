﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class OaEquipmentMap : EntityTypeConfiguration<OaEquipment>
    {
        public OaEquipmentMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.EquipName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.EquipNumber).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.EquipBrand).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EquipModel).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EquipTypeID).IsRequired();
	  
			this.Property(t => t.EquipTotalCount).IsRequired();
	  
			this.Property(t => t.EquipBuyDate).IsRequired();
	  
			this.Property(t => t.EquipNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.EquipSort).IsRequired();
	  
			this.Property(t => t.EquipOrOffice).IsRequired();
	  
			this.Property(t => t.ManageDepartID).IsRequired();
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeleterEmpId).IsRequired();
	  
			this.Property(t => t.DeleterEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeletionTime).IsRequired();


			 // Table & Column Mappings
			this.ToTable("OaEquipment");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.EquipName).HasColumnName("EquipName");
			this.Property(t => t.EquipNumber).HasColumnName("EquipNumber");
			this.Property(t => t.EquipBrand).HasColumnName("EquipBrand");
			this.Property(t => t.EquipModel).HasColumnName("EquipModel");
			this.Property(t => t.EquipTypeID).HasColumnName("EquipTypeID");
			this.Property(t => t.EquipTotalCount).HasColumnName("EquipTotalCount");
			this.Property(t => t.EquipBuyDate).HasColumnName("EquipBuyDate");
			this.Property(t => t.EquipNote).HasColumnName("EquipNote");
			this.Property(t => t.EquipSort).HasColumnName("EquipSort");
			this.Property(t => t.EquipOrOffice).HasColumnName("EquipOrOffice");
			this.Property(t => t.ManageDepartID).HasColumnName("ManageDepartID");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			this.Property(t => t.DeleterEmpName).HasColumnName("DeleterEmpName");
			this.Property(t => t.DeletionTime).HasColumnName("DeletionTime");
			           
			#region Relationships
			this.HasRequired(t => t.FK_OaEquipment_EquipTypeID).WithMany(t => t.FK_OaEquipment_EquipTypeID).HasForeignKey(d => d.EquipTypeID);
			this.HasRequired(t => t.FK_OaEquipment_ManageDepartID).WithMany(t => t.FK_OaEquipment_ManageDepartID).HasForeignKey(d => d.ManageDepartID);
			#endregion
        }
    }
}  
