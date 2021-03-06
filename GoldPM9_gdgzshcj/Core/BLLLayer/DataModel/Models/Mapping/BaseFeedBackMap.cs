﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BaseFeedBackMap : EntityTypeConfiguration<BaseFeedBack>
    {
        public BaseFeedBackMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.SendStatus).IsRequired();
	  
			this.Property(t => t.SendReason).IsRequired().HasMaxLength(10);
	  
			this.Property(t => t.SendNote).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("BaseFeedBack");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.SendStatus).HasColumnName("SendStatus");
			this.Property(t => t.SendReason).HasColumnName("SendReason");
			this.Property(t => t.SendNote).HasColumnName("SendNote");
			           
			#region Relationships
			#endregion
        }
    }
}  
