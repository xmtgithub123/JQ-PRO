﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class OaMail 
    {
        public OaMail()
        {
			#region DefaultValue
			Id=0;
			MailDate= new DateTime(1900,1,1);
			MailTitle="";
			MailNote="";
			MailFlag=0;
			MailIsDelete= false;
			MailIsBBC=0;
			DeletionTime= new DateTime(1900,1,1);
			LastModificationTime= new DateTime(1900,1,1);
			LastModifierEmpId=0;
			LastModifierEmpName="";
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			AgenEmpId=0;
			AgenEmpName="";
			CreatorDepId=0;
			CreatorDepName="";
			DeleterEmpId=0;
			DeleterEmpName="";
			#endregion
			#region 默认实例化
			this.FK_OaMailRead_Id = new List<OaMailRead>();
			#endregion
        }
		///<summary>自增</summary>
        public int Id  { get; set; }

		///<summary>发送日期</summary>
        public DateTime MailDate  { get; set; }

		///<summary>标题</summary>
        public string MailTitle  { get; set; }

		///<summary>内容</summary>
        public string MailNote  { get; set; }

		///<summary>状态(发送0、保存1)</summary>
        public int MailFlag  { get; set; }

		///<summary>是否删除</summary>
        public bool MailIsDelete  { get; set; }

		///<summary>密送</summary>
        public int MailIsBBC  { get; set; }

		///<summary>删除日期</summary>
        public DateTime DeletionTime  { get; set; }

		///<summary>最后修改时间</summary>
        public DateTime LastModificationTime  { get; set; }

		///<summary>最后修改人ID</summary>
        public int LastModifierEmpId  { get; set; }

		///<summary>最后修改人姓名</summary>
        public string LastModifierEmpName  { get; set; }

		///<summary>创建时间</summary>
        public DateTime CreationTime  { get; set; }

		///<summary>创建人ID</summary>
        public int CreatorEmpId  { get; set; }

		///<summary>创建人姓名</summary>
        public string CreatorEmpName  { get; set; }

		///<summary>代理人ID</summary>
        public int AgenEmpId  { get; set; }

		///<summary>代理人姓名</summary>
        public string AgenEmpName  { get; set; }

		///<summary>创建部门</summary>
        public int CreatorDepId  { get; set; }

		///<summary>创建部门姓名</summary>
        public string CreatorDepName  { get; set; }

		///<summary>删除人员</summary>
        public int DeleterEmpId  { get; set; }

		///<summary>删除人员姓名</summary>
        public string DeleterEmpName  { get; set; }

		        public virtual ICollection<OaMailRead> FK_OaMailRead_Id { get; set; }
    }
}