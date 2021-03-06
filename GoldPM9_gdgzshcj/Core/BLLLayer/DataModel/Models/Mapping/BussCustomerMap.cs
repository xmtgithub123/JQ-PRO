﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-10-28 16:43:27
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BussCustomerMap : EntityTypeConfiguration<BussCustomer>
    {
        public BussCustomerMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.CustName).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.CustType).IsRequired();
	  
			this.Property(t => t.CustAddress).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.CustAddressID).IsRequired();
	  
			this.Property(t => t.CustChineseName).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.CustChineseTitle).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CustChineseTel).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CustTechnologyName).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CustTechnologyTitle).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CustTechnologyTel).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CustTel).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CustEmail).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.CustWeb).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CustPost).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CustFax).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.CustBankName).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.CustBankAccount).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.CustBankTariff).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.CustDate).IsRequired();
	  
			this.Property(t => t.CustEstablishDate).IsRequired();
	  
			this.Property(t => t.CustNote).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.CustPersonAll).IsRequired();
	  
			this.Property(t => t.CustCommercial).IsRequired().HasMaxLength(220);
	  
			this.Property(t => t.CustCustomerFee).IsRequired().HasPrecision(18,4);
	  
			this.Property(t => t.CustProjectEmpName).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.CustProfessionals).IsRequired();
	  
			this.Property(t => t.CustEngineers).IsRequired();
	  
			this.Property(t => t.CustJunior).IsRequired();
	  
			this.Property(t => t.CustSkilled).IsRequired();
	  
			this.Property(t => t.CustTypeIDs).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CustSpecilIDs).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CustVoltName).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.CustBusiness).IsRequired().HasMaxLength(2000);
	  
			this.Property(t => t.CustBusinessCreateDate).IsRequired();
	  
			this.Property(t => t.CustBusinessDate).IsRequired();
	  
			this.Property(t => t.CustQualification).IsRequired();
	  
			this.Property(t => t.CustQualiGrade).IsRequired();
	  
			this.Property(t => t.EmpId).IsRequired();
	  
			this.Property(t => t.EmpDate).IsRequired();
	  
			this.Property(t => t.TypeID).IsRequired();
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FlowID).IsRequired();
	  
			this.Property(t => t.FlowTime).IsRequired();
	  
			this.Property(t => t.AgenEmpId).IsRequired();
	  
			this.Property(t => t.AgenEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired();
	  
			this.Property(t => t.DeleterEmpId).IsRequired();


			 // Table & Column Mappings
			this.ToTable("BussCustomer");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.CustName).HasColumnName("CustName");
			this.Property(t => t.CustType).HasColumnName("CustType");
			this.Property(t => t.CustAddress).HasColumnName("CustAddress");
			this.Property(t => t.CustAddressID).HasColumnName("CustAddressID");
			this.Property(t => t.CustChineseName).HasColumnName("CustChineseName");
			this.Property(t => t.CustChineseTitle).HasColumnName("CustChineseTitle");
			this.Property(t => t.CustChineseTel).HasColumnName("CustChineseTel");
			this.Property(t => t.CustTechnologyName).HasColumnName("CustTechnologyName");
			this.Property(t => t.CustTechnologyTitle).HasColumnName("CustTechnologyTitle");
			this.Property(t => t.CustTechnologyTel).HasColumnName("CustTechnologyTel");
			this.Property(t => t.CustTel).HasColumnName("CustTel");
			this.Property(t => t.CustEmail).HasColumnName("CustEmail");
			this.Property(t => t.CustWeb).HasColumnName("CustWeb");
			this.Property(t => t.CustPost).HasColumnName("CustPost");
			this.Property(t => t.CustFax).HasColumnName("CustFax");
			this.Property(t => t.CustBankName).HasColumnName("CustBankName");
			this.Property(t => t.CustBankAccount).HasColumnName("CustBankAccount");
			this.Property(t => t.CustBankTariff).HasColumnName("CustBankTariff");
			this.Property(t => t.CustDate).HasColumnName("CustDate");
			this.Property(t => t.CustEstablishDate).HasColumnName("CustEstablishDate");
			this.Property(t => t.CustNote).HasColumnName("CustNote");
			this.Property(t => t.CustPersonAll).HasColumnName("CustPersonAll");
			this.Property(t => t.CustCommercial).HasColumnName("CustCommercial");
			this.Property(t => t.CustCustomerFee).HasColumnName("CustCustomerFee");
			this.Property(t => t.CustProjectEmpName).HasColumnName("CustProjectEmpName");
			this.Property(t => t.CustProfessionals).HasColumnName("CustProfessionals");
			this.Property(t => t.CustEngineers).HasColumnName("CustEngineers");
			this.Property(t => t.CustJunior).HasColumnName("CustJunior");
			this.Property(t => t.CustSkilled).HasColumnName("CustSkilled");
			this.Property(t => t.CustTypeIDs).HasColumnName("CustTypeIDs");
			this.Property(t => t.CustSpecilIDs).HasColumnName("CustSpecilIDs");
			this.Property(t => t.CustVoltName).HasColumnName("CustVoltName");
			this.Property(t => t.CustBusiness).HasColumnName("CustBusiness");
			this.Property(t => t.CustBusinessCreateDate).HasColumnName("CustBusinessCreateDate");
			this.Property(t => t.CustBusinessDate).HasColumnName("CustBusinessDate");
			this.Property(t => t.CustQualification).HasColumnName("CustQualification");
			this.Property(t => t.CustQualiGrade).HasColumnName("CustQualiGrade");
			this.Property(t => t.EmpId).HasColumnName("EmpId");
			this.Property(t => t.EmpDate).HasColumnName("EmpDate");
			this.Property(t => t.TypeID).HasColumnName("TypeID");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.FlowID).HasColumnName("FlowID");
			this.Property(t => t.FlowTime).HasColumnName("FlowTime");
			this.Property(t => t.AgenEmpId).HasColumnName("AgenEmpId");
			this.Property(t => t.AgenEmpName).HasColumnName("AgenEmpName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			           
			#region Relationships
			this.HasRequired(t => t.FK_BussCustomer_CustAddressID).WithMany(t => t.FK_BussCustomer_CustAddressID).HasForeignKey(d => d.CustAddressID);
			this.HasRequired(t => t.FK_BussCustomer_CustType).WithMany(t => t.FK_BussCustomer_CustType).HasForeignKey(d => d.CustType);
			#endregion
        }
    }
}  
