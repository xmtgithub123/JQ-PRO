﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BaseEmpPermissionMap : EntityTypeConfiguration<BaseEmpPermission>
    {
        public BaseEmpPermissionMap()
        {
			this.HasKey(t => new { t.PermissionEmpID,t.PermissionEmpValue}); 
				  
			this.Property(t => t.PermissionEmpValue).IsRequired();
	  
			this.Property(t => t.PermissionEmpNote).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("BaseEmpPermission");
			this.Property(t => t.PermissionEmpID).HasColumnName("PermissionEmpID");
			this.Property(t => t.PermissionEmpValue).HasColumnName("PermissionEmpValue");
			this.Property(t => t.PermissionEmpNote).HasColumnName("PermissionEmpNote");
			           
			#region Relationships
			this.HasRequired(t => t.FK_BaseEmpPermission_PermissionEmpID).WithMany(t => t.FK_BaseEmpPermission_PermissionEmpID).HasForeignKey(d => d.PermissionEmpID);
			this.HasRequired(t => t.FK_BaseEmpPermission_PermissionEmpValue).WithMany(t => t.FK_BaseEmpPermission_PermissionEmpValue).HasForeignKey(d => d.PermissionEmpValue);
			#endregion
        }
    }
}  
