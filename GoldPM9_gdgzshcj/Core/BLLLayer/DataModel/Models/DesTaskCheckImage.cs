﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class DesTaskCheckImage 
    {
        public DesTaskCheckImage()
        {
			#region DefaultValue
			Id=0;
			CheckID=0;
			CreateEmpID=0;
			CreateEmpName="0";
			CreateDateTime= new DateTime(1900,1,1);
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary></summary>
        public int Id  { get; set; }

		///<summary></summary>
        public int CheckID  { get; set; }

		///<summary></summary>
        public int CreateEmpID  { get; set; }

		///<summary></summary>
        public string CreateEmpName  { get; set; }

		///<summary></summary>
        public DateTime CreateDateTime  { get; set; }

		///<summary></summary>
        public byte[] CheckImage  { get; set; }

		    }
}
