﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-03-20 19:11:35
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class FlowNodeExe 
    {
        public FlowNodeExe()
        {
			#region DefaultValue
			Id=0;
			FlowID=0;
			FlowNodeID=0;
			ExeActionID=0;
			ExeActionDate= new DateTime(1900,1,1);
			ExeArgEmpId=0;
			ExeEmpId=0;
			ExeNote="";
			ExeEmpName="";
			ExeArgEmpName="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>自增</summary>
        public int Id  { get; set; }

		///<summary>流程ID</summary>
        public int FlowID  { get; set; }

		///<summary>流程节点ID</summary>
        public int FlowNodeID  { get; set; }

		///<summary>动作</summary>
        public int ExeActionID  { get; set; }

		///<summary>操作日期</summary>
        public DateTime ExeActionDate  { get; set; }

		///<summary>处理人</summary>
        public int ExeArgEmpId  { get; set; }

		///<summary>经办人</summary>
        public int ExeEmpId  { get; set; }

		///<summary>处理备注</summary>
        public string ExeNote  { get; set; }

		///<summary>经办人姓名</summary>
        public string ExeEmpName  { get; set; }

		///<summary>处理人姓名</summary>
        public string ExeArgEmpName  { get; set; }

		public virtual BaseDataSystem FK_FlowNodeExe_ExeActionID { get; set; }
public virtual Flow FK_FlowNodeExe_FlowID { get; set; }
public virtual FlowNode FK_FlowNodeExe_FlowNodeID { get; set; }
    }
}
