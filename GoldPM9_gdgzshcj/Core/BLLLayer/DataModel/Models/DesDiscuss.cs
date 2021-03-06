﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class DesDiscuss 
    {
        public DesDiscuss()
        {
			#region DefaultValue
			Id=0;
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepId=0;
			CreatorDepName="";
			LastModificationTime= new DateTime(1900,1,1);
			LastModifierEmpId=0;
			LastModifierEmpName="";
			DeleterEmpId=0;
			DeleterEmpName="";
			DeletionTime= new DateTime(1900,1,1);
			AgenCreatorEmpId=0;
			AgenCreatorEmpName="";
			AgenLastModifierEmpId=0;
			AgenLastModifierEmpName="";
			AgenDeleterEmpId=0;
			AgenDeleterEmpName="";
			TalkRefId=0;
			TalkRefTable="";
			TalkGroupId=0;
			TalkTitle="";
			TalkContent="";
			ReadCount=0;
			ReplyCount=0;
			LastReplyEmpName="";
			LastReplyTime= new DateTime(1900,1,1);
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>---讨论主题---</summary>
        public int Id  { get; set; }

		///<summary>创建时间</summary>
        public DateTime CreationTime  { get; set; }

		///<summary>创建人ID</summary>
        public int CreatorEmpId  { get; set; }

		///<summary>创建人姓名</summary>
        public string CreatorEmpName  { get; set; }

		///<summary>创建部门</summary>
        public int CreatorDepId  { get; set; }

		///<summary>创建部门姓名</summary>
        public string CreatorDepName  { get; set; }

		///<summary>最后修改时间</summary>
        public DateTime LastModificationTime  { get; set; }

		///<summary>最后修改人ID</summary>
        public int LastModifierEmpId  { get; set; }

		///<summary>最后修改人姓名</summary>
        public string LastModifierEmpName  { get; set; }

		///<summary>删除人员</summary>
        public int DeleterEmpId  { get; set; }

		///<summary>删除人员姓名</summary>
        public string DeleterEmpName  { get; set; }

		///<summary>删除日期</summary>
        public DateTime DeletionTime  { get; set; }

		///<summary>代理创建人ID</summary>
        public int AgenCreatorEmpId  { get; set; }

		///<summary>代理创建人姓名</summary>
        public string AgenCreatorEmpName  { get; set; }

		///<summary>代理最后修改人ID</summary>
        public int AgenLastModifierEmpId  { get; set; }

		///<summary>代理最后修改人姓名</summary>
        public string AgenLastModifierEmpName  { get; set; }

		///<summary>代理删除人ID</summary>
        public int AgenDeleterEmpId  { get; set; }

		///<summary>代理删除人姓名</summary>
        public string AgenDeleterEmpName  { get; set; }

		///<summary>对应类型ID</summary>
        public int TalkRefId  { get; set; }

		///<summary>对应类型</summary>
        public string TalkRefTable  { get; set; }

		///<summary>记事分组（可以不使用分组）</summary>
        public int TalkGroupId  { get; set; }

		///<summary>记事标题</summary>
        public string TalkTitle  { get; set; }

		///<summary>记事内容</summary>
        public string TalkContent  { get; set; }

		///<summary>阅读数</summary>
        public int ReadCount  { get; set; }

		///<summary>回复数</summary>
        public int ReplyCount  { get; set; }

		///<summary>最后回复人名称</summary>
        public string LastReplyEmpName  { get; set; }

		///<summary>最后回复时间</summary>
        public DateTime LastReplyTime  { get; set; }

		    }
}
