using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Oa.Controllers
{
[Description("OaMeetingRoom")]
public class OaMeetingRoomController : CoreController
    {
        private DBSql.Oa.OaMeetingRoom op = new DBSql.Oa.OaMeetingRoom();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Sys.BaseData data = new DBSql.Sys.BaseData();
        private DBSql.Oa.OaMeetingUse meetingUse = new DBSql.Oa.OaMeetingUse();
        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OaMeetingRoomRegister")));
            return View();
        } 
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var list = op.GetPagedList(PageModel).Select(p => new {
                p.Id,
                p.RoomName,
                p.RoomArea,
                p.RoomPeoLength,
                p.RoomNote,
                RoomStatusName = p.FK_OaMeetingRoom_RoomStatus == null ? "":p.FK_OaMeetingRoom_RoomStatus.BaseName,
                p.RoomStatus
            }).ToList();
            
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        } 
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaMeetingRoom();
            
            List<string> list = PermissionList("OaMeetingRoomRegister");
            if(list.Contains("add"))
            {
                ViewBag.Permission = "['submit', 'close']";
            }
            else
            {
                ViewBag.Permission = "['close']";
            }

            return View("info", model);
        } 
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            List<string> list = PermissionList("OaMeetingRoomRegister");
            if (list.Contains("edit"))
            {
                ViewBag.Permission = "['submit', 'close']";
            }
            else
            {
                ViewBag.Permission = "['close']";
            }

            return View("info", model);
        } 
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new OaMeetingRoom();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

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

        #region  查看会议室使用记录
        /// <summary>
        /// 查看会议室使用记录
        /// </summary>
        /// <returns></returns>
        public ActionResult RoomUseList()
        {
            return View();
        }
        #endregion

        #region  选择会议室列表
        [HttpPost]
        public string SelectRoom()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            int PageNum = TypeHelper.parseInt(Request.Params["page"], 20);
            int Record = TypeHelper.parseInt(Request.Params["rows"], 1);

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            string text = string.Empty;
            if(!string.IsNullOrEmpty(Request.Params["text"]))
            {
                text = Request.Params["text"];
            }
            var roomList = op.GetList(p => p.Id > 0&&p.RoomName.Contains(text));
            var roomUseList = meetingUse.GetList(p=>p.MeetingEndDate>=System.DateTime.Now);//结束时间大于当前时间的记录
            var targetList = from roomitem in roomList
                             join roomUseItem in roomUseList on roomitem.Id equals roomUseItem.MeetingID into target
                             from item in target.DefaultIfEmpty()
                             orderby roomitem.Id ascending
                             select new {
                                 roomitem.Id,
                                 roomitem.RoomName,
                                 roomitem.RoomArea,
                                 roomitem.RoomPeoLength,
                                 roomitem.RoomNote,
                                 roomitem.RoomStatus,
                                 PeoIdent=roomitem.Id,
                                 StateIdent=roomitem.Id,
                                 ProcessId= roomitem.Id,
                                 RoomStatusName = roomitem.FK_OaMeetingRoom_RoomStatus == null ? "" : roomitem.FK_OaMeetingRoom_RoomStatus.BaseName,
                                 StartDate = item == null ? "" : new OaMeetingUseController().GetWeekDays(item.MeetingStartDate),
                                 EndDate = item == null ? "" : new OaMeetingUseController().GetWeekDays(item.MeetingEndDate),
                                 MeetingId = item == null ? 0 : item.Id,
                                 MeetingUseConent = item == null ? "": item.MeetingUseConent
                             };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = targetList.Count(),
                rows = (targetList.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });
        }
        #endregion
    }
}
