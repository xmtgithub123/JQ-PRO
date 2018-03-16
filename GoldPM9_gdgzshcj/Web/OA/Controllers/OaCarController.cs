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

namespace Oa.Controllers
{
    [Description("OaCar")]
    public class OaCarController : CoreController
    {
        private DBSql.OA.OaCar op = new DBSql.OA.OaCar();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("CarCreate")));
            ViewBag.CurrentEmpID = userInfo.EmpID;
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

            var Thwere = QueryBuild<DataModel.Models.OaCar>.True().And(p => p.DeleterEmpId == 0);

            List<string> permissionList = PermissionList("CarCreate");
            if (!permissionList.Contains("allview") && !permissionList.Contains("dep") && !permissionList.Contains("emp"))
            {
                Thwere = Thwere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }
            else if (permissionList.Contains("allview"))
            {

            }
            else if (permissionList.Contains("dep"))
            {
                Thwere = Thwere.And(p => p.CreatorDepId == this.userInfo.EmpDepID);
            }
            else if (permissionList.Contains("emp"))
            {
                Thwere = Thwere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }

            var list = op.GetPagedList(Thwere, PageModel).ToList();

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
            ViewBag.permission = "['submit','close']";
            ViewBag.AllowDelete = true;
            ViewBag.AllowUpload = true;
            var model = new OaCar();
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            var list = PermissionList("CarCreate");
            ViewBag.permission = "['close']";
            ViewBag.AllowDelete = false;
            ViewBag.AllowUpload = false;
            if (list.Contains("alledit") || list.Contains("edit") && model.CreatorEmpId == userInfo.EmpID)
            {
                ViewBag.permission = "['submit','close']";
                ViewBag.AllowDelete = true;
                ViewBag.AllowUpload = true;
            }
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
                op.UpdateOaCarList(id, this.userInfo);
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
            var model = new OaCar();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();

            model.CarNumber = JQ.Util.StringUtil.ReplaceStr(model.CarNumber);
            model.CarName = JQ.Util.StringUtil.ReplaceStr(model.CarName);
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
            reuslt = model.Id;

            var ba = new DBSql.Sys.BaseAttach();
            ba.MoveFile(reuslt, this.userInfo.EmpID, this.userInfo.EmpName);

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion

        [Description("查询所有车辆信息")]
        [HttpPost]
        public string GetAllCarInfo()
        {
            var list = this.op.GetQuery().OrderBy(p=>p.Id).Select(p=>new {
                Id=p.Id,
                text="["+p.CarName+"]"+p.CarNumber
            }).ToList();
            return JavaScriptSerializerUtil.getJson(list);
        }
    }
}
