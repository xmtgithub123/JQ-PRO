﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BussFeeFactMap : EntityTypeConfiguration<BussFeeFact>
    {
        public BussFeeFactMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.FeeDate).IsRequired();
	  
			this.Property(t => t.FeeMoney).IsRequired().HasPrecision(18,4);
	  
			this.Property(t => t.FeeNote).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.EmpId).IsRequired();
	  
			this.Property(t => t.ProjID).IsRequired();
	  
			this.Property(t => t.ConID).IsRequired();
	  
			this.Property(t => t.ConFeeTypeID).IsRequired();
	  
			this.Property(t => t.FormTableID).IsRequired();
	  
			this.Property(t => t.ColAttType1).IsRequired();
	  
			this.Property(t => t.ColAttType2).IsRequired();
	  
			this.Property(t => t.ColAttDate1).IsRequired();
	  
			this.Property(t => t.ColAttDate2).IsRequired();
	  
			this.Property(t => t.ColAttFloat1).IsRequired().HasPrecision(18,2);
	  
			this.Property(t => t.ColAttFloat2).IsRequired().HasPrecision(18,2);
	  
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
	  
			this.Property(t => t.TableXml).IsRequired();


			 // Table & Column Mappings
			this.ToTable("BussFeeFact");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.FeeDate).HasColumnName("FeeDate");
			this.Property(t => t.FeeMoney).HasColumnName("FeeMoney");
			this.Property(t => t.FeeNote).HasColumnName("FeeNote");
			this.Property(t => t.EmpId).HasColumnName("EmpId");
			this.Property(t => t.ProjID).HasColumnName("ProjID");
			this.Property(t => t.ConID).HasColumnName("ConID");
			this.Property(t => t.ConFeeTypeID).HasColumnName("ConFeeTypeID");
			this.Property(t => t.FormTableID).HasColumnName("FormTableID");
			this.Property(t => t.ColAttType1).HasColumnName("ColAttType1");
			this.Property(t => t.ColAttType2).HasColumnName("ColAttType2");
			this.Property(t => t.ColAttDate1).HasColumnName("ColAttDate1");
			this.Property(t => t.ColAttDate2).HasColumnName("ColAttDate2");
			this.Property(t => t.ColAttFloat1).HasColumnName("ColAttFloat1");
			this.Property(t => t.ColAttFloat2).HasColumnName("ColAttFloat2");
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
			this.Property(t => t.TableXml).HasColumnName("TableXml");
			           
			#region Relationships
			this.HasRequired(t => t.FK_BussFeeFact_ConFeeTypeID).WithMany(t => t.FK_BussFeeFact_ConFeeTypeID).HasForeignKey(d => d.ConFeeTypeID);
			this.HasRequired(t => t.FK_BussFeeFact_ConID).WithMany(t => t.FK_BussFeeFact_ConID).HasForeignKey(d => d.ConID);
			this.HasRequired(t => t.FK_BussFeeFact_ProjID).WithMany(t => t.FK_BussFeeFact_ProjID).HasForeignKey(d => d.ProjID);
			#endregion
        }
    }
}  
