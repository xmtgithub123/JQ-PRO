﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-03-03 09:52:35
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class HRAptitudeManager
    {
        public HRAptitudeManager()
        {
            #region DefaultValue
            Id = 0;
            AptitudeName = "";
            RegisterTime = new DateTime(1900, 1, 1);
            EndTime = new DateTime(1900, 1, 1);
            AptitudeEmpName = "";
            IDCard = "";
            Phone = "";
            Address = "";
            IsMess = 0;
            Remark = "";
            AptitudeNumber = "";
            SpecID = 0;
            ProTitleID = 0;
            LevelID = 0;
            AptitudeEmpId = 0;
            CompanyID = 0;
            #endregion
            #region 默认实例化
            #endregion
        }
        ///<summary></summary>
        public int Id { get; set; }

        ///<summary>资质名称</summary>
        public string AptitudeName { get; set; }

        ///<summary>注册时间</summary>
        public DateTime RegisterTime { get; set; }

        ///<summary>到期时间</summary>
        public DateTime EndTime { get; set; }

        ///<summary>注册人</summary>
        public string AptitudeEmpName { get; set; }

        ///<summary>身份证号</summary>
        public string IDCard { get; set; }

        ///<summary>联系电话</summary>
        public string Phone { get; set; }

        ///<summary>地址</summary>
        public string Address { get; set; }

        ///<summary>是否提醒 0不提醒 1提醒</summary>
        public int IsMess { get; set; }

        ///<summary>备注</summary>
        public string Remark { get; set; }

        /// <summary>
        /// 资质证书编号
        /// </summary>
        public string AptitudeNumber { get; set; }

        /// <summary>
        /// 专业ID
        /// </summary>
        public int SpecID { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        public int ProTitleID { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int LevelID { get; set; }

        /// <summary>
        /// 资质注册人
        /// </summary>
        public int AptitudeEmpId { get; set; }

        /// <summary>
        /// 所在公司
        /// </summary>
        public int CompanyID { get; set; }
    }
}