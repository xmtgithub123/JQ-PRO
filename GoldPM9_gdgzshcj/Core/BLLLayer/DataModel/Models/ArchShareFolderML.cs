﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-04-01 10:48:47
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class ArchShareFolderML 
    {
        public ArchShareFolderML()
        {
			#region DefaultValue
			Id=0;
			FileNumber="";
			Manager="";
			FileName="";
			Time= new DateTime(1900,1,1);
			PageNumber=0;
			Remark="";
            ByOrder = 0;
            #endregion
            #region 默认实例化
            #endregion
        }
		///<summary>文档目录</summary>
        public int Id  { get; set; }

		///<summary>文件编号</summary>
        public string FileNumber  { get; set; }

		///<summary>责任者</summary>
        public string Manager  { get; set; }

		///<summary>文件材料题名</summary>
        public string FileName  { get; set; }

		///<summary>日期</summary>
        public DateTime Time  { get; set; }

		///<summary>页数</summary>
        public int PageNumber  { get; set; }

		///<summary>页数</summary>
        public string Remark  { get; set; }

        public int FlderId { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int ByOrder { get; set; }

    }
}