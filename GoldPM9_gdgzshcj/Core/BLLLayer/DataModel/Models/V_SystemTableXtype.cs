using System;
using System.Collections.Generic;

namespace DataModel.Models
{ 
    public partial class V_SystemTableXtype
    {
        public V_SystemTableXtype()
        {
			#region DefaultValue
			this.TableName = "";
			this.XName = "";
			this.XType = "";
			this.CreateDate =  new DateTime(1900,1,1);
			#endregion
		}
		///<summary></summary>
        public string TableName { get; set; }
		///<summary></summary>
        public string XName { get; set; }
		///<summary></summary>
        public string XType { get; set; }
		///<summary></summary>
        public System.DateTime CreateDate { get; set; }
    }
}