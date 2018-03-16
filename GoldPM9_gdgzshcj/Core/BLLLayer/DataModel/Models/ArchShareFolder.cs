﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class ArchShareFolder 
    {
        public ArchShareFolder()
        {
			#region DefaultValue
			Id=0;
			FolderName="";
			FolderParentID=0;
			FolderRemark="";
			FolderShareDeptIds="";
			FolderManagerId=0;
			FolderManagerName="";
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepID=0;
			CreatorDepName="";
			#endregion
			#region 默认实例化
			this.FK_ArchShareFolderFile_ArchShareFolderId = new List<ArchShareFolderFile>();
			#endregion
        }
		///<summary>典型图库及共享文件夹表</summary>
        public int Id  { get; set; }

		///<summary>文件夹名称</summary>
        public string FolderName  { get; set; }

		///<summary>父文件夹名称</summary>
        public int FolderParentID  { get; set; }

		///<summary>说明</summary>
        public string FolderRemark  { get; set; }

		///<summary>共享部门Ids</summary>
        public string FolderShareDeptIds  { get; set; }

		///<summary>管理人员Id</summary>
        public int FolderManagerId  { get; set; }

		///<summary>管理人员姓名</summary>
        public string FolderManagerName  { get; set; }

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

		        public virtual ICollection<ArchShareFolderFile> FK_ArchShareFolderFile_ArchShareFolderId { get; set; }
    }
}
