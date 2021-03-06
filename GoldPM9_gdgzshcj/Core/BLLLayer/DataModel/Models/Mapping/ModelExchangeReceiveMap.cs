﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class ModelExchangeReceiveMap : EntityTypeConfiguration<ModelExchangeReceive>
    {
        public ModelExchangeReceiveMap()
        {
			this.HasKey(t => new { t.Id,t.ModelReceiveSpecID}); 
				  
			this.Property(t => t.ModelReceiveSpecID).IsRequired();
	  
			this.Property(t => t.ModelReceiveNote).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("ModelExchangeReceive");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ModelReceiveSpecID).HasColumnName("ModelReceiveSpecID");
			this.Property(t => t.ModelReceiveNote).HasColumnName("ModelReceiveNote");
			           
			#region Relationships
			this.HasRequired(t => t.FK_ModelExchangeReceive_Id).WithMany(t => t.FK_ModelExchangeReceive_Id).HasForeignKey(d => d.Id);
			this.HasRequired(t => t.FK_ModelExchangeReceive_ModelReceiveSpecID).WithMany(t => t.FK_ModelExchangeReceive_ModelReceiveSpecID).HasForeignKey(d => d.ModelReceiveSpecID);
			#endregion
        }
    }
}  
