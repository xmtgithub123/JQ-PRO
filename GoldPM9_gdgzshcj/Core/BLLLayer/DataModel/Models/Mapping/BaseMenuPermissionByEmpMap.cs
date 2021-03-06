﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BaseMenuPermissionByEmpMap : EntityTypeConfiguration<BaseMenuPermissionByEmp>
    {
        public BaseMenuPermissionByEmpMap()
        {
			this.HasKey(t => new { t.BaseMenuPermissionByEmpValue,t.BaseMenuPermissionByEmpID}); 
				  
			this.Property(t => t.BaseMenuPermissionByEmpID).IsRequired();
	  
			this.Property(t => t.BaseMenuPermissionByEmpNote).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpPermissionValue).IsRequired();


			 // Table & Column Mappings
			this.ToTable("BaseMenuPermissionByEmp");
			this.Property(t => t.BaseMenuPermissionByEmpValue).HasColumnName("BaseMenuPermissionByEmpValue");
			this.Property(t => t.BaseMenuPermissionByEmpID).HasColumnName("BaseMenuPermissionByEmpID");
			this.Property(t => t.BaseMenuPermissionByEmpNote).HasColumnName("BaseMenuPermissionByEmpNote");
			this.Property(t => t.EmpPermissionValue).HasColumnName("EmpPermissionValue");
			           
			#region Relationships
			this.HasRequired(t => t.FK_BaseMenuPermissionByEmp_BaseMenuPermissionByEmpID).WithMany(t => t.FK_BaseMenuPermissionByEmp_BaseMenuPermissionByEmpID).HasForeignKey(d => d.BaseMenuPermissionByEmpID);
			this.HasRequired(t => t.FK_BaseMenuPermissionByEmp_BaseMenuPermissionByEmpValue).WithMany(t => t.FK_BaseMenuPermissionByEmp_BaseMenuPermissionByEmpValue).HasForeignKey(d => d.BaseMenuPermissionByEmpValue);
			#endregion
        }
    }
}  
