﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class BaseEmpAgen 
    {
        public BaseEmpAgen()
        {
			#region DefaultValue
			BaseEmpAgenID=0;
			FromEmpID=0;
			FromEmpName="";
			ToEmpID=0;
			ToEmpName="";
			AgenFlowRefTable="";
			AgenMenu="";
			DateCreate= new DateTime(1900,1,1);
			DateLower= new DateTime(1900,1,1);
			DateUpper= new DateTime(1900,1,1);
			AgenNote="";
			AgenType=0;
			AgenStatus=0;
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>自增ID</summary>
        public int BaseEmpAgenID  { get; set; }

		///<summary>委托人</summary>
        public int FromEmpID  { get; set; }

		///<summary>委托人姓名</summary>
        public string FromEmpName  { get; set; }

		///<summary>代理人</summary>
        public int ToEmpID  { get; set; }

		///<summary>代理人姓名</summary>
        public string ToEmpName  { get; set; }

		///<summary>委托流程</summary>
        public string AgenFlowRefTable  { get; set; }

		///<summary>委托菜单</summary>
        public string AgenMenu  { get; set; }

		///<summary>创建日期</summary>
        public DateTime DateCreate  { get; set; }

		///<summary>委托开始日期</summary>
        public DateTime DateLower  { get; set; }

		///<summary>委托结束日期</summary>
        public DateTime DateUpper  { get; set; }

		///<summary>委托备注</summary>
        public string AgenNote  { get; set; }

		///<summary>委托类别</summary>
        public int AgenType  { get; set; }

		///<summary>委托状态</summary>
        public int AgenStatus  { get; set; }

		    }
}
