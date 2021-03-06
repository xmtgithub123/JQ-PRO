﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BussCustomerEvaluateDetailMap : EntityTypeConfiguration<BussCustomerEvaluateDetail>
    {
        public BussCustomerEvaluateDetailMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.BussCustomerEvaluateID).IsRequired();
	  
			this.Property(t => t.EvaluateEmpId).IsRequired();
	  
			this.Property(t => t.EvaluateDeptId).IsRequired();
	  
			this.Property(t => t.EvaluateIdea).IsRequired();
	  
			this.Property(t => t.EvaluateDate).IsRequired();


			 // Table & Column Mappings
			this.ToTable("BussCustomerEvaluateDetail");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.BussCustomerEvaluateID).HasColumnName("BussCustomerEvaluateID");
			this.Property(t => t.EvaluateEmpId).HasColumnName("EvaluateEmpId");
			this.Property(t => t.EvaluateDeptId).HasColumnName("EvaluateDeptId");
			this.Property(t => t.EvaluateIdea).HasColumnName("EvaluateIdea");
			this.Property(t => t.EvaluateDate).HasColumnName("EvaluateDate");
			           
			#region Relationships
			this.HasRequired(t => t.FK_BussCustomerEvaluateDetail_BussCustomerEvaluateID).WithMany(t => t.FK_BussCustomerEvaluateDetail_BussCustomerEvaluateID).HasForeignKey(d => d.BussCustomerEvaluateID);
			#endregion
        }
    }
}  
