﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BussContractMap : EntityTypeConfiguration<BussContract>
    {
        public BussContractMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ConNumber).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ConStreamNumber).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ConName).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.ConFee).IsRequired().HasPrecision(18,4);
	  
			this.Property(t => t.ConBalanceFee).IsRequired().HasPrecision(18,4);
	  
			this.Property(t => t.ConIsFeeFinished).IsRequired();
	  
			this.Property(t => t.ConNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.ConPay).IsRequired().HasMaxLength(4000);
	  
			this.Property(t => t.CustID).IsRequired();
	  
			this.Property(t => t.CustName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.CustLinkManName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.CustLinkManTel).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.CustLinkManWeb).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ConStatus).IsRequired();
	  
			this.Property(t => t.ConMainNumber).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.ConDate).IsRequired();
	  
			this.Property(t => t.ConFileNumber).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.FatherID).IsRequired();
	  
			this.Property(t => t.CreateEmpId).IsRequired();
	  
			this.Property(t => t.CreateEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreateDate).IsRequired();
	  
			this.Property(t => t.ConType).IsRequired();
	  
			this.Property(t => t.ConYear).IsRequired();
	  
			this.Property(t => t.ConArea).IsRequired();
	  
			this.Property(t => t.ConFulfilType).IsRequired();
	  
			this.Property(t => t.ColAttType1).IsRequired();
	  
			this.Property(t => t.ColAttType2).IsRequired();
	  
			this.Property(t => t.ColAttType3).IsRequired();
	  
			this.Property(t => t.ColAttType4).IsRequired();
	  
			this.Property(t => t.ColAttType5).IsRequired();
	  
			this.Property(t => t.ColAttVal1).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ColAttVal2).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ColAttVal3).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ColAttVal4).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ColAttVal5).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ColAttDate1).IsRequired();
	  
			this.Property(t => t.ColAttDate2).IsRequired();
	  
			this.Property(t => t.ColAttDate3).IsRequired();
	  
			this.Property(t => t.ColAttDate4).IsRequired();
	  
			this.Property(t => t.ColAttDate5).IsRequired();
	  
			this.Property(t => t.ColAttFloat1).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.ColAttFloat2).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.ColAttFloat3).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.ColAttFloat4).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.ColAttFloat5).IsRequired().HasPrecision(18,2);
	  
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

            this.Property(t => t.BGConFee).IsRequired().HasPrecision(18, 4);

            this.Property(t => t.SHYJ);

			 // Table & Column Mappings
			this.ToTable("BussContract");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ConNumber).HasColumnName("ConNumber");
			this.Property(t => t.ConStreamNumber).HasColumnName("ConStreamNumber");
			this.Property(t => t.ConName).HasColumnName("ConName");
			this.Property(t => t.ConFee).HasColumnName("ConFee");
			this.Property(t => t.ConBalanceFee).HasColumnName("ConBalanceFee");
			this.Property(t => t.ConIsFeeFinished).HasColumnName("ConIsFeeFinished");
			this.Property(t => t.ConNote).HasColumnName("ConNote");
			this.Property(t => t.ConPay).HasColumnName("ConPay");
			this.Property(t => t.CustID).HasColumnName("CustID");
			this.Property(t => t.CustName).HasColumnName("CustName");
			this.Property(t => t.CustLinkManName).HasColumnName("CustLinkManName");
			this.Property(t => t.CustLinkManTel).HasColumnName("CustLinkManTel");
			this.Property(t => t.CustLinkManWeb).HasColumnName("CustLinkManWeb");
			this.Property(t => t.ConStatus).HasColumnName("ConStatus");
			this.Property(t => t.ConMainNumber).HasColumnName("ConMainNumber");
			this.Property(t => t.ConDate).HasColumnName("ConDate");
			this.Property(t => t.ConFileNumber).HasColumnName("ConFileNumber");
			this.Property(t => t.FatherID).HasColumnName("FatherID");
			this.Property(t => t.CreateEmpId).HasColumnName("CreateEmpId");
			this.Property(t => t.CreateEmpName).HasColumnName("CreateEmpName");
			this.Property(t => t.CreateDate).HasColumnName("CreateDate");
			this.Property(t => t.ConType).HasColumnName("ConType");
			this.Property(t => t.ConYear).HasColumnName("ConYear");
			this.Property(t => t.ConArea).HasColumnName("ConArea");
			this.Property(t => t.ConFulfilType).HasColumnName("ConFulfilType");
			this.Property(t => t.ColAttType1).HasColumnName("ColAttType1");
			this.Property(t => t.ColAttType2).HasColumnName("ColAttType2");
			this.Property(t => t.ColAttType3).HasColumnName("ColAttType3");
			this.Property(t => t.ColAttType4).HasColumnName("ColAttType4");
			this.Property(t => t.ColAttType5).HasColumnName("ColAttType5");
			this.Property(t => t.ColAttVal1).HasColumnName("ColAttVal1");
			this.Property(t => t.ColAttVal2).HasColumnName("ColAttVal2");
			this.Property(t => t.ColAttVal3).HasColumnName("ColAttVal3");
			this.Property(t => t.ColAttVal4).HasColumnName("ColAttVal4");
			this.Property(t => t.ColAttVal5).HasColumnName("ColAttVal5");
			this.Property(t => t.ColAttDate1).HasColumnName("ColAttDate1");
			this.Property(t => t.ColAttDate2).HasColumnName("ColAttDate2");
			this.Property(t => t.ColAttDate3).HasColumnName("ColAttDate3");
			this.Property(t => t.ColAttDate4).HasColumnName("ColAttDate4");
			this.Property(t => t.ColAttDate5).HasColumnName("ColAttDate5");
			this.Property(t => t.ColAttFloat1).HasColumnName("ColAttFloat1");
			this.Property(t => t.ColAttFloat2).HasColumnName("ColAttFloat2");
			this.Property(t => t.ColAttFloat3).HasColumnName("ColAttFloat3");
			this.Property(t => t.ColAttFloat4).HasColumnName("ColAttFloat4");
			this.Property(t => t.ColAttFloat5).HasColumnName("ColAttFloat5");
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
            this.Property(t => t.BGConFee).HasColumnName("BGConFee");
            this.Property(t => t.SHYJ).HasColumnName("SHYJ");

            #region Relationships
            this.HasRequired(t => t.FK_BussContract_ConStatus).WithMany(t => t.FK_BussContract_ConStatus).HasForeignKey(d => d.ConStatus);
			this.HasRequired(t => t.FK_BussContract_ConType).WithMany(t => t.FK_BussContract_ConType).HasForeignKey(d => d.ConType);
			#endregion
        }
    }
}  
