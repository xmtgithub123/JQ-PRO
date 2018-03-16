using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
namespace Oa.Controllers
{
[Description("OaMeetingUse")]
public class OaMeetingUseController : CoreController
    {
        private DBSql.Oa.OaMeetingUse op = new DBSql.Oa.OaMeetingUse();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Oa.OaMeetingRoom room = new DBSql.Oa.OaMeetingRoom();
        private DBSql.PubFlow.Flow flow = new DBSql.PubFlow.Flow();
        private DBSql.Sys.BaseEmployee emp = new DBSql.Sys.BaseEmployee();
        string[] dayWeeks = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五","星期六" };
        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            List<string> permission = PermissionList("OaMeetingRoomApply");
            ViewBag.permission = JavaScriptSerializerUtil.getJson(permission);
            //将调度权与维护权同步
            if(permission.Contains("alledit"))
            {
                ViewBag.Change = 1;//可以进行调度
            }
            else
            {
                ViewBag.Change = 0;//不可进行调度
            }
            ViewBag.EmpId = userInfo.EmpID;//当前登录账户
            return View();
        } 
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            List<string> permission = PermissionList("OaMeetingRoomApply");//添加部门维护权和个人查看权
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetPagedList(PageModel).ToList();
            if(!string.IsNullOrEmpty(Request.Params["MeetingID"]))
            {
                int MeetingID = TypeHelper.parseInt(Request.Params["MeetingID"]);
                if (string.IsNullOrEmpty(Request.Params["IsAll"]))//是否查看此会议室的使用历史情况
                {
                    // 未使用或者正在使用的记录
                    list = op.GetPagedList(p => p.MeetingID == MeetingID&&p.MeetingEndDate>=DateTime.Now, PageModel).ToList();
                }
                else
                {
                    list = op.GetPagedList(p => p.MeetingID == MeetingID, PageModel).ToList();
                }
            }
            if(!permission.Contains("dep")&&!permission.Contains("allview"))//个人查看权
            {
                list = op.GetPagedList(p=>p.CreatorEmpId==userInfo.EmpID,PageModel).ToList();
            }
            if(permission.Contains("dep")&&!permission.Contains("allview"))//部门查看权
            {
                list = op.GetPagedList(p => p.CreatorDepId == userInfo.EmpDepID, PageModel).ToList();
            }
            var targetList = (from item in list
                             let CreationTime=item.CreationTime.ToString("yyyy-MM-dd")
                             join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "OaMeetingUse") on item.Id equals t1.FlowRefID into nodes
                             from temp in nodes.DefaultIfEmpty()
                             select new
                             {
                                 item.Id,
                                 CreationTime,
                                 item.CreatorEmpName,
                                 item.CreatorEmpId,
                                 StartDate= GetWeekDays(item.MeetingStartDate),
                                 EndDate = GetWeekDays(item.MeetingEndDate),
                                 item.MeetingJoinPeo,
                                 item.MeetingLeader,
                                 LeadEmpName= GetLeaderName(item.MeetingLeader),// 会议室负责人
                                 item.MeetingNote,
                                 item.MeetingUseConent,
                                 item.State,
                                 RoomName=item.FK_OaMeetingUse_MeetingID==null?"":item.FK_OaMeetingUse_MeetingID.RoomName,
                                 StateName=item.State==0?"未确认":" 已确认",
                                 //FlowStatus=SetFlowStatus("OaMeetingUse",item.Id,item.CreatorEmpId,userInfo.EmpID)
                                 FlowID = temp == null ? 0 : temp.Id,
                                 FlowName = temp == null ? "" : temp.FlowName,
                                 FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                                 FlowStatusName = temp == null ? "" : temp.FlowStatusName, 
                                 FlowXml = temp == null ? "" : temp.FlowXml
                             }).Select(m => new { m.Id,m.CreationTime, m.CreatorEmpName, m.CreatorEmpId, m.StartDate, m.EndDate, m.MeetingJoinPeo, m.MeetingLeader, m.LeadEmpName, m.MeetingNote, m.MeetingUseConent, m.State, m.RoomName, m.StateName, m.FlowID, m.FlowName,m.FlowStatusID,m.FlowStatusName,m.FlowXml, FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml) });
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
            });
        } 
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaMeetingUse();
            ViewBag.ApplyPerson = userInfo.EmpName;//申请人
            ViewBag.ApplyDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.StartHour = HourList();//开始 小时
            ViewBag.EndHour = HourList();//结束 小时
            ViewBag.StartMinu = MinutesList();//开始 分钟
            ViewBag.EndMinu = MinutesList();//结束 分钟
            ViewBag.StartDate = System.DateTime.Now.ToString("yyyy-MM-dd");//开始日期
            ViewBag.EndDate = System.DateTime.Now.ToString("yyyy-MM-dd");//结束日期

            ViewBag.bindstartHour = "00";
            ViewBag.bindendHour = "00";
            ViewBag.bindstartMinu = "00";
            ViewBag.bindendMinu = "00";
            ViewBag.meetingRoom = "";


            return View("info", model);
        } 
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.ApplyPerson = model.CreatorEmpName;//申请人
            ViewBag.ApplyDate = model.CreationTime.ToString("yyyy-MM-dd");
            string startHour = model.MeetingStartDate.Hour.ToString("00");
            ViewBag.StartHour = HourList(startHour);//开始 小时
            ViewBag.bindstartHour = startHour;
            string endHour = model.MeetingEndDate.Hour.ToString("00");
            ViewBag.EndHour = HourList(endHour);//结束 小时
            ViewBag.bindendHour = endHour;
            string startMinu = model.MeetingStartDate.Minute.ToString("00");
            ViewBag.StartMinu = MinutesList(startMinu);//开始 分钟
            ViewBag.bindstartMinu = startMinu;
            string endMinu = model.MeetingEndDate.Minute.ToString("00");
            ViewBag.EndMinu = MinutesList(endMinu);//结束 分钟
            ViewBag.bindendMinu = endMinu;
            ViewBag.StartDate = model.MeetingStartDate.ToString("yyyy-MM-dd");//开始日期
            ViewBag.EndDate = model.MeetingEndDate.ToString("yyyy-MM-dd");//结束日期
            //meetingRoom
            DataModel.Models.OaMeetingRoom meeting = room.Get(model.MeetingID);
            if(meeting!=null)
            {
                ViewBag.meetingRoom = meeting.RoomName;
            }
            string page = string.Empty;
            if(!string.IsNullOrEmpty(Request.Params["Change"]))//调度页面
            {
                page = "Transfer";
            }
            else
            {
                page = "Info";//一般页面
            }
            List<string> permission = PermissionList("OaMeetingRoomApply");
            int FlowStatus = SetFlowStatus("OaMeetingUse", model.Id, model.CreatorEmpId, userInfo.EmpID);
            if (permission.Contains("edit")&&(FlowStatus == 0||FlowStatus==1))//默认显示
            {
                //编辑权限(保存和提交按钮显示)
                ViewBag.editPermission = "";
                ViewBag.submitPermission = "";

            }
            else
            {
                if(FlowStatus > 1)// 流程自动控制
                {
                    ViewBag.editPermission = "";
                    ViewBag.submitPermission = "";
                }
                else
                {
                    if(FlowStatus==-1)
                    {
                        //隐藏提交和保存
                        ViewBag.editPermission = ",isShowSave:false";
                        ViewBag.submitPermission = "$('#toolbar').children('a').eq(0).hide();";
                        ViewBag.Upload = "$('#UploadFile1_upload').hide();$('#UploadFile1_delete').hide();";
                    }
                }
                
            }

            return View(page, model);
        } 
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));
            op.UnitOfWork.SaveChanges();
            reuslt++;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new OaMeetingUse();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            DateTime startDate = TypeHelper.parseDateTime(Request.Params["StartDate"]);
            int startHour = TypeHelper.parseInt(Request.Params["startHour"]);
            int startMinu = TypeHelper.parseInt(Request.Params["startMinu"]);
            model.MeetingStartDate = startDate.AddHours(startHour).AddMinutes(startMinu);//计划开始时间
            DateTime endDate = TypeHelper.parseDateTime(Request.Params["EndDate"]);
            int endHour = TypeHelper.parseInt(Request.Params["endHour"]);
            int endMinu = TypeHelper.parseInt(Request.Params["endMinu"]);
            model.MeetingEndDate = endDate.AddHours(endHour).AddMinutes(endMinu);
            int reuslt = 0;                      
            if (model.Id > 0)
            {
                model.MvcDefaultEdit(userInfo.EmpID);
				op.Edit(model);
            }
            else
            {
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
            }
			op.UnitOfWork.SaveChanges();
			if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }
			reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 选择会议室
        /// <summary>
        /// 选择会议室
        /// </summary>
        /// <returns></returns>
        public ActionResult selectReview()
        {
            return View();
        }
        #endregion


        /// <summary>
        /// 检测冲突
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public string  checkConflict(FormCollection form)
        {
            DateTime startDate = TypeHelper.parseDateTime(Request.Params["startDate"]);
            DateTime endDate = TypeHelper.parseDateTime(Request.Params["endDate"]);
            //int MeetingID = TypeHelper.parseInt(form["MeetingID"]);
            //int Id = TypeHelper.parseInt(form["Id"]);
            int Id = TypeHelper.parseInt(Request.Params["Id"]);
            int MeetingID = TypeHelper.parseInt(Request.Params["MeetingID"]);
            int Count = 0;
            if(Id>0)//区别新增状态和编辑状态
            {
                Count = op.GetList(p => p.MeetingID == MeetingID && p.MeetingEndDate > startDate && p.MeetingStartDate < endDate&&p.Id!=Id).Count();//判断2个时间段存在交集
            }
            else
            {
                Count = op.GetList(p => p.MeetingID == MeetingID && p.MeetingEndDate >startDate && p.MeetingStartDate < endDate).Count();//判断2个时间段存在交集
            }
            return Count.ToString();
        }

        #region 确认时间
        /// <summary>
        /// 确认时间
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SureTime()
        {
            int Id = TypeHelper.parseInt(Request.Params["Id"]);
            try
            {
                DataModel.Models.OaMeetingUse model = op.Get(Id);
                model.State = 1;
                op.Edit(model);
                op.UnitOfWork.SaveChanges();
                doResult =  DoResultHelper.doSuccess(1, "操作成功");
            }
            catch(Exception ex)
            {
                doResult = DoResultHelper.doError(ex.Message);
            }
            return Json(doResult);

        }
        #endregion


        /// <summary>
        /// 获取会议负责人
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        private string GetLeaderName(int EmpID)
        {
            string EmpName = string.Empty;
            DataModel.Models.BaseEmployee baseEmp = emp.Get(EmpID);
            if(baseEmp!=null)
            {
                EmpName = baseEmp.EmpName;
            }
            return EmpName;
        }


        /// <summary>
        /// 返回会议室审批的状态（根据状态来判断是否可编辑）
        /// </summary>
        /// <param name="RefID"></param>
        /// <returns></returns>
        public int GetFlowStatus(int RefID,string CreateEmpName)
        {
            int flowStatus = 0;
            DataModel.Models.Flow flowModel = flow.GetList(p => p.FlowRefTable == "OaMeetingUse" && 
            p.FlowRefID == RefID).FirstOrDefault();
            if(flowModel!=null)
            {
                flowStatus = flowModel.FlowStatusID;
                if(flowModel.FlowStatusName.Contains("退回")&&flowModel.FlowStatusName.Contains(CreateEmpName))
                {
                    flowStatus = 0;
                }
            }
            return flowStatus;
        }

        /// <summary>
        /// 获取小时列表信息
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> HourList(string defaultSelect="00")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for(int hour=0;hour<24;hour++)
            {
                SelectListItem item = new SelectListItem();
                string formatHour = hour.ToString("00");
                item.Text = formatHour;
                item.Value = formatHour;
                list.Add(item);
            }
            list.Where(p => p.Value == defaultSelect).FirstOrDefault().Selected = true;
            return list.AsEnumerable();
        }

        /// <summary>
        /// 获取分钟列表
        /// </summary>
        /// <param name="defaultSelect"></param>
        /// <returns></returns>
        private IEnumerable<SelectListItem>MinutesList(string defaultSelect="00")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for (int hour = 0; hour < 60; hour++)
            {
                SelectListItem item = new SelectListItem();
                string formatMinu = hour.ToString("00");
                item.Text = formatMinu;
                item.Value = formatMinu;
                list.Add(item);
            }
            list.Where(p => p.Value == defaultSelect).FirstOrDefault().Selected = true;
            return list.AsEnumerable();

        }


        /// <summary>
        /// 根据日期获取星期几
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetWeekDays(DateTime date)
        {
            string WeekDay = string.Empty;
            DayOfWeek dayOfWeek = date.DayOfWeek;
            WeekDay = dayWeeks[(int)dayOfWeek];
            return date.ToString("yyyy-MM-dd HH:mm")+"  "+WeekDay;
        }
    }
}
