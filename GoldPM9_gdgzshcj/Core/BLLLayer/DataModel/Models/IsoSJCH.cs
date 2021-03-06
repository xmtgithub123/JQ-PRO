﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-03-23 14:51:37
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class IsoSJCH
    {
        public IsoSJCH()
        {
            #region DefaultValue
            Id = 0;
            TableNumber = "";
            Number = "";
            ProjID = 0;
            ProjNumber = "";
            ProjName = "";
            ProjPhaseId = "";
            ProjPhaseName = "";
            JoinSpecIds = "";
            JoinSpecName = "";
            JoinDepIds = "";
            JoinDepName = "";
            CooperativeDesign = 0;
            MeritGoal = 0;
            ProjNote = "";
            PlanFinishTime = new DateTime(1900, 1, 1);
            IsReview = 0;
            ReviewTime = new DateTime(1900, 1, 1);
            ReviewType = 0;
            ProjResources = "0";
            Attachment = "0";
            PlanReport = 0;
            ChangePlanNote = "";
            DeletionTime = new DateTime(1900, 1, 1);
            CreationTime = new DateTime(1900, 1, 1);
            CreatorEmpId = 0;
            CreatorEmpName = "";
            CreatorDepId = 0;
            CreatorDepName = "";
            LastModificationTime = new DateTime(1900, 1, 1);
            LastModifierEmpId = 0;
            LastModifierEmpName = "";
            AgenCreatorEmpId = 0;
            AgenCreatorEmpName = "";
            AgenLastModifierEmpId = 0;
            AgenLastModifierEmpName = "";
            AgenDeleterEmpId = 0;
            AgenDeleterEmpName = "";
            DeleterEmpId = 0;
            DeleterEmpName = "";
            CompanyID = 0;
            #endregion
            #region 默认实例化
            #endregion
        }
        ///<summary>设计策划表</summary>
        public int Id { get; set; }

        /// <summary>
        /// 表制式SCX02-03
        /// </summary>
        public string TableNumber { get; set; }

        ///<summary>编号</summary>
        public string Number { get; set; }

        ///<summary>项目ID</summary>
        public int ProjID { get; set; }

        ///<summary>项目编号</summary>
        public string ProjNumber { get; set; }

        ///<summary>项目名称</summary>
        public string ProjName { get; set; }

        ///<summary>项目阶段</summary>
        public string ProjPhaseId { get; set; }

        ///<summary>阶段名称</summary>
        public string ProjPhaseName { get; set; }

        ///<summary>参与专业IDS</summary>
        public string JoinSpecIds { get; set; }

        ///<summary>参与专业名称</summary>
        public string JoinSpecName { get; set; }

        ///<summary>参与部门IDS</summary>
        public string JoinDepIds { get; set; }

        ///<summary>参与部门名称</summary>
        public string JoinDepName { get; set; }

        ///<summary>合作设计 0分包设计 1联合设计 2外聘人员设计</summary>
        public int CooperativeDesign { get; set; }

        ///<summary>创优目标 0不报优 1院优 2市优 3行优 4国优</summary>
        public int MeritGoal { get; set; }

        ///<summary>项目概况及总体要求</summary>
        public string ProjNote { get; set; }

        ///<summary>计划完成时间</summary>
        public DateTime PlanFinishTime { get; set; }

        ///<summary>是否评审 0要求评审 1不要求评审</summary>
        public int IsReview { get; set; }

        ///<summary>评审时机</summary>
        public DateTime ReviewTime { get; set; }

        ///<summary>评审方式 0专业委员会会议 0无会议 1专业委员会会议 2其他会议</summary>
        public int ReviewType { get; set; }

        ///<summary>项目所需资源 0不需要 1现场设计 2BIM设计 3特殊软件 4现场安全防护用品 3其他需求</summary>
        public string ProjResources { get; set; }

        ///<summary>附件 0不需要 1拟报出的文册总目录 1进度计划表 2策划调整说明</summary>
        public string Attachment { get; set; }

        ///<summary>是否需要编制设计策划报告 0否 1是</summary>
        public int PlanReport { get; set; }

        ///<summary>策划内容调整说明</summary>
        public string ChangePlanNote { get; set; }

        ///<summary>删除日期</summary>
        public DateTime DeletionTime { get; set; }

        ///<summary>创建时间</summary>
        public DateTime CreationTime { get; set; }

        ///<summary>创建人ID</summary>
        public int CreatorEmpId { get; set; }

        ///<summary>创建人姓名</summary>
        public string CreatorEmpName { get; set; }

        ///<summary>创建部门</summary>
        public int CreatorDepId { get; set; }

        ///<summary>创建部门姓名</summary>
        public string CreatorDepName { get; set; }

        ///<summary>最后修改时间</summary>
        public DateTime LastModificationTime { get; set; }

        ///<summary>最后修改人ID</summary>
        public int LastModifierEmpId { get; set; }

        ///<summary>最后修改人姓名</summary>
        public string LastModifierEmpName { get; set; }

        ///<summary>代理创建人ID</summary>
        public int AgenCreatorEmpId { get; set; }

        ///<summary>代理创建人姓名</summary>
        public string AgenCreatorEmpName { get; set; }

        ///<summary>代理最后修改人ID</summary>
        public int AgenLastModifierEmpId { get; set; }

        ///<summary>代理最后修改人姓名</summary>
        public string AgenLastModifierEmpName { get; set; }

        ///<summary>代理删除人ID</summary>
        public int AgenDeleterEmpId { get; set; }

        ///<summary>代理删除人姓名</summary>
        public string AgenDeleterEmpName { get; set; }

        ///<summary>删除人员</summary>
        public int DeleterEmpId { get; set; }

        ///<summary>删除人员姓名</summary>
        public string DeleterEmpName { get; set; }

        ///<summary>所属公司</summary>
        public int CompanyID { get; set; }

    }
}
