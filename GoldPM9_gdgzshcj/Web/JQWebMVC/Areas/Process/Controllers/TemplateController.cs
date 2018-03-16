using JQ.BPM.Data.Extern;
using JQ.BPM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using AppUtility = JQ.BPM.Configuration.Utility;

namespace JQ.BPM.Web.Areas.Process.Controllers
{
    public class TemplateController : JQ.Web.CoreController
    {
        public ViewResult List()
        {
            return View();
        }

        public ViewResult Edit()
        {
            var id = AppUtility.ToInt(Request.QueryString["id"]);
            if (id > 0)
            {
                using (var dataAccessor = new Data.ProcessAccessor())
                {
                    var data = dataAccessor.ProcessTemplates.FirstOrDefault(m => m.ID == id);
                    if (data != null)
                    {
                        ViewBag.Name = data.Name;
                    }
                }
            }
            ViewBag.ID = id;
            return View();
        }

        public ViewResult Build()
        {
            ViewBag.ProcessTemplateID = AppUtility.ToInt(Request.QueryString["processTemplateID"]);
            return View();
        }

        public ViewResult Publish()
        {
            var processTemplateID = AppUtility.ToInt(Request.QueryString["processTemplateID"]);
            if (processTemplateID > 0)
            {
                using (var dataAccessor = new Data.ProcessAccessor())
                {
                    var data = dataAccessor.ProcessTemplates.FirstOrDefault(m => m.ID == processTemplateID);
                    if (data != null)
                    {
                        ViewBag.ProcessName = data.Name;
                        ViewBag.CurrentVersion = data.Version + 1;
                    }
                }
            }
            ViewBag.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.ProcessTemplateID = processTemplateID;
            return View();
        }

        public JsonResult GetList()
        {
            var pageIndex = AppUtility.ToInt(Request.Form["page"]) - 1;
            var pageSize = AppUtility.ToInt(Request.Form["rows"]);
            using (var dataAccessor = new Data.ProcessAccessor())
            {
                using (var dt = dataAccessor.Database.ExecuteDataTable("SELECT * FROM (SELECT ROW_Number() OVER(ORDER BY CreateTime DESC) as ROW_NUMBER,ID,Name,CreateTime,IsDelete,Version FROM [ProcessTemplate] WHERE IsDraft=1 AND IsDelete=0) AS tb WHERE tb.ROW_NUMBER BETWEEN " + pageIndex * pageSize + " AND " + (pageIndex + 1) * pageSize))
                {
                    var list = new List<Dictionary<string, object>>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Dictionary<string, object> result = new Dictionary<string, object>();
                        foreach (DataColumn dc in dt.Columns)
                        {
                            result.Add(dc.ColumnName, dr[dc].ToString());
                        }
                        list.Add(result);
                    }
                    return Json(new { total = dataAccessor.ProcessTemplates.Count(m => !m.IsDelete && m.IsDraft), rows = list });
                }
            }
        }

        public JsonResult Save()
        {
            var name = AppUtility.To<string>(Request.Form["name"]);
            if (string.IsNullOrEmpty(name))
            {
                return Json(new { Result = false, Message = "参数错误！" });
            }
            var id = AppUtility.ToInt(Request.QueryString["id"]);
            Model.ProcessTemplate processTemplate = null;
            using (var dataAccessor = new Data.ProcessAccessor())
            {
                var result = dataAccessor.Database.SqlQuery<int>("SELECT CASE WHEN EXISTS(SELECT 1 FROM ProcessTemplate WHERE Name=@name AND ID!=" + id + ") THEN 1 ELSE 0 END", new SqlParameter("@name", name)).FirstOrDefault();
                if (result == 1)
                {
                    return Json(new { Result = false, Message = "流程名称{" + name + "}已经存在！" });
                }
                if (id > 0)
                {
                    processTemplate = dataAccessor.ProcessTemplates.FirstOrDefault(m => m.ID == id);
                }
                else
                {
                    processTemplate = new Model.ProcessTemplate()
                    {
                        Content = "<process></process>",
                        CreateTime = DateTime.Now,
                        GroupID = Guid.NewGuid().ToString(),
                        IsCurrent = false,
                        IsDelete = false,
                        IsDraft = true,
                        ParentID = 0,
                        Version = 0
                    };
                    dataAccessor.ProcessTemplates.Add(processTemplate);
                }
                processTemplate.Name = name;
                dataAccessor.SaveChanges();
            }
            return Json(new { Result = true });
        }

