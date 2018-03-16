﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class PayBalanceEngineering 
    {
        public PayBalanceEngineering()
        {
			#region DefaultValue
			Id=0;
			BalanceLotID=0;
			EngineeringID=0;
			EngAmountArrange=0;
			PayModelID=0;
			ModelXML="";
			BalanceState=0;
			LastModificationTime= new DateTime(1900,1,1);
			LastModifierEmpId=0;
			LastModifierEmpName="";
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepId=0;
			CreatorDepName="";
			DeleterEmpId=0;
			DeleterEmpName="";
			DeletionTime= new DateTime(1900,1,1);
			#endregion
			#region 默认实例化
			this.FK_PayBalanceUserAccount_BalanceEngineeringID = new List<PayBalanceUserAccount>();
			#endregion
        }
		///<summary>自增</summary>
        public int Id  { get; set; }

		///<summary>默认结算批次ID</summary>
        public int BalanceLotID  { get; set; }

		///<summary>工程ID</summary>
        public int EngineeringID  { get; set; }

		///<summary>本次分配产值</summary>
        public decimal EngAmountArrange  { get; set; }

		///<summary>绩效模块主键ID</summary>
        public int PayModelID  { get; set; }

		///<summary>绩效模块xml</summary>
        public string ModelXML  { get; set; }

		///<summary>工程结算状态</summary>
        public int BalanceState  { get; set; }

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

		public virtual PayBalanceLot FK_PayBalanceEngineering_BalanceLotID { get; set; }
        public virtual ICollection<PayBalanceUserAccount> FK_PayBalanceUserAccount_BalanceEngineeringID { get; set; }
    }
}
