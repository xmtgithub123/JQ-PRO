﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ArchPaperFileMap : EntityTypeConfiguration<ArchPaperFile>
    {
        public ArchPaperFileMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ArchPaperId).IsRequired();
	  
			this.Property(t => t.ParentId).IsRequired();
	  
			this.Property(t => t.FileOrder).IsRequired();
	  
			this.Property(t => t.FileArchNumber).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FileArchOrder).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FileDwgNumber).IsRequired().HasMaxLength(20);
	  
			this.Property(t => t.FileArchName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FileArchType).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FileArchKind).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.LocatorNumber).IsRequired().HasMaxLength(20);
	  
			this.Property(t => t.BoxNumber).IsRequired().HasMaxLength(20);
	  
			this.Property(t => t.ArchConNumber).IsRequired().HasMaxLength(20);
	  
			this.Property(t => t.ArchConName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.ArchConMoney).IsRequired().HasPrecision(18,4);
	  
			this.Property(t => t.ArchConDate).IsRequired();
	  
			this.Property(t => t.Scret).IsRequired();
	  
			this.Property(t => t.RefNumber).IsRequired().HasMaxLength(20);
	  
			this.Property(t => t.ArchSize).IsRequired().HasMaxLength(20);
	  
			this.Property(t => t.ArchUnit).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.UserId).IsRequired();
	  
			this.Property(t => t.UserName).IsRequired().HasMaxLength(20);
	  
			this.Property(t => t.ArchDate).IsRequired();
	  
			this.Property(t => t.CheckEmpId).IsRequired();
	  
			this.Property(t => t.CheckEmpName).IsRequired().HasMaxLength(20);
	  
			this.Property(t => t.CheckDate).IsRequired();
	  
			this.Property(t => t.ArchiveDate).IsRequired();
	  
			this.Property(t => t.ExpiredDate).IsRequired();
	  
			this.Property(t => t.FileBeginDate).IsRequired();
	  
			this.Property(t => t.FileEndDate).IsRequired();
	  
			this.Property(t => t.BeginPage).IsRequired();
	  
			this.Property(t => t.EndPage).IsRequired();
	  
			this.Property(t => t.Pages).IsRequired();
	  
			this.Property(t => t.ArchCount).IsRequired();
	  
			this.Property(t => t.TotalPages).IsRequired();
	  
			this.Property(t => t.DwgPages).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DwgPages4A4).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.ArchContent).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ChangeRemark).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.IndexTitle).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.ArchSource).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.Note).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.ArchXml).IsRequired();
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepID).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("ArchPaperFile");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ArchPaperId).HasColumnName("ArchPaperId");
			this.Property(t => t.ParentId).HasColumnName("ParentId");
			this.Property(t => t.FileOrder).HasColumnName("FileOrder");
			this.Property(t => t.FileArchNumber).HasColumnName("FileArchNumber");
			this.Property(t => t.FileArchOrder).HasColumnName("FileArchOrder");
			this.Property(t => t.FileDwgNumber).HasColumnName("FileDwgNumber");
			this.Property(t => t.FileArchName).HasColumnName("FileArchName");
			this.Property(t => t.FileArchType).HasColumnName("FileArchType");
			this.Property(t => t.FileArchKind).HasColumnName("FileArchKind");
			this.Property(t => t.LocatorNumber).HasColumnName("LocatorNumber");
			this.Property(t => t.BoxNumber).HasColumnName("BoxNumber");
			this.Property(t => t.ArchConNumber).HasColumnName("ArchConNumber");
			this.Property(t => t.ArchConName).HasColumnName("ArchConName");
			this.Property(t => t.ArchConMoney).HasColumnName("ArchConMoney");
			this.Property(t => t.ArchConDate).HasColumnName("ArchConDate");
			this.Property(t => t.Scret).HasColumnName("Scret");
			this.Property(t => t.RefNumber).HasColumnName("RefNumber");
			this.Property(t => t.ArchSize).HasColumnName("ArchSize");
			this.Property(t => t.ArchUnit).HasColumnName("ArchUnit");
			this.Property(t => t.UserId).HasColumnName("UserId");
			this.Property(t => t.UserName).HasColumnName("UserName");
			this.Property(t => t.ArchDate).HasColumnName("ArchDate");
			this.Property(t => t.CheckEmpId).HasColumnName("CheckEmpId");
			this.Property(t => t.CheckEmpName).HasColumnName("CheckEmpName");
			this.Property(t => t.CheckDate).HasColumnName("CheckDate");
			this.Property(t => t.ArchiveDate).HasColumnName("ArchiveDate");
			this.Property(t => t.ExpiredDate).HasColumnName("ExpiredDate");
			this.Property(t => t.FileBeginDate).HasColumnName("FileBeginDate");
			this.Property(t => t.FileEndDate).HasColumnName("FileEndDate");
			this.Property(t => t.BeginPage).HasColumnName("BeginPage");
			this.Property(t => t.EndPage).HasColumnName("EndPage");
			this.Property(t => t.Pages).HasColumnName("Pages");
			this.Property(t => t.ArchCount).HasColumnName("ArchCount");
			this.Property(t => t.TotalPages).HasColumnName("TotalPages");
			this.Property(t => t.DwgPages).HasColumnName("DwgPages");
			this.Property(t => t.DwgPages4A4).HasColumnName("DwgPages4A4");
			this.Property(t => t.ArchContent).HasColumnName("ArchContent");
			this.Property(t => t.ChangeRemark).HasColumnName("ChangeRemark");
			this.Property(t => t.IndexTitle).HasColumnName("IndexTitle");
			this.Property(t => t.ArchSource).HasColumnName("ArchSource");
			this.Property(t => t.Note).HasColumnName("Note");
			this.Property(t => t.ArchXml).HasColumnName("ArchXml");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepID).HasColumnName("CreatorDepID");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			           
			#region Relationships
			this.HasRequired(t => t.FK_ArchPaperFile_ArchPaperId).WithMany(t => t.FK_ArchPaperFile_ArchPaperId).HasForeignKey(d => d.ArchPaperId);
			#endregion
        }
    }
}  
