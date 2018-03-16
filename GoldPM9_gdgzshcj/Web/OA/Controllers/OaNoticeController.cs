using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Data;

namespace Oa.Controllers
{
    [Description("OaNotice")]
    public class OaNoticeController : CoreController
    {
        private DBSql.Oa.OaNotice op = new DBSql.Oa.OaNotice();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
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
            var list = op.GetPagedList(PageModel).ToList();

            if (!string.IsNullOrEmpty(Request.Params["endDate"]) && TypeHelper.isDateTime(Request.Params["endDate"]))
            {
                DateTime endDate = TypeHelper.parseDateTime(Request.Params["endDate"]).AddDays(1);
                list = op.GetPagedList(p => p.CreationTime < endDate, PageModel).ToList();
            }

            var dtoList = from item in list
                          let CreationTime=item.CreationTime.ToString("yyyy-MM-dd")
                          select new
                          {
                              item.Id,
                              item.Title,
                              item.Content,
                              NoticeTypeID = GetBaseInfo(item.NoticeTypeID).BaseName,
                              item.ReadCount,
                              CreationTime,
                              item.CreatorEmpId,
                              item.CreatorEmpName,
                              item.CreatorDepId,
                              item.CreatorDepName,
                              item.DeleterEmpId,
                              item.DeleterEmpName,
                              item.DeletionTime
                          };
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dtoList.ToList()
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaNotice();
            model.CreatorEmpName = userInfo.EmpName;
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            op.Delete(s => id.Contains(s.Id));
            try
            {
                op.UnitOfWork.SaveChanges();
                reuslt = 1;
            }
            catch (Exception)
            {
                
            }
            
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new OaNotice();
            
            if (Id > 0)
            {               
                model = op.Get(Id);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
            }
            else
            {
                model.CreationTime = DateTime.Now;
                model.CreatorEmpId = userInfo.EmpID;
                model.CreatorEmpName = userInfo.EmpName;
                model.CreatorDepId = userInfo.EmpDepID;
                model.CreatorDepName = GetBaseInfo(userInfo.EmpDepID).BaseName;
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            using (var ba = new DBSql.Sys.BaseAttach())
            {
                ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
            }

            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
        /// <summary>
        /// 查看
        /// </summary>
        /// <returns></returns>
        public ActionResult Query(int id)
        {
            ViewBag.HiddenSave = true;
            var model = op.Get(id);
            model.ReadCount = model.ReadCount + 1;
            op.Edit(model);
            op.UnitOfWork.SaveChanges();
            return View("Query",model);
        }
    }
}
