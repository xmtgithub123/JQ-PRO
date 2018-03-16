using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OA.Controllers
{
    public class SchedulerController : CoreController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            var id = JQ.Util.TypeParse.parse<int>(Request.QueryString["id"]);
            DataModel.Models.Scheduler data = null;
            if (id > 0)
            {
                using (var schedulerDB = new DBSql.OA.Scheduler())
                {
                    data = schedulerDB.Get(id);
                }
                if (!string.IsNullOrEmpty(data.JoinEmpIDs))
                {
                    ViewBag.JoinEmpNames = new DBSql.Sys.BaseEmployee().GetEmpName(data.JoinEmpIDs);
                }
            }
            else
            {
                data = new DataModel.Models.Scheduler();
                data.RemindBefore = 5;
                data.RemindBeforeType = 1;
                if (!string.IsNullOrEmpty(Request.QueryString["startTime"]))
                {
                    data.StartTime = JQ.Util.TypeParse.parse<DateTime>(Request.QueryString["startTime"]);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["endTime"]))
                {
                    data.EndTime = JQ.Util.TypeParse.parse<DateTime>(Request.QueryString["endTime"]);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["content"]))
                {
                    data.Content = HttpUtility.HtmlDecode(Request.QueryString["content"]);
                }
            }
            return View(data);
        }

        public ActionResult List()
        {
            var permissions = (PermissionList("OaScheduler"));
            if (permissions.Contains("allview"))
            {
                ViewBag.Permission = "allview";
            }
            else if (permissions.Contains("dep"))
            {
                ViewBag.Permission = "dep";
            }
            else
            {
                //默认
                ViewBag.Permission = "emp";
            }
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.EmpDepID = userInfo.EmpDepID;
            return View();
        }

        public ActionResult ViewDetail()
        {
            using (var schedulerDB = new DBSql.OA.Scheduler())
            {
                var data = schedulerDB.Get(JQ.Util.TypeParse.parse<int>(Request.QueryString["id"]));
                if (!string.IsNullOrEmpty(data.JoinEmpIDs))
                {
                    ViewBag.JoinEmpNames = new DBSql.Sys.BaseEmployee().GetEmpName(data.JoinEmpIDs);
                }
                if (data.RemindBefore < 0)
                {
                    ViewBag.Remind = "无";
                }
                else
                {
                    if (data.RemindBeforeType == 1)
                    {
                        ViewBag.Remind = "提前" + data.RemindBefore + "分钟";
                    }
                    else if (data.RemindBeforeType == 2)
                    {
                        ViewBag.Remind = "提前" + data.RemindBefore + "小时";
                    }
                    else if (data.RemindBeforeType == 3)
                    {
                        ViewBag.Remind = "提前" + data.RemindBefore + "天";
                    }
                }
                return View(data);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public JsonResult Save()
        {
            try
            {
                var id = JQ.Util.TypeParse.parse<int>(Request.Form["id"]);
                DataModel.Models.Scheduler scheduler = null;
                var isFullDay = Request.Form["cbIsFullDay"] == "on";
                DateTime startTime, endTime;
                if (isFullDay)
                {
                    startTime = JQ.Util.TypeParse.parse<DateTime>(Request.Form["txtStartDate"]);
                    endTime = JQ.Util.TypeParse.parse<DateTime>(Request.Form["txtEndDate"]);
                }
                else
                {
                    startTime = JQ.Util.TypeParse.parse<DateTime>(Request.Form["txtStartDate"] + " " + Request.Form["txtStartMinute"] + ":" + Request.Form["txtStartSecond"]);
                    endTime = JQ.Util.TypeParse.parse<DateTime>(Request.Form["txtEndDate"] + " " + Request.Form["txtEndMinute"] + ":" + Request.Form["txtEndSecond"]);
                }
                if ((isFullDay && endTime < startTime) || (!isFullDay && endTime <= startTime))
                {
                    throw new Common.JQException("开始时间不得小于结束时间");
                }
                using (var schedulerDB = new DBSql.OA.Scheduler())
                {
                    if (id > 0)
                    {
                        scheduler = schedulerDB.Get(id);
                    }
                    else
                    {
                        scheduler = new DataModel.Models.Scheduler();
                        scheduler.CreationTime = DateTime.Now;
                        scheduler.CreatorEmpId = userInfo.EmpID;
                        scheduler.CreatorEmpName = userInfo.EmpName;
                        scheduler.RefID = 0;
                        scheduler.RefTable = "";
                    }
                    scheduler.Content = Request.Form["Content"] ?? "";
                    scheduler.IsFullDay = isFullDay;
                    scheduler.StartTime = startTime;
                    scheduler.EndTime = endTime;
                    if (Request.Form["cbIsRemind"] == "on")
                    {
                        //需要提醒
                        scheduler.RemindBefore = JQ.Util.TypeParse.parse<int>(Request.Form["txtRemindBefore"]);
                        scheduler.RemindBeforeType = JQ.Util.TypeParse.parse<int>(Request.Form["txtRemindBeforeType"]);
                    }
                    else
                    {
                        scheduler.RemindBefore = -1;
                        scheduler.RemindBeforeType = 0;
                    }
                    scheduler.JoinEmpIDs = Request.Form["joinEmpIDs"].Trim(',');
                    schedulerDB.CreateOrUpdate(scheduler);
                }
                return Json(DoResultHelper.doSuccess(scheduler, "操作成功"));
            }
            catch (Common.JQException jqe)
            {
                return Json(DoResultHelper.doSuccess(jqe.Message, "操作失败"));
            }
            catch
            {
                return Json(DoResultHelper.doError("操作失败"));
            }
        }

        public JsonResult SaveData()
        {
            var id = JQ.Util.TypeParse.parse<int>(Request.Form["id"]);
            if (id == 0)
            {
                return Json(new { Result = false, Message = "无法找到参数" });
            }
            using (var schedulerDB = new DBSql.OA.Scheduler())
            {
                var scheduler = schedulerDB.Get(id);
                if (scheduler != null)
                {
                    scheduler.StartTime = JQ.Util.TypeParse.parse<DateTime>(Request.Form["startTime"]);
                    scheduler.EndTime = JQ.Util.TypeParse.parse<DateTime>(Request.Form["endTime"]);
                    scheduler.Content = Request.Form["Content"] ?? "";
                    schedulerDB.CreateOrUpdate(scheduler);
                }
            }
            return Json(new { Result = true, Message = "无法找到参数" });
        }

        public JsonResult DeleteData()
        {
            var id = JQ.Util.TypeParse.parse<int>(Request.Form["id"]);
            if (id == 0)
            {
                return Json(new { Result = false, Message = "无法找到参数" });
            }
            using (var schedulerDB = new DBSql.OA.Scheduler())
            {
                schedulerDB.Delete(id);
            }
            return Json(new { Result = true });
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetData()
        {
            DateTime startTime;
            if (!DateTime.TryParse(Request.QueryString["startTime"], out startTime))
            {
                return Json("[]");
            }
            DateTime endTime;
            if (!DateTime.TryParse(Request.QueryString["endTime"], out endTime))
            {
                return Json("[]");
            }
            return Json(new DBSql.OA.Scheduler().GetList(userInfo.EmpID, startTime, endTime), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRemindSchedulers()
        {
            return Json(DBSql.OA.SchedulerRemind.GetToRemindData(userInfo.EmpID, DateTime.Now));
        }

        public string GetList()
        {
            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            queryContext.TextCondtion = (Request.Form["text"] ?? "").Trim();
            if (!string.IsNullOrEmpty(Request.Form["startdate"]))
            {
                DateTime startDate;
                if (DateTime.TryParse(Request.Form["startdate"], out startDate))
                {
                    queryContext.SelectCondtion.Add("StartDate", DateTime.Parse(startDate.ToString("yyyy-MM-dd") + " 00:00:00"));
                }
            }
            if (!string.IsNullOrEmpty(Request.Form["enddate"]))
            {
                DateTime endDate;
                if (DateTime.TryParse(Request.Form["enddate"], out endDate))
                {
                    queryContext.SelectCondtion.Add("EndDate", DateTime.Parse(endDate.ToString("yyyy-MM-dd") + " 23:59:59"));
                }
            }
            if (Request.Form["empID"] != null)
            {
                queryContext.SelectCondtion.Add("EmpID", JQ.Util.TypeParse.parse<int>(Request.Form["empID"]));
            }
            if (Request.Form["departmentID"] != null)
            {
                queryContext.SelectCondtion.Add("DepartmentID", JQ.Util.TypeParse.parse<int>(Request.Form["departmentID"]));
            }
            using (var dt = new DBSql.OA.Scheduler().GetList(queryContext))
            {
                return JQ.Util.JavaScriptSerializerUtil.getJson(new { total = queryContext.PageTotleRowCount, rows = dt });
            }
        }

        public JsonResult IgnoreRemind()
        {
            var id = JQ.Util.TypeParse.parse<int>(Request.Form["id"]);
            if (id == 0)
            {
                return Json(new { Result = false, Meesage = "参数错误" });
            }
            //更新至数据库，更新缓存
            new DBSql.OA.Scheduler().IgnoreRemind(id, userInfo.EmpID);
            return Json(new { Result = true });
        }
    }
}
