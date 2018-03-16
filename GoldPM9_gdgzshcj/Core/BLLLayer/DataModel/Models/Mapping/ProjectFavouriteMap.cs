﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ProjectFavouriteMap : EntityTypeConfiguration<ProjectFavourite>
    {
        public ProjectFavouriteMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ProjectId).IsRequired();
	  
			this.Property(t => t.EmpID).IsRequired();
	  
			this.Property(t => t.Note).IsRequired().HasMaxLength(512);
	  
			this.Property(t => t.CreationTime).IsRequired();


			 // Table & Column Mappings
			this.ToTable("ProjectFavourite");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ProjectId).HasColumnName("ProjectId");
			this.Property(t => t.EmpID).HasColumnName("EmpID");
			this.Property(t => t.Note).HasColumnName("Note");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			           
			#region Relationships
			this.HasRequired(t => t.FK_ProjectFavourite_ProjectId).WithMany(t => t.FK_ProjectFavourite_ProjectId).HasForeignKey(d => d.ProjectId);
			#endregion
        }
    }
}  
