﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class DesExchAttachMap : EntityTypeConfiguration<DesExchAttach>
    {
        public DesExchAttachMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ExchID).IsRequired();
	  
			this.Property(t => t.AttachID).IsRequired();

            this.Property(t => t.AttachVer).IsRequired();


			 // Table & Column Mappings
			this.ToTable("DesExchAttach");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ExchID).HasColumnName("ExchID");
			this.Property(t => t.AttachID).HasColumnName("AttachID");
            this.Property(t => t.AttachVer).HasColumnName("AttachVer");
			           
			#region Relationships
			#endregion
        }
    }
}  
