﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class BaseEmpPermission 
    {
        public BaseEmpPermission()
        {
			#region DefaultValue
			PermissionEmpID=0;
			PermissionEmpValue=0;
			PermissionEmpNote="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>人员</summary>
        public int PermissionEmpID  { get; set; }

		///<summary>角色</summary>
        public int PermissionEmpValue  { get; set; }

		///<summary>备注</summary>
        public string PermissionEmpNote  { get; set; }

		public virtual BaseEmployee FK_BaseEmpPermission_PermissionEmpID { get; set; }
public virtual BaseData FK_BaseEmpPermission_PermissionEmpValue { get; set; }
    }
}
