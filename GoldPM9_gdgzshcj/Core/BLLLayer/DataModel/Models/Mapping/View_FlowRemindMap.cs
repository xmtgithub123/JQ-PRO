﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-20 21:48:15
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class View_FlowRemindMap : EntityTypeConfiguration<View_FlowRemind>
    {
        public View_FlowRemindMap()
        {
			this.HasKey(t => new { t.FlowName,t.FlowRefTable,t.FlowNodeEmpId,t.num}); 
			  
			this.Property(t => t.FlowName).HasMaxLength(100);
	  
			this.Property(t => t.FlowRefTable).HasMaxLength(200);
	  
			this.Property(t => t.FlowNodeEmpId);
	  
			this.Property(t => t.num);


			 // Table & Column Mappings
			this.ToTable("View_FlowRemind");
			this.Property(t => t.FlowName).HasColumnName("FlowName");
			this.Property(t => t.FlowRefTable).HasColumnName("FlowRefTable");
			this.Property(t => t.FlowNodeEmpId).HasColumnName("FlowNodeEmpId");
			this.Property(t => t.num).HasColumnName("num");
			           
			#region Relationships
			#endregion
        }
    }
}  
