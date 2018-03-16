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
[Description("OaEquipStock")]
public class OaEquipStockController : CoreController
    {
        private DBSql.Oa.OaEquipStock op = new DBSql.Oa.OaEquipStock();
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
                PageModel.SelectOrder = "Id desc";
            }

            int EquipFormID = GetRequestValue<int>("EquipFormID");
            string EquipFormType = GetRequestValue<string>("EquipFormType");
            //int EquipOrOffice = GetRequestValue<int>("EquipOrOffice");

            var list = op.GetList(p => p.EquipFormID == EquipFormID&&p.EquipFormType==EquipFormType).Select(p => new
            {
                Id=p.Id,
                EquipId = p.EquipID,
                EquipNumber = p.FK_OaEquipStock_EquipID.EquipNumber,
                EquipName=p.FK_OaEquipStock_EquipID.EquipName,
                EquipModel=p.FK_OaEquipStock_EquipID.EquipModel,
                Number=p.EquipCount,
                EquipFormID
            }).ToList();

            var equipList = from t1 in list
                            join t2 in op.DbContext.Set<OaEquipment>() on t1.EquipId equals t2.Id
                            select t2.EquipOrOffice;
            var EquipOrOffice = equipList == null ? 0 : equipList.First();
            //(from t1 in list
            //                join t2 in op.DbContext.Set<OaEquipUse>() on t1.EquipFormID equals t2.Id
            //                select t2.EquipOrOffice).FirstOrDefault();
            var result = from p in list
                         join t1 in op.DbContext.Set<OaEquipment>().Where(p => p.EquipOrOffice == EquipOrOffice) on p.EquipId equals t1.Id
                         select new
                         {
                             Id = p.Id,
                             EquipId= p.EquipId,
                             EquipNumber = p.EquipNumber,
                             EquipName = p.EquipName,
                             EquipModel = p.EquipModel,
                             Number = p.Number
                         };


            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = result
            });
        } 
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new OaEquipStock();
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
            var model = new OaEquipStock();
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
			reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        } 
        #endregion
    }
}
