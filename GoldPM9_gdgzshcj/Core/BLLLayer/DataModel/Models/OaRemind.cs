﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class OaRemind 
    {
        public OaRemind()
        {
			#region DefaultValue
			Id=0;
			RemTypeID=0;
			EmpId=0;
			RemDate="";
			RemDateEdit= new DateTime(1900,1,1);
			RemAhead=0;
			RemTitle="";
			RemNote="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>自增</summary>
        public int Id  { get; set; }

		///<summary>类别</summary>
        public int RemTypeID  { get; set; }

		///<summary>用户</summary>
        public int EmpId  { get; set; }

		///<summary>日期</summary>
        public string RemDate  { get; set; }

		///<summary>修改日期</summary>
        public DateTime RemDateEdit  { get; set; }

		///<summary>提醒周期</summary>
        public int RemAhead  { get; set; }

		///<summary>标题</summary>
        public string RemTitle  { get; set; }

		///<summary>内容</summary>
        public string RemNote  { get; set; }

		public virtual BaseDataSystem FK_OaRemind_RemTypeID { get; set; }
    }
}
