using System;
using System.Collections.Generic;

namespace DataModel.Models
{ 
    public partial class View_MenuPermit
    {
        public View_MenuPermit()
        {
			#region DefaultValue
			this.MenuID = 0;
			this.EmpID = 0;
            this.Permit = 0;
            #endregion
        }
		///<summary></summary>
        public int MenuID { get; set; }
		///<summary></summary>
        public int EmpID { get; set; }
        public int Permit { get; set; }
    }
}