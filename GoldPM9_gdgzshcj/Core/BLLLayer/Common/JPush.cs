using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cn.jpush.api;
using cn.jpush.api.push.mode;
using System.Configuration;
using cn.jpush.api.push;

namespace Common
{
    public static class JPush
    {
        /// <summary>
        /// 执行推送用户App中的脚本
        /// </summary>
        /// <param name="winName"></param>
        /// <param name="frmName"></param>
        /// <param name="script"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public static int exec(string winName, string frmName, string script, string userIds)
        {
            return push(
                "_execScript",
                winName + "," + frmName,    // winName, frmName
                script,                     // script
                0,
                userIds
            );
        }

        /// <summary>
        /// 极光消息推送
        /// </summary>
        /// <param name="title">标题（不大于20个英文字符或10个中午字符）</param>
        /// <param name="content">内容（大于80个英文字符或40个中文字符会自动截取）</param>
        /// <param name="refTable">目标标示（不大于10个英文字符）</param>
        /// <param name="refID">目标ID</param>
        /// <param name="userIds">推送用户id, 多个用户用英文逗号分隔，eg. 1,2</param>
        /// <param name="flowId">表单流程Id</param>
        /// <param name="flowMultiSignID">表单会签Id</param>
        /// <returns></returns>
        public static int push(string title, string content, string refTable, int refID, string userIds = "", int flowNodeId = 0, int flowMultiSignID = 0)
        {
            if (string.IsNullOrEmpty(userIds))
            {
                return 0;
            }
            return jnoti(title, content, refTable, refID, "alias", userIds, flowNodeId, flowMultiSignID);
        }

        /// <summary>
        /// 极光消息推送
        /// </summary>
        /// <param name="title">标题（不大于20个英文字符或10个中午字符）</param>
        /// <param name="content">内容（大于80个英文字符或40个中文字符会自动截取）</param>
        /// <param name="refTable">目标标示（不大于10个英文字符）</param>
        /// <param name="refID">目标ID</param>
        /// <returns></returns>
        public static int pushAll(string title, string content, string refTable, int refID)
        {
            return jnoti(title, content, refTable, refID, "tags", "");
        }

        /// <summary>
        /// 极光消息推送
        /// </summary>
        /// <param name="title">标题（不大于20个英文字符或10个中午字符）</param>
        /// <param name="content">内容（大于80个英文字符或40个中文字符会自动截取）</param>
        /// <param name="refTable">目标标示（不大于10个英文字符）</param>
        /// <param name="refID">目标ID</param>
        /// <param name="userIds">推送用户id, 多个用户用英文逗号分隔，eg. 1,2</param>
        /// <returns></returns>
        private static int jmess(string title, string content, string refTable, int refID, string type, string userIds = "", int flowNodeId = 0, int flowMultiSignID = 0)
        {
            try
            {
                if (!Convert.ToBoolean(ConfigurationManager.AppSettings["JPush_APP_Enable"]))
                {
                    return 0;
                }

                string JPush_APP_KEY = ConfigurationManager.AppSettings["JPush_APP_KEY"].ToString();
                string JPush_MASTER_SECRET = ConfigurationManager.AppSettings["JPush_MASTER_SECRET"].ToString();
                string JPush_Alias_BeginWith = ConfigurationManager.AppSettings["JPush_Alias_BeginWith"].ToString();

                PushPayload pushPayload = new PushPayload();
                pushPayload.platform = Platform.all();
                if (type == "alias")
                {
                    string[] uIds = userIds.Split(',');
                    List<string> pIds = new List<string>();
                    foreach (string id in uIds)
                    {
                        pIds.Add(JPush_Alias_BeginWith + id);
                    }
                    pushPayload.audience = Audience.s_alias(pIds.ToArray());
                }
                else if (type == "tags")
                {
                    pushPayload.audience = Audience.s_tag(JPush_Alias_BeginWith);
                }
                else
                {
                    return 0;
                }

                pushPayload.message = Message.content(content)
                    .setTitle(title).setContentType("text")
                    .AddExtras("FlowNodeId", flowNodeId)
                    .AddExtras("FlowMultiSignID", flowMultiSignID)
                    .AddExtras("RefTable", refTable)
                    .AddExtras("RefID", refID)
                    .AddExtras("Title", title);

                pushPayload.options.time_to_live = 86400 * 7; // 推送当前用户不在线时，为该用户保留多长时间的离线消息（7天）
                pushPayload.options.apns_production = true;

                JPushClient JPush = new JPushClient(JPush_APP_KEY, JPush_MASTER_SECRET);
                MessageResult ret = JPush.SendPush(pushPayload);
                return ret.msg_id;
            }
            catch
            {               
                return 0;
            }
        }

        /// <summary>
        /// 极光消息推送
        /// </summary>
        /// <param name="title">标题（不大于20个英文字符或10个中午字符）</param>
        /// <param name="content">内容（大于80个英文字符或40个中文字符会自动截取）</param>
        /// <param name="refTable">目标标示（不大于10个英文字符）</param>
        /// <param name="refID">目标ID</param>
        /// <param name="userIds">推送用户id, 多个用户用英文逗号分隔，eg. 1,2</param>
        /// <returns></returns>
        private static int jnoti(string title, string content, string refTable, int refID, string type, string userIds = "", int flowNodeId = 0, int flowMultiSignID = 0)
        {
            try
            {
                if (!Convert.ToBoolean(ConfigurationManager.AppSettings["JPush_APP_Enable"]))
                {
                    return 0;
                }

                string JPush_APP_KEY = ConfigurationManager.AppSettings["JPush_APP_KEY"].ToString();
                string JPush_MASTER_SECRET = ConfigurationManager.AppSettings["JPush_MASTER_SECRET"].ToString();
                string JPush_Alias_BeginWith = ConfigurationManager.AppSettings["JPush_Alias_BeginWith"].ToString();

                PushPayload pushPayload = new PushPayload();
                pushPayload.platform = Platform.all();
                if (type == "alias")
                {
                    string[] uIds = userIds.Split(',');
                    List<string> pIds = new List<string>();
                    foreach (string id in uIds)
                    {
                        pIds.Add(JPush_Alias_BeginWith + id);
                    }
                    pushPayload.audience = Audience.s_alias(pIds.ToArray());
                }
                else if (type == "tags")
                {
                    pushPayload.audience = Audience.s_tag(JPush_Alias_BeginWith);
                }
                else
                {
                    return 0;
                }

                var notification = new Notification();
                notification.IosNotification = new cn.jpush.api.push.notification.IosNotification()
                    .AddExtra("FlowNodeId", flowNodeId)
                    .AddExtra("FlowMultiSignID", flowMultiSignID)
                    .AddExtra("RefTable", refTable)
                    .AddExtra("RefID", refID)
                    .AddExtra("Title", title);
                notification.AndroidNotification = new cn.jpush.api.push.notification.AndroidNotification()
                    .AddExtra("FlowNodeId", flowNodeId)
                    .AddExtra("FlowMultiSignID", flowMultiSignID)
                    .AddExtra("RefTable", refTable)
                    .AddExtra("RefID", refID)
                    .AddExtra("Title", title);
                notification.setAlert(content);
                pushPayload.notification = notification;

                JPushClient JPush = new JPushClient(JPush_APP_KEY, JPush_MASTER_SECRET);
                MessageResult ret = JPush.SendPush(pushPayload);
                return ret.msg_id;
            }
            catch
            {
                return 0;
            }
        }
    }
}
