using System;
using System.Collections.Generic;

namespace DataModel.Models
{ 
    public partial class View_Form
    {
        public View_Form()
        {
			#region DefaultValue
			this.ok = 0;
			#endregion
		}
		///<summary></summary>
        public int ok { get; set; }
    }
}