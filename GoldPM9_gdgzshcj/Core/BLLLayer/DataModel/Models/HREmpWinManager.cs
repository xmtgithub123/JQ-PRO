﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-03-14 16:34:42
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class HREmpWinManager 
    {
        public HREmpWinManager()
        {
			#region DefaultValue
			Id=0;
			AptitudeName="";
			RegisterTime= new DateTime(1900,1,1);
			AptitudeEmpId=0;
			AptitudeEmpName="";
			Remark="";
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
			CompanyID=0;
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>个人获奖信息表</summary>
        public int Id  { get; set; }

		///<summary>奖项名称</summary>
        public string AptitudeName  { get; set; }

		///<summary>获奖时间</summary>
        public DateTime RegisterTime  { get; set; }

		///<summary>获奖人员ID</summary>
        public int AptitudeEmpId  { get; set; }

		///<summary>获奖人员姓名</summary>
        public string AptitudeEmpName  { get; set; }

		///<summary>备注</summary>
        public string Remark  { get; set; }

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

		///<summary>所属公司</summary>
        public int CompanyID  { get; set; }

		    }
}