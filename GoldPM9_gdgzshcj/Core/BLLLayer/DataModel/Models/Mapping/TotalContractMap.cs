﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-01-09 09:25:26
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class TotalContractMap : EntityTypeConfiguration<TotalContract>
    {
        public TotalContractMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.TotalContractNumber).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.TotalContractName).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.TotalContractFee).IsRequired().HasPrecision(18,4);
	  
			this.Property(t => t.TotalContractDate).IsRequired();
	  
			this.Property(t => t.TotalContractStatusID).IsRequired();
	  
			this.Property(t => t.TotalContractNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.TotalContractAccomplish).IsRequired();
	  
			this.Property(t => t.ConIsFinished).IsRequired();
	  
			this.Property(t => t.ConIsFinishedNote).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.EndDate).IsRequired();
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeleterEmpId).IsRequired();
	  
			this.Property(t => t.DeleterEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DeletionTime).IsRequired();


			 // Table & Column Mappings
			this.ToTable("TotalContract");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.TotalContractNumber).HasColumnName("TotalContractNumber");
			this.Property(t => t.TotalContractName).HasColumnName("TotalContractName");
			this.Property(t => t.TotalContractFee).HasColumnName("TotalContractFee");
			this.Property(t => t.TotalContractDate).HasColumnName("TotalContractDate");
			this.Property(t => t.TotalContractStatusID).HasColumnName("TotalContractStatusID");
			this.Property(t => t.TotalContractNote).HasColumnName("TotalContractNote");
			this.Property(t => t.TotalContractAccomplish).HasColumnName("TotalContractAccomplish");
			this.Property(t => t.ConIsFinished).HasColumnName("ConIsFinished");
			this.Property(t => t.ConIsFinishedNote).HasColumnName("ConIsFinishedNote");
			this.Property(t => t.EndDate).HasColumnName("EndDate");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			this.Property(t => t.DeleterEmpName).HasColumnName("DeleterEmpName");
			this.Property(t => t.DeletionTime).HasColumnName("DeletionTime");
			           
			#region Relationships
			this.HasRequired(t => t.FK_TotalContract_TotalContractStatusID).WithMany(t => t.FK_TotalContract_TotalContractStatusID).HasForeignKey(d => d.TotalContractStatusID);
			#endregion
        }
    }
}  
