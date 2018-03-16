﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class BaseHandover 
    {
        public BaseHandover()
        {
			#region DefaultValue
			Id=0;
			HandType=0;
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			HandEmpName="";
			HandEmpId=0;
			HandEmps="";
			HandNote="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>自增ID</summary>
        public int Id  { get; set; }

		///<summary>移交原因</summary>
        public string HandReason  { get; set; }

		///<summary>移交类别</summary>
        public int HandType  { get; set; }

		///<summary>创建时间</summary>
        public DateTime CreationTime  { get; set; }

		///<summary>创建人ID</summary>
        public int CreatorEmpId  { get; set; }

		///<summary>创建人姓名</summary>
        public string CreatorEmpName  { get; set; }

		///<summary>移交人姓名</summary>
        public string HandEmpName  { get; set; }

		///<summary>移交人ID</summary>
        public int HandEmpId  { get; set; }

		///<summary>移交成员</summary>
        public string HandEmps  { get; set; }

		///<summary>移交备注</summary>
        public string HandNote  { get; set; }

		    }
}