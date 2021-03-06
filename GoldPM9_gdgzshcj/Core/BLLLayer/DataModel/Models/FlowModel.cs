﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class FlowModel
    {
        public FlowModel()
        {
            #region DefaultValue
            Id = 0;
            ModelRefTable = "";
            ModelName = "";
            ModelNumber = "";
            ModelVersion = "";
            ModelIsRun = false;
            ModelUrl = "";
            ModelRole = "";
            ModelNum = "";
            ModelIsWord = false;
            ModelFinishSend = "";
            ModelFinishControls = "";
            ModelListUrl = "";
            ModelType = 0;
            ModeModular = "";
            ModelSign = "";
            #endregion
            #region 默认实例化
            this.FK_Flow_FlowModelID = new List<Flow>();
            this.FK_FlowModelNode_FlowModelID = new List<FlowModelNode>();
            #endregion
        }
        ///<summary>模板ID</summary>
        public int Id { get; set; }

        ///<summary>关联表名</summary>
        public string ModelRefTable { get; set; }

        ///<summary>模板名称</summary>
        public string ModelName { get; set; }

        ///<summary>模板编号</summary>
        public string ModelNumber { get; set; }

        ///<summary>版本号</summary>
        public string ModelVersion { get; set; }

        ///<summary>是否流转</summary>
        public bool ModelIsRun { get; set; }

        ///<summary>流转路径</summary>
        public string ModelUrl { get; set; }

        ///<summary>发起角色</summary>
        public string ModelRole { get; set; }

        ///<summary>发起次数</summary>
        public string ModelNum { get; set; }

        ///<summary>自定义流转</summary>
        public bool ModelIsWord { get; set; }

        ///<summary>审批结束发送人员ID</summary>
        public string ModelFinishSend { get; set; }

        ///<summary>审批结束后可以修改控件</summary>
        public string ModelFinishControls { get; set; }

        ///<summary>模板文件二进制</summary>
        public byte[] ModelByte { get; set; }

        ///<summary>处理列表页面路径</summary>
        public string ModelListUrl { get; set; }

        ///<summary>模版类型</summary>
        public int ModelType { get; set; }

        ///<summary>所属模块</summary>
        public string ModeModular { get; set; }

        public string ModelXml
        {
            get; set;
        }

        ///<summary>签名列表控件ID</summary>
        public string ModelSign { get; set; }

        public virtual ICollection<Flow> FK_Flow_FlowModelID { get; set; }
        public virtual ICollection<FlowModelNode> FK_FlowModelNode_FlowModelID { get; set; }
    }
}
