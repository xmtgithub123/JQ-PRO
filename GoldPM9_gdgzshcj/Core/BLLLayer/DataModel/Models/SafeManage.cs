﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-03-03 14:04:55
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class SafeManage
    {
        public SafeManage()
        {
            #region DefaultValue
            Id = 0;
            EngineeringID = 0;
            IsNS = 0;
            IsWS = 0;
            #endregion
            #region 默认实例化
            #endregion
        }
        ///<summary>安全策划</summary>
        public int Id { get; set; }

        ///<summary>工程ID</summary>
        public int EngineeringID { get; set; }

        ///<summary>是否内审 0是 1否</summary>
        public int IsNS { get; set; }

        ///<summary>是否外审 0是 1否</summary>
        public int IsWS { get; set; }

        ///<summary>创建人Id</summary>
        public int CreatorEmpId { get; set; }

        ///<summary>创建人名称</summary>
        public string CreatorEmpName { get; set; }

        ///<summary>创建时间</summary>
        public DateTime CreatorTime { get; set; }

        ///<summary>关联人员策划Id</summary>
        public int EmpManageId { get; set; }
    }
}
