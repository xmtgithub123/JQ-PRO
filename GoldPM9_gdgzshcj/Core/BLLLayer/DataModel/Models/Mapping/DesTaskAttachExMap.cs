﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class DesTaskAttachExMap : EntityTypeConfiguration<DesTaskAttachEx>
    {
        public DesTaskAttachExMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.AttachId).IsRequired();
	  
			this.Property(t => t.AttachVer).IsRequired();
	  
			this.Property(t => t.AttachFlowNode).IsRequired();
	  
			this.Property(t => t.ColAttType1).IsRequired();
	  
			this.Property(t => t.ColAttType2).IsRequired();
	  
			this.Property(t => t.ColAttXml).IsRequired();
	  
			this.Property(t => t.AttachNextFlowNode).IsRequired();


			 // Table & Column Mappings
			this.ToTable("DesTaskAttachEx");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.AttachId).HasColumnName("AttachId");
			this.Property(t => t.AttachVer).HasColumnName("AttachVer");
			this.Property(t => t.AttachFlowNode).HasColumnName("AttachFlowNode");
			this.Property(t => t.ColAttType1).HasColumnName("ColAttType1");
			this.Property(t => t.ColAttType2).HasColumnName("ColAttType2");
			this.Property(t => t.ColAttXml).HasColumnName("ColAttXml");
			this.Property(t => t.AttachNextFlowNode).HasColumnName("AttachNextFlowNode");
			           
			#region Relationships
			#endregion
        }
    }
}  
