﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-01-11 10:29:41
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class HRVitae 
    {
        public HRVitae()
        {
			#region DefaultValue
			Id=0;
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepId=0;
			CreatorDepName="";
			DeleterEmpId=0;
			DeleterEmpName="";
			DeletionTime= new DateTime(1900,1,1);
			EmpID=0;
			OldCom="";
			OldDep="";
			OldPost="";
			StarDate= new DateTime(1900,1,1);
			EndDate= new DateTime(1900,1,1);
			Note="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary></summary>
        public int Id  { get; set; }

		///<summary></summary>
        public DateTime CreationTime  { get; set; }

		///<summary></summary>
        public int CreatorEmpId  { get; set; }

		///<summary></summary>
        public string CreatorEmpName  { get; set; }

		///<summary></summary>
        public int CreatorDepId  { get; set; }

		///<summary></summary>
        public string CreatorDepName  { get; set; }

		///<summary></summary>
        public int DeleterEmpId  { get; set; }

		///<summary></summary>
        public string DeleterEmpName  { get; set; }

		///<summary></summary>
        public DateTime DeletionTime  { get; set; }

		///<summary></summary>
        public int EmpID  { get; set; }

		///<summary></summary>
        public string OldCom  { get; set; }

		///<summary></summary>
        public string OldDep  { get; set; }

		///<summary></summary>
        public string OldPost  { get; set; }

		///<summary></summary>
        public DateTime StarDate  { get; set; }

		///<summary></summary>
        public DateTime EndDate  { get; set; }

		///<summary></summary>
        public string Note  { get; set; }

		    }
}
