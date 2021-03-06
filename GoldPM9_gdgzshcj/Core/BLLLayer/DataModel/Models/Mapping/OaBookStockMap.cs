﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class OaBookStockMap : EntityTypeConfiguration<OaBookStock>
    {
        public OaBookStockMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.BookID).IsRequired();
	  
			this.Property(t => t.BookActionID).IsRequired();
	  
			this.Property(t => t.BookCount).IsRequired();
	  
			this.Property(t => t.BookDateTime).IsRequired();
	  
			this.Property(t => t.BookNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.BookFormID);
	  
			this.Property(t => t.BookFormType).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();


			 // Table & Column Mappings
			this.ToTable("OaBookStock");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.BookID).HasColumnName("BookID");
			this.Property(t => t.BookActionID).HasColumnName("BookActionID");
			this.Property(t => t.BookCount).HasColumnName("BookCount");
			this.Property(t => t.BookDateTime).HasColumnName("BookDateTime");
			this.Property(t => t.BookNote).HasColumnName("BookNote");
			this.Property(t => t.BookFormID).HasColumnName("BookFormID");
			this.Property(t => t.BookFormType).HasColumnName("BookFormType");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			           
			#region Relationships
			this.HasRequired(t => t.FK_OaBookStock_BookID).WithMany(t => t.FK_OaBookStock_BookID).HasForeignKey(d => d.BookID);
			#endregion
        }
    }
}  
