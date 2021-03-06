﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BaseAttachMap : EntityTypeConfiguration<BaseAttach>
    {
        public BaseAttachMap()
        {
			this.HasKey(t => new { t.AttachID}); 
				  
			this.Property(t => t.AttachRefID).IsRequired();
	  
			this.Property(t => t.AttachRefTable).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.AttachGroupID).IsRequired();
	  
			this.Property(t => t.AttachName).IsRequired().HasMaxLength(4000);
	  
			this.Property(t => t.AttachExt).IsRequired().HasMaxLength(10);
	  
			this.Property(t => t.AttachParentID).IsRequired();
	  
			this.Property(t => t.AttachOrderNum).IsRequired();
	  
			this.Property(t => t.AttachOrderPath).IsRequired().HasMaxLength(8000);
	  
			this.Property(t => t.AttachPathIDs).IsRequired().HasMaxLength(8000);
	  
			this.Property(t => t.AttachLevel).IsRequired();
	  
			this.Property(t => t.AttachDir).IsRequired().HasMaxLength(8000);
	  
			this.Property(t => t.AttachSize).IsRequired();
	  
			this.Property(t => t.AttachDateUpload).IsRequired();
	  
			this.Property(t => t.AttachDateChange).IsRequired();
	  
			this.Property(t => t.AttachEmpID).IsRequired();
	  
			this.Property(t => t.AttachEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.AttachVer).IsRequired();
	  
			this.Property(t => t.AttachTag).IsRequired().HasMaxLength(4000);
	  
			this.Property(t => t.AttachGrade).IsRequired();
	  
			this.Property(t => t.ColAttType1).IsRequired();
	  
			this.Property(t => t.ColAttType2).IsRequired();
	  
			this.Property(t => t.ColAttXml).IsRequired();


			 // Table & Column Mappings
			this.ToTable("BaseAttach");
			this.Property(t => t.AttachID).HasColumnName("AttachID");
			this.Property(t => t.AttachRefID).HasColumnName("AttachRefID");
			this.Property(t => t.AttachRefTable).HasColumnName("AttachRefTable");
			this.Property(t => t.AttachGroupID).HasColumnName("AttachGroupID");
			this.Property(t => t.AttachName).HasColumnName("AttachName");
			this.Property(t => t.AttachExt).HasColumnName("AttachExt");
			this.Property(t => t.AttachParentID).HasColumnName("AttachParentID");
			this.Property(t => t.AttachOrderNum).HasColumnName("AttachOrderNum");
			this.Property(t => t.AttachOrderPath).HasColumnName("AttachOrderPath");
			this.Property(t => t.AttachPathIDs).HasColumnName("AttachPathIDs");
			this.Property(t => t.AttachLevel).HasColumnName("AttachLevel");
			this.Property(t => t.AttachDir).HasColumnName("AttachDir");
			this.Property(t => t.AttachSize).HasColumnName("AttachSize");
			this.Property(t => t.AttachDateUpload).HasColumnName("AttachDateUpload");
			this.Property(t => t.AttachDateChange).HasColumnName("AttachDateChange");
			this.Property(t => t.AttachEmpID).HasColumnName("AttachEmpID");
			this.Property(t => t.AttachEmpName).HasColumnName("AttachEmpName");
			this.Property(t => t.AttachVer).HasColumnName("AttachVer");
			this.Property(t => t.AttachTag).HasColumnName("AttachTag");
			this.Property(t => t.AttachGrade).HasColumnName("AttachGrade");
			this.Property(t => t.ColAttType1).HasColumnName("ColAttType1");
			this.Property(t => t.ColAttType2).HasColumnName("ColAttType2");
			this.Property(t => t.ColAttXml).HasColumnName("ColAttXml");
			           
			#region Relationships
			#endregion
        }
    }
}  
