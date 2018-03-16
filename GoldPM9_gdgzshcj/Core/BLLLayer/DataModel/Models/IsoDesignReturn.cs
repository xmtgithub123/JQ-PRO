﻿#region <auto-generated>
//此代码由T4模板自动生成
//生成时间 2017-09-25 18:01:02
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class IsoDesignReturn
    {
        public IsoDesignReturn()
        {
			#region DefaultValue
			Id=0;
			Number="";
			ProjId=0;
			ProjNumer="";
			ProjName="";
			ReturnPerson="";
			ReturnDate= new DateTime(1900,1,1);
			OrganizeDepart="";
			Participant="";
			Text="";
			Unresolved="";
			IsResolved=0;
			IsResponse=0;
			RecordName="";
			TechnologyName="";
			ResponbilityName="";
			RecordDate= new DateTime(1900,1,1);
			TechnologyDate= new DateTime(1900,1,1);
			ResponsibilityDate= new DateTime(1900,1,1);
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
			TableNumber="";
			ProjNumber="";
			ProjPhaseId="";
			ProjPhaseName="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>设计回访纪要表</summary>
        public int Id  { get; set; }

		///<summary>编号</summary>
        public string Number  { get; set; }

		///<summary>项目ID</summary>
        public int ProjId  { get; set; }

		///<summary>项目编号</summary>
        public string ProjNumer  { get; set; }

		///<summary>项目名称</summary>
        public string ProjName  { get; set; }

		///<summary>回访对象</summary>
        public string ReturnPerson  { get; set; }

		///<summary>回访时间</summary>
        public DateTime ReturnDate  { get; set; }

		///<summary>组织部门</summary>
        public string OrganizeDepart  { get; set; }

		///<summary>参与人</summary>
        public string Participant  { get; set; }

		///<summary>内容</summary>
        public string Text  { get; set; }

		///<summary>未解决的</summary>
        public string Unresolved  { get; set; }

		///<summary>是否解决</summary>
        public int IsResolved  { get; set; }

		///<summary>是否反馈</summary>
        public int IsResponse  { get; set; }

		///<summary>记录操作人</summary>
        public string RecordName  { get; set; }

		///<summary>技术质量部</summary>
        public string TechnologyName  { get; set; }

		///<summary>责任人</summary>
        public string ResponbilityName  { get; set; }

		///<summary>记录操作人时间</summary>
        public DateTime RecordDate  { get; set; }

		///<summary>技术质量部时间</summary>
        public DateTime TechnologyDate  { get; set; }

		///<summary>责任人时间</summary>
        public DateTime ResponsibilityDate  { get; set; }

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

		///<summary></summary>
        public string TableNumber  { get; set; }

		///<summary></summary>
        public string ProjNumber  { get; set; }

		///<summary></summary>
        public string ProjPhaseId  { get; set; }

		///<summary></summary>
        public string ProjPhaseName  { get; set; }

		    }
}
