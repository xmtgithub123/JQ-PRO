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
    [Description("IsoGCDZKCTJDJZ")]
    public class IsoGCDZKCTJDJZController : CoreController
    {
        private DBSql.Iso.IsoGCDZKCTJDJZ op = new DBSql.Iso.IsoGCDZKCTJDJZ();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrEmpID = userInfo.EmpID;
            if (GetRequestValue<string>("type") == "xl")
            {
                ViewBag.FormType = "xl";
            }
            else
            {
                ViewBag.FormType = "dxjz";
            }
            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {

            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            var There = QueryBuild<DataModel.Models.IsoGCDZKCTJDJZ>.True();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id desc";
            }
            var formType = ViewBag.FormType;
            if (GetRequestValue<string>("type") == "xl")
            {
                There = There.And(p => p.FormType == "xl");
            }
            else
            {
                There = There.And(p => p.FormType == "dxjz");
            }

            var list = op.GetPagedList(There, PageModel).ToList();
            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "IsoGCDZKCTJDJZ") on p.Id equals t1.FlowRefID into nodes
                            from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                p.Id,
                                p.Number,
                                p.ProjID,
                                p.ProjNumber,
                                p.ProjName,
                                p.ProjPhaseName,
                                p.CreationTime,
                                p.CreatorEmpName,
                                p.CreatorEmpId,
                                p.EngGCDD,
                                p.EngCGJFRQ,
                                p.CustLinkManName,
                                p.CustName,
                                p.EngSJR,
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
                                m.ProjPhaseName,
                                m.CreationTime,
                                m.CreatorEmpName,
                                m.CreatorEmpId,
                                m.EngGCDD,
                                m.EngCGJFRQ,
                                m.CustLinkManName,
                                m.CustName,
                                m.EngSJR,
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

            var model = new IsoGCDZKCTJDJZ();
            if (GetRequestValue<string>("type") == "xl")
            {
                ViewBag.FormType = "xl";
                ViewBag.FormTypeName = "线路";
                model.SJSM = @" 1、线路总长             地点（附图）        等级
2、拟用断面尺寸(附图)         流速           管道压力
3、拟定基底高程(附纵断)
4、重点地段要求
5、其它";
                model.FTSM = @"1、总平面位置图          比例     份      张
2、重点地段平面位置图    比例     份      张
3、纵断                  比例     份      张
4、横断                  比例     份      张";
                model.CGTSYQ = @"（提供1份盖有勘察单位公章并装订成册的成果文件）
（提供1份与上述盖章文件一致的电子文件）";
                model.TableNumber = "SCX02-08";

            }
            else
            {
                ViewBag.FormType = "dxjz";
                ViewBag.FormTypeName = "单项建筑";
                model.SJSM = @"1、建筑名称             地点（附图）           等级
2、拟用基础型式或尺寸（可附图）         
3、拟定基底高程(桥梁专业可略)
4、估计荷载
5、其它";
                model.FTSM = @"1、总平面位置图            比例     份      张
2、其它：";
                model.CGTSYQ = @"（提供1份盖有勘察单位公章并装订成册的成果文件）
（提供1份与上述盖章文件一致的电子文件）";
                model.TableNumber = "SCX02-09 ";
            }
            ViewBag.CurrEmpID = userInfo.EmpID;

            //设计人员
            var SJR = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.设计, 0, 0, 0)
                       select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(SJR);

            var per = PermissionList("ProjectCenterList");

            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                model.ProjID = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
            }

            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            var model = op.Get(id);
            if (model.FormType == "xl")
            {
                ViewBag.FormType = "xl";
                ViewBag.FormTypeName = "线路";
            }
            else
            {
                ViewBag.FormType = "dxjz";
                ViewBag.FormTypeName = "单项建筑";
            }

            //设计人员
            var SJR = (from m in new DBSql.Sys.BaseQualification().GetQualificationEmployee((int)DataModel.NodeType.设计, 0, 0, 0)
                       select new { m.EmpID, m.EmpName }).Distinct();
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(SJR);

            var per = PermissionList("ProjectCenterList");

            if (per.Contains("allDown"))
            {
                ViewBag.Permission = 1;
            }
            else
            {
                ViewBag.Permission = 0;
            }

            ViewBag.CurrEmpID = model.CreatorEmpId;
            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            int result = 0;
            try
            {
                op.Delete(s => id.Contains(s.Id));
                new DBSql.PubFlow.Flow().Delete(id, "IsoGCDZKCTJDJZ");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoGCDZKCTJDJZ");
                }
                result = 1;
            }
            catch (System.Exception ex)
            {
                result = 0;
            }

            DoResult dr = result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doError("操作失败");

            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            var model = new IsoGCDZKCTJDJZ();
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
            reuslt = model.Id;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
