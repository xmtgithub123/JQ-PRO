﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-01-06 15:17:59
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class ArchProjectFolder 
    {
        public ArchProjectFolder()
        {
			#region DefaultValue
			Id=0;
			FolderName="";
			FolderParentID=0;
			FolderRemark="";
			FolderTemplateId=0;
			FolderProjectId=0;
			FolderProjectName="";
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepID=0;
			CreatorDepName="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>项目归档目录表</summary>
        public int Id  { get; set; }

		///<summary>文件夹名称</summary>
        public string FolderName  { get; set; }

		///<summary>父文件夹名称</summary>
        public int FolderParentID  { get; set; }

		///<summary>说明</summary>
        public string FolderRemark  { get; set; }

		///<summary>引用模板目录ID</summary>
        public int FolderTemplateId  { get; set; }

		///<summary>项目ID</summary>
        public int FolderProjectId  { get; set; }

		///<summary>项目名称</summary>
        public string FolderProjectName  { get; set; }

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
