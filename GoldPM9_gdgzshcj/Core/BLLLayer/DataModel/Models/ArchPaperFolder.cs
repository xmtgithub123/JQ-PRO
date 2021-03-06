﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-04-20 09:38:41
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class ArchPaperFolder 
    {
        public ArchPaperFolder()
        {
			#region DefaultValue
			Id=0;
            ProjId = 0;
			ProjNumber="";
			ProjName="";
			ArchNumber="";
			ProjPhaseId="";
			ProjPhaseName="";
			ProjEmpId=0;
			ProjEmpName="";
			Path="";
			Remark="";
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepID=0;
			CreatorDepName="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>纸质档案</summary>
        public int Id  { get; set; }

        /// <summary>
        /// 工程ID
        /// </summary>
        public int ProjId { get; set; }

        ///<summary>工程编号</summary>
        public string ProjNumber  { get; set; }

		///<summary>工程名称</summary>
        public string ProjName  { get; set; }

		///<summary>档案编号</summary>
        public string ArchNumber  { get; set; }

		///<summary>设计阶段ID</summary>
        public string ProjPhaseId  { get; set; }

		///<summary>设计阶段</summary>
        public string ProjPhaseName  { get; set; }

		///<summary>项目负责人ID</summary>
        public int ProjEmpId  { get; set; }

		///<summary>项目负责人</summary>
        public string ProjEmpName  { get; set; }

		///<summary>存放位置</summary>
        public string Path  { get; set; }

		///<summary>备注</summary>
        public string Remark  { get; set; }

		///<summary>创建时间</summary>
        public DateTime CreationTime  { get; set; }

		///<summary>创建人ID</summary>
        public long CreatorEmpId  { get; set; }

		///<summary>创建人姓名</summary>
        public string CreatorEmpName  { get; set; }

		///<summary>创建部门</summary>
        public long CreatorDepID  { get; set; }

		///<summary>创建部门名称</summary>
        public string CreatorDepName  { get; set; }

		    }
}
