﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class ModelIsoCheck 
    {
        public ModelIsoCheck()
        {
			#region DefaultValue
			Id=0;
			PhaseID=0;
			SpecialID=0;
			CheckErrTypeID=0;
			CheckType=0;
			CheckItem="";
			CheckNote="";
			DesTaskCheckId=0;
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepID=0;
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
			#endregion
			#region 默认实例化
			this.FK_IsoCheck_IsoCheckModelId = new List<IsoCheck>();
			#endregion
        }
		///<summary>自增</summary>
        public int Id  { get; set; }

		///<summary>阶段</summary>
        public int PhaseID  { get; set; }

		///<summary>专业</summary>
        public int SpecialID  { get; set; }

		///<summary>错误类型</summary>
        public int CheckErrTypeID  { get; set; }

		///<summary>节点角色，如 设计、校对、审核、批准 ,流程节点</summary>
        public int CheckType  { get; set; }

		///<summary>内容</summary>
        public string CheckItem  { get; set; }

		///<summary>备注说明</summary>
        public string CheckNote  { get; set; }

		///<summary>DesTaskCheck表的ID(校审提取)</summary>
        public int DesTaskCheckId  { get; set; }

		///<summary>创建时间</summary>
        public DateTime CreationTime  { get; set; }

		///<summary>创建人ID</summary>
        public int CreatorEmpId  { get; set; }

		///<summary>创建人姓名</summary>
        public string CreatorEmpName  { get; set; }

		///<summary>创建部门</summary>
        public int CreatorDepID  { get; set; }

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

		        public virtual ICollection<IsoCheck> FK_IsoCheck_IsoCheckModelId { get; set; }
public virtual BaseDataSystem FK_ModelIsoCheck_CheckType { get; set; }
public virtual BaseData FK_ModelIsoCheck_PhaseID { get; set; }
public virtual BaseData FK_ModelIsoCheck_SpecialID { get; set; }
    }
}
