/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9
*           175417739@qq.com                   
********************************************************************/

using System.ComponentModel;

namespace JQ.Web
{
    /// <summary>
    /// 类型的类型
    /// </summary>
    public enum TypeOf
    {
        [Description("控制器")]
        controllers = 1,
        [Description("动作")]
        action = 2
    }
}
