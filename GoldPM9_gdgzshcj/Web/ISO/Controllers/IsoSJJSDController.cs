using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;
using System.Web;

namespace Iso.Controllers
{
[Description("IsoSJJSD")]
public class IsoSJJSDController : CoreController
    {
        private DBSql.Iso.IsoSJJSD op = new DBSql.Iso.IsoSJJSD();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("Iso_SJJSD")));
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

            var per = PermissionList("IsoSJJSD");

            var There = QueryBuild<DataModel.Models.IsoSJJSD>.True();

            if (per.Contains("allview"))
            {

            }else if (per.Contains("dep"))
            {
                There = There.And(p => p.CreatorDepID == userInfo.EmpDepID);
            }else if (per.Contains("emp"))
            {
                There = There.And(p => p.CreatorEmpId == userInfo.EmpID);
            }

            var list = op.GetPagedList(There,PageModel).ToList();
            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "IsoSJJSD") on p.Id equals t1.FlowRefID into nodes
                            from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                p.Id,
                                p.Number,
                                p.ProjID,
                                p.ProjNumber,
                                p.ProjName,
                                p.ManageLevel,
                                p.SJEmpName,
                                p.SSTime,
                                p.JSWJ,
                                p.JSNR,
                                p.CreatorEmpId,
                                FlowID = temp == null ? 0 : temp.Id,
                                FlowName = temp == null ? "" : temp.FlowName,
                                FlowStatusID = temp == null ? 0 : temp.FlowStatusID,
                                FlowStatusName = temp == null ? "" : temp.FlowStatusName,
                                FlowXml = temp == null ? "" : temp.FlowXml
                            }).Select(m => new
                            {
                                m.Id,
                                m.Number,
                                m.ProjID,
                                m.ProjNumber,
                                m.ProjName,
                                m.ManageLevel,
                                m.SJEmpName,
                                m.JSWJ,
                                m.JSNR,
                                m.SSTime,
                                m.CreatorEmpId,
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
            var model = new IsoSJJSD();
            model.SJEmpId = userInfo.EmpID;
            model.SJEmpName = userInfo.EmpName;
            var per = PermissionList("Iso_SJJSD");
            model.TableNumber = "SCX02-15";
            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }

            ViewBag.EmpID = userInfo.EmpID;
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            var per = PermissionList("Iso_SJJSD");
            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }
            ViewBag.EmpID = model.CreatorEmpId;
            model.JSNote = HttpUtility.UrlPathEncode(model.JSNote);
            model.ModifyNote = HttpUtility.UrlPathEncode(model.ModifyNote);
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

                new DBSql.PubFlow.Flow().Delete(id, "IsoSJJSD");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoSJJSD");
                }

                reuslt = 1;
            }
            catch (System.Exception)
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
            var model = new IsoSJJSD();
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
