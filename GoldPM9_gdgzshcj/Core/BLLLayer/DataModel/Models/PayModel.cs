﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class PayModel 
    {
        public PayModel()
        {
			#region DefaultValue
			Id=0;
			ModelName="";
			EngPhaseID=0;
			EngVolID=0;
			ModelXML="";
			ModelIsDeleted= false;
			Note="";
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
			#endregion
        }
		///<summary>自增</summary>
        public int Id  { get; set; }

		///<summary>绩效模板名称</summary>
        public string ModelName  { get; set; }

		///<summary>工程阶段</summary>
        public int EngPhaseID  { get; set; }

		///<summary>电压等级</summary>
        public int EngVolID  { get; set; }

		///<summary>绩效各系数比例</summary>
        public string ModelXML  { get; set; }

		///<summary>删除状态(0,1已删除)</summary>
        public bool ModelIsDeleted  { get; set; }

		///<summary>绩效模板备注</summary>
        public string Note  { get; set; }

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

		public virtual BaseData FK_PayModel_EngPhaseID { get; set; }
public virtual BaseData FK_PayModel_EngVolID { get; set; }
    }
}
