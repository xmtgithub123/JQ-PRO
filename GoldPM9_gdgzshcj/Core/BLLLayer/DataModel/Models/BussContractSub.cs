﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class BussContractSub 
    {
        public BussContractSub()
        {
			#region DefaultValue
			Id=0;
			ConSubNumber="";
			ConSubName="";
			ConSubDate= new DateTime(1900,1,1);
			ConIsFeeFinished= false;
			ConSubStatus=0;
			ConSubFee=0;
			ConSubType=0;
			ConSubCategory=0;
			ConSubCustId=0;
			ConID=0;
			ConSubNote="";
			Condition="";
			ArchNumber="";
			CreateEmpId=0;
			CreateDate= new DateTime(1900,1,1);
			ColAttType1=0;
			ColAttType2=0;
			ColAttType3=0;
			ColAttType4=0;
			ColAttType5=0;
			ColAttVal1="";
			ColAttVal2="";
			ColAttVal3="";
			ColAttVal4="";
			ColAttVal5="";
			ColAttDate1= new DateTime(1900,1,1);
			ColAttDate2= new DateTime(1900,1,1);
			ColAttDate3= new DateTime(1900,1,1);
			ColAttDate4= new DateTime(1900,1,1);
			ColAttDate5= new DateTime(1900,1,1);
			ColAttFloat1=0;
			ColAttFloat2=0;
			ColAttFloat3=0;
			ColAttFloat4=0;
			ColAttFloat5=0;
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
			this.FK_BussSubFeeFact_ConSubId = new List<BussSubFeeFact>();
			this.FK_BussSubFeeInvoice_ConSubId = new List<BussSubFeeInvoice>();
			this.FK_ProjSub_ConSubID = new List<ProjSub>();
			#endregion
        }
		///<summary>自增</summary>
        public int Id  { get; set; }

		///<summary>外委合同编号</summary>
        public string ConSubNumber  { get; set; }

		///<summary>外委合同名称</summary>
        public string ConSubName  { get; set; }

		///<summary>外委合同签订日期</summary>
        public DateTime ConSubDate  { get; set; }

		///<summary>是否置为收清</summary>
        public bool ConIsFeeFinished  { get; set; }

		///<summary>外委合同签订状态</summary>
        public int ConSubStatus  { get; set; }

		///<summary>外委合同金额</summary>
        public decimal ConSubFee  { get; set; }

		///<summary>外委合同类别</summary>
        public int ConSubType  { get; set; }

		///<summary>外包合同类型</summary>
        public int ConSubCategory  { get; set; }

		///<summary>顾客名称ID</summary>
        public int ConSubCustId  { get; set; }

		///<summary>收款合同ID</summary>
        public int ConID  { get; set; }

		///<summary>合同备注</summary>
        public string ConSubNote  { get; set; }

		///<summary>付款条件</summary>
        public string Condition  { get; set; }

		///<summary>归档盒号</summary>
        public string ArchNumber  { get; set; }

		///<summary>创建人</summary>
        public int CreateEmpId  { get; set; }

		///<summary>创建日期</summary>
        public DateTime CreateDate  { get; set; }

		///<summary>扩展类别1</summary>
        public int ColAttType1  { get; set; }

		///<summary>扩展类别2</summary>
        public int ColAttType2  { get; set; }

		///<summary>扩展类别3</summary>
        public int ColAttType3  { get; set; }

		///<summary>扩展类别4</summary>
        public int ColAttType4  { get; set; }

		///<summary>扩展类别5</summary>
        public int ColAttType5  { get; set; }

		///<summary>扩展值1</summary>
        public string ColAttVal1  { get; set; }

		///<summary>扩展值2</summary>
        public string ColAttVal2  { get; set; }

		///<summary>扩展值3</summary>
        public string ColAttVal3  { get; set; }

		///<summary>扩展值4</summary>
        public string ColAttVal4  { get; set; }

		///<summary>扩展值5</summary>
        public string ColAttVal5  { get; set; }

		///<summary>扩展时间1</summary>
        public DateTime ColAttDate1  { get; set; }

		///<summary>扩展时间2</summary>
        public DateTime ColAttDate2  { get; set; }

		///<summary>扩展时间3</summary>
        public DateTime ColAttDate3  { get; set; }

		///<summary>扩展时间4</summary>
        public DateTime ColAttDate4  { get; set; }

		///<summary>扩展时间5</summary>
        public DateTime ColAttDate5  { get; set; }

		///<summary>扩展系数1</summary>
        public decimal ColAttFloat1  { get; set; }

		///<summary>扩展系数2</summary>
        public decimal ColAttFloat2  { get; set; }

		///<summary>扩展系数3</summary>
        public decimal ColAttFloat3  { get; set; }

		///<summary>扩展系数4</summary>
        public decimal ColAttFloat4  { get; set; }

		///<summary>扩展系数5</summary>
        public decimal ColAttFloat5  { get; set; }

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

        /// <summary>
        /// 所属公司 0分公司 1设计公司 2工程公司
        /// </summary>
        public int CompanyID { get; set; }


        public virtual BussContract FK_BussContractSub_ConID { get; set; }
public virtual BaseData FK_BussContractSub_ConSubCategory { get; set; }
public virtual BussCustomer FK_BussContractSub_ConSubCustId { get; set; }
public virtual BaseData FK_BussContractSub_ConSubStatus { get; set; }
public virtual BaseData FK_BussContractSub_ConSubType { get; set; }
        public virtual ICollection<BussSubFeeFact> FK_BussSubFeeFact_ConSubId { get; set; }
        public virtual ICollection<BussSubFeeInvoice> FK_BussSubFeeInvoice_ConSubId { get; set; }
        public virtual ICollection<ProjSub> FK_ProjSub_ConSubID { get; set; }
    }
}