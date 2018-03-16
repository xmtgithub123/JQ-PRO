﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class OaNewsRead 
    {
        public OaNewsRead()
        {
			#region DefaultValue
			Id=0;
			ReadEmpId=0;
			ReadEmpName="";
			ReadDate= new DateTime(1900,1,1);
			ReadEmpIsDel= false;
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>新闻ID</summary>
        public int Id  { get; set; }

		///<summary>接收人ID</summary>
        public int ReadEmpId  { get; set; }

		///<summary>接收人名称</summary>
        public string ReadEmpName  { get; set; }

		///<summary>阅读日期</summary>
        public DateTime ReadDate  { get; set; }

		///<summary>是否已经删除</summary>
        public bool ReadEmpIsDel  { get; set; }

		public virtual OaNew FK_OaNewsRead_Id { get; set; }
    }
}