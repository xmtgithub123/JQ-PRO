﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ArchElecDocumentsMap : EntityTypeConfiguration<ArchElecDocuments>
    {
        public ArchElecDocumentsMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.DocGuid).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DocType).IsRequired();
	  
			this.Property(t => t.ParentId).IsRequired();
	  
			this.Property(t => t.DocName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.DocCode).IsRequired();
	  
			this.Property(t => t.Note).IsRequired().HasMaxLength(500);
	  
			this.Property(t => t.DocXml).IsRequired();
	  
			this.Property(t => t.ContractMoney).IsRequired();
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepID).IsRequired();
	  
			this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.FlowID).IsRequired();
	  
			this.Property(t => t.FlowTime).IsRequired();


			 // Table & Column Mappings
			this.ToTable("ArchElecDocuments");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.DocGuid).HasColumnName("DocGuid");
			this.Property(t => t.DocType).HasColumnName("DocType");
			this.Property(t => t.ParentId).HasColumnName("ParentId");
			this.Property(t => t.DocName).HasColumnName("DocName");
			this.Property(t => t.DocCode).HasColumnName("DocCode");
			this.Property(t => t.Note).HasColumnName("Note");
			this.Property(t => t.DocXml).HasColumnName("DocXml");
			this.Property(t => t.ContractMoney).HasColumnName("ContractMoney");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepID).HasColumnName("CreatorDepID");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.FlowID).HasColumnName("FlowID");
			this.Property(t => t.FlowTime).HasColumnName("FlowTime");
			           
			#region Relationships
			#endregion
        }
    }
}  
