using JQ.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OA.Controllers
{
    public class MessageController : CoreController
    {
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Display(int id)
        { 
            var oaMess = new DBSql.OA.OaMess();
            var message = oaMess.Get(id);
            if (message != null)
            {
                if (oaMess.UpdateReaded(message.FK_OaMessRead_Id.FirstOrDefault(m => m.MessReadEmpId == userInfo.EmpID)))
                {
                    var t = JQ.Util.IO.MessageMonitor.NotifyAsync(userInfo.EmpID, delegate (int empID) { return new DBSql.Oa.OaMessRead().GetNotifyDatas(userInfo.EmpID); });
                }
            }
            return View(message);
        }

        public JsonResult GetList()
        {
            Common.SqlPageInfo quertyContext = new Common.SqlPageInfo();
            if (string.IsNullOrEmpty(quertyContext.SelectOrder))
            {
                quertyContext.SelectOrder = "MessDate desc";
            }
            if (!string.IsNullOrEmpty(Request.Form["status"]))
            {
                quertyContext.SelectCondtion.Add("Status", Request.Form["status"]);
            }
            quertyContext.SelectCondtion.Add("MessReadEmpId", userInfo.EmpID);
            DateTime datetime;
            if (!string.IsNullOrEmpty(Request.Form["starttime"]) && DateTime.TryParse(Request.Form["starttime"], out datetime))
            {
                quertyContext.SelectCondtion.Add("StartTime", DateTime.Parse(datetime.ToString("yyyy-MM-dd") + " 00:00:00"));
            }
            if (!string.IsNullOrEmpty(Request.Form["endtime"]) && DateTime.TryParse(Request.Form["endtime"], out datetime))
            {
                quertyContext.SelectCondtion.Add("EndTime", DateTime.Parse(datetime.ToString("yyyy-MM-dd") + " 23:59:59"));
            }
            var list = new DBSql.OA.OaMess().GetList(quertyContext).ToList();
            return Json(new { total = quertyContext.PageTotleRowCount, rows = list });
        }


        public JsonResult SetReadStatus()
        {
            var status = Common.ModelConvert.ConvertToDefault<int>(Request.Form["status"]);
            if (status == 0)
            {
                return Json(new { Result = false });
            }
            var idSet = Request.Form["idSet"] ?? "";
            if (string.IsNullOrEmpty(idSet))
            {
                return Json(new { Result = false });
            }
            if (status == 1)
            {
                //设置为已读
                new DBSql.OA.OaMess().SetMessageReaded(idSet.Trim(','), userInfo.EmpID);

            }
            else if (status == 2)
            {
                //设置为未读
                new DBSql.OA.OaMess().SetMessageUnReaded(idSet.Trim(','), userInfo.EmpID);
            }
            var t = JQ.Util.IO.MessageMonitor.NotifyAsync(userInfo.EmpID, delegate (int empID) { return new DBSql.Oa.OaMessRead().GetNotifyDatas(userInfo.EmpID); });
            return Json(new { Result = true });
        }

        /// <summary>
        /// 获取出消息数量
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMessages()
        {
            //获取出消息的数量
            return Json(new DBSql.Oa.OaMessRead().GetToDisplayDatas(userInfo.EmpID));
        }

        public JsonResult DeleteRead()
        {
            var idSet = Request.Form["idSet"] ?? "";
            if (string.IsNullOrEmpty(idSet))
            {
                return Json(new { Result = false });
            }
            new DBSql.OA.OaMess().DeleteRead(idSet.Trim(','), userInfo.EmpID);
            return Json(new { Result = true });
        }
    }
}
