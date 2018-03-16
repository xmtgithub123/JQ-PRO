﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-11-16 17:30:36
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ProjManageMap : EntityTypeConfiguration<ProjManage>
    {
        public ProjManageMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.EngineeringID).IsRequired();
	  
			this.Property(t => t.Cost).IsRequired();
	  
			this.Property(t => t.Safe).IsRequired();
	  
			this.Property(t => t.Finished).IsRequired();
	  
			this.Property(t => t.Doc).IsRequired();
	  
			this.Property(t => t.Note).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorTime).IsRequired();
	  
			this.Property(t => t.EmpManageId).IsRequired();


			 // Table & Column Mappings
			this.ToTable("ProjManage");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.EngineeringID).HasColumnName("EngineeringID");
			this.Property(t => t.Cost).HasColumnName("Cost");
			this.Property(t => t.Safe).HasColumnName("Safe");
			this.Property(t => t.Finished).HasColumnName("Finished");
			this.Property(t => t.Doc).HasColumnName("Doc");
			this.Property(t => t.Note).HasColumnName("Note");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorTime).HasColumnName("CreatorTime");
			this.Property(t => t.EmpManageId).HasColumnName("EmpManageId");
			           
			#region Relationships
			#endregion
        }
    }
}  
