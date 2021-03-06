﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-08 16:28:13
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class DesTaskCheckAttach
    {
        public DesTaskCheckAttach()
        {
            #region DefaultValue
            Id = 0;
            CheckId = 0;
            AttachID = 0;
            AttachName = "";
            AttachVer = 0;
            CheckNodeID = 0;
            CheckNodeTypeID = 0;
            CheckIsExamine = false;
            CheckIsCorrective = false;
            CheckIsCorrectiveType = 0;
            HFNote = "";
            #endregion
            #region 默认实例化
            #endregion
        }
        ///<summary>任务校审意见-附件关系表</summary>
        public int Id { get; set; }

        ///<summary>校审建议ID</summary>
        public int CheckId { get; set; }

        ///<summary>附件ID</summary>
        public long AttachID { get; set; }

        ///<summary>文件名称</summary>
        public string AttachName { get; set; }

        ///<summary>版本号</summary>
        public int AttachVer { get; set; }

        ///<summary>提出节点</summary>
        public int CheckNodeID { get; set; }

        ///<summary>提出节点类型</summary>
        public int CheckNodeTypeID { get; set; }

        ///<summary>校审确认</summary>
        public bool CheckIsExamine { get; set; }

        ///<summary>设计确认</summary>
        public bool CheckIsCorrective { get; set; }

        ///<summary>修改意见类型</summary>
        public int CheckIsCorrectiveType { get; set; }

        public string HFNote { get; set; }

    }
}