        public JsonResult Delete()
        {
            var ids = Request.Form["ids"];
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            using (var dataAccessor = new JQ.BPM.Data.ProcessAccessor())
            {
                dataAccessor.Database.ExecuteSqlCommand("UPDATE ProcessTemplate SET IsDelete=1 WHERE GroupID IN (SELECT GroupID FROM ProcessTemplate WHERE ID IN (" + ids + "))");
            }
            return Json(new { Result = true });
        }

        public JsonResult Restore()
        {
            var ids = Request.Form["ids"];
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            using (var dataAccessor = new JQ.BPM.Data.ProcessAccessor())
            {
                dataAccessor.Database.ExecuteSqlCommand("UPDATE ProcessTemplate SET IsDelete=0 WHERE GroupID IN (SELECT GroupID FROM ProcessTemplate WHERE ID IN (" + ids + "))");
            }
            return Json(new { Result = true });
        }

        public JsonResult GetProcessTemplate()
        {
            var templateID = AppUtility.ToInt(Request.Form["id"]);
            if (templateID != 0)
            {
                using (var dataAccessor = new Data.ProcessAccessor())
                {
                    var data = dataAccessor.ProcessTemplates.FirstOrDefault(m => m.ID == templateID);
                    if (data != null)
                    {
                        return Json(new { xml = data.Content, name = data.Name });
                    }
                }
            }
            return Json(new object { });
        }

