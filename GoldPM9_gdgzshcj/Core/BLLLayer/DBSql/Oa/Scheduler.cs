using DBSql.PubFlow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml;

namespace DBSql.OA
{
    public class Scheduler : EFRepository<DataModel.Models.Scheduler>, IDisposable
    {

        public void CreateOrUpdate(DataModel.Models.Scheduler scheduler)
        {
            if (scheduler.IsFullDay)
            {
                if (scheduler.RemindBefore > 0 && scheduler.RemindBeforeType != 3)
                {
                    //当为全天时强制按照天来提醒
                    scheduler.RemindBeforeType = 3;
                }
            }
            XmlDocument xmlDocument = new XmlDocument();
            if (string.IsNullOrEmpty(scheduler.Attributes))
            {
                xmlDocument.LoadXml("<Root></Root>");
            }
            else
            {
                xmlDocument.LoadXml(scheduler.Attributes);
            }
            var isChangeStartTime = false;
            if (scheduler.ID > 0 && (DateTime)this.DbContext.Entry(scheduler).Property("StartTime").OriginalValue != scheduler.StartTime)
            {
                isChangeStartTime = true;
            }
            AddToNotifyAttributes(xmlDocument, scheduler.CreatorEmpId, isChangeStartTime);
            var str = "";
            foreach (var empID in scheduler.JoinEmpIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var temp = 0;
                if (int.TryParse(empID, out temp) && temp > 0)
                {
                    if (str != "")
                    {
                        str += ",";
                    }
                    str += temp;
                    AddToNotifyAttributes(xmlDocument, temp, isChangeStartTime);
                }
            }
            scheduler.JoinEmpIDs = str;
            scheduler.Attributes = xmlDocument.OuterXml;
            scheduler.NotifyTime = GetNotifyTime(scheduler.RemindBefore, scheduler.RemindBeforeType, scheduler.StartTime, scheduler.IsFullDay);
            //验证是否变更过开始时间
            if (scheduler.ID == 0)
            {
                this.DbContext.Set<DataModel.Models.Scheduler>().Add(scheduler);
            }
            this.DbContext.SaveChanges();
            this.DbContext.Entry(scheduler).State = System.Data.Entity.EntityState.Detached;
            SchedulerRemind.PushIntoCache(scheduler, scheduler.Attributes, scheduler.RemindBefore);
        }

        private void AddToNotifyAttributes(XmlDocument xmlDocument, int empID, bool isChangeStartTime)
        {
            if (empID == 0)
            {
                return;
            }
            var node = (XmlElement)xmlDocument.SelectSingleNode("Root/Notify[@EmpID=\"" + empID + "\"]");
            if (node == null)
            {
                node = xmlDocument.CreateElement("Notify");
                node.SetAttribute("EmpID", empID.ToString());
                node.SetAttribute("DateTime", "");
                node.SetAttribute("Status", "0");
                xmlDocument.DocumentElement.AppendChild(node);
            }
            else if (isChangeStartTime)
            {
                node.SetAttribute("DateTime", "");
                node.SetAttribute("Status", "0");
            }
        }


        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            var schedulerDB = this.DbContext.Set<DataModel.Models.Scheduler>();
            var scheduler = schedulerDB.FirstOrDefault(m => m.ID == id);
            if (scheduler != null)
            {
                schedulerDB.Remove(scheduler);
                this.DbContext.SaveChanges();
                SchedulerRemind.RemoveFromCache(scheduler.ID, scheduler.Attributes);
            }
        }

        public List<dynamic> GetList(int empID, DateTime startTime, DateTime endTime)
        {
            var datas = this.GetQuery().Where(m => m.CreatorEmpId == empID && ((startTime <= m.StartTime && endTime >= m.StartTime) || (startTime <= m.EndTime && endTime >= m.EndTime))).Select(m => new { id = m.ID, start_date = m.StartTime, end_date = m.EndTime, text = m.Content, isFullDay = m.IsFullDay }).ToList<dynamic>();
            var result = new List<dynamic>();
            foreach (var data in datas)
            {
                result.Add(new { id = data.id, start_date = data.start_date.ToString("yyyy-MM-dd HH:mm"), end_date = (data.isFullDay ? data.end_date.AddDays(1) : data.end_date).ToString("yyyy-MM-dd HH:mm"), text = data.text });
            }
            return result;
        }

        public List<DataModel.Models.SchedulerBase> GetToRemindList(DateTime preDateTime)
        {
            var sbSQL = new StringBuilder();
            sbSQL.Append("SELECT ID,Content,StartTime,CreatorEmpId,Attributes,IsFullDay,NotifyTime,CreatorEmpName,EndTime");
            sbSQL.Append(" FROM Scheduler WHERE RemindBefore>-1 AND StartTime>=GETDATE() AND Attributes.exist('Root/Notify[@Status=\"0\"]')=1");
            sbSQL.Append(" AND ((IsFullDay=1 AND NotifyTime<=CONVERT(NVARCHAR(10),@datetime,23)) OR (IsFullDay=0 AND NotifyTime<=@datetime))");
            var result = new List<DataModel.Models.SchedulerBase>();
            using (var dt = DAL.DBExecute.ExecuteDataTable(sbSQL.ToString(), new System.Data.SqlClient.SqlParameter("@datetime", preDateTime)))
            {
                foreach (DataRow row in dt.Rows)
                {
                    result.Add(new DataModel.Models.SchedulerBase()
                    {
                        ID = row.Field<int>("ID"),
                        Content = row["Content"].ToString(),
                        StartTime = row.Field<DateTime>("StartTime"),
                        IsFullDay = row.Field<bool>("IsFullDay"),
                        NotifyTime = row.Field<DateTime>("NotifyTime"),
                        CreatorEmpId = row.Field<int>("CreatorEmpId"),
                        CreatorEmpName = row["CreatorEmpName"].ToString(),
                        EndTime = row.Field<DateTime>("EndTime"),
                        JoinEmpIDs = SchedulerRemind.GetJoinEmpIDs(row["Attributes"].ToString())
                    });
                }
            }
            return result;
        }

