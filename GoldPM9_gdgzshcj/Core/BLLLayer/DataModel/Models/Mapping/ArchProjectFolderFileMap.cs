﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-01-04 16:01:40
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ArchProjectFolderFileMap : EntityTypeConfiguration<ArchProjectFolderFile>
    {
        public ArchProjectFolderFileMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ArchProjectFolderId).IsRequired();
	  
			this.Property(t => t.FileName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.FileRemark).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepID).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeletionTime).IsRequired();
	  
			this.Property(t => t.DeleterEmpId).IsRequired();
	  
			this.Property(t => t.DeleterEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FlowID).IsRequired();
	  
			this.Property(t => t.FlowTime).IsRequired();


			 // Table & Column Mappings
			this.ToTable("ArchProjectFolderFile");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ArchProjectFolderId).HasColumnName("ArchProjectFolderId");
			this.Property(t => t.FileName).HasColumnName("FileName");
			this.Property(t => t.FileRemark).HasColumnName("FileRemark");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepID).HasColumnName("CreatorDepID");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.DeletionTime).HasColumnName("DeletionTime");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			this.Property(t => t.DeleterEmpName).HasColumnName("DeleterEmpName");
			this.Property(t => t.FlowID).HasColumnName("FlowID");
			this.Property(t => t.FlowTime).HasColumnName("FlowTime");
			           
			#region Relationships
			#endregion
        }
    }
}  
