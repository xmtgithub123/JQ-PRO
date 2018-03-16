﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BaseRestDayMap : EntityTypeConfiguration<BaseRestDay>
    {
        public BaseRestDayMap()
        {
			this.HasKey(t => new { t.BaseDayID}); 
				  
			this.Property(t => t.BaseYear).IsRequired();
		  
			this.Property(t => t.BaseWeekName).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("BaseRestDay");
			this.Property(t => t.BaseDayID).HasColumnName("BaseDayID");
			this.Property(t => t.BaseYear).HasColumnName("BaseYear");
			this.Property(t => t.BaseDay).HasColumnName("BaseDay");
			this.Property(t => t.BaseWeekName).HasColumnName("BaseWeekName");
			           
			#region Relationships
			#endregion
        }
    }
}  