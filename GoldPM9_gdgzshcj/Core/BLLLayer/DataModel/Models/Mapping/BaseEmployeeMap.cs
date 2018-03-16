﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BaseEmployeeMap : EntityTypeConfiguration<BaseEmployee>
    {
        public BaseEmployeeMap()
        {
			this.HasKey(t => new { t.EmpID}); 
				  
			this.Property(t => t.EmpGUId).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpLogin).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpPassword).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.SalaryPassword).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpDepID).IsRequired();
	  
			this.Property(t => t.EmpDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DepOrder).IsRequired();
	  
			this.Property(t => t.EmpOrder).IsRequired();
	  
			this.Property(t => t.EmpBirthDate).IsRequired();
	  
			this.Property(t => t.EmpTitle).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpTel).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpComputer).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpIPAddress).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpDisk).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpIsDeleted).IsRequired();
	  
			this.Property(t => t.EmpPageSize).IsRequired();
	  
			this.Property(t => t.EmpThemesName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpMenuType).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpIsAgent).IsRequired();
	  
			this.Property(t => t.EmpSignUrl).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.EmpIsBind).IsRequired();
	  
			this.Property(t => t.EmpMacAddress).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpTelNX).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpTelWX).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpFJNum).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpOaMail).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpComMail).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.EmpZWAddress).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.EmpWGAddress).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.EmpNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.EmpPort).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.EmpHead).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.EmpIsSub).IsRequired();
	  
			this.Property(t => t.PayManageCoeff).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.PaySkillCoeff).IsRequired().HasPrecision(18,2);

            this.Property(t => t.CompanyID).IsRequired();


            // Table & Column Mappings
            this.ToTable("BaseEmployee");
			this.Property(t => t.EmpID).HasColumnName("EmpID");
			this.Property(t => t.EmpGUId).HasColumnName("EmpGUId");
			this.Property(t => t.EmpName).HasColumnName("EmpName");
			this.Property(t => t.EmpLogin).HasColumnName("EmpLogin");
			this.Property(t => t.EmpPassword).HasColumnName("EmpPassword");
			this.Property(t => t.SalaryPassword).HasColumnName("SalaryPassword");
			this.Property(t => t.EmpDepID).HasColumnName("EmpDepID");
			this.Property(t => t.EmpDepName).HasColumnName("EmpDepName");
			this.Property(t => t.DepOrder).HasColumnName("DepOrder");
			this.Property(t => t.EmpOrder).HasColumnName("EmpOrder");
			this.Property(t => t.EmpBirthDate).HasColumnName("EmpBirthDate");
			this.Property(t => t.EmpTitle).HasColumnName("EmpTitle");
			this.Property(t => t.EmpTel).HasColumnName("EmpTel");
			this.Property(t => t.EmpComputer).HasColumnName("EmpComputer");
			this.Property(t => t.EmpIPAddress).HasColumnName("EmpIPAddress");
			this.Property(t => t.EmpDisk).HasColumnName("EmpDisk");
			this.Property(t => t.EmpIsDeleted).HasColumnName("EmpIsDeleted");
			this.Property(t => t.EmpPageSize).HasColumnName("EmpPageSize");
			this.Property(t => t.EmpThemesName).HasColumnName("EmpThemesName");
			this.Property(t => t.EmpMenuType).HasColumnName("EmpMenuType");
			this.Property(t => t.EmpIsAgent).HasColumnName("EmpIsAgent");
			this.Property(t => t.EmpSignUrl).HasColumnName("EmpSignUrl");
			this.Property(t => t.EmpIsBind).HasColumnName("EmpIsBind");
			this.Property(t => t.EmpMacAddress).HasColumnName("EmpMacAddress");
			this.Property(t => t.EmpTelNX).HasColumnName("EmpTelNX");
			this.Property(t => t.EmpTelWX).HasColumnName("EmpTelWX");
			this.Property(t => t.EmpFJNum).HasColumnName("EmpFJNum");
			this.Property(t => t.EmpOaMail).HasColumnName("EmpOaMail");
			this.Property(t => t.EmpComMail).HasColumnName("EmpComMail");
			this.Property(t => t.EmpZWAddress).HasColumnName("EmpZWAddress");
			this.Property(t => t.EmpWGAddress).HasColumnName("EmpWGAddress");
			this.Property(t => t.EmpNote).HasColumnName("EmpNote");
			this.Property(t => t.EmpPort).HasColumnName("EmpPort");
			this.Property(t => t.EmpHead).HasColumnName("EmpHead");
			this.Property(t => t.EmpIsSub).HasColumnName("EmpIsSub");
			this.Property(t => t.PayManageCoeff).HasColumnName("PayManageCoeff");
			this.Property(t => t.PaySkillCoeff).HasColumnName("PaySkillCoeff");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            #region Relationships
            this.HasRequired(t => t.FK_BaseEmployee_EmpDepID).WithMany(t => t.FK_BaseEmployee_EmpDepID).HasForeignKey(d => d.EmpDepID);
			#endregion
        }
    }
}  