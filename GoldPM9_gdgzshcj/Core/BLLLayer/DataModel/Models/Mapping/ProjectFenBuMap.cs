﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-12-30 10:44:32
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ProjectFenBuMap : EntityTypeConfiguration<ProjectFenBu>
    {
        public ProjectFenBuMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ProId).IsRequired();
	  
			this.Property(t => t.Point).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.Mem).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("ProjectFenBu");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ProId).HasColumnName("ProId");
			this.Property(t => t.Point).HasColumnName("Point");
			this.Property(t => t.Mem).HasColumnName("Mem");
			           
			#region Relationships
			#endregion
        }
    }
}  
