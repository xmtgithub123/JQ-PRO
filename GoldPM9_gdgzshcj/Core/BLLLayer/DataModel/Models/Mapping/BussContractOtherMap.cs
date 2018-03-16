﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BussContractOtherMap : EntityTypeConfiguration<BussContractOther>
    {
        public BussContractOtherMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ConTypeID).IsRequired();
	  
			this.Property(t => t.ConNumber).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ConrName).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.ConFee).IsRequired().HasPrecision(18,4);
	  
			this.Property(t => t.ConrBalanceFee).IsRequired().HasPrecision(18,4);
	  
			this.Property(t => t.ConIsFeeFinished).IsRequired();
	  
			this.Property(t => t.ConrNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.CustID).IsRequired();
	  
			this.Property(t => t.CustName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.CustBankName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.CustBankAccount).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ConOtherStatus).IsRequired();
	  
			this.Property(t => t.ConOtherSignDate).IsRequired();
	  
			this.Property(t => t.ConOtherType).IsRequired();
	  
			this.Property(t => t.ConOtherFulfilType).IsRequired();
	  
			this.Property(t => t.ConID).IsRequired();
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FlowID).IsRequired();
	  
			this.Property(t => t.FlowTime).IsRequired();
	  
			this.Property(t => t.AgenEmpId).IsRequired();
	  
			this.Property(t => t.AgenEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeleterEmpId).IsRequired();
	  
			this.Property(t => t.DeleterEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeletionTime).IsRequired();

            this.Property(t => t.CompanyID).IsRequired();


            // Table & Column Mappings
            this.ToTable("BussContractOther");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ConTypeID).HasColumnName("ConTypeID");
			this.Property(t => t.ConNumber).HasColumnName("ConNumber");
			this.Property(t => t.ConrName).HasColumnName("ConrName");
			this.Property(t => t.ConFee).HasColumnName("ConFee");
			this.Property(t => t.ConrBalanceFee).HasColumnName("ConrBalanceFee");
			this.Property(t => t.ConIsFeeFinished).HasColumnName("ConIsFeeFinished");
			this.Property(t => t.ConrNote).HasColumnName("ConrNote");
			this.Property(t => t.CustID).HasColumnName("CustID");
			this.Property(t => t.CustName).HasColumnName("CustName");
			this.Property(t => t.CustBankName).HasColumnName("CustBankName");
			this.Property(t => t.CustBankAccount).HasColumnName("CustBankAccount");
			this.Property(t => t.ConOtherStatus).HasColumnName("ConOtherStatus");
			this.Property(t => t.ConOtherSignDate).HasColumnName("ConOtherSignDate");
			this.Property(t => t.ConOtherType).HasColumnName("ConOtherType");
			this.Property(t => t.ConOtherFulfilType).HasColumnName("ConOtherFulfilType");
			this.Property(t => t.ConID).HasColumnName("ConID");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.FlowID).HasColumnName("FlowID");
			this.Property(t => t.FlowTime).HasColumnName("FlowTime");
			this.Property(t => t.AgenEmpId).HasColumnName("AgenEmpId");
			this.Property(t => t.AgenEmpName).HasColumnName("AgenEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			this.Property(t => t.DeleterEmpName).HasColumnName("DeleterEmpName");
			this.Property(t => t.DeletionTime).HasColumnName("DeletionTime");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            #region Relationships
            this.HasRequired(t => t.FK_BussContractOther_ConID).WithMany(t => t.FK_BussContractOther_ConID).HasForeignKey(d => d.ConID);
			this.HasRequired(t => t.FK_BussContractOther_ConOtherFulfilType).WithMany(t => t.FK_BussContractOther_ConOtherFulfilType).HasForeignKey(d => d.ConOtherFulfilType);
			this.HasRequired(t => t.FK_BussContractOther_ConOtherStatus).WithMany(t => t.FK_BussContractOther_ConOtherStatus).HasForeignKey(d => d.ConOtherStatus);
			this.HasRequired(t => t.FK_BussContractOther_ConOtherType).WithMany(t => t.FK_BussContractOther_ConOtherType).HasForeignKey(d => d.ConOtherType);
			#endregion
        }
    }
}  