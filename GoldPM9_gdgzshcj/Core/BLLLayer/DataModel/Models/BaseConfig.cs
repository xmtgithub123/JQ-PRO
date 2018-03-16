﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class BaseConfig 
    {
        public BaseConfig()
        {
			#region DefaultValue
			ConfigID=0;
			ConfigName="";
			ConfigEngName="";
			ConfigValue="";
			ConfigNote="";
			ConfigOrder=0;
			ConfigEmpID=0;
			LastModificationTime= new DateTime(1900,1,1);
			LastModifierEmpId=0;
			LastModifierEmpName="";
			ConfigEmpName="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>配置自增ID</summary>
        public int ConfigID  { get; set; }

		///<summary>配置名称</summary>
        public string ConfigName  { get; set; }

		///<summary>配置英文标识</summary>
        public string ConfigEngName  { get; set; }

		///<summary>配置内容</summary>
        public string ConfigValue  { get; set; }

		///<summary>配置备注</summary>
        public string ConfigNote  { get; set; }

		///<summary>排序字段</summary>
        public int ConfigOrder  { get; set; }

		///<summary>用户ID</summary>
        public int ConfigEmpID  { get; set; }

		///<summary>最后修改时间</summary>
        public DateTime LastModificationTime  { get; set; }

		///<summary>最后修改人ID</summary>
        public int LastModifierEmpId  { get; set; }

		///<summary>最后修改人姓名</summary>
        public string LastModifierEmpName  { get; set; }

		///<summary>用户姓名</summary>
        public string ConfigEmpName  { get; set; }

		///<summary>父级ID</summary>
        public string ParentId  { get; set; }

		    }
}
