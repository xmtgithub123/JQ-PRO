using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JQ.Util
{
    public class PermissionAttribute : Attribute
    {
        public string PermissionNames
        {
            get; set;
        }

        public PermissionAttribute(string permissionNames)
        {
            this.PermissionNames = permissionNames;
        }

        public string A
        {
            get; set;
        }
    }
}
