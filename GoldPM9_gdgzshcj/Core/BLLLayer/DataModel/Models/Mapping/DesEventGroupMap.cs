﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-26 17:27:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class DesEventGroupMap : EntityTypeConfiguration<DesEventGroup>
    {
        public DesEventGroupMap()
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
	  
			this.Property(t => t.EventGroupRefTable).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.EventGroupRefId).IsRequired();
	  
			this.Property(t => t.EventGroupParentId).IsRequired();
	  
			this.Property(t => t.EventGroupPathIds).IsRequired().HasMaxLength(8000);
	  
			this.Property(t => t.EventGroupLevel).IsRequired();
	  
			this.Property(t => t.EventGroupOrderNum).IsRequired();
	  
			this.Property(t => t.EventGroupPath).IsRequired().HasMaxLength(8000);
	  
			this.Property(t => t.EventGroupName).IsRequired().HasMaxLength(200);


			 // Table & Column Mappings
			this.ToTable("DesEventGroup");
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
			this.Property(t => t.EventGroupRefTable).HasColumnName("EventGroupRefTable");
			this.Property(t => t.EventGroupRefId).HasColumnName("EventGroupRefId");
			this.Property(t => t.EventGroupParentId).HasColumnName("EventGroupParentId");
			this.Property(t => t.EventGroupPathIds).HasColumnName("EventGroupPathIds");
			this.Property(t => t.EventGroupLevel).HasColumnName("EventGroupLevel");
			this.Property(t => t.EventGroupOrderNum).HasColumnName("EventGroupOrderNum");
			this.Property(t => t.EventGroupPath).HasColumnName("EventGroupPath");
			this.Property(t => t.EventGroupName).HasColumnName("EventGroupName");
			           
			#region Relationships
			#endregion
        }
    }
}  
