/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2014/11/11
*           175417739@qq.com                   
********************************************************************/

using log4net.Appender;
using log4net.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JQ.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace JQ.Log
{
    /// <summary>
    /// 日记记录HTTP方式
    /// </summary>
    public class HttpAppender : AppenderSkeleton
    {
        #region 公共
        /// <summary>
        /// 远程地址
        /// </summary>
        public string remoteAddress { get; set; }
        /// <summary>
        /// 队列大小
        /// </summary>
        public int queueSize { get; set; }
        #endregion

        #region 私有
        /// <summary>
        /// 事件队列
        /// </summary>
        private ArrayList eventQueue;
        #endregion

        #region Public Instance Constructors
        public HttpAppender()
            : base()
        {
            eventQueue = new ArrayList();
        }
        #endregion

        #region Override implementation of AppenderSkeleton
        protected override void Append(LoggingEvent loggingEvent)
        {
            lock (eventQueue.SyncRoot)
            {
                eventQueue.Add(loggingEvent);
                if (eventQueue.Count >= queueSize)
                {
                    this.clear();
                }
            }
        }
        #endregion

        #region Public Instance Methods
        /// <summary>
        /// Clear the list of events
        /// </summary>
        /// <remarks>
        /// Clear the list of events
        /// </remarks>
        virtual public void clear()
        {
            lock (eventQueue.SyncRoot)
            {
                saveToServer();
                eventQueue.Clear();
            }
        }

        /// <summary>
        /// Gets the events that have been logged.
        /// </summary>
        /// <returns>The events that have been logged</returns>
        /// <remarks>
        /// <para>
        /// Gets the events that have been logged.
        /// </para>
        /// </remarks>
        virtual public LoggingEvent[] getEvents()
        {
            lock (eventQueue.SyncRoot)
            {
                return (LoggingEvent[])eventQueue.ToArray(typeof(LoggingEvent));
            }
        }

        #endregion Public Instance Methods

        #region private methods
        private void saveToServer()
        {
            lock (eventQueue.SyncRoot)
            {
                List<LogModel> logList = new List<LogModel>();
                foreach (LoggingEvent evt in eventQueue)
                {
                    try
                    {
                        if (StringUtil.startsWithIgnoreCase(evt.LoggerName, "rwp_"))
                        {
                            LogModel logModel = new LogModel { loggerName = evt.LoggerName, timeStamp = evt.TimeStamp, renderedMessage = evt.RenderedMessage };
                            logList.Add(logModel);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        continue;
                    }
                }
                saveToServer(logList);
            }
        }

        private void saveToServer(List<LogModel> logList)
        {
            try
            {
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
                timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
                string str = JsonConvert.SerializeObject(logList, Newtonsoft.Json.Formatting.Indented, timeConverter);
                postStrToServer(str, remoteAddress);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return;
            }
        }

        private void postStrToServer(string content, string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = System.Text.Encoding.UTF8;
                client.UploadStringAsync(new Uri(url), (content));
            }
        }

        #endregion
    }
}
