using System;
using System.Collections.Generic;

namespace DataModel.Models
{ 
    public partial class V_SystemTableFKey
    {
        public V_SystemTableFKey()
        {
			#region DefaultValue
			this.FKName = "";
			this.TableName = "";
			this.ColName = "";
			this.RefTableName = "";
			this.RefColName = "";
			this.CnstIsNotRepl = "";
			this.CnstIsUpdateCascade = "";
			this.CnstIsDeleteCascade = "";
			#endregion
		}
		///<summary></summary>
        public string FKName { get; set; }
		///<summary></summary>
        public string TableName { get; set; }
		///<summary></summary>
        public string ColName { get; set; }
		///<summary></summary>
        public string RefTableName { get; set; }
		///<summary></summary>
        public string RefColName { get; set; }
		///<summary></summary>
        public string CnstIsNotRepl { get; set; }
		///<summary></summary>
        public string CnstIsUpdateCascade { get; set; }
		///<summary></summary>
        public string CnstIsDeleteCascade { get; set; }
    }
}