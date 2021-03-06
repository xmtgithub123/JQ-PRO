﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class OaNewMap : EntityTypeConfiguration<OaNew>
    {
        public OaNewMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.NewsTypeID).IsRequired();
	  
			this.Property(t => t.NewsTitle).IsRequired().HasMaxLength(250);
	  
			this.Property(t => t.NewsDate).IsRequired();
	  
			this.Property(t => t.NewsContent).IsRequired();
	  
			this.Property(t => t.NewsOrNotice).IsRequired();
	  
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
	  
			this.Property(t => t.NewsImage).IsRequired().HasMaxLength(255);
	  
			this.Property(t => t.NewsDescription).IsRequired().HasMaxLength(255);


			 // Table & Column Mappings
			this.ToTable("OaNew");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.NewsTypeID).HasColumnName("NewsTypeID");
			this.Property(t => t.NewsTitle).HasColumnName("NewsTitle");
			this.Property(t => t.NewsDate).HasColumnName("NewsDate");
			this.Property(t => t.NewsContent).HasColumnName("NewsContent");
			this.Property(t => t.NewsOrNotice).HasColumnName("NewsOrNotice");
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
			this.Property(t => t.NewsImage).HasColumnName("NewsImage");
			this.Property(t => t.NewsDescription).HasColumnName("NewsDescription");
			           
			#region Relationships
			this.HasRequired(t => t.FK_OaNew_NewsTypeID).WithMany(t => t.FK_OaNew_NewsTypeID).HasForeignKey(d => d.NewsTypeID);
			#endregion
        }
    }
}  
