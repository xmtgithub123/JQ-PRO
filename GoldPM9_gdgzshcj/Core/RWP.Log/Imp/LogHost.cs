/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2014/11/11
*           175417739@qq.com                   
********************************************************************/

using JQ.Util;
using System.Collections.Generic;

namespace JQ.Log
{
    /// <summary>
    /// 日志服务
    /// </summary>
    public class LogHost : ILogHost
    {
        /// <summary>
        /// 日志推送接口
        /// </summary>
        /// <param name="logList"></param>
        public void postLog(List<LogModel> logList)
        {
            if (logList.isNotEmpty())
            {
                foreach (LogModel item in logList)
                {
                    LogCache.appendLogToCache(item);
                }
            }
        }
    }
}
