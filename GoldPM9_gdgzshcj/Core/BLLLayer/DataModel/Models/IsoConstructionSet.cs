﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-04-19 16:27:16
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class IsoConstructionSet 
    {
        public IsoConstructionSet()
        {
			#region DefaultValue
			Id=0;
            TableNumber = "";
            Number = "";
            ProjId = 0;
            ProjNumber = "";
            ProjName = "";
            ProjPhaseId = "";
            ProjPhaseName = "";
            ConstructionCompany ="";
			ConstructionResponsibility="";
			OrganizationCompany="";
			OrganizationContacts="";
			SetPosition="";
			SetDate= new DateTime(1900,1,1);
			SetValue="";
			SetResponsibility="";
			DeletionTime= new DateTime(1900,1,1);
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepId=0;
			CreatorDepName="";
			LastModificationTime= new DateTime(1900,1,1);
			LastModifierEmpId=0;
			LastModifierEmpName="";
			AgenCreatorEmpId=0;
			AgenCreatorEmpName="";
			AgenLastModifierEmpId=0;
			AgenLastModifierEmpName="";
			AgenDeleterEmpId=0;
			AgenDeleterEmpName="";
			DeleterEmpId=0;
			DeleterEmpName="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>设计评审纪要表</summary>
        public int Id  { get; set; }

        /// <summary>
        /// 表制式
        /// </summary>
        public string TableNumber { get; set; }

        ///<summary>编号</summary>
        public string Number { get; set; }

        ///<summary>项目ID</summary>
        public int ProjId { get; set; }

        ///<summary>项目编号</summary>
        public string ProjNumber { get; set; }

        ///<summary>项目名称</summary>
        public string ProjName { get; set; }

        /// <summary>
        /// 项目阶段ID
        /// </summary>
        public string ProjPhaseId { get; set; }

        /// <summary>
        /// 项目阶段名称
        /// </summary>
        public string ProjPhaseName { get; set; }

        ///<summary>施工单位</summary>
        public string ConstructionCompany  { get; set; }

		///<summary>施工技术负责人</summary>
        public string ConstructionResponsibility  { get; set; }

		///<summary>建设单位</summary>
        public string OrganizationCompany  { get; set; }

		///<summary>建设联系人</summary>
        public string OrganizationContacts  { get; set; }

		///<summary>交底地点</summary>
        public string SetPosition  { get; set; }

		///<summary>交底时间</summary>
        public DateTime SetDate  { get; set; }

		///<summary>交底内容</summary>
        public string SetValue  { get; set; }

		///<summary>交底负责人</summary>
        public string SetResponsibility  { get; set; }

		///<summary>删除日期</summary>
        public DateTime DeletionTime  { get; set; }

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

		///<summary>删除人员</summary>
        public int DeleterEmpId  { get; set; }

		///<summary>删除人员姓名</summary>
        public string DeleterEmpName  { get; set; }

		    }
}