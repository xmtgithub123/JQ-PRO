using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Common.Data.Extenstions;

namespace Iso.Controllers
{
    [Description("IsoXMCB")]
    public class IsoXMCBController : CoreController
    {
        private DBSql.Iso.IsoXMCB op = new DBSql.Iso.IsoXMCB();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        private DBSql.Project.Project dbProject = new DBSql.Project.Project();
        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("Iso_XMCB")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            List<string> result = PermissionList("Iso_XMCB");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var queryInfo = Request.Form["queryInfo"];
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == "txtLike")
                    {
                        PageModel.TextCondtion = _child.Value;
                    }
                    else if (_child.Key == "CTStartTime")
                    {
                        PageModel.SelectCondtion.Add("CTStartTime", _child.Value);
                    }
                    else if (_child.Key == "CTEndTime")
                    {
                        PageModel.SelectCondtion.Add("CTEndTime", _child.Value);
                    }
                }
            }

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "ISO.Id desc";
            }
            if (!(result.Contains("allview") || result.Contains("alledit")))
            {
                if (result.Contains("dep"))
                {
                    PageModel.SelectCondtion.Add("QueryDeptID", this.userInfo.EmpDepID);
                }
                else
                {
                    PageModel.SelectCondtion.Add("QueryEmpID", this.userInfo.EmpID);
                }
            }
            var dt = op.GetListInfo(PageModel, this.userInfo);

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("Iso_XMCB")));
            var model = new IsoXMCB();

            model.CreationTime = DateTime.Now;
            model.CreatorEmpName = userInfo.EmpName;
            model.CreatorDepID = userInfo.EmpDepID;
            model.CreatorEmpId = userInfo.EmpID;
            ViewBag.CreatorEmpId = model.CreatorEmpId;

            ViewBag.ProjNumbe = "";
            ViewBag.ProjName = "";
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("Iso_XMCB")));
            var model = op.Get(id);

            var _project = dbProject.Get(model.ProjID);
            ViewBag.ProjNumber = _project.ProjNumber;
            ViewBag.ProjName = _project.ProjName;
            ViewBag.CreatorEmpId = model.CreatorEmpId;
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 0;
            try
            {
                op.UpdateIsoXMCBInfo(id, this.userInfo);
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "IsoXMCB");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoXMCB");
                }
                reuslt = 1;
            }
            catch
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
            var model = new IsoXMCB();
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
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion


    }

}
