using System;
using System.Collections.Generic;

namespace DataModel.Models
{ 
    public partial class V_SystemTableStructure
    {
        public V_SystemTableStructure()
        {
			#region DefaultValue
			this.TableName = "";
			this.TableNote = "";
			this.ColIndex = 0;
			this.ColName = "";
			this.IsIdentity = "";
			this.IsPK = "";
			this.ColType = "";
			this.ColLength = "";
			this.ColLen = "";
			this.ColScale = "";
			this.IsToNull = "";
			this.DefaultText = "";
			this.ColNote = "";
			#endregion
		}
		///<summary></summary>
        public string TableName { get; set; }
		///<summary></summary>
        public string TableNote { get; set; }
		///<summary></summary>
        public int ColIndex { get; set; }
		///<summary></summary>
        public string ColName { get; set; }
		///<summary></summary>
        public string IsIdentity { get; set; }
		///<summary></summary>
        public string IsPK { get; set; }
		///<summary></summary>
        public string ColType { get; set; }
		///<summary></summary>
        public string ColLength { get; set; }
		///<summary></summary>
        public string ColLen { get; set; }
		///<summary></summary>
        public string ColScale { get; set; }
		///<summary></summary>
        public string IsToNull { get; set; }
		///<summary></summary>
        public string DefaultText { get; set; }
		///<summary></summary>
        public string ColNote { get; set; }
    }
}