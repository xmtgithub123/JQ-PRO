﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class DesTaskCheckImageMap : EntityTypeConfiguration<DesTaskCheckImage>
    {
        public DesTaskCheckImageMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.CheckID).IsRequired();
	  
			this.Property(t => t.CreateEmpID).IsRequired();
	  
			this.Property(t => t.CreateEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreateDateTime).IsRequired();
	  
			this.Property(t => t.CheckImage).IsRequired();


			 // Table & Column Mappings
			this.ToTable("DesTaskCheckImage");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.CheckID).HasColumnName("CheckID");
			this.Property(t => t.CreateEmpID).HasColumnName("CreateEmpID");
			this.Property(t => t.CreateEmpName).HasColumnName("CreateEmpName");
			this.Property(t => t.CreateDateTime).HasColumnName("CreateDateTime");
			this.Property(t => t.CheckImage).HasColumnName("CheckImage");
			           
			#region Relationships
			#endregion
        }
    }
}  
