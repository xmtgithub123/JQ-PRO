﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class DesTaskAttachEx 
    {
        public DesTaskAttachEx()
        {
			#region DefaultValue
			Id=0;
			AttachId=0;
			AttachVer=0;
			AttachFlowNode="";
			ColAttType1=0;
			ColAttType2=0;
			ColAttXml="";
			AttachNextFlowNode="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>----任务附件扩展表----</summary>
        public long Id  { get; set; }

		///<summary>关联附件ID</summary>
        public long AttachId  { get; set; }

		///<summary>关联附件版本</summary>
        public int AttachVer  { get; set; }

		///<summary>附件校审流程配置</summary>
        public string AttachFlowNode  { get; set; }

		///<summary>扩展类别1</summary>
        public int ColAttType1  { get; set; }

		///<summary>扩展类别2</summary>
        public int ColAttType2  { get; set; }

		///<summary>扩展类别</summary>
        public string ColAttXml  { get; set; }

		///<summary>附件下一流程首节点配置</summary>
        public string AttachNextFlowNode  { get; set; }

		    }
}