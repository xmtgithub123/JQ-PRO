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

namespace Iso.Controllers
{
[Description("IsoSJTJTGD")]
public class IsoSJTJTGDController : CoreController
    {
        private DBSql.Iso.IsoSJTJTGD op = new DBSql.Iso.IsoSJTJTGD();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("Iso_SJTJTG")));
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
            var per = PermissionList("Iso_SJTJTG");

            var There = QueryBuild<DataModel.Models.IsoSJTJTGD>.True();

            if (per.Contains("allview"))
            {

            }
            else if (per.Contains("dep"))
            {
                There = There.And(p => p.CreatorDepId == userInfo.EmpDepID);
            }
            else if (per.Contains("emp"))
            {
                There = There.And(p => p.CreatorEmpId == userInfo.EmpID);
            }

            var list = op.GetPagedList(There, PageModel).ToList();

            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "IsoSJTJTGD") on p.Id equals t1.FlowRefID into nodes
                            from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                p.Id,
                                p.Number,
                                p.ProjId,
                                p.ProjNumber,
                                p.ProjName,
                                p.CreatorEmpId,
                                p.TGSpecId,
                                p.TGSpecName,
                                p.TGSpecHeaderId,
                                p.TGSpecHeader,
                                p.TGTime,
                                p.JSSpecId,
                                p.JSSpecName,
                                p.JSSpecHeaderId,
                                p.JSSpecHeader,
                                p.JSTime,
                                FlowID = temp == null ? 0 : temp.Id,
                                FlowName = temp == null ? "" : temp.FlowName,
                                FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                                FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                                FlowXml = temp == null ? "" : temp.FlowXml
                            }).Select(m => new
                            {
                                m.Id,
                                m.Number,
                                m.ProjId,
                                m.ProjNumber,
                                m.ProjName,
                                m.CreatorEmpId,
                                m.TGSpecId,
                                m.TGSpecName,
                                m.TGSpecHeaderId,
                                m.TGSpecHeader,
                                m.TGTime,
                                m.JSSpecId,
                                m.JSSpecName,
                                m.JSSpecHeaderId,
                                m.JSSpecHeader,
                                m.JSTime,
                                m.FlowID,
                                m.FlowName,
                                m.FlowStatusID,
                                m.FlowStatusName,
                                m.FlowXml,
                                FlowTurnedEmpIDs = JQ.Util.TypeParse.GetTurnedEmpIDs(m.FlowXml)
                            });

            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = showList
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new IsoSJTJTGD();
            ViewBag.CurrEmpID = userInfo.EmpID;
            model.TableNumber = "SCX02-11";
            model.TGTime = DateTime.Now;
            var per = JavaScriptSerializerUtil.getJson((PermissionList("Iso_SJTJTG")));
            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            ViewBag.CurrEmpID = model.CreatorEmpId;

            var per = JavaScriptSerializerUtil.getJson((PermissionList("Iso_SJTJTG")));
            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
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
                op.Delete(s => id.Contains(s.Id));
                op.UnitOfWork.SaveChanges();

                new DBSql.PubFlow.Flow().Delete(id, "IsoSJTJTGD");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoSJTJTGD");
                }

                reuslt = 1;
            }
            catch (Exception)
            {
                reuslt = 0;
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doError("操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new IsoSJTJTGD();
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
			if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }
			reuslt = model.Id ;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
