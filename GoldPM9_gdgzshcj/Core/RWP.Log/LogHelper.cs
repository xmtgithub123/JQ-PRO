/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2014/11/11
*           175417739@qq.com                   
********************************************************************/

using log4net;
using System;
using System.IO;

namespace JQ.Log
{
    /// <summary>
    /// 日志助手
    /// </summary>
    public class LogHelper
    {
        private LogHelper()
        {
        }

        //加rwp前缀的目的，用于Debug时屏蔽 其他类型日志
        private static readonly ILog rwp_logInfo = LogManager.GetLogger("rwp_LogInfo");
        private static readonly ILog rwp_logError = LogManager.GetLogger("rwp_LogError");
        private static readonly ILog rwp_logConsole = LogManager.GetLogger("rwp_LogConsole");

        #region 设置参数
        /// <summary>
        /// 设置参数
        /// </summary>
        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="path">配置文件路径</param>
        public static void SetConfig(string path)
        {
            if (!File.Exists(path))
                return;

            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="configFile">配置文件对象</param>
        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        /// <summary>
        /// 设置HTTP方式记录日志
        /// </summary>
        /// <param name="httpAppender">HttpAppender对象</param>
        public static void SetHttpAppender(HttpAppender httpAppender)
        {
            log4net.Config.BasicConfigurator.Configure(httpAppender);
        }
        #endregion

        #region 信息
        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="info">信息对象</param>
        public static void Info(object info)
        {
            if (rwp_logInfo.IsInfoEnabled)
            {
                rwp_logInfo.Info(info);
            }
        }

        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="info">信息对象</param>
        /// <param name="ex">异常对象</param>
        public static void Info(object info, Exception ex)
        {
            if (rwp_logInfo.IsInfoEnabled)
            {
                rwp_logInfo.Info(info, ex);
            }
        }
        #endregion

        #region 错误
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="info">错误信息对象</param>
        public static void Error(object info)
        {
            if (rwp_logError.IsErrorEnabled)
            {
                rwp_logError.Error(info);
            }
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void Error(Exception ex)
        {
            if (rwp_logError.IsErrorEnabled)
            {
                rwp_logError.Error(string.Format("{0}\r\n{1}\r\n{2}\r\n", ex.Message, ex.StackTrace, ex.InnerException));
            }
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="info">信息对象</param>
        /// <param name="ex">异常对象</param>
        public static void Error(object info, Exception ex)
        {
            if (rwp_logError.IsErrorEnabled)
            {
                rwp_logError.Error(string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n", info, ex.Message, ex.StackTrace, ex.InnerException));
            }
        }
        #endregion

        #region 调试
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="info">调试信息对象</param>
        public static void Debug(object info)
        {
            if (rwp_logConsole.IsDebugEnabled)
            {
                rwp_logConsole.Debug(info);
            }
        }
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void Debug(Exception ex)
        {
            if (rwp_logConsole.IsDebugEnabled)
            {
                rwp_logConsole.Debug("", ex);
            }
        }
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="info">信息对象</param>
        /// <param name="ex">异常对象</param>
        public static void Debug(object info, Exception ex)
        {
            if (rwp_logConsole.IsDebugEnabled)
            {
                rwp_logConsole.Debug(info, ex);
            }
        }
        #endregion

    }
}
