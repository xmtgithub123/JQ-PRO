using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;

namespace Core.Controllers
{
    [Description("ModelIsoCheck")]
    public class ModelIsoCheckController : CoreController
    {
        private DBSql.Core.ModelIsoCheck op = new DBSql.Core.ModelIsoCheck();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ErrorModel")));
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json(FormCollection Tcondition)
        {
            List<string> result = PermissionList("ErrorModel");
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var TWhere = QueryBuild<DataModel.Models.ModelIsoCheck>.True();

            if (!result.Contains("allview") && !result.Contains("dep") && !result.Contains("emp"))
            {
                TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }
            else if (result.Contains("allview"))
            {

            }
            else if (result.Contains("dep"))
            {
                TWhere = TWhere.And(p => p.CreatorDepID == this.userInfo.EmpDepID);
            }
            else if (result.Contains("emp"))
            {
                TWhere = TWhere.And(p => p.CreatorEmpId == this.userInfo.EmpID);
            }

            var list = op.GetPagedList(TWhere, PageModel).ToList();
            BaseDataSystem dbModel = op.DbContext.Set<DataModel.Models.BaseDataSystem>().FirstOrDefault(p => p.BaseNameEng == "DesignErrorType");
            var ErrorList = op.DbContext.Set<DataModel.Models.BaseDataSystem>().Where(p => p.BaseOrder.StartsWith(dbModel.BaseOrder + "_")).ToList();
            var target = from item in list
                         select new
                         {
                             item.Id,
                             item.PhaseID,
                             PhaseName = item.FK_ModelIsoCheck_PhaseID == null ? "" : item.FK_ModelIsoCheck_PhaseID.BaseName,
                             item.SpecialID,
                             SpecialName = item.FK_ModelIsoCheck_SpecialID == null ? "" : (item.FK_ModelIsoCheck_SpecialID.BaseID == 0 ? "汇总专业" : item.FK_ModelIsoCheck_SpecialID.BaseName),
                             item.CheckType,
                             CheckTypeName = item.FK_ModelIsoCheck_CheckType == null ? "" : item.FK_ModelIsoCheck_CheckType.BaseName,
                             item.CheckItem,
                             item.CheckNote,
                             DeleterEmpId=item.DeleterEmpId>0?"停用":"启用",
                             ErrorTypeName = ErrorList.FirstOrDefault(p => p.BaseID == item.CheckErrTypeID) == null ? "" : ErrorList.FirstOrDefault(p => p.BaseID == item.CheckErrTypeID).BaseName,
                         };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = target
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new ModelIsoCheck();
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
            int reuslt = op.DelModelIsoCheckForm(id, userInfo.EmpID);
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new ModelIsoCheck();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.CheckType = 19;
            Common.ModelConvert.MvcDefaultSave<DataModel.Models.ModelIsoCheck>(model, this.userInfo);
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
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
