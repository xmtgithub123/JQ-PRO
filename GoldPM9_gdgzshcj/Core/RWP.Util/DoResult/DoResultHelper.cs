/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9
*           175417739@qq.com                   
********************************************************************/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JQ.Util
{
    /// <summary>
    /// 运行结果助手
    /// </summary>
    public abstract class DoResultHelper
    {
        #region 执行成功
        /// <summary>
        /// 执行成功
        /// </summary>
        /// <param name="stateMsg">执行反馈消息</param>
        /// <returns></returns>
        public static DoResult doSuccess(string stateMsg)
        {
            return doSuccess(null, stateMsg, string.Empty);
        }
        /// <summary>
        /// 执行成功
        /// </summary>
        /// <param name="stateValue">执行返回值</param>
        /// <param name="stateMsg">执行反馈消息</param>
        /// <returns></returns>
        public static DoResult doSuccess(object stateValue, string stateMsg)
        {
            return doSuccess(stateValue, stateMsg, string.Empty);
        }
        /// <summary>
        /// 执行成功
        /// </summary>
        /// <param name="stateValue">执行返回值</param>
        /// <param name="stateMsg">执行反馈消息</param>
        /// <param name="url">跳转Url</param>
        /// <returns></returns>
        public static DoResult doSuccess(object stateValue, string stateMsg, string url)
        {
            return new DoResult { stateType = DoResultType.success, stateValue = stateValue, stateMsg = stateMsg.Replace("\"", "＂").Replace("'", "＇"), url = url };
        }
        #endregion

        #region 执行失败
        /// <summary>
        /// 执行失败
        /// </summary>
        /// <param name="stateMsg">错误消息</param>
        /// <returns></returns>
        public static DoResult doError(string stateMsg)
        {
            return doError(stateMsg, string.Empty, null);
        }
        /// <summary>
        /// 执行失败
        /// </summary>
        /// <param name="stateMsg">错误消息</param>
        /// <param name="validationResults">字段错误信息集合</param>
        /// <returns></returns>
        public static DoResult doError(string stateMsg, IEnumerable<ValidationResult> validationResults)
        {
            return doError(stateMsg, string.Empty, validationResults);
        }
        /// <summary>
        /// 执行失败
        /// </summary>
        /// <param name="stateMsg">错误消息</param>
        /// <param name="url">跳转Url</param>
        /// <returns></returns>
        public static DoResult doError(string stateMsg, string url)
        {
            return doError(stateMsg, url, null);
        }
        /// <summary>
        /// 执行失败
        /// </summary>
        /// <param name="stateMsg">错误消息</param>
        /// <param name="url">跳转Url</param>
        /// <param name="validationResults">字段错误信息集合</param>
        /// <returns></returns>
        public static DoResult doError(string stateMsg, string url, IEnumerable<ValidationResult> validationResults)
        {
            DoResultType resultType = DoResultType.fail;
            if (validationResults.isNotEmpty())
            {
                resultType = DoResultType.validFail;
            }
            return new DoResult { stateType = resultType, stateMsg = stateMsg.Replace("\"", "＂").Replace("'", "＇"), url = url, validationResults = resultType == DoResultType.fail ? null : validationResults.ToList() };
        }

        /// <summary>
        /// 没有权限执行此Action
        /// </summary>
        /// <returns></returns>
        public static DoResult doUnAuthorized()
        {
            return new DoResult { stateType = DoResultType.unauthorized, stateMsg = null, url = null, validationResults = null };
        }
        #endregion

        #region 验证消息
        /// <summary>
        /// 验证消息
        /// </summary>
        /// <param name="validationResults">消息列表</param>
        /// <returns></returns>
        public static DoResult doIsValid(IEnumerable<ValidationResult> validationResults)
        {
            DoResultType resultType = DoResultType.success;
            if (validationResults.isNotEmpty())
            {
                resultType = DoResultType.validFail;
            }
            return new DoResult { stateType = resultType, validationResults = resultType == DoResultType.success ? null : validationResults.ToList() };
        }
        #endregion

        #region 验证消息生成字符串
        /// <summary>
        /// 验证消息生成字符串
        /// </summary>
        /// <param name="validationResults">消息列表</param>
        /// <returns></returns>
        public static string validationResultString(IEnumerable<ValidationResult> validationResults)
        {
            string msgString = string.Empty;
            if (validationResults.isNotEmpty())
            {
                foreach (var item in validationResults)
                {
                    msgString += item.ErrorMessage + "\r\n";
                }
            }
            return msgString;
        }
        #endregion
    }
}
