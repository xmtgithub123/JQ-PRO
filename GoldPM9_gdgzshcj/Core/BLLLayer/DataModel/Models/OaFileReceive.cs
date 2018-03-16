﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-19 11:56:09
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class OaFileReceive 
    {
        public OaFileReceive()
        {
			#region DefaultValue
			Id=0;
			OaFileDate= new DateTime(1900,1,1);
			OaFileName="";
			OaFilePage=0;
			OaFileSend="";
			OaFileGetEmp="";
			OaFileGetEmpDept="";
			OaFileGetNote="";
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
		///<summary>自增</summary>
        public int Id  { get; set; }

		///<summary>发文日期</summary>
        public DateTime OaFileDate  { get; set; }

		///<summary>文件名称</summary>
        public string OaFileName  { get; set; }

		///<summary>页数</summary>
        public int OaFilePage  { get; set; }

		///<summary>收文部门</summary>
        public string OaFileSend  { get; set; }

		///<summary>发文人</summary>
        public string OaFileGetEmp  { get; set; }

		///<summary>发文部门</summary>
        public string OaFileGetEmpDept  { get; set; }

		///<summary>备注</summary>
        public string OaFileGetNote  { get; set; }

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

		    }
}
