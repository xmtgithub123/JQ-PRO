﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class BaseRestDay 
    {
        public BaseRestDay()
        {
			#region DefaultValue
			BaseDayID=0;
			BaseYear=0;
			BaseDay= new DateTime(1900,1,1);
			BaseWeekName="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>自增</summary>
        public int BaseDayID  { get; set; }

		///<summary>年份</summary>
        public int BaseYear  { get; set; }

		///<summary>日期</summary>
        public DateTime BaseDay  { get; set; }

		///<summary>日期名称</summary>
        public string BaseWeekName  { get; set; }

		    }
}
