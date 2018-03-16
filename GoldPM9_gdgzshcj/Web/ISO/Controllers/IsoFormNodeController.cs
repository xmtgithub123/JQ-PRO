using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
namespace Iso.Controllers
{
[Description("IsoFormNode")]
public class IsoFormNodeController : CoreController
    {
        private DBSql.Iso.IsoFormNode op = new DBSql.Iso.IsoFormNode();
        private DoResult doResult = DoResultHelper.doError("未知错误!");
        private DBSql.Sys.BaseData baseData = new DBSql.Sys.BaseData();

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
                PageModel.SelectOrder = "FormID desc";
            }
            var list = op.GetPagedList(PageModel).ToList();
            
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
        #endregion

        #region 设计评审记录Json"
        [Description("设计评审记录Json")]

        [HttpPost]
        public string ReviewJson()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "FormID desc";
            }
            
            var list = op.GetPagedList(p=>p.ColAttVal5== "IsoFormDesOutPutReview", PageModel).ToList();
            if(Request.QueryString["FormID"]!=null)
            {
                long FormID = (long)TypeHelper.parseInt(Request.QueryString["FormID"]);
                list = op.GetPagedList(p => p.ColAttVal5 == "IsoFormDesOutPutReview"&& p.FormID == FormID, PageModel).ToList();
            }
            var targetList = from item in list
                             let special= baseData.Get(item.ColAttType1)
                             select new
                             {
                                 item.FormID,
                                 item.RefID,
                                 SpecialId=item.ColAttType1,//专业ID
                                 SpecialName= special==null?"":special.BaseName,//专业名称
                                 ReviewTarget=item.ColAttVal1,//评审对象
                                 ReviewContent=item.ColAttVal2,//评审内容
                                 ReviewResult=item.ColAttVal3,//评审结果
                                 ReviewSugg=item.ColAttVal4//评审意见

                             };
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = targetList
            });

        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new IsoFormNode();
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
            op.Delete(s => id.Contains(s.FormID));
            op.UnitOfWork.SaveChanges();
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        } 
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int FormID)
        {
            var model = new IsoFormNode();
            if (FormID > 0)
            {
                model = op.Get(FormID);
            }
            model.MvcSetValue();

            int reuslt = 0;                      
            if (model.FormID > 0)
            {
				op.Edit(model);
            }
            else
            {
                op.Add(model);
            }
			op.UnitOfWork.SaveChanges();
			reuslt = model.FormID ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FormID, "操作成功") : DoResultHelper.doSuccess(model.FormID, "操作失败");
            return Json(dr);
        } 
        #endregion
    }
}
