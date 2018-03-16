using System;
using System.Collections.Generic;

namespace DataModel.Models
{ 
    public partial class AllBaseEmployee
    {
        public AllBaseEmployee()
        {
			#region DefaultValue
			this.EmpID = 0;
			this.EmpGUId = "";
			this.EmpName = "";
			this.EmpLogin = "";
			this.EmpPassword = "";
			this.SalaryPassword = "";
			this.EmpDepID = 0;
			this.EmpDepName = "";
			this.DepOrder = 0;
			this.EmpOrder = 0;
			this.EmpBirthDate =  new DateTime(1900,1,1);
			this.EmpTitle = "";
			this.EmpTel = "";
			this.EmpComputer = "";
			this.EmpIPAddress = "";
			this.EmpDisk = "";
			this.EmpIsDeleted =  false;
			this.EmpPageSize = 0;
			this.EmpThemesName = "";
			this.EmpMenuType = "";
			this.EmpIsAgent =  false;
			this.EmpSignUrl = "";
			this.EmpIsBind =  false;
			this.EmpMacAddress = "";
			this.EmpTelNX = "";
			this.EmpTelWX = "";
			this.EmpFJNum = "";
			this.EmpOaMail = "";
			this.EmpComMail = "";
			this.EmpZWAddress = "";
			this.EmpWGAddress = "";
			this.EmpNote = "";
			this.EmpPort = "";
			this.EmpHead = "";
			this.EmpIsSub = 0;
			this.DepartmentID = 0;
			this.DepartmentName = "";
			this.DepartmentOrder = "";
			this.RoleName = "";
            this.CompanyID = 0;
            PayManageCoeff = 0.00M;
            PaySkillCoeff = 0.00M;
			#endregion
		}
		///<summary></summary>
        public int EmpID { get; set; }
		///<summary></summary>
        public string EmpGUId { get; set; }
		///<summary></summary>
        public string EmpName { get; set; }
		///<summary></summary>
        public string EmpLogin { get; set; }
		///<summary></summary>
        public string EmpPassword { get; set; }
		///<summary></summary>
        public string SalaryPassword { get; set; }
		///<summary></summary>
        public int EmpDepID { get; set; }
		///<summary></summary>
        public string EmpDepName { get; set; }
		///<summary></summary>
        public int DepOrder { get; set; }
		///<summary></summary>
        public int EmpOrder { get; set; }
		///<summary></summary>
        public System.DateTime EmpBirthDate { get; set; }
		///<summary></summary>
        public string EmpTitle { get; set; }
		///<summary></summary>
        public string EmpTel { get; set; }
		///<summary></summary>
        public string EmpComputer { get; set; }
		///<summary></summary>
        public string EmpIPAddress { get; set; }
		///<summary></summary>
        public string EmpDisk { get; set; }
		///<summary></summary>
        public bool EmpIsDeleted { get; set; }
		///<summary></summary>
        public int EmpPageSize { get; set; }
		///<summary></summary>
        public string EmpThemesName { get; set; }
		///<summary></summary>
        public string EmpMenuType { get; set; }
		///<summary></summary>
        public bool EmpIsAgent { get; set; }
		///<summary></summary>
        public string EmpSignUrl { get; set; }
		///<summary></summary>
        public bool EmpIsBind { get; set; }
		///<summary></summary>
        public string EmpMacAddress { get; set; }
		///<summary></summary>
        public string EmpTelNX { get; set; }
		///<summary></summary>
        public string EmpTelWX { get; set; }
		///<summary></summary>
        public string EmpFJNum { get; set; }
		///<summary></summary>
        public string EmpOaMail { get; set; }
		///<summary></summary>
        public string EmpComMail { get; set; }
		///<summary></summary>
        public string EmpZWAddress { get; set; }
		///<summary></summary>
        public string EmpWGAddress { get; set; }
		///<summary></summary>
        public string EmpNote { get; set; }
		///<summary></summary>
        public string EmpPort { get; set; }
		///<summary></summary>
        public string EmpHead { get; set; }
		///<summary></summary>
        public int EmpIsSub { get; set; }
		///<summary></summary>
        public int DepartmentID { get; set; }
		///<summary></summary>
        public string DepartmentName { get; set; }
		///<summary></summary>
        public string DepartmentOrder { get; set; }
		///<summary></summary>
        public string RoleName { get; set; }

        ///<summary>管理人员系数</summary>
        public decimal PayManageCoeff { get; set; }

        ///<summary>绩效技能系数</summary>
        public decimal PaySkillCoeff { get; set; }

        public int CompanyID { get; set; }
    }
}