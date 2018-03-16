using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class KeyValue
    {
        public string Key { set; get; }
        public string Value { set; get; }
    }

    public class MenuPermitSet
    {
        ///<summary>菜单ID</summary>
        public int MenuID { get; set; }

        ///<summary>菜单需要权限</summary>
        public int MenuPermissions { get; set; }

    }
    public class MenuPermit
    {
        ///<summary>菜单ID</summary>
        public int MenuID { get; set; }

        ///<summary></summary>
        public int ParentID { get; set; }

        ///<summary>菜单名称</summary>
        public string MenuName { get; set; }

        ///<summary>菜单标识</summary>
        public string MenuNameEng { get; set; }

        ///<summary>排序字段</summary>
        public string MenuOrder { get; set; }

        ///<summary>菜单路径</summary>
        public string MenuCommand { get; set; }

        ///<summary>菜单帮助路径</summary>
        public string MenuHelp { get; set; }

        ///<summary>菜单图标</summary>
        public string MenuImageUrl { get; set; }

        ///<summary>菜单备注</summary>
        public string MenuNote { get; set; }

        ///<summary>是否二级权限</summary>
        public bool MenuIsSecond { get; set; }

        ///<summary>是否必须菜单</summary>
        public bool MenuIsMust { get; set; }

        ///<summary>菜单需要权限</summary>
        public int MenuPermissions { get; set; }


        public string add { get; set; }
        public string edit { get; set; }
        public string del { get; set; }
        public string export { get; set; }
        public string emp { get; set; }
        public string dep { get; set; }
        public string allview { get; set; }
        public string alledit { get; set; }
        public string allDown { get; set; }
    }

    public class BaseQualification
    {
        ///<summary>资格用户ID</summary>
        public int QualificationEmpID { get; set; }

        ///<summary>专业ID</summary>
        public int QualificationSpecID { get; set; }

        ///<summary>资格（负责人、设校审批）</summary>
        public int QualificationValue { get; set; }
  
    }
}
