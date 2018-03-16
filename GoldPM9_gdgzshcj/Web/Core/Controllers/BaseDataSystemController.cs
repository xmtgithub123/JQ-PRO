using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Core.Controllers
{
    [Description("BaseDataSystem")]
    public class BaseDataSystemController : CoreController
    {
        private DBSql.Sys.BaseDataSystem op = new DBSql.Sys.BaseDataSystem();
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
                PageModel.SelectOrder = "BaseID desc";
            }
            var list = op.GetPagedDynamic(PageModel).ToList();

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
            var model = new BaseDataSystem();
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
            int reuslt = 1;
            op.Delete(s => id.Contains(s.BaseID));
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int BaseID)
        {
            var model = new BaseDataSystem();
            if (BaseID > 0)
            {
                model = op.Get(BaseID);
            }
            model.MvcSetValue();

            int reuslt = 0;
            if (model.BaseID > 0)
            {
                op.Edit(model);
            }
            else
            {
                op.Add(model);
            }
            op.UnitOfWork.SaveChanges();
            reuslt = model.BaseID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.BaseID, "操作成功") : DoResultHelper.doSuccess(model.BaseID, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 树json write by 吴俊鹏
        /// <summary>
        /// 树json write by 吴俊鹏
        /// </summary>
        /// <param name="engName"></param>
        /// <param name="isSel">是否包含选择项</param>
        /// <returns></returns>
        [Description("树json")]
        public ActionResult treejson(string engName, string isfilter = "0")
        {
            if (string.IsNullOrEmpty(engName))
            {
                return null;
            }
            var model = new DBSql.Sys.BaseDataSystem().GetBaseDataInfoByEngName(engName);
            var list = op.getTreeData(model, isfilter);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