        private bool _isDisposed;
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

        private DateTime GetNotifyTime(int remindBefore, int remindBeforType, DateTime startTime, bool isFullDay)
        {
            if (remindBefore < 0)
            {
                return JQ.Util.TypeParse.DefaultDateTime;
            }
            switch (remindBeforType)
            {
                case 1:
                    //分:
                    return startTime.AddMinutes(-remindBefore);
                case 2:
                    //时
                    return startTime.AddHours(-remindBefore);
                case 3:
                    //天
                    return startTime.AddDays(-remindBefore);
                default:
                    return JQ.Util.TypeParse.DefaultDateTime;
            }
        }

        public DataTable GetList(Common.SqlPageInfo queryContext)
        {
            var sbSQL = new StringBuilder(" FROM Scheduler s LEFT JOIN BaseEmployee be on be.EmpID=s.CreatorEmpId LEFT JOIN BaseData bd on bd.BaseID=be.EmpDepID");
            var sbCondition = new StringBuilder(" WHERE 1=1");
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(queryContext.TextCondtion))
            {
                sbCondition.Append(" AND (s.Content like @text OR be.EmpName like @text)");
                sqlParameters.Add(new SqlParameter("@text", "%" + queryContext.TextCondtion + "%"));
            }
            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString().Trim() == "")
                    {
                        continue;
                    }
                    switch (de.Key.ToString())
                    {
                        case "EmpID":
                            sbCondition.Append(" AND s.CreatorEmpID=" + de.Value.ToString());
                            break;
                        case "DepartmentID":
                            sbCondition.Append(" AND be.EmpDepID=" + de.Value.ToString());
                            break;
                    }
                }
            }
            if (queryContext.SelectCondtion["StartDate"] != null && queryContext.SelectCondtion["EndDate"] != null)
            {
                sbCondition.Append(" AND (s.StartTime BETWEEN @StartDate AND @EndDate OR s.EndTime BETWEEN @StartDate AND @EndDate)");
                sqlParameters.Add(new SqlParameter("@StartDate", queryContext.SelectCondtion["StartDate"]));
                sqlParameters.Add(new SqlParameter("@EndDate", queryContext.SelectCondtion["EndDate"]));
            }
            else if (queryContext.SelectCondtion["StartDate"] != null)
            {
                sbCondition.Append(" AND (s.StartTime>=@StartDate OR s.EndTime>=@StartDate)");
                sqlParameters.Add(new SqlParameter("@StartDate", queryContext.SelectCondtion["StartDate"]));
            }
            else if (queryContext.SelectCondtion["EndDate"] != null)
            {
                sbCondition.Append(" AND (s.EndTime<=@EndDate OR s.EndTime<=@EndDate)");
                sqlParameters.Add(new SqlParameter("@EndDate", queryContext.SelectCondtion["EndDate"]));
            }
            if (string.IsNullOrEmpty(queryContext.SelectOrder))
            {
                queryContext.SelectOrder = "bd.BaseOrder,be.EmpOrder,s.StartTime DESC,s.EndTime DESC";
            }
            queryContext.PageTotleRowCount = Common.ModelConvert.ConvertToDefault<int>(DAL.DBExecute.ExecuteScalar("SELECT Count(1)" + sbSQL.ToString() + sbCondition.ToString(), sqlParameters.ToArray()));
            return DAL.DBExecute.ExecuteDataTable("SELECT * FROM (SELECT ID,Content,StartTime,EndTime,be.EmpName,bd.BaseName AS DepartmentName,IsFullDay,(CASE JoinEmpIDs WHEN '' THEN '' ELSE (SELECT EmpName+',' FROM BaseEmployee WHERE CHARINDEX(','+CAST(EmpID AS NVARCHAR)+',',','+JoinEmpIDs+',')>0 FOR XML PATH('')) END) AS JoinEmpNames,row_number() OVER (ORDER BY " + queryContext.SelectOrder + ") AS row_number" + sbSQL.ToString() + sbCondition.ToString() + ") AS tb WHERE tb.row_number BETWEEN " + (((queryContext.PageIndex - 1) * queryContext.PageSize) + 1) + " AND " + (queryContext.PageIndex * queryContext.PageSize), sqlParameters.ToArray());
        }

        /// <summary>
        /// 忽略提醒
        /// </summary>
        /// <param name="id">日程的ID</param>
        /// <param name="empID">用户ID</param>
        public void IgnoreRemind(int id, int empID)
        {
            DAL.DBExecute.ExecuteNonQuery("UPDATE Scheduler SET Attributes.modify('replace value of (Root/Notify[@EmpID=\""+ empID + "\"]/@Status)[1] with \"1\"') WHERE ID=" + id);
            SchedulerRemind.Ignore(id, empID);
        }
    }
}
