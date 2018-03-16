/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9
*           175417739@qq.com                   
********************************************************************/

namespace JQ.Util
{
    /// <summary>
    /// 运行结果类型
    /// </summary>
    public enum DoResultType
    {
        /// <summary>
        /// 成功
        /// </summary>
        success,
        /// <summary>
        /// 失败
        /// </summary>
        fail,
        /// <summary>
        /// 验证失败
        /// </summary>
        validFail,
        /// <summary>
        /// 警告
        /// </summary>
        warning,
        /// <summary>
        /// 信息
        /// </summary>
        info,
        /// <summary>
        /// 没有权限
        /// </summary>
        unauthorized
    }
}
