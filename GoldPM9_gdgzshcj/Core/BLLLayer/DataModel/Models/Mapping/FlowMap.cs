﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class FlowMap : EntityTypeConfiguration<Flow>
    {
        public FlowMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.FlowRefTable).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.FlowRefID).IsRequired();
	  
			this.Property(t => t.FlowName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.FlowNumber).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.FlowVersion).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.FlowIsRun).IsRequired();
	  
			this.Property(t => t.FlowUrl).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.FlowRole).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.FlowNum).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.FlowIsWord).IsRequired();
	  
			this.Property(t => t.FlowStartDate).IsRequired();
	  
			this.Property(t => t.FlowFinishDate).IsRequired();
	  
			this.Property(t => t.FlowFinishSend).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.FlowFinishControls).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.FlowStatusID).IsRequired();
	  
			this.Property(t => t.FlowStatusName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.FlowControlXml).IsRequired();
	  
			this.Property(t => t.FlowXml).IsRequired();
	  
			this.Property(t => t.FlowByte);
	  
			this.Property(t => t.FlowListUrl).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.FlowModelID).IsRequired();
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.AgenEmpId).IsRequired();
	  
			this.Property(t => t.AgenEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FlowTitle).IsRequired().HasMaxLength(126);
	  
			this.Property(t => t.FlowModular).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.FlowSign).IsRequired().HasMaxLength(100);


			 // Table & Column Mappings
			this.ToTable("Flow");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.FlowRefTable).HasColumnName("FlowRefTable");
			this.Property(t => t.FlowRefID).HasColumnName("FlowRefID");
			this.Property(t => t.FlowName).HasColumnName("FlowName");
			this.Property(t => t.FlowNumber).HasColumnName("FlowNumber");
			this.Property(t => t.FlowVersion).HasColumnName("FlowVersion");
			this.Property(t => t.FlowIsRun).HasColumnName("FlowIsRun");
			this.Property(t => t.FlowUrl).HasColumnName("FlowUrl");
			this.Property(t => t.FlowRole).HasColumnName("FlowRole");
			this.Property(t => t.FlowNum).HasColumnName("FlowNum");
			this.Property(t => t.FlowIsWord).HasColumnName("FlowIsWord");
			this.Property(t => t.FlowStartDate).HasColumnName("FlowStartDate");
			this.Property(t => t.FlowFinishDate).HasColumnName("FlowFinishDate");
			this.Property(t => t.FlowFinishSend).HasColumnName("FlowFinishSend");
			this.Property(t => t.FlowFinishControls).HasColumnName("FlowFinishControls");
			this.Property(t => t.FlowStatusID).HasColumnName("FlowStatusID");
			this.Property(t => t.FlowStatusName).HasColumnName("FlowStatusName");
			this.Property(t => t.FlowControlXml).HasColumnName("FlowControlXml");
			this.Property(t => t.FlowXml).HasColumnName("FlowXml");
			this.Property(t => t.FlowByte).HasColumnName("FlowByte");
			this.Property(t => t.FlowListUrl).HasColumnName("FlowListUrl");
			this.Property(t => t.FlowModelID).HasColumnName("FlowModelID");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.AgenEmpId).HasColumnName("AgenEmpId");
			this.Property(t => t.AgenEmpName).HasColumnName("AgenEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.FlowTitle).HasColumnName("FlowTitle");
			this.Property(t => t.FlowModular).HasColumnName("FlowModular");
			this.Property(t => t.FlowSign).HasColumnName("FlowSign");
			           
			#region Relationships
			this.HasRequired(t => t.FK_Flow_FlowModelID).WithMany(t => t.FK_Flow_FlowModelID).HasForeignKey(d => d.FlowModelID);
			#endregion
        }
    }
}  