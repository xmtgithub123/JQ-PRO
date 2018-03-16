﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class BussSubFeeInvoice 
    {
        public BussSubFeeInvoice()
        {
			#region DefaultValue
			Id=0;
			ProjSubId=0;
			ConSubId=0;
			SubFeeInvoiceType=0;
			SubFeeInvoiceMoney=0;
			SubFeeInvoiceDate= new DateTime(1900,1,1);
			SubFeeInvoiceEmpId=0;
			subFeeInvoiceNote="";
			SubXml="";
			FormTableID=0;
			ColAttType1=0;
			ColAttType2=0;
			ColAttDate1= new DateTime(1900,1,1);
			ColAttDate2= new DateTime(1900,1,1);
			ColAttFloat1=0;
			ColAttFloat2=0;
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

		///<summary>关联ProjectID</summary>
        public int ProjSubId  { get; set; }

		///<summary>关联ContractSubID</summary>
        public int ConSubId  { get; set; }

		///<summary>收票类型</summary>
        public int SubFeeInvoiceType  { get; set; }

		///<summary>收票金额</summary>
        public decimal SubFeeInvoiceMoney  { get; set; }

		///<summary>收票日期</summary>
        public DateTime SubFeeInvoiceDate  { get; set; }

		///<summary>登记人</summary>
        public int SubFeeInvoiceEmpId  { get; set; }

		///<summary>收票说明</summary>
        public string subFeeInvoiceNote  { get; set; }

		///<summary>收票信息说明</summary>
        public string SubXml  { get; set; }

		///<summary>来源表ID</summary>
        public int FormTableID  { get; set; }

		///<summary>扩展类别1</summary>
        public int ColAttType1  { get; set; }

		///<summary>扩展类别2</summary>
        public int ColAttType2  { get; set; }

		///<summary>扩展时间1</summary>
        public DateTime ColAttDate1  { get; set; }

		///<summary>扩展时间2</summary>
        public DateTime ColAttDate2  { get; set; }

		///<summary>扩展系数1</summary>
        public decimal ColAttFloat1  { get; set; }

		///<summary>扩展系数2</summary>
        public decimal ColAttFloat2  { get; set; }

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

		public virtual BussContractSub FK_BussSubFeeInvoice_ConSubId { get; set; }
public virtual IsoForm FK_BussSubFeeInvoice_FormTableID { get; set; }
public virtual ProjSub FK_BussSubFeeInvoice_ProjSubId { get; set; }
public virtual BaseEmployee FK_BussSubFeeInvoice_SubFeeInvoiceEmpId { get; set; }
public virtual BaseData FK_BussSubFeeInvoice_SubFeeInvoiceType { get; set; }
    }
}
