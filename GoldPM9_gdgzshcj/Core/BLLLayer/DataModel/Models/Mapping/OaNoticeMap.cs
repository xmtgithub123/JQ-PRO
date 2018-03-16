﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class OaNoticeMap : EntityTypeConfiguration<OaNotice>
    {
        public OaNoticeMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.Title).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.Content).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.NoticeTypeID).IsRequired();
	  
			this.Property(t => t.ReadCount).IsRequired();
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeleterEmpId).IsRequired();
	  
			this.Property(t => t.DeleterEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeletionTime).IsRequired();


			 // Table & Column Mappings
			this.ToTable("OaNotice");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Title).HasColumnName("Title");
			this.Property(t => t.Content).HasColumnName("Content");
			this.Property(t => t.NoticeTypeID).HasColumnName("NoticeTypeID");
			this.Property(t => t.ReadCount).HasColumnName("ReadCount");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			this.Property(t => t.DeleterEmpName).HasColumnName("DeleterEmpName");
			this.Property(t => t.DeletionTime).HasColumnName("DeletionTime");
			           
			#region Relationships
			#endregion
        }
    }
}  
