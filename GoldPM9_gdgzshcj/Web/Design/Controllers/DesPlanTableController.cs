using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq.Expressions;
using Common.Data.Extenstions;

namespace Design.Controllers
{
    [Description("项目计划表")]
    public class DesPlanTableController : CoreController
    {
        private DBSql.Design.DesPlanTable op = new DBSql.Design.DesPlanTable();
        private DoResult doResult = DoResultHelper.doError("未知错误!");

        private class gridModel
        {
            public int fromId { get; set; }
            public string DangerName { get; set; }
            public string DangerContent { get; set; }
            public string DangerType { get; set; }
        }

        /// <summary>
        /// 项目计划表 保存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PlanTableInfoSave(int id)
        {
            int reuslt = 0;
            DBSql.Design.DesPlanTable _opPlanTable = new DBSql.Design.DesPlanTable();
            DesPlanTable planModel = new DesPlanTable();
            if (id > 0)
            {
                planModel = _opPlanTable.Get(id);
            }
            planModel.MvcSetValue();
            #region 处理表格数据
            List<gridModel> _list = new List<gridModel>();
            if (Request.Params["inputdesignDanger"] != null)
            {
                List<gridModel> DangerList = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<gridModel>>(Request.Params["inputdesignDanger"].ToString());
                if (DangerList.Count > 0)
                {
                    _list.AddRange(DangerList);
                }
            }
            if (Request.Params["inputdesignEmergency"] != null)
            {
                List<gridModel> EmergencyList = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<gridModel>>(Request.Params["inputdesignEmergency"].ToString());
                if (EmergencyList.Count > 0)
                {
                    _list.AddRange(EmergencyList);
                }
            }
            #endregion
            using (var trans = op.DbContext.Database.BeginTransaction())
            {
                try
                {
                    if (id > 0)
                    {
                        planModel.MvcDefaultEdit(userInfo.EmpID);
                        _opPlanTable.Edit(planModel);
                        _opPlanTable.DbContext.SaveChanges();
                        //插入ISOFormNode中
                        List<IsoFormNode> _NodeList = op.DbContext.Set<DataModel.Models.IsoFormNode>().Where(p => p.FormID == planModel.Id && (p.ColAttVal5 == "DangerType" || p.ColAttVal5 == "EmergencyType")).ToList();
                        foreach (IsoFormNode itNode in _NodeList)
                        {
                            op.DbContext.Set<DataModel.Models.IsoFormNode>().Remove(itNode);
                        }
                        op.DbContext.SaveChanges();

                        int indexRow = 0;
                        foreach (gridModel it in _list)
                        {
                            indexRow++;
                            DataModel.Models.IsoFormNode node = new IsoFormNode();
                            node.FormID = planModel.Id;
                            node.RefID = indexRow;
                            node.ColAttVal1 = it.DangerName;
                            node.ColAttVal2 = it.DangerContent;
                            node.ColAttVal5 = it.DangerType;
                            op.DbContext.Set<DataModel.Models.IsoFormNode>().Add(node);
                        }
                        op.DbContext.SaveChanges();
                        reuslt = planModel.Id;
                    }
                    else
                    {
                        planModel.MvcDefaultSave(userInfo.EmpID);
                        _opPlanTable.Add(planModel);
                        _opPlanTable.DbContext.SaveChanges();
                        //插入ISOFromNode
                        int indexRow = 0;
                        foreach (gridModel it in _list)
                        {
                            indexRow++;
                            DataModel.Models.IsoFormNode node = new IsoFormNode();
                            node.FormID = planModel.Id;
                            node.RefID = indexRow;
                            node.ColAttVal1 = it.DangerName;
                            node.ColAttVal2 = it.DangerContent;
                            node.ColAttVal5 = it.DangerType;
                            op.DbContext.Set<DataModel.Models.IsoFormNode>().Add(node);
                        }

                        var ba = new DBSql.Sys.BaseAttach();
                        ba.DbContextRepository(op.DbContext);
                        ba.MoveFile(planModel.Id, userInfo.EmpID, userInfo.EmpName);

                        op.DbContext.SaveChanges();
                        reuslt = planModel.Id;
                    }
                    trans.Commit();
                    reuslt = planModel.Id;
                }
                catch (System.Exception ex)
                {
                    trans.Rollback();
                    reuslt = -1;
                    throw;
                }

            }
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(planModel.Id, "操作成功") : DoResultHelper.doSuccess(planModel.Id, "操作失败");
            return Json(dr);
        }

