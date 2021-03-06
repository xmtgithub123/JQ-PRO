﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class FlowModelMap : EntityTypeConfiguration<FlowModel>
    {
        public FlowModelMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.ModelRefTable).IsRequired().HasMaxLength(400);
	  
			this.Property(t => t.ModelName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ModelNumber).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ModelVersion).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ModelIsRun).IsRequired();
	  
			this.Property(t => t.ModelUrl).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ModelRole).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ModelNum).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ModelIsWord).IsRequired();
	  
			this.Property(t => t.ModelFinishSend).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.ModelFinishControls).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.ModelByte);
	  
			this.Property(t => t.ModelListUrl).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.ModelType).IsRequired();
	  
			this.Property(t => t.ModeModular).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.ModelSign).IsRequired().HasMaxLength(100);


			 // Table & Column Mappings
			this.ToTable("FlowModel");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.ModelRefTable).HasColumnName("ModelRefTable");
			this.Property(t => t.ModelName).HasColumnName("ModelName");
			this.Property(t => t.ModelNumber).HasColumnName("ModelNumber");
			this.Property(t => t.ModelVersion).HasColumnName("ModelVersion");
			this.Property(t => t.ModelIsRun).HasColumnName("ModelIsRun");
			this.Property(t => t.ModelUrl).HasColumnName("ModelUrl");
			this.Property(t => t.ModelRole).HasColumnName("ModelRole");
			this.Property(t => t.ModelNum).HasColumnName("ModelNum");
			this.Property(t => t.ModelIsWord).HasColumnName("ModelIsWord");
			this.Property(t => t.ModelFinishSend).HasColumnName("ModelFinishSend");
			this.Property(t => t.ModelFinishControls).HasColumnName("ModelFinishControls");
			this.Property(t => t.ModelByte).HasColumnName("ModelByte");
			this.Property(t => t.ModelListUrl).HasColumnName("ModelListUrl");
			this.Property(t => t.ModelType).HasColumnName("ModelType");
			this.Property(t => t.ModeModular).HasColumnName("ModeModular");
			this.Property(t => t.ModelSign).HasColumnName("ModelSign");
			           
			#region Relationships
			#endregion
        }
    }
}  
