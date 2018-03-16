using JQ.Web;
using JQ.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using DataModel.Models;
using DBSql.Design.Dto;

namespace Core.Controllers
{
    [Description("管理后台")]
    [AuthIgnore]
    public class layoutController : CoreController
    {
        private readonly DBSql.Sys.BaseMenu menu = new DBSql.Sys.BaseMenu();
        private readonly DBSql.Sys.BaseEmployee dbEmp = new DBSql.Sys.BaseEmployee();

        [Description("框架")]
        public ActionResult Default()
        {
            var list = menu.GetBaseMenuListByPermit(userInfo.EmpID).Where(p => p.MenuIsSecond == false).Where(p => p.MenuOrder.Length == 2).ToList();
            ViewBag.CurrentUserName = userInfo.EmpName;
            ViewBag.IsAgentLogin = userInfo.AgenEmpID > 0;
            ViewBag.PageSize = userInfo.EmpPageSize <= 0 ? 20 : userInfo.EmpPageSize;
            //委托模块菜单
            if (userInfo.AgenTypeID != 0)
            {
                var arr = userInfo.AgenMenu.Trim().Split(',');
                list = list.Where(s => arr.Contains(s.MenuNameEng)).ToList();
            }
            //if (userInfo.AgenEmpID > 0 && !string.IsNullOrEmpty(userInfo.AgenFlow))
            //{
            //    ViewBag.AgentFlows = userInfo.AgenFlow;
            //}
            ViewBag.AgentFlows = userInfo.AgenFlow;
            ViewBag.AgentTypeID = userInfo.AgenTypeID;
            ViewBag.headmenu = list;

            ViewBag.RegStatus = new DBSql.Sys.BaseConfig().GetRegStatus();

            return View(this.userInfo);
        }


        [Description("密码修改")]
        public ActionResult changepwd()
        {
            return View();
        }

        public ActionResult DefaultEmpty()
        {
            return View();
        }

        [Description("更新主题")]
        [HttpPost]
        public JsonResult setthemes(string theme, string menu)
        {
            DoResult result = dbEmp.UpdateEmpThemes(this.userInfo.EmpID, theme, menu);
            if (result.stateType == DoResultType.success)
            {
                if (Session["Employee"] != null) Session.Remove("Employee");

                userInfo.EmpThemesName = theme;
                userInfo.EmpMenuType = menu;

                Session["Employee"] = userInfo;
            }
            return Json(result);
        }

        [Description("确认密码修改")]
        [HttpPost]
        public ActionResult modifpassword(PassWordInfo model)
        {
            var op = new DBSql.Sys.BaseEmployee();

            var emp = new DBSql.Sys.BaseEmployee();
            var baseEmployeeInfo = GetEmpInfo(userInfo.EmpID);
            string old = EncryInfo.EncrypPassword(model.oldPassWord.Trim());

            if (baseEmployeeInfo.EmpPassword != old)
            {
                return Json(DoResultHelper.doSuccess("旧密码输入有误！"));
            }

            baseEmployeeInfo.EmpPassword = EncryInfo.EncrypPassword(model.newPassWord.Trim());
            int result = emp.UpdateBaseEmployeeInfo(baseEmployeeInfo, null);

            DoResult dr = result > 0 ? DoResultHelper.doSuccess("操作成功") : DoResultHelper.doSuccess("操作失败");
            return Json(dr);

        }

        public ActionResult allicon()
        {
            return View();
        }

        private void GetLastLoginInfo()
        {

        }