        public string GetProcess()
        {
            var id = AppUtility.ToInt(Request.Form["id"]);
            if (id > 0)
            {
                using (var dataAccessor = new Data.ProcessAccessor())
                {
                    using (var dt = dataAccessor.Database.ExecuteDataTable("SELECT pt.Content,p.Name FROM Process p LEFT JOIN ProcessTemplate pt ON pt.ID=p.TemplateID WHERE p.ID=" + id))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            //获取出颜色
                            using (var dtActivities = dataAccessor.Database.ExecuteDataTable("SELECT ID,Name,[Status],Symbol,Setting,'' AS Color,CONVERT(NVARCHAR(16),StartTime,20) AS StartTime,CONVERT(NVARCHAR(16),EndTime,20) AS EndTime,Actor,ActorName FROM Activity WHERE ProcessID=" + id))
                            {
                                dtActivities.Columns.Add("ActorsInfo");
                                foreach (DataRow row in dtActivities.Rows)
                                {
                                    XmlDocument xmlDocuemnt = new XmlDocument();
                                    xmlDocuemnt.LoadXml(row["Setting"].ToString());
                                    row["color"] = GetColor((ActivityStatus)row.Field<int>("Status"), xmlDocuemnt.DocumentElement.GetAttribute("isBack") == "1");
                                    XmlDocument actorDocument = new XmlDocument();
                                    actorDocument.LoadXml("<Root></Root>");
                                    switch (row.Field<int>("Status"))
                                    {
                                        case (int)ActivityStatus.Activated:
                                            //从候选池中获取出人
                                            foreach (XmlElement xmlElement in xmlDocuemnt.SelectNodes("Root/ActorPool/Actor[@mode='user']"))
                                            {
                                                var newE = actorDocument.CreateElement("Actor");
                                                newE.SetAttribute("name", xmlElement.GetAttribute("name"));
                                                newE.SetAttribute("isFinished", "0");
                                                newE.InnerText = xmlElement.InnerText;
                                                actorDocument.DocumentElement.AppendChild(newE);
                                            }
                                            if (row["Symbol"].ToString() == "signactivity")
                                            {
                                                //是否有已经完成的人员
                                                foreach (XmlElement xmlElement in xmlDocuemnt.SelectNodes("Root/Record/Sign"))
                                                {
                                                    var newE = actorDocument.CreateElement("Actor");
                                                    newE.SetAttribute("name", xmlElement.GetAttribute("name"));
                                                    newE.SetAttribute("time", DateTime.Parse(xmlElement.GetAttribute("time")).ToString("yyyy-MM-dd HH:mm"));
                                                    newE.SetAttribute("isFinished", "1");
                                                    newE.InnerText = xmlElement.GetAttribute("actor");
                                                    actorDocument.DocumentElement.AppendChild(newE);
                                                }
                                            }
                                            break;
                                        case (int)ActivityStatus.Finished:
                                            if (row["Symbol"].ToString() == "activity")
                                            {
                                                var newE = actorDocument.CreateElement("Actor");
                                                newE.SetAttribute("name", row["ActorName"].ToString());
                                                newE.SetAttribute("isFinished", "1");
                                                newE.InnerText = row["Actor"].ToString();
                                                actorDocument.DocumentElement.AppendChild(newE);
                                            }
                                            else if (row["Symbol"].ToString() == "signactivity")
                                            {
                                                foreach (XmlElement xmlElement in xmlDocuemnt.SelectNodes("Root/Record/Sign"))
                                                {
                                                    var newE = actorDocument.CreateElement("Actor");
                                                    newE.SetAttribute("name", xmlElement.GetAttribute("name"));
                                                    newE.SetAttribute("time", DateTime.Parse(xmlElement.GetAttribute("time")).ToString("yyyy-MM-dd HH:mm"));
                                                    newE.SetAttribute("isFinished", "1");
                                                    newE.InnerText = xmlElement.GetAttribute("actor");
                                                    actorDocument.DocumentElement.AppendChild(newE);
                                                }
                                            }
                                            break;
                                        case (int)ActivityStatus.UnActivated:
                                            break;
                                    }
                                    row["ActorsInfo"] = actorDocument.OuterXml;
                                }
                                return AppUtility.ObjectToJson(new { xml = dt.Rows[0]["Content"].ToString(), name = dt.Rows[0]["Name"].ToString(), steps = dtActivities });
                            }
                        }
                    }
                }
            }
            return "{}";
        }

        private string GetColor(ActivityStatus status, bool isBack)
        {
            switch (status)
            {
                case ActivityStatus.UnActivated:
                    return "#EBEBEB";
                case ActivityStatus.Activated:
                    return "#F6FA0F";
                case ActivityStatus.Finished:
                    if (isBack)
                    {
                        return "#FF9548";
                    }
                    else
                    {
                        return "#8ED2FF";
                    }
                case ActivityStatus.Ignored:
                case ActivityStatus.Skiped:
                case ActivityStatus.Suppended:
                default:
                    return "#000000";
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public JsonResult SaveChart()
        {
            var templateID = AppUtility.ToInt(Request.Form["id"]);
            var xml = Request.Form["data"];
            if (templateID == 0 || string.IsNullOrEmpty(xml))
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            using (var dataAccessor = new Data.ProcessAccessor())
            {
                var template = dataAccessor.ProcessTemplates.FirstOrDefault(m => m.ID == templateID);
                if (template == null)
                {
                    return Json(new { Result = false, Message = "无法找到需要保存的实例" });
                }
                template.Content = xml;
                dataAccessor.SaveChanges();
            }
            return Json(new { Result = true });
        }

        [ValidateInput(false)]
        public JsonResult SaveChartWithPublish()
        {
            var templateID = AppUtility.ToInt(Request.Form["id"]);
            var xml = Request.Form["data"];
            if (templateID == 0 || string.IsNullOrEmpty(xml))
            {
                return Json(new { Result = false, Message = "参数错误" });
            }
            using (var dataAccessor = new Data.ProcessAccessor())
            {
                var template = dataAccessor.ProcessTemplates.FirstOrDefault(m => m.ID == templateID);
                if (template == null)
                {
                    return Json(new { Result = false, Message = "无法找到需要保存的实例" });
                }
                template.Content = xml;
                //发布新的版本
                var processTemplate = new Model.ProcessTemplate()
                {
                    Content = template.Content,
                    CreateTime = DateTime.Now,
                    GroupID = template.GroupID,
                    Version = template.Version + 1,
                    IsDelete = false,
                    IsDraft = false,
                    IsCurrent = Request.Form["isCurrentVersion"] == "1",
                    Name = template.Name,
                    ParentID = template.ID
                };
                if (processTemplate.IsCurrent)
                {
                    var currentVersion = dataAccessor.ProcessTemplates.FirstOrDefault(m => m.GroupID == template.GroupID && m.IsCurrent);
                    if (currentVersion != null)
                    {
                        currentVersion.IsCurrent = false;
                    }
                }
                dataAccessor.ProcessTemplates.Add(processTemplate);
                template.Version = processTemplate.Version;
                dataAccessor.SaveChanges();
            }
            return Json(new { Result = true });
        }

        /// <summary>
        /// 获取出所有的版本
        /// </summary>
        /// <returns></returns>
        public string GetAllVersion()
        {
            var templateID = AppUtility.ToInt(Request.Form["templateID"]);
            if (templateID == 0)
            {
                return "[]";
            }
            using (var dataAccessor = new Data.ProcessAccessor())
            {
                using (var dt = dataAccessor.Database.ExecuteDataTable("SELECT ID,Name,[Version],IsCurrent FROM ProcessTemplate WHERE IsDelete=0 AND ParentID=" + templateID + " ORDER BY IsCurrent DESC,[Version]"))
                {
                    return AppUtility.ObjectToJson(dt);
                }
            }
        }


        public JsonResult GetAllActivities()
        {
            var templateVersionID = AppUtility.ToInt(Request.Form["versionID"]);
            string content = null;
            if (templateVersionID == 0)
            {
                var templateID = AppUtility.ToInt(Request.Form["id"]);
                if (templateID == 0)
                {
                    return Json(new object[] { });
                }
                using (var dataAccessor = new Data.ProcessAccessor())
                {
                    content = dataAccessor.Database.SqlQuery<string>("SELECT Content FROM ProcessTemplate WHERE ParentID=" + templateID + " AND IsCurrent=1").FirstOrDefault();
                }
            }
            else
            {
                using (var dataAccessor = new Data.ProcessAccessor())
                {
                    content = dataAccessor.Database.SqlQuery<string>("SELECT Content FROM ProcessTemplate WHERE ID=" + templateVersionID).FirstOrDefault();
                }
            }
            if (string.IsNullOrEmpty(content))
            {
                return Json(new object[] { });
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);
            var items = new List<Model.SelectItem>();
            foreach (XmlElement xmlElement in xmlDocument.SelectNodes("process/activity|process/signactivity"))
            {
                var item = new Model.SelectItem();
                item.Value = xmlElement.GetAttribute("name");
                item.Text = xmlElement.GetAttribute("displayname");
                items.Add(item);
            }
            return Json(items);
        }

        public string GetActionLog()
        {
            var processID = AppUtility.ToInt(Request.Form["processID"]);
            if (processID == 0)
            {
                return AppUtility.ObjectToJson(new { total = 0, rows = new object[] { } });
            }
            Data.QueryContext queryContext = new Data.QueryContext()
            {
                PageIndex = AppUtility.ToInt(Request.Form["page"]) - 1,
                PageSize = AppUtility.ToInt(Request.Form["rows"])
            };
            queryContext.Columns = "*";
            queryContext.Tables = "ActivityActionLog";
            queryContext.Conditions = "ProcessID=" + processID;
            queryContext.Order = "Time";
            using (var dataAccessor = new Data.ProcessAccessor())
            {
                using (var dt = dataAccessor.Database.ExecuteDataTable(queryContext.GetQuerySqlString()))
                {
                    dt.Columns.Add("ActionName");
                    dt.Columns.Add("TimeText");
                    foreach (DataRow dr in dt.Rows)
                    {
                        //获取出数据
                        dr["ActionName"] = Core.Behavior.SubmitAction.GetText(dr.Field<int>("Action"));
                        dr["TimeText"] = dr.Field<DateTime>("Time").ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    dt.Columns.Remove("Action");
                    dt.Columns.Remove("Time");
                    return AppUtility.ObjectToJson(new { total = dataAccessor.Database.SqlQuery<int>(queryContext.GetCalculateSqlString()).FirstOrDefault(), rows = dt });
                }
            }
        }
    }
}