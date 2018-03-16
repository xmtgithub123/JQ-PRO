using System;

namespace JQ.Util
{
    /// <summary>
    /// 日志助手
    /// </summary>
    public abstract class LogUtil
    {
        /// <summary>
        /// 锁定对象
        /// </summary>
        private static object smslog = new object();
        /// <summary>
        /// 是否Debug
        /// </summary>
        public static bool isDebug = false;
        /// <summary>
        /// 日志文件夹路径
        /// </summary>
        public static string logPath = string.Empty;

        /// <summary>
        /// 写日志
        /// </summary>
        public static void writeLog(object error)
        {
            if (string.IsNullOrEmpty(logPath))
            {
                logPath = getLogPath();
                IOUtil.creatDirectory(logPath);
            }
            LogUtil.writeLog(logPath, error);
        }

        /// <summary>
        /// 写日志 锁定
        /// </summary>
        /// <param name="error"></param>
        public static void writeLogForLock(object error)
        {
            lock (smslog)
            {
                writeLog(error);
            }
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="error"></param>
        public static void writeLogFroDeBug(object error)
        {
            if (isDebug)
            {
                writeLog(error);
            }
        }

        /// <summary>
        /// 调试日志 锁定
        /// </summary>
        /// <param name="error"></param>
        public static void writeLogFroDeBugAndLock(object error)
        {
            lock (smslog)
            {
                writeLogFroDeBug(error);
            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logstr"></param>
        public static void writeLog(string logPath, object error)
        {
            string TxtPath = logPath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string line = "".PadRight(20, '=') + "\r\n";
            string errorString = line;
            if (error is Exception)
            {
                Exception ex = (Exception)error;
                errorString += "出现应用程序未处理的异常：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
                if (ex != null)
                {
                    errorString += string.Format("异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                   ex.GetType().Name, ex.Message, ex.StackTrace);
                }
                else
                {
                    errorString = string.Format("应用程序线程错误:{0}\r\n", ex);
                }
            }
            else
            {
                errorString += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + TypeHelper.parseString(error) + "\r\n";
            }
            errorString += line;
            IOUtil.writeTxt(errorString, TxtPath);
        }

        /// <summary>
        /// 获取日志文件夹路径
        /// </summary>
        /// <returns></returns>
        private static string getLogPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "log";
        }
    }
}
