using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using Common.Data.Extenstions;

namespace Iso.Controllers
{
[Description("IsoConstructionSet")]
public class IsoConstructionSetController : CoreController
    {
        private DBSql.Iso.IsoConstructionSet op = new DBSql.Iso.IsoConstructionSet();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.EmpId = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
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
            var Thwere = QueryBuild<DataModel.Models.IsoConstructionSet>.True();
            List<string> permissionList = PermissionList("ProjectCenterList");

            if (!permissionList.Contains("dep") && !permissionList.Contains("allview"))
            {
                Thwere = Thwere.And(p => p.CreatorEmpId == userInfo.EmpID);//个人查看权
            }
            else
            {
                if (!permissionList.Contains("allview") && permissionList.Contains("dep"))
                {
                    Thwere = Thwere.And(p => p.CreatorDepId == userInfo.EmpDepID);//部门查看权
                }
            }
            Thwere = Thwere.And(p => p.DeleterEmpId == 0);

            var list = op.GetPagedList(Thwere,PageModel).ToList();
            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "IsoConstructionSet") on p.Id equals t1.FlowRefID into nodes
                            from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                Id = p.Id,
                                Number = p.Number,
                                ProjNumber = p.ProjNumber,
                                ProjName=p.ProjName,
                                ConstructionCompany=p.ConstructionCompany,
                                ConstructionResponsibility=p.ConstructionResponsibility,
                                OrganizationCompany=p.OrganizationCompany,
                                OrganizationContacts=p.OrganizationContacts,
                                SetDate=p.SetDate,
                                SetValue=p.SetValue,
                                SetResponsibility=p.SetResponsibility,
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
                                m.ProjNumber,
                                m.ProjName,
                                m.ConstructionCompany,
                                m.ConstructionResponsibility ,
                                m.OrganizationCompany,
                                m.OrganizationContacts ,
                                m.SetDate,
                                m.SetValue,
                                m.SetResponsibility,
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
            var model = new IsoConstructionSet();
            ViewBag.CreateEmpId = userInfo.EmpID;
            model.TableNumber = "SCX04-01";

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                model.ProjId = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
            }

            var per = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
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
            ViewBag.CreateEmpId = model.CreatorEmpId;

            var per = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
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

                new DBSql.PubFlow.Flow().Delete(id, "IsoConstructionSet");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoConstructionSet");
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
            var model = new IsoConstructionSet();
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
