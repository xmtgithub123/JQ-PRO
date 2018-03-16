﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:27
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class DesDelivery 
    {
        public DesDelivery()
        {
			#region DefaultValue
			DeliveryID=0;
			ProjID=0;
			ProjNumber="";
			ProjName="";
			DeliveryDate= new DateTime(1900,1,1);
			DeliveryTypeID=0;
			DeliveryPhase="";
			DeliverySpec="";
			DeliveryVol="";
			DeliveryDesignEmpName="";
			DeliveryBlueDate= new DateTime(1900,1,1);
			DeliveryCompany="";
			DeliveryRecEmpName="";
			DeliveryDetailNum=0;
			DeliveryNote="";
			#endregion
			#region 默认实例化
			#endregion
        }
		///<summary>---交付单---</summary>
        public int DeliveryID  { get; set; }

		///<summary>工程</summary>
        public int ProjID  { get; set; }

		///<summary>工程编号</summary>
        public string ProjNumber  { get; set; }

		///<summary>工程名称</summary>
        public string ProjName  { get; set; }

		///<summary>交付日期</summary>
        public DateTime DeliveryDate  { get; set; }

		///<summary>交付方式</summary>
        public int DeliveryTypeID  { get; set; }

		///<summary>交付专业</summary>
        public string DeliveryPhase  { get; set; }

		///<summary>交付阶段</summary>
        public string DeliverySpec  { get; set; }

		///<summary>交付卷册</summary>
        public string DeliveryVol  { get; set; }

		///<summary>设计人</summary>
        public string DeliveryDesignEmpName  { get; set; }

		///<summary>晒图日期</summary>
        public DateTime DeliveryBlueDate  { get; set; }

		///<summary>接收单位</summary>
        public string DeliveryCompany  { get; set; }

		///<summary>接收人</summary>
        public string DeliveryRecEmpName  { get; set; }

		///<summary>份数</summary>
        public int DeliveryDetailNum  { get; set; }

		///<summary>交付备注</summary>
        public string DeliveryNote  { get; set; }

		    }
}