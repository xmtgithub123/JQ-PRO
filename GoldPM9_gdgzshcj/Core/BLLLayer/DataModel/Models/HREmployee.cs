﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-12-30 16:09:45
#endregion
using System;
using System.Collections.Generic;
namespace DataModel.Models
{
    public class HREmployee
    {
        public HREmployee()
        {
            #region DefaultValue
            Id = 0;
            CreationTime = new DateTime(1900, 1, 1);
            CreatorEmpId = 0;
            CreatorEmpName = "";
            CreatorDepId = 0;
            CreatorDepName = "";
            DeleterEmpId = 0;
            DeleterEmpName = "";
            DeletionTime = new DateTime(1900, 1, 1);
            EmpName = "";
            EmpIder = "";
            EmpSexID = 0;
            EmpBirthday = new DateTime(1900, 1, 1);
            EmpNation = "";
            EmpStatusID = 0;
            DepID = 0;
            EmpPost = "";
            EmpJoinDate = new DateTime(1900, 1, 1);
            EmpLiveDate = new DateTime(1900, 1, 1);
            EmpAccountsAddress = "";
            EmpHomeAddress = "";
            EmpPhoneNumber = "";
            LastEducationID = 0;
            School = "";
            Specil = "";
            UrgenContactPerson = "";
            UrgenContactPersonTel = "";
            Note = "";
            SysEmpId = 0;
            VacationDays = 0;
            VacationDays1 = 0;
            CompanyID = 0;
            #endregion
            #region 默认实例化
            #endregion
        }
        ///<summary></summary>
        public int Id { get; set; }

        ///<summary></summary>
        public DateTime CreationTime { get; set; }

        ///<summary></summary>
        public int CreatorEmpId { get; set; }

        ///<summary></summary>
        public string CreatorEmpName { get; set; }

        ///<summary></summary>
        public int CreatorDepId { get; set; }

        ///<summary></summary>
        public string CreatorDepName { get; set; }

        ///<summary></summary>
        public int DeleterEmpId { get; set; }

        ///<summary></summary>
        public string DeleterEmpName { get; set; }

        ///<summary></summary>
        public DateTime DeletionTime { get; set; }

        ///<summary></summary>
        public string EmpName { get; set; }

        ///<summary></summary>
        public string EmpIder { get; set; }

        ///<summary></summary>
        public int EmpSexID { get; set; }

        ///<summary></summary>
        public DateTime EmpBirthday { get; set; }

        ///<summary></summary>
        public string EmpNation { get; set; }

        ///<summary></summary>
        public int EmpStatusID { get; set; }

        ///<summary></summary>
        public int DepID { get; set; }

        ///<summary></summary>
        public string EmpPost { get; set; }

        ///<summary></summary>
        public DateTime EmpJoinDate { get; set; }

        ///<summary></summary>
        public DateTime EmpLiveDate { get; set; }

        ///<summary></summary>
        public string EmpAccountsAddress { get; set; }

        ///<summary></summary>
        public string EmpHomeAddress { get; set; }

        ///<summary></summary>
        public string EmpPhoneNumber { get; set; }

        ///<summary></summary>
        public int LastEducationID { get; set; }

        ///<summary></summary>
        public string School { get; set; }

        ///<summary></summary>
        public string Specil { get; set; }

        ///<summary></summary>
        public string UrgenContactPerson { get; set; }

        ///<summary></summary>
        public string UrgenContactPersonTel { get; set; }

        ///<summary></summary>
        public string Note { get; set; }

        /// <summary>
        /// 系统同步过来人员ID
        /// </summary>
        public int SysEmpId { get; set; }

        ///<summary>
        ///年休假天数
        /// </summary>
        public int VacationDays{ get; set; }


        ///<summary>
        ///剩余年休假天数
        /// </summary>
        public int VacationDays1 { get; set; }

        public int CompanyID { get; set; }

    }
}
