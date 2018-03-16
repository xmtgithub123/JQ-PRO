﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class OaMeetingUse 
    {
        public OaMeetingUse()
        {
			#region DefaultValue
			Id=0;
			MeetingStartDate= new DateTime(1900,1,1);
			MeetingEndDate= new DateTime(1900,1,1);
			MeetingID=0;
			MeetingLeader=0;
			MeetingUseConent="";
			State=0;
			MeetingJoinPeo=0;
			MeetingNote="";
			LastModificationTime= new DateTime(1900,1,1);
			LastModifierEmpId=0;
			LastModifierEmpName="";
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			FlowID=0;
			FlowTime= new DateTime(1900,1,1);
			AgenEmpId=0;
			AgenEmpName="";
			CreatorDepId=0;
			CreatorDepName="";
			DeleterEmpId=0;
			DeleterEmpName="";
			DeletionTime= new DateTime(1900,1,1);
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>自增ID</summary>
        public int Id  { get; set; }

		///<summary>计划开始</summary>
        public DateTime MeetingStartDate  { get; set; }

		///<summary>计划结束</summary>
        public DateTime MeetingEndDate  { get; set; }

		///<summary>会议室ID</summary>
        public int MeetingID  { get; set; }

		///<summary>会议室负责人</summary>
        public int MeetingLeader  { get; set; }

		///<summary>用途</summary>
        public string MeetingUseConent  { get; set; }

		///<summary>时间确认(0未确认,1已确认)</summary>
        public int State  { get; set; }

		///<summary>参与人数</summary>
        public int MeetingJoinPeo  { get; set; }

		///<summary>备注</summary>
        public string MeetingNote  { get; set; }

		///<summary>最后修改时间</summary>
        public DateTime LastModificationTime  { get; set; }

		///<summary>最后修改人ID</summary>
        public int LastModifierEmpId  { get; set; }

		///<summary>最后修改人姓名</summary>
        public string LastModifierEmpName  { get; set; }

		///<summary>创建时间</summary>
        public DateTime CreationTime  { get; set; }

		///<summary>创建人ID</summary>
        public int CreatorEmpId  { get; set; }

		///<summary>创建人姓名</summary>
        public string CreatorEmpName  { get; set; }

		///<summary>流程ID</summary>
        public long FlowID  { get; set; }

		///<summary>审批结束时间</summary>
        public DateTime FlowTime  { get; set; }

		///<summary>代理人ID</summary>
        public int AgenEmpId  { get; set; }

		///<summary>代理人姓名</summary>
        public string AgenEmpName  { get; set; }

		///<summary>创建部门</summary>
        public int CreatorDepId  { get; set; }

		///<summary>创建部门姓名</summary>
        public string CreatorDepName  { get; set; }

		///<summary>删除人员</summary>
        public int DeleterEmpId  { get; set; }

		///<summary>删除人员姓名</summary>
        public string DeleterEmpName  { get; set; }

		///<summary>删除日期</summary>
        public DateTime DeletionTime  { get; set; }

		public virtual OaMeetingRoom FK_OaMeetingUse_MeetingID { get; set; }
    }
}
