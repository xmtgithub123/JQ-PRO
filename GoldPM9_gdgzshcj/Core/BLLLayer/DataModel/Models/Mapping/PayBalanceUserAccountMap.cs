﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class PayBalanceUserAccountMap : EntityTypeConfiguration<PayBalanceUserAccount>
    {
        public PayBalanceUserAccountMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.BalanceLotID).IsRequired();
	  
			this.Property(t => t.BalanceEngineeringID).IsRequired();
	  
			this.Property(t => t.EmpId).IsRequired();
	  
			this.Property(t => t.BalanceReason).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.BalanceType).IsRequired();
	  
			this.Property(t => t.SpecID).IsRequired();
	  
			this.Property(t => t.BalancePercent).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.BalanceAmount).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.BalanceMoney).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.ManageCoeff).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.PupilCoeff).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.BalanceNote).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("PayBalanceUserAccount");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.BalanceLotID).HasColumnName("BalanceLotID");
			this.Property(t => t.BalanceEngineeringID).HasColumnName("BalanceEngineeringID");
			this.Property(t => t.EmpId).HasColumnName("EmpId");
			this.Property(t => t.BalanceReason).HasColumnName("BalanceReason");
			this.Property(t => t.BalanceType).HasColumnName("BalanceType");
			this.Property(t => t.SpecID).HasColumnName("SpecID");
			this.Property(t => t.BalancePercent).HasColumnName("BalancePercent");
			this.Property(t => t.BalanceAmount).HasColumnName("BalanceAmount");
			this.Property(t => t.BalanceMoney).HasColumnName("BalanceMoney");
			this.Property(t => t.ManageCoeff).HasColumnName("ManageCoeff");
			this.Property(t => t.PupilCoeff).HasColumnName("PupilCoeff");
			this.Property(t => t.BalanceNote).HasColumnName("BalanceNote");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			           
			#region Relationships
			this.HasRequired(t => t.FK_PayBalanceUserAccount_BalanceEngineeringID).WithMany(t => t.FK_PayBalanceUserAccount_BalanceEngineeringID).HasForeignKey(d => d.BalanceEngineeringID);
			this.HasRequired(t => t.FK_PayBalanceUserAccount_SpecID).WithMany(t => t.FK_PayBalanceUserAccount_SpecID).HasForeignKey(d => d.SpecID);
			#endregion
        }
    }
}  
