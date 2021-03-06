﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-04-20 09:38:46
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ArchPaperFolderMap : EntityTypeConfiguration<ArchPaperFolder>
    {
        public ArchPaperFolderMap()
        {
			this.HasKey(t => new { t.Id});

            this.Property(t => t.ProjId).IsRequired();

            this.Property(t => t.ProjNumber).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.ProjName).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.ArchNumber).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.ProjPhaseId).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.ProjPhaseName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ProjEmpId).IsRequired();
	  
			this.Property(t => t.ProjEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.Path).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.Remark).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepID).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("ArchPaperFolder");
			this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProjId).HasColumnName("ProjId");
            this.Property(t => t.ProjNumber).HasColumnName("ProjNumber");
			this.Property(t => t.ProjName).HasColumnName("ProjName");
			this.Property(t => t.ArchNumber).HasColumnName("ArchNumber");
			this.Property(t => t.ProjPhaseId).HasColumnName("ProjPhaseId");
			this.Property(t => t.ProjPhaseName).HasColumnName("ProjPhaseName");
			this.Property(t => t.ProjEmpId).HasColumnName("ProjEmpId");
			this.Property(t => t.ProjEmpName).HasColumnName("ProjEmpName");
			this.Property(t => t.Path).HasColumnName("Path");
			this.Property(t => t.Remark).HasColumnName("Remark");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepID).HasColumnName("CreatorDepID");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			           
			#region Relationships
			#endregion
        }
    }
}  
