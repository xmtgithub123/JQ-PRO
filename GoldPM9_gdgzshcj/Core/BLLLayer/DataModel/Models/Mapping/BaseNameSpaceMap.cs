﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BaseNameSpaceMap : EntityTypeConfiguration<BaseNameSpace>
    {
        public BaseNameSpaceMap()
        {
			this.HasKey(t => new { t.TableNames}); 
				  
			this.Property(t => t.DBSqlNameSpace).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.WebNameSpace).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.ControllersNameSpace).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.Remark).IsRequired().HasMaxLength(200);


			 // Table & Column Mappings
			this.ToTable("BaseNameSpace");
			this.Property(t => t.TableNames).HasColumnName("TableNames");
			this.Property(t => t.DBSqlNameSpace).HasColumnName("DBSqlNameSpace");
			this.Property(t => t.WebNameSpace).HasColumnName("WebNameSpace");
			this.Property(t => t.ControllersNameSpace).HasColumnName("ControllersNameSpace");
			this.Property(t => t.Remark).HasColumnName("Remark");
			           
			#region Relationships
			#endregion
        }
    }
}  