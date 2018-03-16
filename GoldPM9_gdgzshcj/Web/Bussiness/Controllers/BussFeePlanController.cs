using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;

namespace Bussiness.Controllers
{
    [Description("BussFeePlan")]
    public class BussFeePlanController : CoreController
    {
        private DBSql.Bussiness.BussFeePlan op = new DBSql.Bussiness.BussFeePlan();
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
        public string json(FormCollection condition)
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "FeeDate asc";
            }
            var TWhere = QueryBuild<DataModel.Models.BussFeePlan>.True();
            if (condition["ConID"] != null)
            {
                int Conid = Common.ExtensionMethods.Value<int>(condition["ConID"]);
                TWhere = TWhere.And(p => p.ConID == Conid);
            }
            TWhere = TWhere.And(p => p.DeleterEmpId == 0);
            var list = op.GetPagedList(TWhere, PageModel).ToList().Select(p => new
            {
                p.Id,
                p.ConID,
                ProjID = p.ProjID == 0 ? -1 : p.ProjID,
                p.FeeDate,
                p.FeeMoney,
                p.FeeNote,
                p.IsFinished,
                p.CreatorEmpName,
                text = p.ProjID == 0 ? "未指定项目" : ((new DBSql.Project.Project().Get(p.ProjID)) == null ? "" : new DBSql.Project.Project().Get(p.ProjID).ProjName),
            });

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
            var model = new BussFeePlan();
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

        public ActionResult delByIDs(FormCollection condition)
        {
            string ids = condition["ids"].ToString();
            int[] id = (from n in ids.Split(',') where n != "" select Common.ExtensionMethods.Value<int>(n)).ToArray();
            if (id.Length == 0) return Json(null);
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
            var model = new BussFeePlan();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            model.MvcSetValue();
            model.ProjID = model.ProjID == -1 ? 0 : model.ProjID;
            var a = Request["FinishedCheck"].ToString();
            model.IsFinished = a == "1" ? true : false;
            int reuslt = 0;
            if (model.Id > 0)
            {
                op.Edit(model);
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;
            }
            else
            {
                model.MvcDefaultSave(userInfo.EmpID);
                op.Add(model);
                op.UnitOfWork.SaveChanges();
                reuslt = model.Id;
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
