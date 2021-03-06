﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BaseEmpAgenMap : EntityTypeConfiguration<BaseEmpAgen>
    {
        public BaseEmpAgenMap()
        {
			this.HasKey(t => new { t.BaseEmpAgenID}); 
				  
			this.Property(t => t.FromEmpID).IsRequired();
	  
			this.Property(t => t.FromEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.ToEmpID).IsRequired();
	  
			this.Property(t => t.ToEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.AgenFlowRefTable).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.AgenMenu).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.DateCreate).IsRequired();
	  
			this.Property(t => t.DateLower).IsRequired();
	  
			this.Property(t => t.DateUpper).IsRequired();
	  
			this.Property(t => t.AgenNote).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.AgenType).IsRequired();
	  
			this.Property(t => t.AgenStatus).IsRequired();


			 // Table & Column Mappings
			this.ToTable("BaseEmpAgen");
			this.Property(t => t.BaseEmpAgenID).HasColumnName("BaseEmpAgenID");
			this.Property(t => t.FromEmpID).HasColumnName("FromEmpID");
			this.Property(t => t.FromEmpName).HasColumnName("FromEmpName");
			this.Property(t => t.ToEmpID).HasColumnName("ToEmpID");
			this.Property(t => t.ToEmpName).HasColumnName("ToEmpName");
			this.Property(t => t.AgenFlowRefTable).HasColumnName("AgenFlowRefTable");
			this.Property(t => t.AgenMenu).HasColumnName("AgenMenu");
			this.Property(t => t.DateCreate).HasColumnName("DateCreate");
			this.Property(t => t.DateLower).HasColumnName("DateLower");
			this.Property(t => t.DateUpper).HasColumnName("DateUpper");
			this.Property(t => t.AgenNote).HasColumnName("AgenNote");
			this.Property(t => t.AgenType).HasColumnName("AgenType");
			this.Property(t => t.AgenStatus).HasColumnName("AgenStatus");
			           
			#region Relationships
			#endregion
        }
    }
}  