        public string GetComBoContent()
        {
            var Thwere = QueryBuild<DataModel.Models.DesPlanTable>.True();
            if (Request.Params["Type"] != null && Request.Params["Type"] == "DangerType")
            {
                Thwere = Thwere.And(p => p.DangerType != "");
            }
            if (Request.Params["Type"] != null && Request.Params["Type"] == "EmergencyType")
            {
                Thwere = Thwere.And(p => p.EmergencyType != "");
            }
            var list = op.GetList(Thwere).Select(p => new { p.DangerType, p.EmergencyType });
            return JavaScriptSerializerUtil.getJson(list);
        }

        public string GetgridContent()
        {
            DBSql.Iso.IsoFormNode NodeList = new DBSql.Iso.IsoFormNode();
            var Thwere = QueryBuild<DataModel.Models.IsoFormNode>.True();
            if (Request.Params["formId"] == null)
            {
                return JavaScriptSerializerUtil.getJson(DoResultHelper.doSuccess("-1", "缺少关键主键"));
            }
            int formId = Common.ExtensionMethods.Value<int>(Request.Params["formId"]);
            Thwere = Thwere.And(p => p.FormID == formId);
            if (Request.Params["Type"] != null && Request.Params["Type"] == "DangerType")
            {
                Thwere = Thwere.And(p => p.ColAttVal5 == "DangerType");
            }
            if (Request.Params["Type"] != null && Request.Params["Type"] == "Emergency")
            {
                Thwere = Thwere.And(p => p.ColAttVal5 == "EmergencyType");
            }
            var list = NodeList.GetList(Thwere).Select(p => new { fromId = p.RefID, DangerName = p.ColAttVal1, DangerContent = p.ColAttVal2, DangerType = p.ColAttVal5 });
            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = list
            });
        }

        public string GetDangerContent()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "FormID  ASC";
            }
            DBSql.Iso.IsoFormNode NodeList = new DBSql.Iso.IsoFormNode();
            var Thwere = QueryBuild<DataModel.Models.IsoFormNode>.True();
            if (Request.Params["type"] != null && Request.Params["type"] == "designDanger")
            {
                Thwere = Thwere.And(p => p.ColAttVal5 == "DangerType");
            }
            else if (Request.Params["type"] != null && Request.Params["type"] == "designEmergency")
            {
                Thwere = Thwere.And(p => p.ColAttVal5 == "EmergencyType");
            }
            else
            {
                Thwere = Thwere.And(p => p.ColAttVal5 == "");
            }
            if (Request.Params["Id"] != null)
            {
                int id = Common.ExtensionMethods.Value<int>(Request.Params["Id"]);
                Thwere = Thwere.And(p => p.FormID == id);
            }
            var list = NodeList.GetPagedList(Thwere, PageModel).Select(p => new { fromId = p.FormID + "00" + p.RefID, DangerName = p.ColAttVal1, DangerContent = p.ColAttVal2, DangerType = p.ColAttVal5 });
            return JavaScriptSerializerUtil.getJson(new
            {
                total = list.Count(),
                rows = list
            });
        }

        public ActionResult selectPlan()
        {
            return View();
        }

        public string JsonPlan()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();

            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "p.Id  desc";
            }
            PageModel.SelectCondtion.Add("ProjDeleterEmpId", "");
            PageModel.SelectCondtion.Add("groupDeleterEmpId", "");

            var list = op.GetDesPlanList(PageModel);
            return JavaScriptSerializerUtil.getJson(new
            {
                total = PageModel.PageTotleRowCount,
                rows = list
            });
        }
    }
}
