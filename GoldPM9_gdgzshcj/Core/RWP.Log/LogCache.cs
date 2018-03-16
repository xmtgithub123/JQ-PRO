/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2014/11/11
*           175417739@qq.com                   
********************************************************************/

using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JQ.Log
{
    public class LogCache
    {
        /// <summary>
        /// 缓存开启状态，默认关闭，使用时开启，否则可能会导致内存溢出
        /// </summary>
        public static bool isCache = false;
        #region 私有
        /// <summary>
        /// 事件队列
        /// </summary>
        private static ConcurrentQueue<LogModel> logModelQueue = new ConcurrentQueue<LogModel>();
        #endregion

        /// <summary>
        /// 添加日志到缓存对接
        /// </summary>
        /// <param name="logModel"></param>
        public static void appendLogToCache(LogModel logModel)
        {
            if (isCache)
            {
                logModelQueue.Enqueue(logModel);
            }
        }

        /// <summary>
        /// 获取日志队列
        /// </summary>
        /// <returns></returns>
        public static List<LogModel> getLogModelQueue()
        {
            List<LogModel> _logModelQueue = new List<LogModel>();
            for (int i = 0; i < logModelQueue.Count; i++)
            {
                LogModel logModel;
                if (logModelQueue.TryDequeue(out logModel))
                {
                    _logModelQueue.Add(logModel);
                }
            }
            return _logModelQueue;
        }

    }
}
