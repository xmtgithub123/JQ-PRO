﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class DesTaskGantt 
    {
        public DesTaskGantt()
        {
			#region DefaultValue
			Id=0;
			ProjId=0;
			TypeId=0;
			TaskId=0;
			ParentId=0;
			ManageEmpId=0;
			Name="";
			Depends="";
			Progress=0;
			Duration=0;
			Path="";
			Level=0;
			DatePlanStart= new DateTime(1900,1,1);
			DatePlanFinish= new DateTime(1900,1,1);
			KeyPointType=0;
			startIsMilestone= false;
			endIsMilestone= false;
			Description="";
			Order=0;
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepID=0;
			CreatorDepName="";
			LastModificationTime= new DateTime(1900,1,1);
			LastModifierEmpId=0;
			LastModifierEmpName="";
			DeleterEmpId=0;
			DeleterEmpName="";
			DeletionTime= new DateTime(1900,1,1);
			AgenCreatorEmpId=0;
			AgenCreatorEmpName="";
			AgenLastModifierEmpId=0;
			AgenLastModifierEmpName="";
			AgenDeleterEmpId=0;
			AgenDeleterEmpName="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>DesGantt表主键</summary>
        public int Id  { get; set; }

		///<summary>项目表的ID</summary>
        public int ProjId  { get; set; }

		///<summary></summary>
        public int TypeId  { get; set; }

		///<summary>DesTask表的ID</summary>
        public long TaskId  { get; set; }

		///<summary>父ID</summary>
        public int ParentId  { get; set; }

		///<summary>管理人员</summary>
        public int ManageEmpId  { get; set; }

		///<summary>工作内容</summary>
        public string Name  { get; set; }

		///<summary>相关关系</summary>
        public string Depends  { get; set; }

		///<summary>完成比列</summary>
        public decimal Progress  { get; set; }

		///<summary>间隔天数</summary>
        public int Duration  { get; set; }

		///<summary>相对路径</summary>
        public string Path  { get; set; }

		///<summary></summary>
        public int Level  { get; set; }

		///<summary>计划开始时间</summary>
        public DateTime DatePlanStart  { get; set; }

		///<summary>计划结束时间</summary>
        public DateTime DatePlanFinish  { get; set; }

		///<summary>关键节点类型</summary>
        public int KeyPointType  { get; set; }

		///<summary>里程碑开始标识</summary>
        public bool startIsMilestone  { get; set; }

		///<summary>里程碑结束标识</summary>
        public bool endIsMilestone  { get; set; }

		///<summary>其他描述</summary>
        public string Description  { get; set; }

		///<summary>排序</summary>
        public int Order  { get; set; }

		///<summary>创建时间</summary>
        public DateTime CreationTime  { get; set; }

		///<summary>创建人ID</summary>
        public int CreatorEmpId  { get; set; }

		///<summary>创建人姓名</summary>
        public string CreatorEmpName  { get; set; }

		///<summary>创建部门</summary>
        public int CreatorDepID  { get; set; }

		///<summary>创建部门姓名</summary>
        public string CreatorDepName  { get; set; }

		///<summary>最后修改时间</summary>
        public DateTime LastModificationTime  { get; set; }

		///<summary>最后修改人ID</summary>
        public int LastModifierEmpId  { get; set; }

		///<summary>最后修改人姓名</summary>
        public string LastModifierEmpName  { get; set; }

		///<summary>删除人员</summary>
        public int DeleterEmpId  { get; set; }

		///<summary>删除人员姓名</summary>
        public string DeleterEmpName  { get; set; }

		///<summary>删除日期</summary>
        public DateTime DeletionTime  { get; set; }

		///<summary>代理创建人ID</summary>
        public int AgenCreatorEmpId  { get; set; }

		///<summary>代理创建人姓名</summary>
        public string AgenCreatorEmpName  { get; set; }

		///<summary>代理最后修改人ID</summary>
        public int AgenLastModifierEmpId  { get; set; }

		///<summary>代理最后修改人姓名</summary>
        public string AgenLastModifierEmpName  { get; set; }

		///<summary>代理删除人ID</summary>
        public int AgenDeleterEmpId  { get; set; }

		///<summary>代理删除人姓名</summary>
        public string AgenDeleterEmpName  { get; set; }

		    }
}
