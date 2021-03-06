﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class ModelReceive 
    {
        public ModelReceive()
        {
			#region DefaultValue
			Id=0;
			PhaseID=0;
			SpecialID=0;
			ExchReceiveItem="";
			ProjectTypeID=0;
			ModelReceiveName="";
			ModelReceiveParentId=0;
			ModelReceiveIsMust= false;
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>自增</summary>
        public int Id  { get; set; }

		///<summary>阶段</summary>
        public int PhaseID  { get; set; }

		///<summary>专业</summary>
        public int SpecialID  { get; set; }

		///<summary>内容</summary>
        public string ExchReceiveItem  { get; set; }

		///<summary>项目类型</summary>
        public int ProjectTypeID  { get; set; }

		///<summary>模板名称</summary>
        public string ModelReceiveName  { get; set; }

		///<summary>父节ID</summary>
        public int ModelReceiveParentId  { get; set; }

		///<summary>是否必要条件</summary>
        public bool ModelReceiveIsMust  { get; set; }

		public virtual BaseData FK_ModelReceive_PhaseID { get; set; }
public virtual BaseData FK_ModelReceive_ProjectTypeID { get; set; }
public virtual BaseData FK_ModelReceive_SpecialID { get; set; }
    }
}
