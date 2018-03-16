using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Bussiness.Controllers
{
[Description("BusProjContractRelation")]
public class BusProjContractRelationController : CoreController
    {
        private DBSql.Bussiness.BusProjContractRelation op = new DBSql.Bussiness.BusProjContractRelation();
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
                PageModel.SelectOrder = "ID,desc";
            }
            var list = op.GetPagedList(PageModel).ToList();
            
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
            var model = new BusProjContractRelation();
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
            op.Delete(s => id.Contains(s.ID));
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int ID)
        {
            var model = new BusProjContractRelation();
            if (ID > 0)
            {
                model = op.Get(ID);
            }
            model.MvcSetValue();

            int reuslt = 0;                      
            if (model.ID > 0)
            {
				op.Edit(model);
            }
            else
            {
                op.Add(model);
				reuslt = model.ID ;
            }
			op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.ID, "操作成功") : DoResultHelper.doSuccess(model.ID, "操作失败");
            return Json(dr);
        } 
        #endregion
    }
}
