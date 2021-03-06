﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class BussTendersCost 
    {
        public BussTendersCost()
        {
			#region DefaultValue
			Id=0;
			BussTendersInfoID=0;
			BussTendersInfoDetailID=0;
			TenderFee=0;
			TenderFeePayTime= new DateTime(1900,1,1);
			TenderAgentFee=0;
			TenderAgentFeePayTime= new DateTime(1900,1,1);
			BidBondPay=0;
			BidBondPayTime= new DateTime(1900,1,1);
			BidBondBack=0;
			BidBondBackTime= new DateTime(1900,1,1);
			PerformanceBondPay=0;
			PerformanceBondPayTime= new DateTime(1900,1,1);
			PerformanceBondBack=0;
			PerformanceBondBackTime= new DateTime(1900,1,1);
			CostNote="";
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepId=0;
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
		///<summary>主键</summary>
        public int Id  { get; set; }

		///<summary>投标表ID</summary>
        public int BussTendersInfoID  { get; set; }

		///<summary>投标单位ID</summary>
        public int BussTendersInfoDetailID  { get; set; }

		///<summary>标书费</summary>
        public decimal TenderFee  { get; set; }

		///<summary>标书费缴纳时间</summary>
        public DateTime TenderFeePayTime  { get; set; }

		///<summary>招标代理费</summary>
        public decimal TenderAgentFee  { get; set; }

		///<summary>招标代理费缴纳时间</summary>
        public DateTime TenderAgentFeePayTime  { get; set; }

		///<summary>投标保证金缴纳金额</summary>
        public decimal BidBondPay  { get; set; }

		///<summary>投标保证金缴纳时间</summary>
        public DateTime BidBondPayTime  { get; set; }

		///<summary>投标保证金退还金额</summary>
        public decimal BidBondBack  { get; set; }

		///<summary>投标保证金退还时间</summary>
        public DateTime BidBondBackTime  { get; set; }

		///<summary>履约保证金缴纳金额</summary>
        public decimal PerformanceBondPay  { get; set; }

		///<summary>投标保证金缴纳时间</summary>
        public DateTime PerformanceBondPayTime  { get; set; }

		///<summary>履约保证金退还金额</summary>
        public decimal PerformanceBondBack  { get; set; }

		///<summary>履约保证金退还时间</summary>
        public DateTime PerformanceBondBackTime  { get; set; }

		///<summary>备注说明</summary>
        public string CostNote  { get; set; }

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

		public virtual BussTendersInfoDetail FK_BussTendersCost_BussTendersInfoDetailID { get; set; }
public virtual BussTendersInfo FK_BussTendersCost_BussTendersInfoID { get; set; }
    }
}
