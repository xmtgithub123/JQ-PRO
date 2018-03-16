﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class DesExchMap : EntityTypeConfiguration<DesExch>
    {
        public DesExchMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
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
	  
			this.Property(t => t.ProjId).IsRequired();

            this.Property(t => t.PhaseId).IsRequired();
	  
			this.Property(t => t.taskGroupId).IsRequired();
	  
			this.Property(t => t.ExchSpecId).IsRequired();
	  
			this.Property(t => t.ExchSpecName).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.ExchType).IsRequired();
	  
			this.Property(t => t.ExchPriority).IsRequired();
	  
			this.Property(t => t.FlowId).IsRequired();
	  
			this.Property(t => t.ExchEmpId).IsRequired();
	  
			this.Property(t => t.ExchEmpName).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.ExchEmpDepId).IsRequired();
	  
			this.Property(t => t.ExchEmpDepName).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.ExchTitle).IsRequired().HasMaxLength(2000);
	  
			this.Property(t => t.ExchContent).IsRequired().HasMaxLength(4000);
	  
			this.Property(t => t.ExchIsInvalid).IsRequired();
            this.Property(t => t.ExchTaskId).IsRequired();


			 // Table & Column Mappings
			this.ToTable("DesExch");
			this.Property(t => t.Id).HasColumnName("Id");
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
			this.Property(t => t.ProjId).HasColumnName("ProjId");
			this.Property(t => t.taskGroupId).HasColumnName("taskGroupId");
			this.Property(t => t.ExchSpecId).HasColumnName("ExchSpecId");
            this.Property(t => t.PhaseId).HasColumnName("PhaseId");
			this.Property(t => t.ExchSpecName).HasColumnName("ExchSpecName");
			this.Property(t => t.ExchType).HasColumnName("ExchType");
			this.Property(t => t.ExchPriority).HasColumnName("ExchPriority");
			this.Property(t => t.FlowId).HasColumnName("FlowId");
			this.Property(t => t.ExchEmpId).HasColumnName("ExchEmpId");
			this.Property(t => t.ExchEmpName).HasColumnName("ExchEmpName");
			this.Property(t => t.ExchEmpDepId).HasColumnName("ExchEmpDepId");
			this.Property(t => t.ExchEmpDepName).HasColumnName("ExchEmpDepName");
			this.Property(t => t.ExchTitle).HasColumnName("ExchTitle");
			this.Property(t => t.ExchContent).HasColumnName("ExchContent");
			this.Property(t => t.ExchIsInvalid).HasColumnName("ExchIsInvalid");
            this.Property(t => t.ExchTaskId).HasColumnName("ExchTaskId");			           
			#region Relationships
			#endregion
        }
    }
}  