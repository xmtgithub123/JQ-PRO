using DBSql.Design.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Core.Controllers
{
    public class NotifyController : Controller
    {
        public ActionResult Index()
        {
            var empID = JQ.Util.TypeParse.parse<int>(Request.QueryString["empID"]);
            if (empID == 0)
            {
                ViewBag.Result = "0";
                return View("Content");
            }
            else
            {
                var isTestMode = Request.QueryString["GetC"] != null;
                var result = LoadNotify(empID, isTestMode, JQ.Util.TypeParse.parse<int>(Request.QueryString["agentTypeID"]));
                if (isTestMode)
                {
                    ViewBag.Result = result;
                    return View("Content");
                }
                return View();
            }
        }

        private string LoadNotify(int empID, bool isTestMode, int agentTypeID)
        {
            string agentFlows = Request.QueryString["agentFlows"];
            ViewBag.Loaded = true;
            var amount = 0;
            var flow = new DBSql.PubFlow.Flow();
            //项目表单
            amount = flow.GetToDoAmount(empID, 1, agentTypeID, agentFlows);
            if (isTestMode && amount > 0)
            {
                return "1";
            }
            ViewBag.ProjectFormAmount = amount;
            //办公表单
            amount = flow.GetToDoAmount(empID, 2, agentTypeID, agentFlows);
            if (isTestMode && amount > 0)
            {
                return "1";
            }
            ViewBag.OAFormAmount = amount;
            //提资数量
            var eschRec = new DBSql.Design.DesExchRec();
            amount = eschRec.GetRecCountByEmpID(empID, agentTypeID, agentFlows);
            if (isTestMode && amount > 0)
            {
                return "1";
            }
            ViewBag.ExchangeAmount = amount;
            //校审任务数量
            string DesTaskApproveMode = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("DesTaskApproveMode").ConfigValue; // 任务校审模式   单步 0；同步 1
            amount = new DBSql.Design.DesTask().GetTaskRemindTodoAmount(
                empID,
                DesTaskApproveMode == "0" ?
                    String.Format("{0}", DBSql.Design.FlowNodeStatus.进行中.ToString("D"))
                    :
                    String.Format("{0},{1}", DBSql.Design.FlowNodeStatus.已安排.ToString("D"), DBSql.Design.FlowNodeStatus.进行中.ToString("D")),
                new DesTaskRemindPermission(empID)
            );
            if (isTestMode && amount > 0)
            {
                return "1";
            }
            ViewBag.TaskAmount = amount;
            if (agentTypeID == 0)
            {
                //消息
                amount = new DBSql.Oa.OaMessRead().GetUnReadMessage(empID);
                if (isTestMode && amount > 0)
                {
                    return "1";
                }
                ViewBag.MessageAmount = amount;
                //未读邮件
                amount = new DBSql.Oa.OaMailRead().GetUnreadMail(empID);
                if (isTestMode && amount > 0)
                {
                    return "1";
                }
                ViewBag.MailAmount = amount;
                //日程提醒
                amount = DBSql.OA.SchedulerRemind.GetToRemindData(empID, DateTime.Now).Count;
                if (isTestMode && amount > 0)
                {
                    return "1";
                }
                ViewBag.SchedulerAmount = amount;
            }
            else
            {
                ViewBag.MessageAmount = 0;
                ViewBag.MailAmount = 0;
                ViewBag.SchedulerAmount = 0;
            }
            return "0";
        }
    }
}
