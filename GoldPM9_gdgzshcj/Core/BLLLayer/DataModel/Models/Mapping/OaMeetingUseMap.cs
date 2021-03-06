﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class OaMeetingUseMap : EntityTypeConfiguration<OaMeetingUse>
    {
        public OaMeetingUseMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.MeetingStartDate).IsRequired();
	  
			this.Property(t => t.MeetingEndDate).IsRequired();
	  
			this.Property(t => t.MeetingID).IsRequired();
	  
			this.Property(t => t.MeetingLeader).IsRequired();
	  
			this.Property(t => t.MeetingUseConent).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.State).IsRequired();
	  
			this.Property(t => t.MeetingJoinPeo).IsRequired();
	  
			this.Property(t => t.MeetingNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FlowID).IsRequired();
	  
			this.Property(t => t.FlowTime).IsRequired();
	  
			this.Property(t => t.AgenEmpId).IsRequired();
	  
			this.Property(t => t.AgenEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeleterEmpId).IsRequired();
	  
			this.Property(t => t.DeleterEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeletionTime).IsRequired();


			 // Table & Column Mappings
			this.ToTable("OaMeetingUse");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.MeetingStartDate).HasColumnName("MeetingStartDate");
			this.Property(t => t.MeetingEndDate).HasColumnName("MeetingEndDate");
			this.Property(t => t.MeetingID).HasColumnName("MeetingID");
			this.Property(t => t.MeetingLeader).HasColumnName("MeetingLeader");
			this.Property(t => t.MeetingUseConent).HasColumnName("MeetingUseConent");
			this.Property(t => t.State).HasColumnName("State");
			this.Property(t => t.MeetingJoinPeo).HasColumnName("MeetingJoinPeo");
			this.Property(t => t.MeetingNote).HasColumnName("MeetingNote");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.FlowID).HasColumnName("FlowID");
			this.Property(t => t.FlowTime).HasColumnName("FlowTime");
			this.Property(t => t.AgenEmpId).HasColumnName("AgenEmpId");
			this.Property(t => t.AgenEmpName).HasColumnName("AgenEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			this.Property(t => t.DeleterEmpName).HasColumnName("DeleterEmpName");
			this.Property(t => t.DeletionTime).HasColumnName("DeletionTime");
			           
			#region Relationships
			this.HasRequired(t => t.FK_OaMeetingUse_MeetingID).WithMany(t => t.FK_OaMeetingUse_MeetingID).HasForeignKey(d => d.MeetingID);
			#endregion
        }
    }
}  
