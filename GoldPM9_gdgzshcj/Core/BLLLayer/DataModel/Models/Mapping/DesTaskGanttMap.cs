﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class DesTaskGanttMap : EntityTypeConfiguration<DesTaskGantt>
    {
        public DesTaskGanttMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ProjId).IsRequired();
	  
			this.Property(t => t.TypeId).IsRequired();
	  
			this.Property(t => t.TaskId).IsRequired();
	  
			this.Property(t => t.ParentId).IsRequired();
	  
			this.Property(t => t.ManageEmpId).IsRequired();
	  
			this.Property(t => t.Name).IsRequired();
	  
			this.Property(t => t.Depends).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.Progress).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.Duration).IsRequired();
	  
			this.Property(t => t.Path).IsRequired().HasMaxLength(255);
	  
			this.Property(t => t.Level).IsRequired();
	  
			this.Property(t => t.DatePlanStart).IsRequired();
	  
			this.Property(t => t.DatePlanFinish).IsRequired();
	  
			this.Property(t => t.KeyPointType).IsRequired();
	  
			this.Property(t => t.startIsMilestone).IsRequired();
	  
			this.Property(t => t.endIsMilestone).IsRequired();
	  
			this.Property(t => t.Description).IsRequired();
	  
			this.Property(t => t.Order).IsRequired();
	  
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
			this.ToTable("DesTaskGantt");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ProjId).HasColumnName("ProjId");
			this.Property(t => t.TypeId).HasColumnName("TypeId");
			this.Property(t => t.TaskId).HasColumnName("TaskId");
			this.Property(t => t.ParentId).HasColumnName("ParentId");
			this.Property(t => t.ManageEmpId).HasColumnName("ManageEmpId");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.Depends).HasColumnName("Depends");
			this.Property(t => t.Progress).HasColumnName("Progress");
			this.Property(t => t.Duration).HasColumnName("Duration");
			this.Property(t => t.Path).HasColumnName("Path");
			this.Property(t => t.Level).HasColumnName("Level");
			this.Property(t => t.DatePlanStart).HasColumnName("DatePlanStart");
			this.Property(t => t.DatePlanFinish).HasColumnName("DatePlanFinish");
			this.Property(t => t.KeyPointType).HasColumnName("KeyPointType");
			this.Property(t => t.startIsMilestone).HasColumnName("startIsMilestone");
			this.Property(t => t.endIsMilestone).HasColumnName("endIsMilestone");
			this.Property(t => t.Description).HasColumnName("Description");
			this.Property(t => t.Order).HasColumnName("Order");
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
			#endregion
        }
    }
}  