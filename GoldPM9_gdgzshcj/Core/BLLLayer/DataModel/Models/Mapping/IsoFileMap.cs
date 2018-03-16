﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-08 13:56:52
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class IsoFileMap : EntityTypeConfiguration<IsoFile>
    {
        public IsoFileMap()
        {
			this.HasKey(t => new { t.FileID}); 
				  
			this.Property(t => t.FileType).IsRequired();
	  
			this.Property(t => t.FileName).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.FileNumber).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FileReNumber).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FileVersion).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FileDate).IsRequired();
	  
			this.Property(t => t.FileIsDeleted).IsRequired();
	  
			this.Property(t => t.FileIsModules).IsRequired();
	  
			this.Property(t => t.FileTypeName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FileZFDate).IsRequired();
	  
			this.Property(t => t.FileNote).IsRequired().HasMaxLength(500);
	  
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
			this.ToTable("IsoFile");
			this.Property(t => t.FileID).HasColumnName("FileID");
			this.Property(t => t.FileType).HasColumnName("FileType");
			this.Property(t => t.FileName).HasColumnName("FileName");
			this.Property(t => t.FileNumber).HasColumnName("FileNumber");
			this.Property(t => t.FileReNumber).HasColumnName("FileReNumber");
			this.Property(t => t.FileVersion).HasColumnName("FileVersion");
			this.Property(t => t.FileDate).HasColumnName("FileDate");
			this.Property(t => t.FileIsDeleted).HasColumnName("FileIsDeleted");
			this.Property(t => t.FileIsModules).HasColumnName("FileIsModules");
			this.Property(t => t.FileTypeName).HasColumnName("FileTypeName");
			this.Property(t => t.FileZFDate).HasColumnName("FileZFDate");
			this.Property(t => t.FileNote).HasColumnName("FileNote");
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