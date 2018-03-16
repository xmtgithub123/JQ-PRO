﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ArchShareFolderFileMap : EntityTypeConfiguration<ArchShareFolderFile>
    {
        public ArchShareFolderFileMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ArchShareFolderId).IsRequired();
	  
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
			this.ToTable("ArchShareFolderFile");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ArchShareFolderId).HasColumnName("ArchShareFolderId");
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
			this.HasRequired(t => t.FK_ArchShareFolderFile_ArchShareFolderId).WithMany(t => t.FK_ArchShareFolderFile_ArchShareFolderId).HasForeignKey(d => d.ArchShareFolderId);
			#endregion
        }
    }
}  