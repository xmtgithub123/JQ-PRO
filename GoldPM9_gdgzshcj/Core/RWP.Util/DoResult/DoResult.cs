/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9
*           175417739@qq.com                   
********************************************************************/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JQ.Util
{
    /// <summary>
    /// 运行结果
    /// </summary>
    public class DoResult
    {
        /// <summary>
        /// 运行结果 构造函数
        /// </summary>
        public DoResult()
        {
        }
        /// <summary>
        /// 状态类型
        /// </summary>
        public DoResultType stateType { get; set; }
        /// <summary>
        /// 结果值
        /// </summary>
        public object stateValue { get; set; }
        /// <summary>
        /// 结果消息
        /// </summary>
        public string stateMsg { get; set; }
        /// <summary>
        /// 跳转页面
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 验证信息列表
        /// </summary>
        public List<ValidationResult> validationResults { get; set; }
    }
}
