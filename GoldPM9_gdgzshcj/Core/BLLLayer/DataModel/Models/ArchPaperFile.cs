﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class ArchPaperFile 
    {
        public ArchPaperFile()
        {
			#region DefaultValue
			Id=0;
			ArchPaperId=0;
			ParentId=0;
			FileOrder=0;
			FileArchNumber="";
			FileArchOrder="";
			FileDwgNumber="";
			FileArchName="";
			FileArchType="";
			FileArchKind="";
			LocatorNumber="";
			BoxNumber="";
			ArchConNumber="";
			ArchConName="";
			ArchConMoney=0;
			ArchConDate= new DateTime(1900,1,1);
			Scret=0;
			RefNumber="";
			ArchSize="";
			ArchUnit="";
			UserId=0;
			UserName="";
			ArchDate= new DateTime(1900,1,1);
			CheckEmpId=0;
			CheckEmpName="";
			CheckDate= new DateTime(1900,1,1);
			ArchiveDate= new DateTime(1900,1,1);
			ExpiredDate=0;
			FileBeginDate= new DateTime(1900,1,1);
			FileEndDate= new DateTime(1900,1,1);
			BeginPage=0;
			EndPage=0;
			Pages=0;
			ArchCount=0;
			TotalPages=0;
			DwgPages="";
			DwgPages4A4="";
			ArchContent="";
			ChangeRemark="";
			IndexTitle="";
			ArchSource="";
			Note="";
			ArchXml="";
			LastModificationTime= new DateTime(1900,1,1);
			LastModifierEmpId=0;
			LastModifierEmpName="‘’";
			CreationTime= new DateTime(1900,1,1);
			CreatorEmpId=0;
			CreatorEmpName="‘’";
			CreatorDepID=0;
			CreatorDepName="‘’";
			#endregion
			#region 默认实例化
			this.FK_ArchPaperRelationElec_ArchPaperFileId = new List<ArchPaperRelationElec>();
			#endregion
        }
		///<summary>案卷目录表</summary>
        public int Id  { get; set; }

		///<summary>ArchProj表ID</summary>
        public int ArchPaperId  { get; set; }

		///<summary>暂留，可当作多卷的总目录</summary>
        public int ParentId  { get; set; }

		///<summary>系统序号</summary>
        public int FileOrder  { get; set; }

		///<summary>文件档号</summary>
        public string FileArchNumber  { get; set; }

		///<summary>卷内序号</summary>
        public string FileArchOrder  { get; set; }

		///<summary>文件图纸编号</summary>
        public string FileDwgNumber  { get; set; }

		///<summary>文件题名</summary>
        public string FileArchName  { get; set; }

		///<summary>文件类型：党群、办公</summary>
        public string FileArchType  { get; set; }

		///<summary>文件种类：通知，公告</summary>
        public string FileArchKind  { get; set; }

		///<summary>库位号</summary>
        public string LocatorNumber  { get; set; }

		///<summary>盒号</summary>
        public string BoxNumber  { get; set; }

		///<summary>合同编号</summary>
        public string ArchConNumber  { get; set; }

		///<summary>合同名称</summary>
        public string ArchConName  { get; set; }

		///<summary>合同金额</summary>
        public decimal ArchConMoney  { get; set; }

		///<summary>签订日期</summary>
        public DateTime ArchConDate  { get; set; }

		///<summary>密级类型,baseID</summary>
        public int Scret  { get; set; }

		///<summary>互见号</summary>
        public string RefNumber  { get; set; }

		///<summary>卷盒规格</summary>
        public string ArchSize  { get; set; }

		///<summary>责任单位</summary>
        public string ArchUnit  { get; set; }

		///<summary>责任人</summary>
        public int UserId  { get; set; }

		///<summary>责任名字</summary>
        public string UserName  { get; set; }

		///<summary>成文日期</summary>
        public DateTime ArchDate  { get; set; }

		///<summary>检查人</summary>
        public int CheckEmpId  { get; set; }

		///<summary>检查人姓名</summary>
        public string CheckEmpName  { get; set; }

		///<summary>检查日期</summary>
        public DateTime CheckDate  { get; set; }

		///<summary>归档日期</summary>
        public DateTime ArchiveDate  { get; set; }

		///<summary>保管期限,baseID</summary>
        public int ExpiredDate  { get; set; }

		///<summary>文件开始日期</summary>
        public DateTime FileBeginDate  { get; set; }

		///<summary>文件结束日期</summary>
        public DateTime FileEndDate  { get; set; }

		///<summary>起件号</summary>
        public int BeginPage  { get; set; }

		///<summary>止件号</summary>
        public int EndPage  { get; set; }

		///<summary>页数</summary>
        public int Pages  { get; set; }

		///<summary>份数或件数</summary>
        public int ArchCount  { get; set; }

		///<summary>总页数</summary>
        public int TotalPages  { get; set; }

		///<summary>图纸张数</summary>
        public string DwgPages  { get; set; }

		///<summary>折合A4张数</summary>
        public string DwgPages4A4  { get; set; }

		///<summary>说明</summary>
        public string ArchContent  { get; set; }

		///<summary>图纸变更说明</summary>
        public string ChangeRemark  { get; set; }

		///<summary>主题词</summary>
        public string IndexTitle  { get; set; }

		///<summary>来源</summary>
        public string ArchSource  { get; set; }

		///<summary>备注</summary>
        public string Note  { get; set; }

		///<summary>图纸其它属性</summary>
        public string ArchXml  { get; set; }

		///<summary>最后修改时间</summary>
        public DateTime LastModificationTime  { get; set; }

		///<summary>最后修改人ID</summary>
        public long LastModifierEmpId  { get; set; }

		///<summary>最后修改人姓名</summary>
        public string LastModifierEmpName  { get; set; }

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

		public virtual ArchPaper FK_ArchPaperFile_ArchPaperId { get; set; }
        public virtual ICollection<ArchPaperRelationElec> FK_ArchPaperRelationElec_ArchPaperFileId { get; set; }
    }
}