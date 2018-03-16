/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9
*           175417739@qq.com                   
********************************************************************/

using System.Collections.Generic;

namespace JQ.Web
{
    /// <summary>
    /// Mvc动作信息
    /// </summary>
    public class MvcActionInfo
    {
        /// <summary>
        /// 说明
        /// </summary>
        public string display
        {
            get;
            set;
        }

        /// <summary>
        /// 所属区域
        /// </summary>
        public string area
        {
            get;
            set;
        }

        /// <summary>
        /// 控制器动作 Controllers|Action
        /// </summary>
        public string controllerAction
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public TypeOf typeOf
        {
            get;
            set;
        }
        /// <summary>
        /// 子动作
        /// </summary>
        public IEnumerable<MvcActionInfo> children
        {
            get;
            set;
        }

        public string state { get; set; }
    }
}
