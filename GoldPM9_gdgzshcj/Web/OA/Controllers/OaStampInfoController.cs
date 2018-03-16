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
    [Description("OaStampInfo")]
    public class OaStampInfoController : CoreController
    {
        private DBSql.Oa.OaStampInfo op = new DBSql.Oa.OaStampInfo();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        DBSql.Sys.BaseData dbBaseData = new DBSql.Sys.BaseData();
        DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("OaSealInfo")));
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
            //var list = op.GetPagedDynamic(PageModel).ToList();

            //return JavaScriptSerializerUtil.getJson(new
            //{
            //    total = PageModel.PageTotleRowCount,
            //    rows = list
            //});

            var list = op.GetPagedList(PageModel).ToList();
            //展示需要的列信息
            var dt = from f in list
                     select new
                     {
                         Id = f.Id,
                         f.StampName,
                         StampTypeID = f.FK_OaStampInfo_StampTypeID == null ? "" : f.FK_OaStampInfo_StampTypeID.BaseName,
                         f.KeepEmpName,
                         f.StampStartDate,
                         f.StampNote,
                         StampStatusID = f.StampStatusID == 0 ? "" : dbBaseData.GetBaseDataInfo(f.StampStatusID).BaseName,
                     };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = dt.ToList()
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaStampInfo();
            ViewData["KeepEmpId1"] = model.KeepEmpId.ToString();
            ViewData["KeepEmpId2"] = model.KeepEmpId.ToString();
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewData["KeepEmpId1"] = model.KeepEmpId.ToString();
            ViewData["KeepEmpId2"] = dbBaseEmployee.GetBaseEmployeeInfo(model.KeepEmpId) == null ? "0" : dbBaseEmployee.GetBaseEmployeeInfo(model.KeepEmpId).EmpDepID.ToString();
            ViewBag.permission = "['add','submit']";
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 1;
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
            var model = new OaStampInfo();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            var _KeepEmpId = Request.Form["KeepEmpId"];
            model.KeepEmpId = TypeHelper.parseInt(_KeepEmpId.Split('-')[0]);
            if (model.KeepEmpId > 0)
            {
                model.KeepEmpName = dbBaseEmployee.GetBaseEmployeeInfo(model.KeepEmpId).EmpName;
            }
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
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion


        #region   下拉选择模板(combogrid)
        [HttpPost]
        [Description("模板下拉")]
        public string GetModelList()
        {
            int PageNum = TypeHelper.parseInt(Request.Form["page"], 10);
            int Record = TypeHelper.parseInt(Request.Form["rows"], 1);
            var condition = TypeHelper.parseString(Request.Form["Words"]).Trim();

            var list = new DBSql.PubFlow.FlowModel().GetList(p => p.Id != 0).ToList();//要查询所有,p.Id!=0恒为true
            var a = (from item in list
                     where item.ModelName.Contains(condition)&& item.ModelRefTable == "OaStampUse"// 根据模板名称进行筛选
                     //orderby item.Id descending
                     select new
                     {
                         item.Id,
                         item.ModelName
                     }).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = a.Count,
                rows = (a.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });
        }
        #endregion
    }
}
