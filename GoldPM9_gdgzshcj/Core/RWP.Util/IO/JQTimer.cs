using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace JQ.Util.IO
{
    /// <summary>
    /// 定时器
    /// 现在为每隔1分钟触发一次
    /// </summary>
    public class JQTimer
    {

        private static JQTimer _Default;
        /// <summary>
        /// 默认项
        /// </summary>
        public static JQTimer Default
        {
            get
            {
                if (_Default == null)
                {
                    GetDefaultTimer();
                }
                return _Default;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void GetDefaultTimer()
        {
            if (_Default == null)
            {
                _Default = new JQTimer();
            }
        }

        private System.Threading.Thread thread;
        /// <summary>
        /// 参数
        /// </summary>
        /// <param name="datetime"></param>
        public delegate void OnTimerElapsedEventArgs(DateTime datetime);
        /// <summary>
        /// 触发的事件（每隔一分钟）
        /// </summary>
        public event OnTimerElapsedEventArgs OnTimerElapsed;
        /// <summary>
        /// 是否正在运行中
        /// </summary>
        public bool IsRuning
        {
            get
            {
                if (thread == null)
                {
                    return false;
                }
                return thread.IsAlive;
            }
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Begin()
        {
            if (thread == null)
            {
                thread = new Thread(delegate ()
                {
                    while (true)
                    {
                        Thread temp = new Thread(delegate ()
                        {
                            if (OnTimerElapsed != null)
                            {
                                OnTimerElapsed(DateTime.Now);
                            }
                        });
                        temp.Start();
                        Thread.Sleep(60000);
                    }
                });
            }
            thread.Start();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Stop()
        {
            thread.Abort();
        }
    }
}
