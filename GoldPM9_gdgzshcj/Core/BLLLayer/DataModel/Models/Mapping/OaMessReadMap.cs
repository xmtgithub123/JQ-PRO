﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class OaMessReadMap : EntityTypeConfiguration<OaMessRead>
    {
        public OaMessReadMap()
        {
			this.HasKey(t => new { t.Id,t.MessReadEmpId}); 
				  
			this.Property(t => t.MessReadEmpId).IsRequired();
	  
			this.Property(t => t.MessReadEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.MessReadDate).IsRequired();
	  
			this.Property(t => t.MessReadIsDeleted).IsRequired();
	  
			this.Property(t => t.MessReadNote).IsRequired().HasMaxLength(500);


			 // Table & Column Mappings
			this.ToTable("OaMessRead");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.MessReadEmpId).HasColumnName("MessReadEmpId");
			this.Property(t => t.MessReadEmpName).HasColumnName("MessReadEmpName");
			this.Property(t => t.MessReadDate).HasColumnName("MessReadDate");
			this.Property(t => t.MessReadIsDeleted).HasColumnName("MessReadIsDeleted");
			this.Property(t => t.MessReadNote).HasColumnName("MessReadNote");
			           
			#region Relationships
			this.HasRequired(t => t.FK_OaMessRead_Id).WithMany(t => t.FK_OaMessRead_Id).HasForeignKey(d => d.Id);
			#endregion
        }
    }
}  
