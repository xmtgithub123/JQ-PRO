﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ArchBorrowMap : EntityTypeConfiguration<ArchBorrow>
    {
        public ArchBorrowMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ProjectID).IsRequired();
	  
			this.Property(t => t.ArchPaperId).IsRequired();
	  
			this.Property(t => t.BorrowDate).IsRequired();
	  
			this.Property(t => t.BorrowPlanRetrunDate).IsRequired();
	  
			this.Property(t => t.BorrowEmpId).IsRequired();
	  
			this.Property(t => t.BorrowEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.BorrowNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.BorrowMgrEmpId).IsRequired();
	  
			this.Property(t => t.ReturnMgrEmpId).IsRequired();
	  
			this.Property(t => t.ReturnNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.RemindDate).IsRequired();
	  
			this.Property(t => t.RemindNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.FlowID).IsRequired();
	  
			this.Property(t => t.FlowTime).IsRequired();


			 // Table & Column Mappings
			this.ToTable("ArchBorrow");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ProjectID).HasColumnName("ProjectID");
			this.Property(t => t.ArchPaperId).HasColumnName("ArchPaperId");
			this.Property(t => t.BorrowDate).HasColumnName("BorrowDate");
			this.Property(t => t.BorrowPlanRetrunDate).HasColumnName("BorrowPlanRetrunDate");
			this.Property(t => t.BorrowEmpId).HasColumnName("BorrowEmpId");
			this.Property(t => t.BorrowEmpName).HasColumnName("BorrowEmpName");
			this.Property(t => t.BorrowNote).HasColumnName("BorrowNote");
			this.Property(t => t.BorrowMgrEmpId).HasColumnName("BorrowMgrEmpId");
			this.Property(t => t.ReturnMgrEmpId).HasColumnName("ReturnMgrEmpId");
			this.Property(t => t.ReturnNote).HasColumnName("ReturnNote");
			this.Property(t => t.RemindDate).HasColumnName("RemindDate");
			this.Property(t => t.RemindNote).HasColumnName("RemindNote");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.FlowID).HasColumnName("FlowID");
			this.Property(t => t.FlowTime).HasColumnName("FlowTime");
			           
			#region Relationships
			this.HasRequired(t => t.FK_ArchBorrow_ArchPaperId).WithMany(t => t.FK_ArchBorrow_ArchPaperId).HasForeignKey(d => d.ArchPaperId);
			#endregion
        }
    }
}  
