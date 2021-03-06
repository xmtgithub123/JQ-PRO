﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class BussContract
    {
        public BussContract()
        {
            #region DefaultValue
            Id = 0;
            ConNumber = "";
            ConStreamNumber = "";
            ConName = "";
            ConFee = 0;
            ConBalanceFee = 0;
            ConIsFeeFinished = false;
            ConNote = "";
            ConPay = "";
            CustID = 0;
            CustName = "";
            CustLinkManName = "";
            CustLinkManTel = "";
            CustLinkManWeb = "";
            ConStatus = 0;
            ConMainNumber = "";
            ConDate = new DateTime(1900, 1, 1);
            ConFileNumber = "";
            FatherID = 0;
            CreateEmpId = 0;
            CreateEmpName = "";
            CreateDate = new DateTime(1900, 1, 1);
            ConType = 0;
            ConYear = 0;
            ConArea = 0;
            ConFulfilType = 0;
            ColAttType1 = 0;
            ColAttType2 = 0;
            ColAttType3 = 0;
            ColAttType4 = 0;
            ColAttType5 = 0;
            ColAttVal1 = "";
            ColAttVal2 = "";
            ColAttVal3 = "";
            ColAttVal4 = "";
            ColAttVal5 = "";
            ColAttDate1 = new DateTime(1900, 1, 1);
            ColAttDate2 = new DateTime(1900, 1, 1);
            ColAttDate3 = new DateTime(1900, 1, 1);
            ColAttDate4 = new DateTime(1900, 1, 1);
            ColAttDate5 = new DateTime(1900, 1, 1);
            ColAttFloat1 = 0;
            ColAttFloat2 = 0;
            ColAttFloat3 = 0;
            ColAttFloat4 = 0;
            ColAttFloat5 = 0;
            LastModificationTime = new DateTime(1900, 1, 1);
            LastModifierEmpId = 0;
            LastModifierEmpName = "";
            CreationTime = new DateTime(1900, 1, 1);
            CreatorEmpId = 0;
            CreatorEmpName = "";
            FlowID = 0;
            FlowTime = new DateTime(1900, 1, 1);
            AgenEmpId = 0;
            AgenEmpName = "";
            CreatorDepId = 0;
            CreatorDepName = "";
            DeleterEmpId = 0;
            DeleterEmpName = "";
            DeletionTime = new DateTime(1900, 1, 1);
            SHYJ = "";
            #endregion
            #region 默认实例化
            this.FK_BusProjContractRelation_ConID = new List<BusProjContractRelation>();
            this.FK_BussContractOther_ConID = new List<BussContractOther>();
            this.FK_BussContractSub_ConID = new List<BussContractSub>();
            this.FK_BussFeeFact_ConID = new List<BussFeeFact>();
            this.FK_BussFeeInvoice_ConID = new List<BussFeeInvoice>();
            this.FK_BussFeePlan_ConID = new List<BussFeePlan>();
            #endregion
        }
        ///<summary>自增</summary>
        public int Id { get; set; }

        ///<summary>合同编号</summary>
        public string ConNumber { get; set; }

        ///<summary>合同流水编号</summary>
        public string ConStreamNumber { get; set; }

        ///<summary>合同名称</summary>
        public string ConName { get; set; }

        ///<summary>合同金额</summary>
        public decimal ConFee { get; set; }

        ///<summary>结算金额</summary>
        public decimal ConBalanceFee { get; set; }

        ///<summary>是否置为收清</summary>
        public bool ConIsFeeFinished { get; set; }

        ///<summary>备注</summary>
        public string ConNote { get; set; }

        ///<summary>付款条件</summary>
        public string ConPay { get; set; }

        ///<summary>客户联系人ID</summary>
        public int CustID { get; set; }

        ///<summary>客户名称</summary>
        public string CustName { get; set; }

        ///<summary>联系人</summary>
        public string CustLinkManName { get; set; }

        ///<summary>联系电话</summary>
        public string CustLinkManTel { get; set; }

        ///<summary>邮箱</summary>
        public string CustLinkManWeb { get; set; }

        ///<summary>合同状态</summary>
        public int ConStatus { get; set; }

        ///<summary>主合同编号</summary>
        public string ConMainNumber { get; set; }

        ///<summary>签订日期</summary>
        public DateTime ConDate { get; set; }

        ///<summary>归档盒号</summary>
        public string ConFileNumber { get; set; }

        ///<summary>父合同的ID</summary>
        public int FatherID { get; set; }

        ///<summary>登记人</summary>
        public int CreateEmpId { get; set; }

        ///<summary>(登记人名称)</summary>
        public string CreateEmpName { get; set; }

        ///<summary>登记日期</summary>
        public DateTime CreateDate { get; set; }

        ///<summary>合同类别</summary>
        public int ConType { get; set; }

        ///<summary>合同年度</summary>
        public int ConYear { get; set; }

        ///<summary>合同区域</summary>
        public int ConArea { get; set; }

        ///<summary>合同履行方式</summary>
        public int ConFulfilType { get; set; }

        ///<summary>扩展类别1</summary>
        public int ColAttType1 { get; set; }

        ///<summary>扩展类别2</summary>
        public int ColAttType2 { get; set; }

        ///<summary>扩展类别3</summary>
        public int ColAttType3 { get; set; }

        ///<summary>扩展类别4</summary>
        public int ColAttType4 { get; set; }

        ///<summary>扩展类别5</summary>
        public int ColAttType5 { get; set; }

        ///<summary>扩展值1</summary>
        public string ColAttVal1 { get; set; }

        ///<summary>扩展值2</summary>
        public string ColAttVal2 { get; set; }

        ///<summary>扩展值3</summary>
        public string ColAttVal3 { get; set; }

        ///<summary>扩展值4</summary>
        public string ColAttVal4 { get; set; }

        ///<summary>扩展值5</summary>
        public string ColAttVal5 { get; set; }

        ///<summary>扩展时间1</summary>
        public DateTime ColAttDate1 { get; set; }

        ///<summary>扩展时间2</summary>
        public DateTime ColAttDate2 { get; set; }

        ///<summary>扩展时间3</summary>
        public DateTime ColAttDate3 { get; set; }

        ///<summary>扩展时间4</summary>
        public DateTime ColAttDate4 { get; set; }

        ///<summary>扩展时间5</summary>
        public DateTime ColAttDate5 { get; set; }

        ///<summary>扩展系数1</summary>
        public decimal ColAttFloat1 { get; set; }

        ///<summary>扩展系数2</summary>
        public decimal ColAttFloat2 { get; set; }

        ///<summary>扩展系数3</summary>
        public decimal ColAttFloat3 { get; set; }

        ///<summary>扩展系数4</summary>
        public decimal ColAttFloat4 { get; set; }

        ///<summary>扩展系数5</summary>
        public decimal ColAttFloat5 { get; set; }

        ///<summary>最后修改时间</summary>
        public DateTime LastModificationTime { get; set; }

        ///<summary>最后修改人ID</summary>
        public int LastModifierEmpId { get; set; }

        ///<summary>最后修改人姓名</summary>
        public string LastModifierEmpName { get; set; }

        ///<summary>创建时间</summary>
        public DateTime CreationTime { get; set; }

        ///<summary>创建人ID</summary>
        public int CreatorEmpId { get; set; }

        ///<summary>创建人姓名</summary>
        public string CreatorEmpName { get; set; }

        ///<summary>流程ID</summary>
        public long FlowID { get; set; }

        ///<summary>审批结束时间</summary>
        public DateTime FlowTime { get; set; }

        ///<summary>代理人ID</summary>
        public int AgenEmpId { get; set; }

        ///<summary>代理人姓名</summary>
        public string AgenEmpName { get; set; }

        ///<summary>创建部门</summary>
        public int CreatorDepId { get; set; }

        ///<summary>创建部门姓名</summary>
        public string CreatorDepName { get; set; }

        ///<summary>删除人员</summary>
        public int DeleterEmpId { get; set; }

        ///<summary>删除人员姓名</summary>
        public string DeleterEmpName { get; set; }

        ///<summary>删除日期</summary>
        public DateTime DeletionTime { get; set; }

        public decimal BGConFee { get; set; }

        public string SHYJ { get; set; }

        /// <summary>
        /// 所属公司 0分公司 1设计公司 2工程公司
        /// </summary>
        public int CompanyID { get; set; }

        public virtual ICollection<BusProjContractRelation> FK_BusProjContractRelation_ConID { get; set; }
        public virtual BaseData FK_BussContract_ConStatus { get; set; }
        public virtual BaseData FK_BussContract_ConType { get; set; }
        public virtual ICollection<BussContractOther> FK_BussContractOther_ConID { get; set; }
        public virtual ICollection<BussContractSub> FK_BussContractSub_ConID { get; set; }
        public virtual ICollection<BussFeeFact> FK_BussFeeFact_ConID { get; set; }
        public virtual ICollection<BussFeeInvoice> FK_BussFeeInvoice_ConID { get; set; }
        public virtual ICollection<BussFeePlan> FK_BussFeePlan_ConID { get; set; }
    }
}
