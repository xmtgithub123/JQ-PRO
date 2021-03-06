﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ModelTreeMap : EntityTypeConfiguration<ModelTree>
    {
        public ModelTreeMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ParentId).IsRequired();
	  
			this.Property(t => t.NodeName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.QueryValue).IsRequired();
	  
			this.Property(t => t.refTable).IsRequired().HasMaxLength(20);
	  
			this.Property(t => t.RefFieldName).IsRequired();
	  
			this.Property(t => t.IsShow).IsRequired();
	  
			this.Property(t => t.IsDeleted).IsRequired();
	  
			this.Property(t => t.ActionUrl).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("ModelTree");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ParentId).HasColumnName("ParentId");
			this.Property(t => t.NodeName).HasColumnName("NodeName");
			this.Property(t => t.QueryValue).HasColumnName("QueryValue");
			this.Property(t => t.refTable).HasColumnName("refTable");
			this.Property(t => t.RefFieldName).HasColumnName("RefFieldName");
			this.Property(t => t.IsShow).HasColumnName("IsShow");
			this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
			this.Property(t => t.ActionUrl).HasColumnName("ActionUrl");
			           
			#region Relationships
			#endregion
        }
    }
}  
