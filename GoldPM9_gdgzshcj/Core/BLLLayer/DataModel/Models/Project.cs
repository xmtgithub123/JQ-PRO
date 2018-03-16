﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-11-01 10:26:26
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class Project
    {
        public Project()
        {
            #region DefaultValue
            Id = 0;
            ProjNumber = "";
            ProjName = "";
            ProjNote = "";
            ParentId = 0;
            DateCreate = new DateTime(1900, 1, 1);
            DatePlanStart = new DateTime(1900, 1, 1);
            DatePlanFinish = new DateTime(1900, 1, 1);
            DatePlanDeliver = new DateTime(1900, 1, 1);
            DeleterEmpId = 0;
            DeleterEmpName = "";
            DeletionTime = new DateTime(1900, 1, 1);
            ProjTaskContent = "";
            ProjDemand = "";
            ProjNoteOther = "";
            CustID = 0;
            CustName = "";
            CustLinkMan = "";
            CustLinkTel = "";
            CustLinkWeb = "";
            ProjStatus = 0;
            ProjTypeID = 0;
            ProjTaskSource = 0;
            ProjAreaID = 0;
            ProjPropertyID = 0;
            ProjVoltID = 0;
            ProjPhaseIds = "";
            ProjPhaseInfo = "";
            ProjDepId = 0;
            ProjJoinDepIds = "";
            ProjFeeSource = "";
            ProjGradeID = 0;
            TaskBasisName = "";
            TaskBasisNumber = "";
            ProjEmpId = 0;
            ProjEmpName = "";
            ProjFee = 0;
            CreatorDepName = "";
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
            ColAttType1 = 0;
            ColAttType2 = 0;
            ColAttType3 = 0;
            ColAttType4 = 0;
            ColAttType5 = 0;
            ColAttType6 = 0;
            ColAttType7 = 0;
            ColAttType8 = 0;
            ColAttType9 = 0;
            ColAttType10 = 0;
            ColAttType11 = 0;
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
            ColAttXml = "";
            ProjBanlanceFee = 0;
            ProjBanlanceStatus = 0;
            IsQuote = 0;
            BridgeFact = 0;
            CompanyID = 0;
            FProjEmpId = 0;
            FProjEmpName = "";
            #endregion
            #region 默认实例化
            this.FK_BusProjContractRelation_ProjID = new List<BusProjContractRelation>();
            this.FK_IsoForm_EngID = new List<IsoForm>();
            this.FK_IsoForm_ProjID = new List<IsoForm>();
            this.FK_ProjCost_ProjID = new List<ProjCost>();
            this.FK_ProjectFavourite_ProjectId = new List<ProjectFavourite>();
            this.FK_ProjSub_ProjID = new List<ProjSub>();
            #endregion
        }
        ///<summary>自增</summary>
        public int Id { get; set; }

        ///<summary>任务编号</summary>
        public string ProjNumber { get; set; }

        ///<summary>任务名称</summary>
        public string ProjName { get; set; }

        ///<summary>项目备注</summary>
        public string ProjNote { get; set; }

        ///<summary>父级ID</summary>
        public int ParentId { get; set; }

        ///<summary>立项时间</summary>
        public DateTime DateCreate { get; set; }

        ///<summary>计划开始</summary>
        public DateTime DatePlanStart { get; set; }

        ///<summary>计划结束</summary>
        public DateTime DatePlanFinish { get; set; }

        ///<summary>计划交付时间</summary>
        public DateTime DatePlanDeliver { get; set; }

        ///<summary>删除人员</summary>
        public int DeleterEmpId { get; set; }

        ///<summary>删除人员姓名</summary>
        public string DeleterEmpName { get; set; }

        ///<summary>删除日期</summary>
        public DateTime DeletionTime { get; set; }

        ///<summary>任务内容</summary>
        public string ProjTaskContent { get; set; }

        ///<summary>项目要求</summary>
        public string ProjDemand { get; set; }

        ///<summary>其它备注</summary>
        public string ProjNoteOther { get; set; }

        ///<summary></summary>
        public int CustID { get; set; }

        ///<summary>顾客名称</summary>
        public string CustName { get; set; }

        ///<summary>联系人</summary>
        public string CustLinkMan { get; set; }

        ///<summary>联系电话</summary>
        public string CustLinkTel { get; set; }

        ///<summary>网络联系方式</summary>
        public string CustLinkWeb { get; set; }

        ///<summary>工程进度状态</summary>
        public int ProjStatus { get; set; }

        ///<summary>项目类型</summary>
        public int ProjTypeID { get; set; }

        ///<summary>任务来源</summary>
        public int ProjTaskSource { get; set; }

        ///<summary>项目地区</summary>
        public int ProjAreaID { get; set; }

        ///<summary>项目性质</summary>
        public int ProjPropertyID { get; set; }

        ///<summary>电压性质</summary>
        public int ProjVoltID { get; set; }

        ///<summary>项目阶段</summary>
        public string ProjPhaseIds { get; set; }

        ///<summary></summary>
        public string ProjPhaseInfo { get; set; }

        ///<summary>负责部门</summary>
        public int ProjDepId { get; set; }

        ///<summary>协办部门</summary>
        public string ProjJoinDepIds { get; set; }

        ///<summary>资金来源</summary>
        public string ProjFeeSource { get; set; }

        ///<summary>紧急程度</summary>
        public int ProjGradeID { get; set; }

        ///<summary>任务依据</summary>
        public string TaskBasisName { get; set; }

        ///<summary>依据编号</summary>
        public string TaskBasisNumber { get; set; }

        ///<summary>项目设总</summary>
        public int ProjEmpId { get; set; }

        ///<summary>设总姓名</summary>
        public string ProjEmpName { get; set; }

        ///<summary>项目费用</summary>
        public decimal ProjFee { get; set; }

        ///<summary>创建部门姓名</summary>
        public string CreatorDepName { get; set; }

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

        ///<summary>扩展类别5</summary>
        public int ColAttType6 { get; set; }

        ///<summary>扩展类别5</summary>
        public int ColAttType7 { get; set; }

        ///<summary>扩展类别5</summary>
        public int ColAttType8 { get; set; }

        ///<summary>扩展类别5</summary>
        public int ColAttType9 { get; set; }

        ///<summary>扩展类别5</summary>
        public int ColAttType10 { get; set; }

        ///<summary>扩展类别5</summary>
        public int ColAttType11 { get; set; }

        ///<summary>项目任务书中的编号</summary>
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

        ///<summary>扩展XML</summary>
        public string ColAttXml { get; set; }

        ///<summary>项目绩效产值</summary>
        public decimal ProjBanlanceFee { get; set; }

        ///<summary>项目绩效状态</summary>
        public int ProjBanlanceStatus { get; set; }

        ///<summary></summary>
        public Guid BridgeGuid { get; set; }

        ///<summary></summary>
        public int IsQuote { get; set; }

        ///<summary></summary>
        public int BridgeFact { get; set; }

        /// <summary>
        /// 所属公司 0分公司 1设计公司 2工程公司
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// 副项目负责人ID
        /// </summary>
        public int FProjEmpId { get; set; }

        /// <summary>
        /// 副项目负责人姓名
        /// </summary>
        public string FProjEmpName { get; set; }

        public virtual ICollection<BusProjContractRelation> FK_BusProjContractRelation_ProjID { get; set; }
        public virtual ICollection<IsoForm> FK_IsoForm_EngID { get; set; }
        public virtual ICollection<IsoForm> FK_IsoForm_ProjID { get; set; }
        public virtual ICollection<ProjCost> FK_ProjCost_ProjID { get; set; }
        public virtual BaseData FK_Project_ProjAreaID { get; set; }
        public virtual BaseData FK_Project_ProjDepId { get; set; }
        public virtual BaseData FK_Project_ProjGradeID { get; set; }
        public virtual BaseData FK_Project_ProjPropertyID { get; set; }
        public virtual BaseDataSystem FK_Project_ProjStatus { get; set; }
        public virtual BaseData FK_Project_ProjTaskSource { get; set; }
        public virtual BaseData FK_Project_ProjTypeID { get; set; }
        public virtual BaseData FK_Project_ProjVoltID { get; set; }
        public virtual ICollection<ProjectFavourite> FK_ProjectFavourite_ProjectId { get; set; }
        public virtual ICollection<ProjSub> FK_ProjSub_ProjID { get; set; }
    }
}