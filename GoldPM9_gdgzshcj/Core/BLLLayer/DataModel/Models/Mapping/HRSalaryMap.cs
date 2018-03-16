﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-01-16 10:47:23
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class HRSalaryMap : EntityTypeConfiguration<HRSalary>
    {
        public HRSalaryMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeleterEmpId).IsRequired();
	  
			this.Property(t => t.DeleterEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeletionTime).IsRequired();
	  
			this.Property(t => t.EmpID).IsRequired();
	  
			this.Property(t => t.HireTypeID).IsRequired();
	  
			this.Property(t => t.BaseSalary).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.MeritSalary).IsRequired().HasPrecision(18,2);


			 // Table & Column Mappings
			this.ToTable("HRSalary");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			this.Property(t => t.DeleterEmpName).HasColumnName("DeleterEmpName");
			this.Property(t => t.DeletionTime).HasColumnName("DeletionTime");
			this.Property(t => t.EmpID).HasColumnName("EmpID");
			this.Property(t => t.HireTypeID).HasColumnName("HireTypeID");
			this.Property(t => t.BaseSalary).HasColumnName("BaseSalary");
			this.Property(t => t.MeritSalary).HasColumnName("MeritSalary");
			           
			#region Relationships
			#endregion
        }
    }
}  