using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DBSql.OA
{
    /// <summary>
    /// 日程提醒类
    /// </summary>
    public static class SchedulerRemind
    {
        static SchedulerRemind()
        {
            JQ.Util.IO.JQTimer.Default.OnTimerElapsed += DBSql.OA.SchedulerRemind.Remind;
        }

        private static object lockobj = new object();

        private static List<DataModel.Models.SchedulerBase> _cacheToRemind;

        public static DateTime LastCacheTime;

        private static List<DataModel.Models.SchedulerBase> GetToRemindCache()
        {
            if (_cacheToRemind == null)
            {
                LastCacheTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:59:00"));
                _cacheToRemind = new Scheduler().GetToRemindList(LastCacheTime);
            }
            return _cacheToRemind;
        }


        private static List<SchedulerNotify> GetToRemindData(DateTime datetime)
        {
            try
            {
                Monitor.Enter(lockobj);
                if (datetime.Minute == 0)
                {
                    _cacheToRemind = null;
                    //缓存一个小时之内的数据
                    //cacheToRemind = new Scheduler().GetToRemindList(datetime.AddMinutes(59));
                }
                //获取出本一分钟之内需要提醒的数据
                var _data = GetToRemindCache();
                var result = new List<SchedulerNotify>();
                for (var i = 0; i < _data.Count; i++)
                {
                    if (_data[i].StartTime < datetime)
                    {
                        foreach (var empID in _data[i].JoinEmpIDs.Split(','))
                        {
                            var temp = 0;
                            if (int.TryParse(empID, out temp) && temp > 0)
                            {
                                AppendToRemove(result, temp, _data[i].ID);
                            }
                        }
                        _data.RemoveAt(i);
                        i--;
                        continue;
                    }
                    if (_data[i].IsFullDay)
                    {
                        //如果为全天日程，不需要再实时计算，提醒在登录时以及实时推送
                        continue;
                    }
                    else
                    {
                        var timespan = datetime - _data[i].NotifyTime;
                        if (timespan.Minutes == 0 || timespan.Minutes == 1)
                        {
                            foreach (var empID in _data[i].JoinEmpIDs.Split(','))
                            {
                                var temp = 0;
                                if (int.TryParse(empID, out temp) && temp > 0)
                                {
                                    AppendToAdd(result, temp, _data[i]);
                                }
                            }
                        }
                    }
                }
                return result;
            }
            finally
            {
                Monitor.Exit(lockobj);
            }
        }

        /// <summary>
        /// 从当前缓存中移除
        /// </summary>
        /// <param name="schedulerID"></param>
        public static void RemoveFromCache(int schedulerID, string attributes)
        {
            try
            {
                Monitor.Enter(lockobj);
                var datas = GetToRemindCache();
                foreach (var data in datas)
                {
                    if (data.ID == schedulerID)
                    {
                        NotifyWhenRemove(GetJoinEmpIDs(attributes), data.ID);
                        datas.Remove(data);
                    }
                }
            }
            finally
            {
                Monitor.Exit(lockobj);
            }
        }

        public static string GetJoinEmpIDs(string attributes)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(attributes);
            var str = "";
            foreach (XmlElement xNotify in xmlDocument.SelectNodes("Root/Notify[@Status=\"0\"]"))
            {
                if (str != "")
                {
                    str += ",";
                }
                str += xNotify.GetAttribute("EmpID");
            }
            return str;
        }

        private static void NotifyWhenRemove(string joinEmpIDs, int dataID)
        {
            foreach (var empID in joinEmpIDs.Split(','))
            {
                var temp = 0;
                if (int.TryParse(empID, out temp) && temp > 0)
                {
                    JQ.Util.IO.MessageMonitor.Notify(temp, new { action = "SchedulerRemind", type = "remove", data = dataID });
                }
            }
        }

        private static void NotifyWhenAdd(string joinEmpIDs, DataModel.Models.SchedulerBase data)
        {
            //直接通知
            foreach (var empID in joinEmpIDs.Split(','))
            {
                var temp = 0;
                if (int.TryParse(empID, out temp) && temp > 0)
                {
                    JQ.Util.IO.MessageMonitor.Notify(temp, new { action = "SchedulerRemind", type = "change", data = data });
                }
            }
        }
        /// <summary>
        /// 加入到Cache中
        /// </summary>
        /// <param name=""></param>

        public static void PushIntoCache(DataModel.Models.SchedulerBase scheduler, string attributes, int remindBefore)
        {
            //判断数据是否准确，验证是否有要加入到当前的cache中
            try
            {
                Monitor.Enter(lockobj);
                var datas = GetToRemindCache();
                if (scheduler.StartTime <= DateTime.Now || remindBefore == -1 || scheduler.NotifyTime > LastCacheTime)
                {
                    //不需要通知，如果存在，则需要移除掉
                    foreach (var data in datas)
                    {
                        if (data.ID == scheduler.ID)
                        {
                            NotifyWhenRemove(data.JoinEmpIDs, scheduler.ID);
                            datas.Remove(data);
                            break;
                        }
                    }
                }
                else if (scheduler.StartTime > DateTime.Now && scheduler.NotifyTime <= LastCacheTime)
                {
                    //移除原先老的
                    for (var i = 0; i < datas.Count; i++)
                    {
                        if (datas[i].ID == scheduler.ID)
                        {
                            datas.RemoveAt(i);
                        }
                    }
                    scheduler.JoinEmpIDs = GetJoinEmpIDs(attributes);
                    //验证是否发送通知
                    if (scheduler.NotifyTime <= DateTime.Now)
                    {
                        //发出通知
                        NotifyWhenAdd(GetJoinEmpIDs(attributes), scheduler);
                    }
                    else
                    {
                        NotifyWhenRemove(scheduler.JoinEmpIDs, scheduler.ID);
                    }
                    datas.Add(scheduler);
                }
            }
            finally
            {
                Monitor.Exit(lockobj);
            }

        }

        private static void AppendToRemove(List<SchedulerNotify> datas, int empID, int schedulerID)
        {
            var data = datas.FirstOrDefault(m => m.EmpID == empID);
            if (data == null)
            {
                data = new SchedulerNotify();
                data.EmpID = empID;
                datas.Add(data);
            }
            if (!data.ToRemoveData.Contains(schedulerID))
            {
                data.ToRemoveData.Add(schedulerID);
            }
        }

        private static void AppendToAdd(List<SchedulerNotify> datas, int empID, DataModel.Models.SchedulerBase scheduler)
        {
            var data = datas.FirstOrDefault(m => m.EmpID == empID);
            if (data == null)
            {
                data = new SchedulerNotify();
                data.EmpID = empID;
                datas.Add(data);
            }
            if (data.ToAddData.FirstOrDefault(m => m.ID == scheduler.ID) == null)
            {
                data.ToAddData.Add(scheduler);
            }
        }

        /// <summary>
        /// 获取出需要提醒的数据
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static List<dynamic> GetToRemindData(int empID, DateTime datetime)
        {
            try
            {
                Monitor.Enter(lockobj);
                var _data = GetToRemindCache();
                var result = new List<dynamic>();
                for (var i = 0; i < _data.Count; i++)
                {
                    if ((_data[i].IsFullDay || _data[i].NotifyTime <= datetime) && ("," + _data[i].JoinEmpIDs + ",").IndexOf("," + empID.ToString() + ",") > -1)
                    {
                        //result.Add(new { ID = _data[i].ID, StartTime = _data[i].StartTime.ToString("yyyy-MM-dd HH:mm"), EndTime = _data[i].EndTime.ToString("yyyy-MM-dd HH:mm"), CreatorEmpName = _data[i].CreatorEmpName, CreatorEmpId = _data[i].CreatorEmpId, IsFullDay = _data[i].IsFullDay, Content = _data[i].Content });
                        result.Add(_data[i]);
                    }
                }
                return result;
            }
            finally
            {
                Monitor.Exit(lockobj);
            }
        }

        /// <summary>
        /// 忽略
        /// </summary>
        public static void Ignore(int schedulerID, int empID)
        {
            try
            {
                Monitor.Enter(lockobj);
                var _data = GetToRemindCache();
                for (var i = 0; i < _data.Count; i++)
                {
                    if (_data[i].ID == schedulerID)
                    {
                        _data[i].JoinEmpIDs = ("," + _data[i].JoinEmpIDs + ",").Replace("," + empID.ToString() + ",", ",").Trim(',');
                        NotifyWhenRemove(empID.ToString(), schedulerID);
                        break;
                    }
                }
            }
            finally
            {
                Monitor.Exit(lockobj);
            }
        }

        /// <summary>
        /// 提醒
        /// </summary>
        private static void Remind(DateTime datetime)
        {
            //记录日志
            //JQ.Util.IOUtil.WriteLog("执行Scheduler.Remind：" + datetime.ToString("yyyy-MM-dd HH:mm:ss"));
            //从数据库中读取缓存的数据
            var toNotifyData = GetToRemindData(datetime);
            foreach (var data in toNotifyData)
            {
                //var str1 = "";
                //foreach (var remove in data.ToRemoveData)
                //{
                //    if (str1 != "")
                //    {
                //        str1 += ",";
                //    }
                //    str1 += remove.ToString();
                //}
                //var str2 = "";
                //foreach (var add in data.ToAddData)
                //{
                //    if (str2 != "")
                //    {
                //        str2 += ",";
                //    }
                //    str1 += add.ToString();
                //}
                //JQ.Util.IOUtil.WriteLog("发送通知：EmpID：" + data.EmpID + ";remove:" + str1 + ",;add:" + str2);
                JQ.Util.IO.MessageMonitor.Notify(data.EmpID, data);
            }
        }

        private class SchedulerNotify
        {
            public SchedulerNotify()
            {
                ToAddData = new List<DataModel.Models.SchedulerBase>();
                ToRemoveData = new List<int>();
                action = "SchedulerRemind";
                type = "timer";
            }

            public string action
            {
                get; set;
            }

            public string type
            {
                get; set;
            }

            public int EmpID
            {
                get; set;
            }

            public List<DataModel.Models.SchedulerBase> ToAddData
            {
                get; set;
            }

            public List<int> ToRemoveData
            {

                get; set;
            }
        }
    }
}
