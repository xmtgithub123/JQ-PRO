using Common;
using DataModel.Models;
using DBSql;
using JQ.Util;
using JQ.Web;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Engineering.Controllers
{
    [Description("ProjManage")]
    public class ProjManageController : CoreController
    {
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            if (Request["inType"] == "2" && Request["name"] == userInfo.EmpName)
                ViewBag.isAdd = "['add','edit','del', 'export']";
            else
                ViewBag.isAdd = "['edit','del', 'export']";
            ViewBag.CurrentEmpID = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjManage")));

            return View();
        }
        #endregion

        #region 列表json
        [Description("列表json")]
        [HttpPost]
        public string json()
        {
            int recordcount = 0;
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            ProjManageData projBus = new ProjManageData();
            DataTable list = projBus.GetList(PageModel.PageSize, PageModel.PageIndex, PageModel.PredicateValue != null ? PageModel.PredicateValue[0].ToString() : "", "Id DESC", out recordcount);
            //var result = list.Select("EngineeringId = " + TypeHelper.parseInt(Request["EngineeringId"]));

            if (Request["inType"] == "2")
            {
                var result = from p in list.AsEnumerable()
                             where p.Field<int>("EngineeringId") == TypeHelper.parseInt(Request["EngineeringId"])
                             select new
                             {
                                 EngineeringName = p.Field<string>("EngineeringName"),
                                 Cost = p.Field<string>("Cost"),
                                 EngineeringId = p.Field<int>("EngineeringId"),
                                 Id = p.Field<int>("Id"),
                                 Safe = p.Field<string>("Safe"),
                                 Finished = p.Field<string>("Finished"),
                                 Doc = p.Field<string>("Doc"),
                                 Note = p.Field<string>("Note"),
                                 CreatorEmpId = p.Field<int>("CreatorEmpId"),
                                 CreatorEmpName = p.Field<string>("CreatorEmpName"),
                                 CreatorTime = p.Field<DateTime>("CreatorTime"),
                                 FlowID = p.Field<int?>("FlowID"),
                                 FlowName = p.Field<string>("FlowName"),
                                 FlowStatusID = p.Field<int?>("FlowStatusID"),
                                 FlowStatusName = p.Field<string>("FlowStatusName"),
                                 FlowTurnedEmpIDs = p.Field<string>("FlowTurnedEmpIDs")
                             };
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = result.Count(),
                    rows = result.Skip(PageModel.PageSize * (PageModel.PageIndex - 1)).Take(PageModel.PageSize)
                });
            }

            return JavaScriptSerializerUtil.getJson(new
            {
                total = recordcount,
                rows = list
            });
        }
        #endregion

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new ProjManage();

            model.EngineeringID = string.IsNullOrEmpty(Request["EngID"]) ? 0 : Convert.ToInt32(Request["EngID"]);
            ViewBag.CreatorEmpId = userInfo.EmpID;
            int empid = TypeHelper.parseInt(Request["empid"]);
            if (empid != 0)
            {
                ViewBag.ProjName = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == empid).ProjName;
                ViewBag.ProjNumber = new DBSql.Engineering.EmpManage().FirstOrDefault(p => p.Id == empid).ProjNumber;
            }
            else
            {
                ViewBag.ProjNumber = "";
                ViewBag.ProjName = "";
            }
            return View("info", model);
        }
        #endregion

        #region 编辑
        [Description("编辑")]
        public ActionResult edit(int id)
        {
            ProjManageData projBus = new ProjManageData();
            var model = DTHelper.GetEntity<ProjManage>(projBus.Get(id));

            ViewBag.CreatorEmpId = model.CreatorEmpId;
            ViewBag.ProjName = new DBSql.Project.Project().FirstOrDefault(p => p.Id == model.EngineeringID).ProjName;
            ViewBag.ProjNumber = new DBSql.Project.Project().FirstOrDefault(p => p.Id == model.EngineeringID).ProjNumber;

            return View("info", model);
        }
        #endregion

        #region 删除
        [Description("删除")]
        public ActionResult del(int[] id)
        {
            ProjManageData projBus = new ProjManageData();
            var flowIds = new DBSql.PubFlow.Flow().GetList(p => p.Id > 0).Where(p => p.FlowRefTable == "ProjManage" && id.Contains(p.FlowRefID)).Select(p => p.Id);
            var flowNodeExe = new DBSql.PubFlow.FlowNodeExe();
            flowNodeExe.Delete(p => flowIds.Contains(p.FlowID));
            flowNodeExe.UnitOfWork.SaveChanges();
            var flowNode = new DBSql.PubFlow.FlowNode();
            flowNode.Delete(p => flowIds.Contains(p.FlowID));
            flowNode.UnitOfWork.SaveChanges();
            var flow = new DBSql.PubFlow.Flow();
            flow.Delete(p => flowIds.Contains(p.Id));
            flow.UnitOfWork.SaveChanges();

            int reuslt = projBus.Delete(id);
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }
        #endregion

        #region 保存
        [Description("保存")]
        [HttpPost]
        public ActionResult save(int Id)
        {
            ProjManageData projBus = new ProjManageData();
            ProjManage model = new ProjManage();

            if (Id > 0)
            {
                model = DTHelper.GetEntity<ProjManage>(projBus.Get(Id));
            }
            model.MvcSetValue();

            model.CreatorEmpId = userInfo.EmpID;
            model.CreatorEmpName = userInfo.EmpName;
            model.CreatorTime = DateTime.Now;

            int reuslt = 0;
            if (model.Id > 0)
            {
                reuslt = projBus.Update(model);
            }
            else
            {
                reuslt = model.Id = projBus.Insert(model);
            }

            if (Id <= 0)
            {
                using (var ba = new DBSql.Sys.BaseAttach())
                {
                    ba.MoveFile(model.Id, userInfo.EmpID, userInfo.EmpName);
                }
            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doSuccess(model.Id, "操作失败");
            return Json(dr);
        }
        #endregion
    }
}
