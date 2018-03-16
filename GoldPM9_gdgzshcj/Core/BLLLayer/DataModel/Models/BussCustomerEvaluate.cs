﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class BussCustomerEvaluate 
    {
        public BussCustomerEvaluate()
        {
			#region DefaultValue
			Id=0;
			CustID=0;
			ProjID=0;
			ReceiveEmpIDs="";
			EvaluaterNote="";
			LastModificationTime= new DateTime(1900,1,1);
			LastModifierEmpId=0;
			LastModifierEmpName="";
			OutstandingNote="";
			MainOutstanding="";
			AptitudeCondition="";
			ManageSystem="";
			AwardCondition="";
			OperateCondition="";
			EvaluateConclusion="";
			FlowID=0;
			FlowTime= new DateTime(1900,1,1);
			AgenEmpId=0;
			AgenEmpName="";
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepId=0;
			CreatorDepName="";
			EvaluationTypeID=0;
			CustNumber="";
			#endregion
			#region 默认实例化
			this.FK_BussCustomerEvaluateDetail_BussCustomerEvaluateID = new List<BussCustomerEvaluateDetail>();
			#endregion
        }
		///<summary>主键</summary>
        public int Id  { get; set; }

		///<summary>客户ID</summary>
        public int CustID  { get; set; }

		///<summary>项目ID</summary>
        public int ProjID  { get; set; }

		///<summary>接受人员</summary>
        public string ReceiveEmpIDs  { get; set; }

		///<summary>评价说明</summary>
        public string EvaluaterNote  { get; set; }

		///<summary>最后修改时间</summary>
        public DateTime LastModificationTime  { get; set; }

		///<summary>最后修改人ID</summary>
        public int LastModifierEmpId  { get; set; }

		///<summary>最后修改人姓名</summary>
        public string LastModifierEmpName  { get; set; }

		///<summary>提供的产品或服务</summary>
        public string OutstandingNote  { get; set; }

		///<summary>主要业绩</summary>
        public string MainOutstanding  { get; set; }

		///<summary>资质情况</summary>
        public string AptitudeCondition  { get; set; }

		///<summary>管理体系情况</summary>
        public string ManageSystem  { get; set; }

		///<summary>获奖情况</summary>
        public string AwardCondition  { get; set; }

		///<summary>经营情况</summary>
        public string OperateCondition  { get; set; }

		///<summary>评价结论</summary>
        public string EvaluateConclusion  { get; set; }

		///<summary>流程ID</summary>
        public long FlowID  { get; set; }

		///<summary>审批结束时间</summary>
        public DateTime FlowTime  { get; set; }

		///<summary>代理人ID</summary>
        public int AgenEmpId  { get; set; }

		///<summary>代理人姓名</summary>
        public string AgenEmpName  { get; set; }

		///<summary>创建时间</summary>
        public DateTime CreationTime  { get; set; }

		///<summary>创建人ID</summary>
        public int CreatorEmpId  { get; set; }

		///<summary>创建人姓名</summary>
        public string CreatorEmpName  { get; set; }

		///<summary>创建部门</summary>
        public int CreatorDepId  { get; set; }

		///<summary>创建部门姓名</summary>
        public string CreatorDepName  { get; set; }

		///<summary>评价结果类型</summary>
        public int EvaluationTypeID  { get; set; }

		///<summary>供方编号</summary>
        public string CustNumber  { get; set; }

		public virtual BussCustomer FK_BussCustomerEvaluate_CustID { get; set; }
        public virtual ICollection<BussCustomerEvaluateDetail> FK_BussCustomerEvaluateDetail_BussCustomerEvaluateID { get; set; }
    }
}