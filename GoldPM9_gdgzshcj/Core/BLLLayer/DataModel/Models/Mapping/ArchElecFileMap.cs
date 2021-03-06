﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ArchElecFileMap : EntityTypeConfiguration<ArchElecFile>
    {
        public ArchElecFileMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ArchProjId).IsRequired();
	  
			this.Property(t => t.ArchElecFileId).IsRequired();
	  
			this.Property(t => t.ArchElecFileRefTable).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.ParentId).IsRequired();
	  
			this.Property(t => t.ElecFileName).IsRequired().HasMaxLength(4000);
	  
			this.Property(t => t.ElecFileType).IsRequired();
	  
			this.Property(t => t.ElecSize).IsRequired();
	  
			this.Property(t => t.ElecFilePath).IsRequired().HasMaxLength(8000);
	  
			this.Property(t => t.ElecFileVersionId).IsRequired();
	  
			this.Property(t => t.ElecScret).IsRequired();
	  
			this.Property(t => t.ElecExt).IsRequired().HasMaxLength(10);
	  
			this.Property(t => t.ElecFileUnit).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FileEmpId).IsRequired();
	  
			this.Property(t => t.FileEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.Note).IsRequired().HasMaxLength(8000);
	  
			this.Property(t => t.ElecFileXml).IsRequired();
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepID).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("ArchElecFile");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ArchProjId).HasColumnName("ArchProjId");
			this.Property(t => t.ArchElecFileId).HasColumnName("ArchElecFileId");
			this.Property(t => t.ArchElecFileRefTable).HasColumnName("ArchElecFileRefTable");
			this.Property(t => t.ParentId).HasColumnName("ParentId");
			this.Property(t => t.ElecFileName).HasColumnName("ElecFileName");
			this.Property(t => t.ElecFileType).HasColumnName("ElecFileType");
			this.Property(t => t.ElecSize).HasColumnName("ElecSize");
			this.Property(t => t.ElecFilePath).HasColumnName("ElecFilePath");
			this.Property(t => t.ElecFileVersionId).HasColumnName("ElecFileVersionId");
			this.Property(t => t.ElecScret).HasColumnName("ElecScret");
			this.Property(t => t.ElecExt).HasColumnName("ElecExt");
			this.Property(t => t.ElecFileUnit).HasColumnName("ElecFileUnit");
			this.Property(t => t.FileEmpId).HasColumnName("FileEmpId");
			this.Property(t => t.FileEmpName).HasColumnName("FileEmpName");
			this.Property(t => t.Note).HasColumnName("Note");
			this.Property(t => t.ElecFileXml).HasColumnName("ElecFileXml");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepID).HasColumnName("CreatorDepID");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			           
			#region Relationships
			this.HasRequired(t => t.FK_ArchElecFile_ArchElecFileId).WithMany(t => t.FK_ArchElecFile_ArchElecFileId).HasForeignKey(d => d.ArchElecFileId);
			this.HasRequired(t => t.FK_ArchElecFile_ArchProjId).WithMany(t => t.FK_ArchElecFile_ArchProjId).HasForeignKey(d => d.ArchProjId);
			#endregion
        }
    }
}  
