﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class IsoForm 
    {
        public IsoForm()
        {
			#region DefaultValue
			FormID=0;
			ProjID=0;
			EngID=0;
			PhaseID=0;
			SpecID=0;
			TaskGroupID=0;
			TaskID=0;
			FormTypeID=0;
			FormDate= new DateTime(1900,1,1);
			FormName="";
			FormReason="";
			FormNote="";
			FormLinkURL="";
			ColAttType1=0;
			ColAttType2=0;
			ColAttType3=0;
			ColAttType4=0;
			ColAttType5=0;
			ColAttVal1="";
			ColAttVal2="";
			ColAttVal3="";
			ColAttVal4="";
			ColAttVal5="";
			ColAttDate1= new DateTime(1900,1,1);
			ColAttDate2= new DateTime(1900,1,1);
			ColAttDate3= new DateTime(1900,1,1);
			ColAttDate4= new DateTime(1900,1,1);
			ColAttDate5= new DateTime(1900,1,1);
			FormCtlXml="";
			RefTable="";
			RefID=0;
			FlowID=0;
			FlowTime= new DateTime(1900,1,1);
			LastModificationTime= new DateTime(1900,1,1);
			LastModifierEmpId=0;
			LastModifierEmpName="";
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="";
			CreatorDepId=0;
			CreatorDepName="";
			DeletionTime= new DateTime(1900,1,1);
			DeleterEmpId=0;
			DeleterEmpName="";
			AgenEmpId=0;
			AgenEmpName="";
			#endregion
			#region 默认实例化
			this.FK_BussSubFeeInvoice_FormTableID = new List<BussSubFeeInvoice>();
			this.FK_IsoFormNode_FormID = new List<IsoFormNode>();
			#endregion
        }
		///<summary>自增</summary>
        public int FormID  { get; set; }

		///<summary>工程</summary>
        public int ProjID  { get; set; }

		///<summary>子项</summary>
        public int EngID  { get; set; }

		///<summary>阶段</summary>
        public int PhaseID  { get; set; }

		///<summary>专业</summary>
        public int SpecID  { get; set; }

		///<summary>任务分组</summary>
        public long TaskGroupID  { get; set; }

		///<summary>调整节点ID</summary>
        public long TaskID  { get; set; }

		///<summary>类别</summary>
        public int FormTypeID  { get; set; }

		///<summary>表单日期</summary>
        public DateTime FormDate  { get; set; }

		///<summary>表单名称</summary>
        public string FormName  { get; set; }

		///<summary>原因</summary>
        public string FormReason  { get; set; }

		///<summary>备注</summary>
        public string FormNote  { get; set; }

		///<summary>链接</summary>
        public string FormLinkURL  { get; set; }

		///<summary>扩展类别</summary>
        public int ColAttType1  { get; set; }

		///<summary>扩展类别</summary>
        public int ColAttType2  { get; set; }

		///<summary>扩展类别</summary>
        public int ColAttType3  { get; set; }

		///<summary>扩展类别</summary>
        public int ColAttType4  { get; set; }

		///<summary>扩展类别</summary>
        public int ColAttType5  { get; set; }

		///<summary>扩展值</summary>
        public string ColAttVal1  { get; set; }

		///<summary>扩展值2</summary>
        public string ColAttVal2  { get; set; }

		///<summary>扩展值</summary>
        public string ColAttVal3  { get; set; }

		///<summary>扩展值</summary>
        public string ColAttVal4  { get; set; }

		///<summary>扩展值</summary>
        public string ColAttVal5  { get; set; }

		///<summary>扩展时间1</summary>
        public DateTime ColAttDate1  { get; set; }

		///<summary>扩展时间2</summary>
        public DateTime ColAttDate2  { get; set; }

		///<summary>扩展时间3</summary>
        public DateTime ColAttDate3  { get; set; }

		///<summary>扩展时间4</summary>
        public DateTime ColAttDate4  { get; set; }

		///<summary>扩展时间5</summary>
        public DateTime ColAttDate5  { get; set; }

		///<summary>扩展</summary>
        public string FormCtlXml  { get; set; }

		///<summary>关联表名</summary>
        public string RefTable  { get; set; }

		///<summary>关联ID</summary>
        public int RefID  { get; set; }

		///<summary>流程ID</summary>
        public long FlowID  { get; set; }

		///<summary>审批结束时间</summary>
        public DateTime FlowTime  { get; set; }

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

		///<summary>创建部门</summary>
        public int CreatorDepId  { get; set; }

		///<summary>创建部门姓名</summary>
        public string CreatorDepName  { get; set; }

		///<summary>删除日期</summary>
        public DateTime DeletionTime  { get; set; }

		///<summary>删除人员</summary>
        public int DeleterEmpId  { get; set; }

		///<summary>删除人员姓名</summary>
        public string DeleterEmpName  { get; set; }

		///<summary>代理人ID</summary>
        public int AgenEmpId  { get; set; }

		///<summary>代理人姓名</summary>
        public string AgenEmpName  { get; set; }

		        public virtual ICollection<BussSubFeeInvoice> FK_BussSubFeeInvoice_FormTableID { get; set; }
public virtual BaseData FK_IsoForm_ColAttType1 { get; set; }
public virtual BaseData FK_IsoForm_ColAttType2 { get; set; }
public virtual BaseData FK_IsoForm_ColAttType3 { get; set; }
public virtual Project FK_IsoForm_EngID { get; set; }
public virtual BaseData FK_IsoForm_PhaseID { get; set; }
public virtual Project FK_IsoForm_ProjID { get; set; }
public virtual BaseData FK_IsoForm_SpecID { get; set; }
public virtual DesTaskGroup FK_IsoForm_TaskGroupID { get; set; }
public virtual DesTask FK_IsoForm_TaskID { get; set; }
        public virtual ICollection<IsoFormNode> FK_IsoFormNode_FormID { get; set; }
    }
}
