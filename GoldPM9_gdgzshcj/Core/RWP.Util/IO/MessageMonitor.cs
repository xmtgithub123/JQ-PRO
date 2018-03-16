using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JQ.Util.IO
{
    /// <summary>
    /// 消息推送管理类
    /// </summary>
    [HubName("MessageMonitor")]
    public class MessageMonitor : Hub
    {
        private static readonly List<MessageClient> clients = new List<MessageClient>();

        private object lockObj = new object();
        /// <summary>
        /// 异步方式通知客户端
        /// </summary>
        /// <param name="empID">用户ID</param>
        /// <param name="data">数据信息</param>
        public static async Task NotifyAsync(int empID, object data)
        {
            await Task.Run(() => { Notify(empID, data); });
        }

        /// <summary>
        /// 异步方式通知客户端
        /// </summary>
        /// <returns></returns>
        public static async Task NotifyAsync(int empID, Func<int, object> getData)
        {
            await Task.Run(() => { Notify(empID, getData); });
        }

        /// <summary>
        /// 异步方式通知客户端
        /// </summary>
        /// <returns></returns>
        public static async Task NotifyAsync(int empID, Func<int, NotifyOption, object> getData)
        {
            await Task.Run(() => { Notify(empID, getData); });
        }

        /// <summary>
        /// 异步方式通知客户端
        /// </summary>
        /// <param name="empIDs"></param>
        /// <param name="getData"></param>
        /// <returns></returns>
        public static async Task NotifyAsync(IEnumerable<int> empIDs, Func<int, object> getData)
        {
            await Task.Run(() => { Notify(empIDs, getData); });
        }

        /// <summary>
        /// 异步方式通知客户端
        /// </summary>
        /// <param name="empIDs"></param>
        /// <param name="getData"></param>
        /// <returns></returns>
        public static async Task NotifyAsync(IEnumerable<int> empIDs, Func<int, NotifyOption, object> getData)
        {
            await Task.Run(() => { Notify(empIDs, getData); });
        }

        /// <summary>
        /// 通知客户端
        /// </summary>
        /// <param name="empID">用户ID</param>
        /// <param name="data">数据信息</param>
        public static void Notify(int empID, object data)
        {
            foreach (var client in clients)
            {
                if (client.EmpID == empID)
                {
                    GlobalHost.ConnectionManager.GetHubContext<MessageMonitor>().Clients.Client(client.ConnectionID).notify(data);
                }
            }
        }

        /// <summary>
        /// 通知客户端
        /// </summary>
        public static void Notify(int empID, Func<int, object> getData)
        {
            var connections = new List<string>();
            foreach (var client in clients)
            {
                if (client.EmpID == empID)
                {
                    connections.Add(client.ConnectionID);
                }
            }
            if (connections.Count > 0)
            {
                var obj = getData(empID);
                if (obj != null)
                {
                    foreach (var connectionID in connections)
                    {
                        GlobalHost.ConnectionManager.GetHubContext<MessageMonitor>().Clients.Client(connectionID).notify(obj);
                    }
                }
            }
        }

        /// <summary>
        /// 通知客户端
        /// </summary>
        /// <param name="empID"></param>
        /// <param name="getData"></param>
        public static void Notify(int empID, Func<int, NotifyOption, object> getData)
        {
            foreach (var client in clients)
            {
                if (client.EmpID == empID)
                {
                    var obj = getData(client.EmpID, client.Option);
                    if (obj != null)
                    {
                        GlobalHost.ConnectionManager.GetHubContext<MessageMonitor>().Clients.Client(client.ConnectionID).notify(obj);
                    }
                }
            }
        }

        /// <summary>
        /// 通知客户端
        /// </summary>
        /// <param name="empIDs">用户ID集合</param>
        /// <param name="getData">参数的方法</param>
        public static void Notify(IEnumerable<int> empIDs, Func<int, object> getData)
        {
            var hubClients = GlobalHost.ConnectionManager.GetHubContext<MessageMonitor>().Clients;
            foreach (var client in clients)
            {
                if (empIDs.Contains(client.EmpID))
                {
                    var obj = getData(client.EmpID);
                    if (obj != null)
                    {
                        hubClients.Client(client.ConnectionID).notify(obj);
                    }
                }
            }
        }

        /// <summary>
        /// 通知客户端
        /// </summary>
        /// <param name="empIDs">用户ID集合</param>
        /// <param name="getData">方法</param>
        public static void Notify(IEnumerable<int> empIDs, Func<int, NotifyOption, object> getData)
        {
            foreach (var client in clients)
            {
                if (empIDs.Contains(client.EmpID))
                {
                    var obj = getData(client.EmpID, client.Option);
                    if (obj != null)
                    {
                        GlobalHost.ConnectionManager.GetHubContext<MessageMonitor>().Clients.Client(client.ConnectionID).notify(obj);
                    }
                }
            }
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            PushToClients(JQ.Util.TypeParse.parse<int>(this.Context.Request.QueryString["empID"]), this.Context.Request.QueryString["agentFlows"], this.Context.ConnectionId);
            return base.OnConnected();
        }

        private void PushToClients(int empID, string agentFlows, string connectionID)
        {
            try
            {
                Monitor.Enter(lockObj);
                if (empID == 0 || clients.FirstOrDefault(m => m.ConnectionID == connectionID) != null)
                {
                    return;
                }
                var client = new MessageClient() { EmpID = empID, ConnectionID = connectionID };
                if (!string.IsNullOrEmpty(agentFlows))
                {
                    client.Option = new NotifyOption();
                    client.Option.AgentFlows = agentFlows;
                }
                clients.Add(client);
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                Monitor.Enter(lockObj);
                for (var i = 0; i < clients.Count; i++)
                {
                    if (clients[i].ConnectionID == this.Context.ConnectionId)
                    {
                        //断开连接的主动通知
                        //Clients.Client(this.Context.ConnectionId).onDisconnect(clients[i]);
                        clients.RemoveAt(i);
                        break;
                    }
                }
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// 重新连接
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            PushToClients(JQ.Util.TypeParse.parse<int>(this.Context.Request.QueryString["empID"]), this.Context.Request.QueryString["agentFlows"], this.Context.ConnectionId);
            return base.OnReconnected();
        }

        /// <summary>
        /// 注销时候的调用
        /// </summary>
        /// <param name="connectionID"></param>
        public void Logout()
        {
            try
            {
                Monitor.Enter(lockObj);
                for (var i = 0; i < clients.Count; i++)
                {
                    if (clients[i].ConnectionID == this.Context.ConnectionId)
                    {
                        clients.RemoveAt(i);
                        break;
                    }
                }
            }
            catch
            {
                Monitor.Exit(lockObj);
            }
        }

        /// <summary>
        /// 消息客户端包装
        /// </summary>
        public class MessageClient
        {
            /// <summary>
            /// 用户ID
            /// </summary>
            public int EmpID
            {
                get; set;
            }

            /// <summary>
            /// 连接ID
            /// </summary>
            public string ConnectionID
            {
                get; set;
            }

            /// <summary>
            /// 授权信息
            /// </summary>
            public NotifyOption Option
            {
                get; set;
            }
        }

        /// <summary>
        /// 通知的参数
        /// </summary>
        public class NotifyOption
        {
            /// <summary>
            /// 授权委托类型
            /// </summary>
            public int AgentType
            {
                get; set;
            }

            /// <summary>
            /// 授权的菜单名称
            /// </summary>
            public string AgentMenuNames
            {
                get; set;
            }

            /// <summary>
            /// 授权的流程名称
            /// </summary>
            public string AgentFlows
            {
                get; set;
            }
        }
    }
}
