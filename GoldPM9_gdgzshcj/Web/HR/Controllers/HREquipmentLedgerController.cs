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

namespace HR.Controllers
{
    [Description("HREquipmentLedger")]
    public class HREquipmentLedgerController : CoreController
    {
        private DBSql.HR.HREquipmentLedger op = new DBSql.HR.HREquipmentLedger();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson(PermissionList("HREquipmentLedger"));
            ViewBag.TimeNow = DateTime.Now.ToString("yyyy-MM-dd");
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

            var There = QueryBuild<DataModel.Models.HREquipmentLedger>.True();

            #region 权限控制
            List<string> result = PermissionList("HREquipmentLedger");

            if (!result.Contains("allview"))
            {
                var hrEmpModel = new DBSql.HR.HREmployee().GetList(p => p.SysEmpId == userInfo.EmpID).FirstOrDefault();
                There = There.And(p => p.EquipmentEmpId == hrEmpModel.Id);
            }
            if (userInfo.CompanyID != 0)
            {
                There = There.And(p => p.CompanyID == userInfo.CompanyID);
            }
            #endregion

            var list = op.GetPagedList(There, PageModel).ToList();

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
            var model = new HREquipmentLedger();
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
            var model = new HREquipmentLedger();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.CompanyID = userInfo.CompanyID;
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

        #region 修改设备状态
        [Description("修改设备状态")]
        [HttpPost]
        public ActionResult editState(int id, int state)
        {
            var model = op.Get(id);
            model.State = state;
            model.MvcDefaultEdit(userInfo);
            op.UnitOfWork.SaveChanges();

            int reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
