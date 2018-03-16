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
    [Description("ModelExchange")]
    public class ModelExchangeController : CoreController
    {
        private DBSql.Core.ModelExchange op = new DBSql.Core.ModelExchange();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ModelExchange")));
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
                PageModel.SelectOrder = "Id desc";
            }
            var TWhere = QueryBuild<DataModel.Models.ModelExchange>.True();
            string KeyJQSearch = Request.Form["KeyJQSearch"];
            int ProjectPhase =TypeHelper.parseInt( Request.Form["ProjectPhase"]);
            int Special = TypeHelper.parseInt(Request.Form["Special"]);

            if (!string.IsNullOrEmpty(KeyJQSearch))
            {
                TWhere = TWhere.And(p => p.ModelExchangeName.Contains(KeyJQSearch) || p.ModelExchangeContent.Contains(KeyJQSearch));
            }
            //if (ProjectPhase != 0)
            //{
            //    TWhere = TWhere.And(p => p.ModelExchangePhaseID ==ProjectPhase );
            //}
            //if (Special != 0)
            //{
            //    TWhere = TWhere.And(p => p.ModelExchangeSpecID == Special);
            //}

            var list = op.GetPagedList(TWhere, PageModel).ToList();
            var targetList = from p in list
                             select new
                             {
                                 Id = p.Id,
                                 ModelExchangePhaseID = p.FK_ModelExchange_ModelExchangePhaseID.BaseName,
                                 ModelExchangeSpecID = p.FK_ModelExchange_ModelExchangeSpecID.BaseName,
                                 ModelExchangeName = p.ModelExchangeName,
                                 ModelExchangeContent = p.ModelExchangeContent,
                                 ModelExchangePlanDays = p.ModelExchangePlanDays,
                                 ////ReciveSpecName = string.Join(",", (from m in p.FK_ModelExchangeReceive_Id select m.FK_ModelExchangeReceive_ModelReceiveSpecID.BaseName)),
                                 ReciveSpecName = string.Join(",", (from m in p.FK_ModelExchangeReceive_Id select m.FK_ModelExchangeReceive_ModelReceiveSpecID.BaseName).ToList()),
                             };

         
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
            });
              
        }
        #endregion

        private string IDs = string.Empty;

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new ModelExchange();
            ViewBag.IDs = "";
            ViewBag.editPermission = "['submit','close']";
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            if (model.FK_ModelExchangeReceive_Id.isNotEmpty())
            {
                IDs = string.Join(",", model.FK_ModelExchangeReceive_Id.ToList().Select(m => m.ModelReceiveSpecID));
            }
            ViewBag.IDs = !string.IsNullOrEmpty(IDs) ? IDs : "";
            List<string> permission = PermissionList("ModelExchange");
            if(permission.Contains("edit")||permission.Contains("alledit"))//编辑权或者维护权
            {
                ViewBag.editPermission = "['submit','close']";
            }
            else
            {
                ViewBag.editPermission = "['close']";
            }
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int reuslt = 1;
            //op.Delete(s => id.Contains(s.Id));
            //op.UnitOfWork.SaveChanges();

            op.DeleteModelExchange(id);
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id, int[] ModelReceiveSpecID = null)
        {
            var model = new ModelExchange();
            if (Id > 0)
            {
                model = op.Get(Id);
            }
            string ids = "";
            if (ModelReceiveSpecID.isNotEmpty())
            {
                foreach (var item in ModelReceiveSpecID)
                {
                    ids += item + ",";
                }
            }
            ids = ids.Trim(',');

            model.MvcSetValue();
            var s = ModelReceiveSpecID;
            int reuslt = 0;
            if (model.Id > 0)
            {
                //op.Edit(model);
                op.UpdateModelExchange(model, ids);
            }
            else
            {
                //op.Add(model);
                op.InsertModelExchange(model, ids);
            }
            //op.UnitOfWork.SaveChanges();
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
