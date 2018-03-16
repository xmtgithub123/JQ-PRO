/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2014/11/11
*           175417739@qq.com                   
********************************************************************/
using System;

namespace JQ.Log
{
    /// <summary>
    /// 日志模型
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string loggerName
        {
            get;
            set;
        }

        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime timeStamp
        {
            get;
            set;
        }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string renderedMessage
        {
            get;
            set;
        }
    }
}