        public ActionResult WorkCenter()
        {
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.EmpName = userInfo.EmpName;
            ViewBag.EmpInfo = userInfo.EmpName + (userInfo.AgenEmpID > 0 ? "（" + userInfo.AgenEmpName + "）" : "");
            var empDetail = new DBSql.Sys.BaseEmployee().GetBaseEmployeeInfo(userInfo.EmpID);

            if (userInfo.AgenEmpID > 0 && !string.IsNullOrEmpty(userInfo.AgenFlow))
            {
                ViewBag.AgentFlow = userInfo.AgenFlow;
            }
            ViewBag.Note = empDetail.EmpNote;
            //获取出公告
            using (var dbContext = new DAL.JQPM9_DefaultContext())
            {

                //获取最后登录信息
                var lastlogin = dbContext.BaseLogs.Where(s => s.BaseLogEmpID == userInfo.EmpID && s.BaseLogRefTable == "login").OrderByDescending(s => s.BaseLogDate).Take(2).ToList();

                if (lastlogin.Count() == 2)
                {
                    if (lastlogin[1] != null)
                    {
                        ViewBag.LastIP = lastlogin[1].BaseLogIP == "::1" ? "127.0.0.1" : lastlogin[1].BaseLogIP;

                        ViewBag.LastDate = lastlogin[1].BaseLogDate.ToString("yyyy-MM-dd HH:mm");
                    }
                }


                ViewBag.NoticeList = JQ.Util.JavaScriptSerializerUtil.getJson(dbContext.OaNotices.Join(dbContext.BaseDatas, t => t.NoticeTypeID, t1 => t1.BaseID, (t, t1) => new { t, t1 }).Where(m => m.t.DeleterEmpId == 0).OrderByDescending(m => m.t.CreationTime).Take(5).Select(m => new { m.t.Id, m.t.Title, m.t.ReadCount, m.t.CreationTime, m.t.CreatorEmpName, NoticeTypeText = m.t1.BaseName, m.t.Content }).ToList());
                ViewBag.NewsList = JQ.Util.JavaScriptSerializerUtil.getJson((from t in dbContext.OaNews
                                                                             join t1 in dbContext.BaseDatas on t.NewsTypeID equals t1.BaseID into temp
                                                                             from datas in temp.DefaultIfEmpty()
                                                                             where t.DeleterEmpId == 0
                                                                             orderby t.NewsDate descending
                                                                             select new { t.Id, t.NewsTitle, t.CreatorEmpName, t.NewsDate, NewsTypeText = datas.BaseName }).Take(5).ToList());
                //获取出前5条带图片的新闻
                ViewBag.ImageNews = dbContext.OaNews.Where(m => m.DeleterEmpId == 0 && m.NewsImage != "").OrderByDescending(m => m.NewsDate).Select(m => new { m.Id, m.NewsTitle, m.NewsImage }).Take(5).ToList().Select(m => new OaNew() { Id = m.Id, NewsTitle = m.NewsTitle, NewsImage = m.NewsImage }).ToList();
                ViewBag.DiscussList = JQ.Util.JavaScriptSerializerUtil.getJson(dbContext.DesDiscusss.Where(m => m.DeleterEmpId == 0).OrderByDescending(m => m.CreationTime).Take(6).Select(m => new
                {

                    TalkTitle = m.TalkTitle + "(" + m.ReplyCount + "/" + m.ReadCount + ")",
                    m.CreationTime,
                    m.Id
                }).ToList());
                var model = new DBSql.Sys.BaseConfig().FirstOrDefault(x => x.ConfigEngName == "DesTaskPreAlert");
                if (null != model)
                {
                    int days = TypeHelper.parseInt(model.ConfigValue);
                    DateTime limitTime = DateTime.Now.AddDays(days);
                    ViewBag.TaskGanttList = JQ.Util.JavaScriptSerializerUtil.getJson(dbContext.DesTaskGantts.Where(m => m.DeleterEmpId == 0 && m.KeyPointType > 0 && m.DatePlanStart <= limitTime && m.Progress < 100).OrderBy(m => m.DatePlanStart).Take(6).Select(m => new { m.Name, m.DatePlanStart, m.Id }).ToList());
                }
            }
            if (userInfo.AgenEmpID == 0)
            {
                //暂不支持代理用户的代理用户的嵌套
                using (var dt = DAL.DBExecute.ExecuteDataTable("SELECT BaseEmpAgenID,FromEmpName,CONVERT(NVARCHAR(10),DateLower,23) AS DateLower,CONVERT(NVARCHAR(10),DateUpper,23) AS DateUpper,(SELECT ModelName+'，' FROM FlowModel WHERE CHARINDEX(','+ModelRefTable+',',','+AgenFlowRefTable+',')>0 FOR XML PATH('')) AS Flows,(SELECT MenuName+'，' FROM BaseMenu WHERE CHARINDEX(','+MenuNameEng+',',','+AgenMenu+',')>0 FOR XML PATH('')) AS Menus,AgenType,(CASE AgenType WHEN 0 THEN '全部权限' WHEN 1 THEN '菜单权限' WHEN 2 THEN '流程权限' END) AS AgenTypeText FROM BaseEmpAgen WHERE ToEmpID=" + userInfo.EmpID + " AND AgenStatus=0 AND @date BETWEEN DateLower AND DateUpper ORDER BY DateCreate", new System.Data.SqlClient.SqlParameter("@date", DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00"))))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        row["Flows"] = row["Flows"].ToString().TrimEnd('，');
                        row["Menus"] = row["Menus"].ToString().TrimEnd('，');
                    }
                    ViewBag.EmgAgents = JQ.Util.JavaScriptSerializerUtil.getJson(dt);
                }
            }
            else
            {
                ViewBag.EmgAgents = "[]";
            }
            ViewBag.TaskAmount = JQ.Util.TypeParse.parse<int>(DAL.DBExecute.ExecuteScalar("SELECT COUNT(1) FROM dbo.DesTask WHERE DeleterEmpId=0 AND TaskType=0 AND TaskEmpID=" + userInfo.EmpID));
            ViewBag.JoinProjectAmount = JQ.Util.TypeParse.parse<int>(DAL.DBExecute.ExecuteScalar("SELECT COUNT(1) FROM dbo.DesTaskGroup WHERE DeleterEmpId = 0 AND TaskGroupType = 1 AND TaskGroupEmpID =" + userInfo.EmpID));
            return View();
        }

        //通知页面
        public ActionResult Notify()
        {
            var empID = JQ.Util.TypeParse.parse<int>(Request.QueryString["empID"]);
            if (empID == 0)
            {
                ViewBag.Loaded = false;
            }
            else
            {
                ViewBag.Loaded = true;
                string DesTaskApproveMode = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("DesTaskApproveMode").ConfigValue; // 任务校审模式   单步 0；同步 1
                ViewBag.TaskAmount = new DBSql.Design.DesTask().GetTaskRemindTodoAmount(
                    empID,
                    DesTaskApproveMode == "0" ?
                        String.Format("{0}", DBSql.Design.FlowNodeStatus.进行中.ToString("D"))
                        :
                        String.Format("{0},{1}", DBSql.Design.FlowNodeStatus.已安排.ToString("D"), DBSql.Design.FlowNodeStatus.进行中.ToString("D")),
                    new DesTaskRemindPermission(userInfo)
                );
                var flow = new DBSql.PubFlow.Flow();
                ViewBag.ProjectFormAmount = flow.GetToDoAmount(userInfo.EmpID, 1, userInfo.AgenTypeID, userInfo.AgenFlow);
                ViewBag.OAFormAmount = flow.GetToDoAmount(userInfo.EmpID, 2, userInfo.AgenTypeID, userInfo.AgenFlow);
                var eschRec = new DBSql.Design.DesExchRec();
                ViewBag.ExchangeAmount = eschRec.GetRecCountByEmpID(userInfo.EmpID, userInfo.AgenTypeID, userInfo.AgenFlow);
            }
            return View();
        }

        /// <summary>
        /// 当前用户的快捷方式管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShortcutManage()
        {
            return View();
        }

        /// <summary>
        /// 获取出当前用户的快捷方式
        /// </summary>
        /// <returns></returns>
        public JsonResult GetShortcuts()
        {
            return Json(new DBSql.Sys.BaseMenu().GetShortcuts(userInfo.EmpID));
        }

        /// <summary>
        /// 获取出前10条的收藏工程
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFavouriteProjects()
        {
            using (var projectFavourite = new DBSql.Project.ProjectFavourite())
            {
                return Json(projectFavourite.GetProjectList(userInfo.EmpID, 10));
            }
        }

        /// <summary>
        /// 切换至代理登录用户
        /// </summary>
        /// <returns></returns>
        public JsonResult SwitchToAgentUser()
        {
            var empAgenID = JQ.Util.TypeParse.parse<int>(Request.Form["empAgenID"]);
            if (empAgenID == 0)
            {
                return Json(new { Result = false, Message = "身份无效！" });
            }
            //获取出EmpAgenInfo
            using (var dbContext = new DAL.JQPM9_DefaultContext())
            {
                var data = dbContext.BaseEmpAgens.FirstOrDefault(m => m.BaseEmpAgenID == empAgenID);
                if (data != null)
                {
                    try
                    {
                        SaveSessionInfo(data);
                    }
                    catch (Common.JQException exception)
                    {
                        return Json(new { Result = false, Message = exception.Message });
                    }
                    catch
                    {
                        return Json(new { Result = false, Message = "发生未经处理的异常" });
                    }
                    InsertLog();
                }
            }
            return Json(new { Result = true, Url = Url.Content("~") });
        }

        private void InsertLog()
        {
            DataModel.Models.BaseLog log = new DataModel.Models.BaseLog();
            log.BaseLogEmpID = userInfo.EmpID;
            log.EmpName = userInfo.EmpName;
            log.BaseLogTypeID = 0;
            log.BaseLogDate = DateTime.Now;
            log.BaseLogIP = GetIP();
            log.BaseLogRefTable = "LoginAgent";
            log.BaseLogRefID = userInfo.AgenEmpID;
            log.BaseLogRefHTML = "BaseEmployee";
            DBSql.Sys.BaseLog logAdd = new DBSql.Sys.BaseLog();
            logAdd.Add(log);
            logAdd.UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 获取邮件（前5条）
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMails()
        {
            return Json(new DBSql.Oa.OaMailRead().GetToDisplayDatas(userInfo.EmpID));
        }

        /// <summary>
        /// 切回登录
        /// </summary>
        /// <returns></returns>
        public JsonResult SwitchAgent()
        {
            var empID = userInfo.AgenEmpID;
            if (empID == 0)
            {
                return Json(new { Result = false, Message = "无法验证您原先的身份！" });
            }
            var emp = new DBSql.Sys.BaseEmployee().GetBaseEmployeeInfo(empID);
            if (emp != null)
            {
                SaveSessionInfo(emp);
                CookieUtil.saveCookie("UID", emp.EmpID.ToString(), 24);
                CookieUtil.clear("AgentID");
            }
            return Json(new { Result = true });
        }
    }
}
