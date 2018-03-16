﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class PayBalanceEngineeringMap : EntityTypeConfiguration<PayBalanceEngineering>
    {
        public PayBalanceEngineeringMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.BalanceLotID).IsRequired();
	  
			this.Property(t => t.EngineeringID).IsRequired();
	  
			this.Property(t => t.EngAmountArrange).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.PayModelID).IsRequired();
	  
			this.Property(t => t.ModelXML).IsRequired();
	  
			this.Property(t => t.BalanceState).IsRequired();
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeleterEmpId).IsRequired();
	  
			this.Property(t => t.DeleterEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeletionTime).IsRequired();


			 // Table & Column Mappings
			this.ToTable("PayBalanceEngineering");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.BalanceLotID).HasColumnName("BalanceLotID");
			this.Property(t => t.EngineeringID).HasColumnName("EngineeringID");
			this.Property(t => t.EngAmountArrange).HasColumnName("EngAmountArrange");
			this.Property(t => t.PayModelID).HasColumnName("PayModelID");
			this.Property(t => t.ModelXML).HasColumnName("ModelXML");
			this.Property(t => t.BalanceState).HasColumnName("BalanceState");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			this.Property(t => t.DeleterEmpName).HasColumnName("DeleterEmpName");
			this.Property(t => t.DeletionTime).HasColumnName("DeletionTime");
			           
			#region Relationships
			this.HasRequired(t => t.FK_PayBalanceEngineering_BalanceLotID).WithMany(t => t.FK_PayBalanceEngineering_BalanceLotID).HasForeignKey(d => d.BalanceLotID);
			#endregion
        }
    }
}  
