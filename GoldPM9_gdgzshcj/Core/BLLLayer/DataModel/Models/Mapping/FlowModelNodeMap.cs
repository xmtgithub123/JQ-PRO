﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class FlowModelNodeMap : EntityTypeConfiguration<FlowModelNode>
    {
        public FlowModelNodeMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.FlowModelID).IsRequired();
	  
			this.Property(t => t.NodeTypeID).IsRequired();
	  
			this.Property(t => t.NodeName).IsRequired().HasMaxLength(400);
	  
			this.Property(t => t.NodeParentID).IsRequired();
	  
			this.Property(t => t.NodeOrder).IsRequired();
	  
			this.Property(t => t.NodeBackID).IsRequired();
	  
			this.Property(t => t.NodeEmpIDs).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.NodeWriteControlsName).IsRequired().HasMaxLength(1000);
	  
			this.Property(t => t.NodeIsToFinish).IsRequired();
	  
			this.Property(t => t.NodeIsToPass).IsRequired();
	  
			this.Property(t => t.NodeIsToMessage).IsRequired();
	  
			this.Property(t => t.NodeValidateGroup).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.NodeNodeSkipID).IsRequired();
	  
			this.Property(t => t.NodeRoleS).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.NodeThisDep).IsRequired();
	  
			this.Property(t => t.NodeSelectToBack).IsRequired();
	  
			this.Property(t => t.NodeSelectToSkip).IsRequired();
	  
			this.Property(t => t.NodeFinishToNext).IsRequired();
	  
			this.Property(t => t.NodeSameId).IsRequired();
	  
			this.Property(t => t.NodeSignControlName).IsRequired().HasMaxLength(200);
	  
			this.Property(t => t.NodeDays).IsRequired().HasPrecision(18,1);
	  
			this.Property(t => t.NodeAutoFinished).IsRequired();
	  
			this.Property(t => t.NodeUrl).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.NodeAppDefaultValue).IsRequired().HasMaxLength(20);
	  
			this.Property(t => t.NodeAppIsRequired).IsRequired();
	  
			this.Property(t => t.NodeAppIsLastTime).IsRequired();
	  
			this.Property(t => t.NodeIsFactNext).IsRequired();

            this.Property(t => t.NodeXml).IsRequired();

            // Table & Column Mappings
            this.ToTable("FlowModelNode");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.FlowModelID).HasColumnName("FlowModelID");
			this.Property(t => t.NodeTypeID).HasColumnName("NodeTypeID");
			this.Property(t => t.NodeName).HasColumnName("NodeName");
			this.Property(t => t.NodeParentID).HasColumnName("NodeParentID");
			this.Property(t => t.NodeOrder).HasColumnName("NodeOrder");
			this.Property(t => t.NodeBackID).HasColumnName("NodeBackID");
			this.Property(t => t.NodeEmpIDs).HasColumnName("NodeEmpIDs");
			this.Property(t => t.NodeWriteControlsName).HasColumnName("NodeWriteControlsName");
			this.Property(t => t.NodeIsToFinish).HasColumnName("NodeIsToFinish");
			this.Property(t => t.NodeIsToPass).HasColumnName("NodeIsToPass");
			this.Property(t => t.NodeIsToMessage).HasColumnName("NodeIsToMessage");
			this.Property(t => t.NodeValidateGroup).HasColumnName("NodeValidateGroup");
			this.Property(t => t.NodeNodeSkipID).HasColumnName("NodeNodeSkipID");
			this.Property(t => t.NodeRoleS).HasColumnName("NodeRoleS");
			this.Property(t => t.NodeThisDep).HasColumnName("NodeThisDep");
			this.Property(t => t.NodeSelectToBack).HasColumnName("NodeSelectToBack");
			this.Property(t => t.NodeSelectToSkip).HasColumnName("NodeSelectToSkip");
			this.Property(t => t.NodeFinishToNext).HasColumnName("NodeFinishToNext");
			this.Property(t => t.NodeSameId).HasColumnName("NodeSameId");
			this.Property(t => t.NodeSignControlName).HasColumnName("NodeSignControlName");
			this.Property(t => t.NodeDays).HasColumnName("NodeDays");
			this.Property(t => t.NodeAutoFinished).HasColumnName("NodeAutoFinished");
			this.Property(t => t.NodeUrl).HasColumnName("NodeUrl");
			this.Property(t => t.NodeAppDefaultValue).HasColumnName("NodeAppDefaultValue");
			this.Property(t => t.NodeAppIsRequired).HasColumnName("NodeAppIsRequired");
			this.Property(t => t.NodeAppIsLastTime).HasColumnName("NodeAppIsLastTime");
			this.Property(t => t.NodeIsFactNext).HasColumnName("NodeIsFactNext");
            this.Property(t => t.NodeXml).HasColumnName("NodeXml");
            #region Relationships
            this.HasRequired(t => t.FK_FlowModelNode_FlowModelID).WithMany(t => t.FK_FlowModelNode_FlowModelID).HasForeignKey(d => d.FlowModelID);
			#endregion
        }
    }
}  
