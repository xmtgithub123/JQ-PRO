﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-07-20 21:48:15
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class View_FlowOnHandMap : EntityTypeConfiguration<View_FlowOnHand>
    {
        public View_FlowOnHandMap()
        {
			this.HasKey(t => new { t.FlowRefTable,t.FlowRefID,t.FlowName,t.FlowNumber,t.FlowVersion,t.FlowIsRun,t.FlowUrl,t.FlowRole,t.FlowNum,t.FlowIsWord,t.FlowStartDate,t.FlowFinishDate,t.FlowFinishSend,t.FlowFinishControls,t.CreatorEmpId,t.CreationTime,t.FlowStatusID,t.FlowStatusName,t.FlowControlXml,t.FlowXml,t.FlowListUrl,t.FlowModelID,t.Id,t.FlowID,t.FlowNodeTypeID,t.FlowNodeName,t.FlowNodeParentID,t.FlowNodeOrder,t.FlowNodeBackID,t.FlowNodeEmpId,t.FlowNodeEmpName,t.FlowNodeEmpIDs,t.FlowNodeWriteControlsName,t.FlowNodeNote,t.FlowNodeStatusID,t.FlowNodeDate,t.FlowNodeIsToFinish,t.FlowNodeIsToPass,t.FlowNodeIsToMessage,t.FlowNodeValidateGroup,t.FlowNodeNodeSkipID,t.FlowNodeNodeRoles,t.FlowNodeNodeThisDep,t.FlowNodeSelectToBack,t.FlowNodeFinishToNext,t.FlowNodeFromEmpId,t.FlowNodeFromEmpName,t.FlowNodeFromDateTime,t.FlowNodeRefIsDelete,t.FlowNodeIsRemind,t.FlowNodeSameId,t.FlowNodeSignControlName,t.FlowModelNodeID,t.FlowNodeDays,t.FlowNodeAutoFinished,t.FlowNodeAutoStatus,t.FlowNodeIsFactNext,t.FlowNodeUrl,t.FlowNodeAppDefaultValue,t.FlowNodeAppIsRequired,t.FlowNodeAppIsLastTime,t.AgenEmpId,t.AgenEmpName,t.CreatorDepId,t.CreatorDepName,t.FlowNodeSelectToSkip}); 
			  
			this.Property(t => t.FlowRefTable).HasMaxLength(200);
	  
			this.Property(t => t.FlowRefID);
	  
			this.Property(t => t.FlowName).HasMaxLength(100);
	  
			this.Property(t => t.FlowNumber).HasMaxLength(100);
	  
			this.Property(t => t.FlowVersion).HasMaxLength(100);
	  
			this.Property(t => t.FlowIsRun);
	  
			this.Property(t => t.FlowUrl).HasMaxLength(100);
	  
			this.Property(t => t.FlowRole).HasMaxLength(100);
	  
			this.Property(t => t.FlowNum).HasMaxLength(100);
	  
			this.Property(t => t.FlowIsWord);
	  
			this.Property(t => t.FlowStartDate);
	  
			this.Property(t => t.FlowFinishDate);
	  
			this.Property(t => t.FlowFinishSend).HasMaxLength(1000);
	  
			this.Property(t => t.FlowFinishControls).HasMaxLength(1000);
	  
			this.Property(t => t.CreatorEmpId);
	  
			this.Property(t => t.CreationTime);
	  
			this.Property(t => t.FlowStatusID);
	  
			this.Property(t => t.FlowStatusName).HasMaxLength(100);
	  
			this.Property(t => t.FlowControlXml);
	  
			this.Property(t => t.FlowXml);
	  
			this.Property(t => t.FlowListUrl).HasMaxLength(200);
	  
			this.Property(t => t.FlowModelID);
	  
			this.Property(t => t.Id);
	  
			this.Property(t => t.FlowID);
	  
			this.Property(t => t.FlowNodeTypeID);
	  
			this.Property(t => t.FlowNodeName).HasMaxLength(400);
	  
			this.Property(t => t.FlowNodeParentID);
	  
			this.Property(t => t.FlowNodeOrder);
	  
			this.Property(t => t.FlowNodeBackID);
	  
			this.Property(t => t.FlowNodeEmpId);
	  
			this.Property(t => t.FlowNodeEmpName).HasMaxLength(50);
	  
			this.Property(t => t.FlowNodeEmpIDs).HasMaxLength(500);
	  
			this.Property(t => t.FlowNodeWriteControlsName).HasMaxLength(1000);
	  
			this.Property(t => t.FlowNodeNote).HasMaxLength(1000);
	  
			this.Property(t => t.FlowNodeStatusID);
	  
			this.Property(t => t.FlowNodeDate);
	  
			this.Property(t => t.FlowNodeIsToFinish);
	  
			this.Property(t => t.FlowNodeIsToPass);
	  
			this.Property(t => t.FlowNodeIsToMessage);
	  
			this.Property(t => t.FlowNodeValidateGroup).HasMaxLength(100);
	  
			this.Property(t => t.FlowNodeNodeSkipID);
	  
			this.Property(t => t.FlowNodeNodeRoles).HasMaxLength(100);
	  
			this.Property(t => t.FlowNodeNodeThisDep);
	  
			this.Property(t => t.FlowNodeSelectToBack);
	  
			this.Property(t => t.FlowNodeFinishToNext);
	  
			this.Property(t => t.FlowNodeFromEmpId);
	  
			this.Property(t => t.FlowNodeFromEmpName).HasMaxLength(50);
	  
			this.Property(t => t.FlowNodeFromDateTime);
	  
			this.Property(t => t.FlowNodeRefIsDelete);
	  
			this.Property(t => t.FlowNodeIsRemind);
	  
			this.Property(t => t.FlowNodeSameId);
	  
			this.Property(t => t.FlowNodeSignControlName).HasMaxLength(200);
	  
			this.Property(t => t.FlowModelNodeID);
	  
			this.Property(t => t.FlowNodeDays);
	  
			this.Property(t => t.FlowNodeAutoFinished);
	  
			this.Property(t => t.FlowNodeAutoStatus);
	  
			this.Property(t => t.FlowNodeIsFactNext);
	  
			this.Property(t => t.FlowNodeUrl).HasMaxLength(100);
	  
			this.Property(t => t.FlowNodeAppDefaultValue).HasMaxLength(20);
	  
			this.Property(t => t.FlowNodeAppIsRequired);
	  
			this.Property(t => t.FlowNodeAppIsLastTime);
	  
			this.Property(t => t.AgenEmpId);
	  
			this.Property(t => t.AgenEmpName).HasMaxLength(50);
	  
			this.Property(t => t.CreatorDepId);
	  
			this.Property(t => t.CreatorDepName).HasMaxLength(50);
	  
			this.Property(t => t.FlowNodeSelectToSkip);


			 // Table & Column Mappings
			this.ToTable("View_FlowOnHand");
			this.Property(t => t.FlowRefTable).HasColumnName("FlowRefTable");
			this.Property(t => t.FlowRefID).HasColumnName("FlowRefID");
			this.Property(t => t.FlowName).HasColumnName("FlowName");
			this.Property(t => t.FlowNumber).HasColumnName("FlowNumber");
			this.Property(t => t.FlowVersion).HasColumnName("FlowVersion");
			this.Property(t => t.FlowIsRun).HasColumnName("FlowIsRun");
			this.Property(t => t.FlowUrl).HasColumnName("FlowUrl");
			this.Property(t => t.FlowRole).HasColumnName("FlowRole");
			this.Property(t => t.FlowNum).HasColumnName("FlowNum");
			this.Property(t => t.FlowIsWord).HasColumnName("FlowIsWord");
			this.Property(t => t.FlowStartDate).HasColumnName("FlowStartDate");
			this.Property(t => t.FlowFinishDate).HasColumnName("FlowFinishDate");
			this.Property(t => t.FlowFinishSend).HasColumnName("FlowFinishSend");
			this.Property(t => t.FlowFinishControls).HasColumnName("FlowFinishControls");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.FlowStatusID).HasColumnName("FlowStatusID");
			this.Property(t => t.FlowStatusName).HasColumnName("FlowStatusName");
			this.Property(t => t.FlowControlXml).HasColumnName("FlowControlXml");
			this.Property(t => t.FlowXml).HasColumnName("FlowXml");
			this.Property(t => t.FlowListUrl).HasColumnName("FlowListUrl");
			this.Property(t => t.FlowModelID).HasColumnName("FlowModelID");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.FlowID).HasColumnName("FlowID");
			this.Property(t => t.FlowNodeTypeID).HasColumnName("FlowNodeTypeID");
			this.Property(t => t.FlowNodeName).HasColumnName("FlowNodeName");
			this.Property(t => t.FlowNodeParentID).HasColumnName("FlowNodeParentID");
			this.Property(t => t.FlowNodeOrder).HasColumnName("FlowNodeOrder");
			this.Property(t => t.FlowNodeBackID).HasColumnName("FlowNodeBackID");
			this.Property(t => t.FlowNodeEmpId).HasColumnName("FlowNodeEmpId");
			this.Property(t => t.FlowNodeEmpName).HasColumnName("FlowNodeEmpName");
			this.Property(t => t.FlowNodeEmpIDs).HasColumnName("FlowNodeEmpIDs");
			this.Property(t => t.FlowNodeWriteControlsName).HasColumnName("FlowNodeWriteControlsName");
			this.Property(t => t.FlowNodeNote).HasColumnName("FlowNodeNote");
			this.Property(t => t.FlowNodeStatusID).HasColumnName("FlowNodeStatusID");
			this.Property(t => t.FlowNodeDate).HasColumnName("FlowNodeDate");
			this.Property(t => t.FlowNodeIsToFinish).HasColumnName("FlowNodeIsToFinish");
			this.Property(t => t.FlowNodeIsToPass).HasColumnName("FlowNodeIsToPass");
			this.Property(t => t.FlowNodeIsToMessage).HasColumnName("FlowNodeIsToMessage");
			this.Property(t => t.FlowNodeValidateGroup).HasColumnName("FlowNodeValidateGroup");
			this.Property(t => t.FlowNodeNodeSkipID).HasColumnName("FlowNodeNodeSkipID");
			this.Property(t => t.FlowNodeNodeRoles).HasColumnName("FlowNodeNodeRoles");
			this.Property(t => t.FlowNodeNodeThisDep).HasColumnName("FlowNodeNodeThisDep");
			this.Property(t => t.FlowNodeSelectToBack).HasColumnName("FlowNodeSelectToBack");
			this.Property(t => t.FlowNodeFinishToNext).HasColumnName("FlowNodeFinishToNext");
			this.Property(t => t.FlowNodeFromEmpId).HasColumnName("FlowNodeFromEmpId");
			this.Property(t => t.FlowNodeFromEmpName).HasColumnName("FlowNodeFromEmpName");
			this.Property(t => t.FlowNodeFromDateTime).HasColumnName("FlowNodeFromDateTime");
			this.Property(t => t.FlowNodeRefIsDelete).HasColumnName("FlowNodeRefIsDelete");
			this.Property(t => t.FlowNodeIsRemind).HasColumnName("FlowNodeIsRemind");
			this.Property(t => t.FlowNodeSameId).HasColumnName("FlowNodeSameId");
			this.Property(t => t.FlowNodeSignControlName).HasColumnName("FlowNodeSignControlName");
			this.Property(t => t.FlowModelNodeID).HasColumnName("FlowModelNodeID");
			this.Property(t => t.FlowNodeDays).HasColumnName("FlowNodeDays");
			this.Property(t => t.FlowNodeAutoFinished).HasColumnName("FlowNodeAutoFinished");
			this.Property(t => t.FlowNodeAutoStatus).HasColumnName("FlowNodeAutoStatus");
			this.Property(t => t.FlowNodeIsFactNext).HasColumnName("FlowNodeIsFactNext");
			this.Property(t => t.FlowNodeUrl).HasColumnName("FlowNodeUrl");
			this.Property(t => t.FlowNodeAppDefaultValue).HasColumnName("FlowNodeAppDefaultValue");
			this.Property(t => t.FlowNodeAppIsRequired).HasColumnName("FlowNodeAppIsRequired");
			this.Property(t => t.FlowNodeAppIsLastTime).HasColumnName("FlowNodeAppIsLastTime");
			this.Property(t => t.AgenEmpId).HasColumnName("AgenEmpId");
			this.Property(t => t.AgenEmpName).HasColumnName("AgenEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.FlowNodeSelectToSkip).HasColumnName("FlowNodeSelectToSkip");
			           
			#region Relationships
			#endregion
        }
    }
}  
