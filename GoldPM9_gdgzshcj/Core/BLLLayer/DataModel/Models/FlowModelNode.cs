﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class FlowModelNode
    {
        public FlowModelNode()
        {
            #region DefaultValue
            Id = 0;
            FlowModelID = 0;
            NodeTypeID = 0;
            NodeName = "";
            NodeParentID = 0;
            NodeOrder = 0;
            NodeBackID = 0;
            NodeEmpIDs = "";
            NodeWriteControlsName = "";
            NodeIsToFinish = false;
            NodeIsToPass = false;
            NodeIsToMessage = false;
            NodeValidateGroup = "";
            NodeNodeSkipID = 0;
            NodeRoleS = "";
            NodeThisDep = false;
            NodeSelectToBack = false;
            NodeSelectToSkip = false;
            NodeFinishToNext = false;
            NodeSameId = 0;
            NodeSignControlName = "";
            NodeDays = 0;
            NodeAutoFinished = false;
            NodeUrl = "";
            NodeAppDefaultValue = "同意";
            NodeAppIsRequired = false;
            NodeAppIsLastTime = false;
            NodeIsFactNext = false;
            NodeXml = "<Root />";
            #endregion
            #region 默认实例化
            #endregion
        }
        ///<summary>节点ID</summary>
        public int Id { get; set; }

        ///<summary>模板ID</summary>
        public int FlowModelID { get; set; }

        ///<summary>节点类别［会签时为-1］</summary>
        public int NodeTypeID { get; set; }

        ///<summary>节点名称</summary>
        public string NodeName { get; set; }

        ///<summary>父级节点</summary>
        public int NodeParentID { get; set; }

        ///<summary>排序</summary>
        public int NodeOrder { get; set; }

        ///<summary>回退节点</summary>
        public int NodeBackID { get; set; }

        ///<summary>资格人员</summary>
        public string NodeEmpIDs { get; set; }

        ///<summary>控件设置</summary>
        public string NodeWriteControlsName { get; set; }

        ///<summary>直接审批结束</summary>
        public bool NodeIsToFinish { get; set; }

        ///<summary>允许直接否决</summary>
        public bool NodeIsToPass { get; set; }

        ///<summary>审批结束后发送消息提醒</summary>
        public bool NodeIsToMessage { get; set; }

        ///<summary>分组验证</summary>
        public string NodeValidateGroup { get; set; }

        ///<summary>跳步节点</summary>
        public int NodeNodeSkipID { get; set; }

        ///<summary>资格角色</summary>
        public string NodeRoleS { get; set; }

        ///<summary>列出本部门人员</summary>
        public bool NodeThisDep { get; set; }

        ///<summary>选择回退</summary>
        public bool NodeSelectToBack { get; set; }

        ///<summary>选择跳步</summary>
        public bool NodeSelectToSkip { get; set; }

        ///<summary>审批结束重新提交</summary>
        public bool NodeFinishToNext { get; set; }

        ///<summary>相同节点人员</summary>
        public int NodeSameId { get; set; }

        ///<summary>签名控件名称</summary>
        public string NodeSignControlName { get; set; }

        ///<summary>默认时效</summary>
        public decimal NodeDays { get; set; }

        ///<summary>自动审批</summary>
        public bool NodeAutoFinished { get; set; }

        ///<summary>流转路径</summary>
        public string NodeUrl { get; set; }

        ///<summary>默认审批意见</summary>
        public string NodeAppDefaultValue { get; set; }

        ///<summary>审批意见是否必填</summary>
        public bool NodeAppIsRequired { get; set; }

        ///<summary>上次意见是否显示</summary>
        public bool NodeAppIsLastTime { get; set; }

        ///<summary>运行时自动选人提交</summary>
        public bool NodeIsFactNext { get; set; }  

        public string NodeXml
        {
            get; set;
        }

        public virtual FlowModel FK_FlowModelNode_FlowModelID { get; set; }
    }
}
