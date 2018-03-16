﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class OaMessMap : EntityTypeConfiguration<OaMess>
    {
        public OaMessMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.MessDate).IsRequired();
	  
			this.Property(t => t.MessTag).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.MessTitle).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.MessLinkTitle).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.MessLinkUrl).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.MessNote).IsRequired();
	  
			this.Property(t => t.MessIsAutoReturn).IsRequired();
	  
			this.Property(t => t.MessEmpId).IsRequired();
	  
			this.Property(t => t.MessEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.MessIsSystem).IsRequired();
	  
			this.Property(t => t.MessIsDeleted).IsRequired();
	  
			this.Property(t => t.MessRefTable).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.MessRefID).IsRequired();


			 // Table & Column Mappings
			this.ToTable("OaMess");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.MessDate).HasColumnName("MessDate");
			this.Property(t => t.MessTag).HasColumnName("MessTag");
			this.Property(t => t.MessTitle).HasColumnName("MessTitle");
			this.Property(t => t.MessLinkTitle).HasColumnName("MessLinkTitle");
			this.Property(t => t.MessLinkUrl).HasColumnName("MessLinkUrl");
			this.Property(t => t.MessNote).HasColumnName("MessNote");
			this.Property(t => t.MessIsAutoReturn).HasColumnName("MessIsAutoReturn");
			this.Property(t => t.MessEmpId).HasColumnName("MessEmpId");
			this.Property(t => t.MessEmpName).HasColumnName("MessEmpName");
			this.Property(t => t.MessIsSystem).HasColumnName("MessIsSystem");
			this.Property(t => t.MessIsDeleted).HasColumnName("MessIsDeleted");
			this.Property(t => t.MessRefTable).HasColumnName("MessRefTable");
			this.Property(t => t.MessRefID).HasColumnName("MessRefID");
			           
			#region Relationships
			#endregion
        }
    }
}  
