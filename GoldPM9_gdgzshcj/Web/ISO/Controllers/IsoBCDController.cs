using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Common.Data.Extenstions;
using System;

namespace Iso.Controllers
{
[Description("IsoBCD")]
public class IsoBCDController : CoreController
    {
        private DBSql.Iso.IsoBCD op = new DBSql.Iso.IsoBCD();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            ViewBag.CurrEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectCenterList")));
            return View();
        }

        public ActionResult selectBCFile()
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

            var per = PermissionList("Iso_XMBWL");

            var There = QueryBuild<DataModel.Models.IsoBCD>.True();
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

            var list = op.GetPagedList(There,PageModel).ToList();
            var showList = (from p in list
                            join t1 in op.DbContext.Set<DataModel.Models.Flow>().Where(f => f.FlowRefTable == "IsoBCD") on p.Id equals t1.FlowRefID into nodes
                            from temp in nodes.DefaultIfEmpty()
                            select new
                            {
                                p.Id,
                                p.Number,
                                p.ProjId,
                                p.ProjNumber,
                                p.ProjName,
                                p.BCS,
                                p.CDS,
                                p.JCNumber,
                                p.ZL,
                                p.CreatorEmpId,
                                p.ProjTypeName,
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
                                m.BCS,
                                m.CDS,
                                m.JCNumber,
                                m.ZL,
                                m.CreatorEmpId,
                                m.ProjTypeName,
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
            var model = new IsoBCD();
            model.CreatorEmpId = userInfo.EmpID;
            model.CreatorEmpName = userInfo.EmpName;
            ViewBag.CurrEmpID = userInfo.EmpID;

            DataModel.Models.DesTaskGroup dtg = new DBSql.Design.DesTaskGroup().Get(GetRequestValue<int>("taskGroupId"));
            if (dtg != null)
            {
                model.ProjId = dtg.ProjId;
                model.ProjName = dtg.ProjName;
                model.ProjNumber = dtg.ProjNumber;
                model.ProjPhaseId = GetRequestValue<string>("phaseID");
                model.ProjPhaseName = new DBSql.Sys.BaseData().Get(GetRequestValue<int>("phaseID")).BaseName;
                model.ZJNR = dtg.Id.ToString();
                ViewBag.TaskGroupId = model.ZJNR;
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
            ViewBag.CurrEmpID = model.CreatorEmpId;
            ViewBag.TaskGroupId = model.ZJNR;

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

                new DBSql.PubFlow.Flow().Delete(id, "IsoBCD");
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.Delete(id, "IsoBCD");
                }
                reuslt = 1;
            }
            catch (System.Exception ex)
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
            var model = new IsoBCD();
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

        #region 获取可报出文件
        public string GetBCFile(int id)
        {
            DataTable dt = new DBSql.Iso.IsoBCD().GetBCFile(id);
            return JavaScriptSerializerUtil.getJson(new
            {
                rows = dt
            });
        }
        #endregion

        public ActionResult selectJC()
        {
            return View("selectJC");
        }

        public string getJCs(int Id)
        {
            if (Id == 0)
                return "";
            var op1 = new DBSql.Design.DesTask();
            var op2 = new DBSql.Design.DesTaskGroup();
            var op3 = new DBSql.Iso.IsoSJBGD();
            var op4 = new DBSql.Project.Project();
            string Ids = "";
            if(Request["fromType"] == "1")
                Ids = op3.FirstOrDefault(p => p.Id == Id).JCName.Trim(',');
            else
                Ids = op.FirstOrDefault(p => p.Id == Id).JCName.Trim(',');
            string[] taskIds = Ids.Split(',');
            var list = op1.GetList(p => p.Id == -1).Select(p=> new {
                p.Id,
                p.TaskName,
                ProjName = op2.FirstOrDefault(o=>o.Id == p.TaskGroupId) == null ? "": op2.FirstOrDefault(o => o.Id == p.TaskGroupId).ProjName,
                ProjEmpId = op4.FirstOrDefault(o => o.Id == p.ProjId) == null ? "" : op4.FirstOrDefault(o => o.Id == p.ProjId).ProjEmpId.ToString(),
                ProjEmpName = op4.FirstOrDefault(o => o.Id == p.ProjId) == null ? "" : op4.FirstOrDefault(o => o.Id == p.ProjId).ProjEmpName
            });//初始化 空值
            for (int i = 0; i < taskIds.Length; i++)
            {
                int taskId = TypeHelper.parseInt(taskIds[i]);
                var listT = op1.GetList(p => p.Id == taskId).Select(p=>new
                {
                    p.Id,
                    p.TaskName,
                    ProjName = op2.FirstOrDefault(o => o.Id == p.TaskGroupId) == null ? "" : op2.FirstOrDefault(o => o.Id == p.TaskGroupId).ProjName,
                    ProjEmpId = op4.FirstOrDefault(o => o.Id == p.ProjId) == null ? "" : op4.FirstOrDefault(o => o.Id == p.ProjId).ProjEmpId.ToString(),
                    ProjEmpName = op4.FirstOrDefault(o => o.Id == p.ProjId) == null ? "" : op4.FirstOrDefault(o => o.Id == p.ProjId).ProjEmpName
                }).ToList();
                list = list.ToList().Union(listT);  //每次循环jcname中的id 找到对应的DesTask数据添加
            }
            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = list
            });
        }
    }
}
