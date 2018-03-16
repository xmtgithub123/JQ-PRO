using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using JQ.Util;
using JQ.Web;
using DataModel.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Reflection;
using Common.Data.Extenstions;
using System.Xml;
using System.Data;

namespace Iso.Controllers
{
    [Description("IsoForm")]
    public class IsoFormController : CoreController
    {
        private DBSql.Iso.IsoForm op = new DBSql.Iso.IsoForm();
        private DBSql.PubFlow.Flow flow = new DBSql.PubFlow.Flow();
        private DBSql.Design.DesExch desExch = new DBSql.Design.DesExch();


        private DoResult doResult = DoResultHelper.doError("未知错误!");

        #region 列表
        [Description("列表")]
        public ActionResult list()
        {
            return View();
        }
        #endregion

        [Description("在办表单")]
        public ActionResult listUnFinish()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("InHandQualityTable")));
            ViewBag.EmpId = userInfo.EmpID;
            return View();
        }

        [Description("已完表单")]
        public ActionResult listFinish()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("FinishedQualityTable")));
            return View();
        }

        [Description("项目表单")]
        public ActionResult ProjFormList()
        {
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectQualityTable")));
            return View();
        }

        public ActionResult ProjFormDetail(int projID, int taskGroupId)
        {
            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            //通过taskGroupID找到PhaseID
            if (taskGroupId > 0)
            {
                var taskGroup = new DBSql.Design.DesTaskGroup().Get(taskGroupId);
                if (taskGroup != null)
                {
                    ViewBag.PhaseID = taskGroup.TaskGroupPhaseId;
                }
                else
                {
                    ViewBag.PhaseID = 0;
                }
            }
            else
            {
                ViewBag.PhaseID = 0;
            }
            return View();
        }

        public ActionResult IsoFormDetail(int projID, int taskGroupId)
        {
            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            //通过taskGroupID找到PhaseID
            if (taskGroupId > 0)
            {
                var taskGroup = new DBSql.Design.DesTaskGroup().Get(taskGroupId);
                if (taskGroup != null)
                {
                    ViewBag.PhaseID = taskGroup.TaskGroupPhaseId;
                }
                else
                {
                    ViewBag.PhaseID = 0;
                }
            }
            else
            {
                ViewBag.PhaseID = 0;
            }
            return View();
        }

        public string getAllFormJson()
        {
            return JavaScriptSerializerUtil.getJson(new DBSql.PubFlow.FlowModel().GetProjectFlow());
        }

        public string GetSelectList()
        {
            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            queryContext.SelectOrder = "id DESC";
            var TWhere = QueryBuild<DataModel.Models.Flow>.True();
            TWhere = TWhere.And(p => p.FlowStatusID != (int)DataModel.FlowStatus.审批结束);

            List<string> result = PermissionList("InHandQualityTable");
            if (!(result.Contains("allview") || result.Contains("alledit")))
            {
                if (result.Contains("dep"))
                {
                    TWhere = TWhere.And(p => p.CreatorDepId == userInfo.EmpDepID);
                }
                else
                {
                    TWhere = TWhere.And(p => p.CreatorEmpId == userInfo.EmpID);
                }
            }

            var list = flow.GetPagedList(TWhere, queryContext).ToList();
            var a = from f in list
                    join x in flow.DbContext.Set<DataModel.Models.FlowNode>().Where(p => p.FlowNodeStatusID == (int)DataModel.NodeStatus.轮到) on f.Id equals x.FlowID into nodes
                    from temp in nodes.DefaultIfEmpty()
                    select new
                    {
                        f.Id,
                        FlowName = f.FlowName,
                        CreationTime = f.CreationTime,
                        CreatorEmpName = f.CreatorEmpName,
                        FlowStatusID = f.FlowStatusID,
                        FlowStatusName = f.FlowStatusName,

                        FlowUrl = string.Format(f.FlowUrl, f.FlowRefID, 0, 0),
                        NodeState = userInfo.EmpID == temp.FlowNodeEmpId ? "处理" : "查看"
                    };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = queryContext.PageTotleRowCount,
                rows = a.ToList()
            });
        }

        public string GetSelectFinishList()
        {
            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            queryContext.SelectOrder = "id DESC";

            Expression<Func<Flow, bool>> predicate = GetExpression(Request);
            List<string> result = PermissionList("FinishedQualityTable");
            if (!(result.Contains("allview") || result.Contains("alledit")))
            {
                if (result.Contains("dep"))
                {
                    predicate = predicate.And(p => p.CreatorDepId == userInfo.EmpDepID);
                }
                else
                {
                    predicate = predicate.And(p => p.CreatorEmpId == userInfo.EmpID);
                }
            }

            var list = flow.GetPagedList(predicate, queryContext).ToList();

            var a = from f in list
                    select new
                    {
                        f.Id,
                        f.FlowName,
                        //FlowName = f.FlowName,
                        CreationTime = f.CreationTime,
                        CreatorEmpName = f.CreatorEmpName,
                        FlowStatusID = f.FlowStatusID,
                        FlowStatusName = f.FlowStatusName,
                        FlowUrl = string.Format(f.FlowUrl, f.FlowRefID, 0, 0),
                        //FlowStatusName1 = (f.FlowStatusID == 1 ? "未提交审批" : "123"),
                    };

            return JavaScriptSerializerUtil.getJson(new
            {
                total = queryContext.PageTotleRowCount,
                rows = a.ToList()
            });
        }

        public string GetFormsByProjID()
        {
            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            if (!string.IsNullOrEmpty(Request.Form["text"]))
            {
                queryContext.TextCondtion = Request.Form["text"];
            }

            if (!string.IsNullOrEmpty(Request.Form["FormIDs[]"]))
            {
                queryContext.SelectCondtion.Add("FlowModelIDs", Request.Form["FormIDs[]"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["FlowStatus"]))
            {
                queryContext.SelectCondtion.Add("FlowStatus", JQ.Util.TypeParse.parse<int>(Request.Form["FlowStatus"]));
            }
            using (var dt = op.GetList(queryContext, JQ.Util.TypeParse.parse<int>(Request.Form["projectID"]), JQ.Util.TypeParse.parse<int>(Request.Form["phaseID"])))
            {
                dt.Columns.Add("DialogWidth");
                dt.Columns.Add("DialogHeight");
                dt.Columns.Add("NodeState");
                foreach (DataRow row in dt.Rows)
                {
                    row["FlowUrl"] = string.Format(row["FlowUrl"].ToString(), row["FlowRefID"].ToString(), "0", "0");
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(row["FlowXml"].ToString());
                    var xDialog = (XmlElement)xmlDocument.SelectSingleNode("Root/ProcessDialog");
                    if (xDialog == null)
                    {
                        row["DialogWidth"] = 800;
                        row["DialogHeight"] = 600;
                    }
                    else
                    {
                        if (xDialog.GetAttribute("Width") != "")
                        {
                            row["DialogWidth"] = JQ.Util.TypeParse.parse<int>(xDialog.GetAttribute("Width"));
                        }
                        else
                        {
                            row["DialogWidth"] = 800;
                        }
                        if (xDialog.GetAttribute("Height") != "")
                        {
                            row["DialogHeight"] = JQ.Util.TypeParse.parse<int>(xDialog.GetAttribute("Height"));
                        }
                        else
                        {
                            row["DialogHeight"] = 600;
                        }
                    }
                    row["NodeState"] = userInfo.EmpID.ToString().Equals(row["FlowNodeEmpId"].ToString()) ? "处理" : "查看";

                }
                dt.Columns.Remove("FlowXml");
                return JavaScriptSerializerUtil.getJson(new
                {
                    total = queryContext.PageTotleRowCount,
                    rows = dt
                });
            }
        }

        DBSql.PubFlow.Flow opFlow = new DBSql.PubFlow.Flow();
        DBSql.PubFlow.FlowNode opFlowNode = new DBSql.PubFlow.FlowNode();
        public string GetFormsByProjID1()
        {
            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            int projId = TypeParse.parse<int>(Request.Form["projectID"]);
            string phaseId = Request["phaseID"];
            var l1 = new DBSql.Iso.IsoSJCH().GetList(p =>p.DeleterEmpId == 0 && p.ProjID == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName,p.CreatorEmpId });
            var l1f = from p in l1
                      join o in opFlow.GetList(i => i.FlowRefTable == "IsoSJCH") on p.Id equals o.FlowRefID into oList
                      from oResult in oList.DefaultIfEmpty()
                      join u in opFlowNode.GetList(y => y.FlowNodeStatusID == 29) on oResult == null ? 0 : oResult.Id equals u.FlowID into uList
                      from uResult in uList.DefaultIfEmpty()
                      select new
                      {
                          ID = p.Id,
                          p.ProjName,
                          p.ProjPhaseName,
                          p.CreationTime,
                          p.CreatorEmpId,
                          p.CreatorEmpName,
                          FlowId = oResult == null ? 0:oResult.Id,
                          FlowUrl = string.Format("Iso/IsoSJCH/edit?id={0}&flowNodeID={1}", p.Id, "0"),// : string.Format(oResult.FlowUrl.ToString(), p.Id, "0"),
                          FlowTitle = p.ProjName + "的设计策划单",
                          FlowStatusName = oResult == null ? "未审批" : oResult.FlowStatusName,
                          FlowName = "设计策划单",
                          FlowStatusID = oResult == null ? 0 : oResult.FlowStatusID,
                          FlowNodeEmpId = uResult == null ? 0 : uResult.FlowNodeEmpId,
                          //如果没有流程 判断是否自己创建  如果是 则处理
                          //如果有流程判断流程是否结束 如果结束 则查看
                          //如果流程没有结束 判断创建人name是否是自己 如果是处理
                          NodeState = oResult == null ? (p.CreatorEmpName == userInfo.EmpName ? "处理" : "查看") : oResult.FlowStatusName == "审批结束" ? "查看" : userInfo.EmpID.Equals(uResult == null ? (p.CreatorEmpName == userInfo.EmpName ? userInfo.EmpID : -1) : uResult.FlowNodeEmpId) ? "处理" : "查看",
                      };

            var l3 = new DBSql.Iso.IsoRemark ().GetList(p => p.DeleterEmpId == 0 && p.ProjId == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l4 = new DBSql.Iso.IsoZYZDJYB().GetList(p => p.DeleterEmpId == 0 && p.ProjId == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l5 = new DBSql.Iso.IsoSJPSJYB().GetList(p => p.DeleterEmpId == 0 && p.ProjId == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l6 = new DBSql.Iso.IsoBCD().GetList(p => p.DeleterEmpId == 0 && p.ProjId == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l7 = new DBSql.Iso.IsoDesignReturn().GetList(p => p.DeleterEmpId == 0 && p.ProjId == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l8 = new DBSql.Iso.IsoDWGZLXD().GetList(p => p.DeleterEmpId == 0 && p.ProjID == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l9 = new DBSql.Iso.IsoBGSJRWTZD().GetList(p => p.DeleterEmpId == 0 && p.ProjID == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l10 = new DBSql.Iso.IsoBCRWTZD().GetList(p => p.DeleterEmpId == 0 && p.ProjID == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l11 = new DBSql.Iso.IsoRWPSTZD().GetList(p => p.DeleterEmpId == 0 && p.ProjID == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l12 = new DBSql.Iso.IsoGCDZKCTJDJZ().GetList(p => p.DeleterEmpId == 0 && p.ProjID == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l13 = new DBSql.Iso.IsoConstructionSet().GetList(p => p.DeleterEmpId == 0 && p.ProjId == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l14 = new DBSql.Iso.IsoConstructionCoordination().GetList(p => p.DeleterEmpId == 0 && p.ProjId == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l15 = new DBSql.Iso.IsoConsultRecord().GetList(p => p.DeleterEmpId == 0 && p.ProjId == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l16 = new DBSql.Iso.IsoTechnologyChange().GetList(p => p.DeleterEmpId == 0 && p.ProjId == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName,p.CreatorEmpId });
            var l17 = new DBSql.Iso.IsoSJBGD().GetList(p => p.DeleterEmpId == 0 && p.ProjID == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName, p.CreatorEmpId });
            var l18 = new DBSql.Iso.IsoGCCLTJD().GetList(p => p.DeleterEmpId == 0 && p.ProjID == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName,p.CreatorEmpId });

            var r3 =  getISOList(l3.ToDataTable() , "IsoRemark", "项目备忘录", "iso/IsoRemark/edit?id={0}&flowNodeID={1}");
            var r4 =  getISOList(l4.ToDataTable() , "IsoZYZDJYB", "专业指导纪要表", "iso/IsoZYZDJYB/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r5 =  getISOList(l5.ToDataTable() , "IsoSJPSJYB", "设计评审纪要表", "iso/IsoSJPSJYB/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r6 =  getISOList(l6.ToDataTable() , "IsoBCD", "设计报出单", "Iso/IsoBCD/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r7 =  getISOList(l7.ToDataTable() , "IsoDesignReturn", "设计回访纪要表", "iso/IsoDesignReturn/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r8 =  getISOList(l8.ToDataTable() , "IsoDWGZLXD", "对外工作联系单", "ISO/IsoDWGZLXD/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r9 =  getISOList(l9.ToDataTable() , "IsoBGSJRWTZD", "变更复核设计任务通知单", "ISO/IsoBGSJRWTZD/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r10 = getISOList(l10.ToDataTable(), "IsoBCRWTZD", "补充任务通知单", "ISO/IsoBCRWTZD/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r11 = getISOList(l11.ToDataTable(), "IsoRWPSTZD", "任务评审通知单", "ISO/IsoRWPSTZD/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r12 = getISOList(l12.ToDataTable(), "IsoGCDZKCTJDJZ", "工程地质勘察条件单", "Iso/IsoGCDZKCTJDJZ/edit?CompanyType=SJ&id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r13 = getISOList(l13.ToDataTable(), "IsoConstructionSet", "施工交底记录单", "iso/IsoConstructionSet/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r14 = getISOList(l14.ToDataTable(), "IsoConstructionCoordination", "施工配合日志", "iso/IsoConstructionCoordination/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r15 = getISOList(l15.ToDataTable(), "IsoConsultRecord", "施工洽商记录", "iso/IsoConsultRecord/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r16 = getISOList(l16.ToDataTable(), "IsoTechnologyChange", "技术档案修改、替换、作废申请单", "iso/IsoTechnologyChange/edit?id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r17 = getISOList(l17.ToDataTable(), "IsoSJBGD", "变更设计单", "Iso/IsoSJBGD/edit?CompanyType=FGS&id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var r18 = getISOList(l18.ToDataTable(), "IsoGCCLTJD", "工程测量条件单", "Iso/IsoGCCLTJD/edit?CompanyType=SJ&id={0}&flowNodeID={1}&flowMultiSignID={2}");
            var result = l1f.ToDataTable().AsEnumerable().Union(r3.AsEnumerable().ToList())
                .Union(r4.AsEnumerable().ToList())
                .Union(r5.AsEnumerable().ToList())
                .Union(r6.AsEnumerable().ToList())
                .Union(r7.AsEnumerable().ToList())
                .Union(r8.AsEnumerable().ToList())
                .Union(r9.AsEnumerable().ToList())
                .Union(r10.AsEnumerable().ToList())
                .Union(r11.AsEnumerable().ToList())
                .Union(r12.AsEnumerable().ToList())
                .Union(r13.AsEnumerable().ToList())
                .Union(r14.AsEnumerable().ToList())
                .Union(r15.AsEnumerable().ToList())
                .Union(r16.AsEnumerable().ToList())
                .Union(r17.AsEnumerable().ToList())
                .Union(r18.AsEnumerable().ToList()).Select(p => new {
                    ID = p.Field<int>("Id"),
                    ProjName = p.Field<string>("ProjName"),
                    ProjPhaseName = p.Field<string>("ProjPhaseName"),
                    CreationTime = p.Field<DateTime>("CreationTime"),
                    CreatorEmpId = p.Field<int>("CreatorEmpId"),
                    CreatorEmpName = p.Field<string>("CreatorEmpName"),
                    FlowId = p.Field<int>("FlowId"),
                    FlowUrl = p.Field<string>("FlowUrl"),
                    FlowTitle = p.Field<string>("FlowTitle"),
                    FlowStatusName = p.Field<string>("FlowStatusName"),
                    FlowName = p.Field<string>("FlowName"),
                    FlowStatusID = p.Field<int>("FlowStatusID"),
                    FlowNodeEmpId = p.Field<int>("FlowNodeEmpId"),
                    NodeState = p.Field<string>("NodeState"),
                }).OrderByDescending(p=>p.CreationTime);
            string tableName = Request["tableName"];
            if (tableName.isNotEmpty())
                result = result.Where(p => p.FlowName == tableName).OrderByDescending(p=>p.CreationTime);

            int PageSize = queryContext.PageSize;
            int PageIndex = queryContext.PageIndex;

            return JavaScriptSerializerUtil.getJson(new
            {
                total = result.Count(),
                rows = result.Skip(PageSize * (PageIndex - 1)).Take(PageSize)
            });
        }

        public DataTable getISOList(DataTable list,string refTable,string refTableName,string url)
        {
            //var l2 = new DBSql.Iso.IsoGCCLTJD().GetList(p => p.ProjID == projId && p.ProjPhaseId.Contains(phaseId)).Select(p => new { p.Id, p.ProjName, p.ProjPhaseName, p.CreationTime, p.CreatorEmpName });
            var obj = from p in list.AsEnumerable().ToList()
                      join o in opFlow.GetList(i => i.FlowRefTable == refTable) on p.Field<int>("Id") equals o.FlowRefID into oList
                      from oResult in oList.DefaultIfEmpty()
                      join u in opFlowNode.GetList(y => y.FlowNodeStatusID == 29) on oResult == null ? 0 : oResult.Id equals u.FlowID into uList
                      from uResult in uList.DefaultIfEmpty()
                      select new
                      {
                          ID = p.Field<int>("Id"),
                          ProjName = p.Field<string>("ProjName"),
                          ProjPhaseName = p.Field<string>("ProjPhaseName"),
                          CreationTime = p.Field<DateTime>("CreationTime"),
                          CreatorEmpId=p.Field<int>("CreatorEmpId"),
                          CreatorEmpName = p.Field<string>("CreatorEmpName"),
                          FlowId = oResult == null ? 0 : oResult.Id,
                          FlowUrl = string.Format(url, p.Field<int>("Id"),"0","0"),//oResult == null ? "" : string.Format(oResult.FlowUrl.ToString(), p.Field<int>("Id"), "0", "0"),
                          FlowTitle = p.Field<string>("ProjName") + "的" + refTableName,
                          FlowStatusName = oResult == null ? "未审批" : oResult.FlowStatusName,
                          FlowName = refTableName,//"工程测量条件单",
                          FlowStatusID = oResult == null ? 0 : oResult.FlowStatusID,
                          FlowNodeEmpId = uResult == null ? 0 : uResult.FlowNodeEmpId,
                          //如果没有流程 判断是否自己创建  如果是 则处理
                          //如果有流程判断流程是否结束 如果结束 则查看
                          //如果流程没有结束 判断创建人name是否是自己 如果是处理
                          NodeState = oResult == null ? (p.Field<string>("CreatorEmpName") == userInfo.EmpName ? "处理":"查看") : oResult.FlowStatusName == "审批结束" ?  "查看" : userInfo.EmpID.Equals(uResult == null ? (p.Field<string>("CreatorEmpName") == userInfo.EmpName ? userInfo.EmpID : -1) : uResult.FlowNodeEmpId) ? "处理" : "查看",
                      };
            return obj.ToDataTable();
        }

        public string GetSpecByProjID()
        {
            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            int PageNum = TypeHelper.parseInt(Request.Form["page"]);
            int Record = TypeHelper.parseInt(Request.Form["rows"]);
            queryContext.SelectOrder = "id DESC";
            if (Request.Form["ProjID"] == null)
            {
                throw new ArgumentNullException("ProjID");
            }
            int projID = TypeHelper.parseInt(Request.Form["ProjID"].ToString(), 0);
            if (projID == 0)
            {
                throw new ArgumentNullException("ProjID");
            }
            if (Request.Form["TaskGroupID"] == null)
            {
                throw new ArgumentNullException("TaskGroupID");
            }
            int taskGroupID = TypeHelper.parseInt(Request.Form["TaskGroupID"].ToString(), 0);
            if (projID == 0)
            {
                throw new ArgumentNullException("TaskGroupID");
            }
            //var list = new DBSql.Design.DesTask().GetPagedList(d => d.ProjId == projID && d.TaskGroupId == taskGroupID, queryContext).ToList();
            //var list = new DBSql.Design.DesTask().GetPagedList(d => d.ProjId == projID && d.TaskGroupId == taskGroupID).ToList();

            var list = new DBSql.Design.DesTask().GetList(d => d.ProjId == projID && d.TaskGroupId == taskGroupID && d.DeleterEmpId == 0).ToList();
            var listExch = desExch.GetPagedList(d => d.ProjId == projID && d.taskGroupId == taskGroupID&&d.DeleterEmpId==0, queryContext).ToList();

            List<object> res = new List<object>();
            list.ForEach(item =>
            {
                if (item.TaskType == 1)
                {
                    list.Where(l => l.TaskType == 0 && l.TaskSpecId == item.TaskSpecId && l.TaskPhaseId == item.TaskPhaseId&&l.TaskStatus==3).OrderBy(l => l.TaskPhaseId).ToList().ForEach(_desTask =>
                          {
                              res.Add(new
                              {
                                  SpecName = item.TaskName,//专业
                                  TaskName = _desTask.TaskName,//卷册
                                  TaskEmpName = _desTask.TaskEmpName,//设计人
                                  TaskID = _desTask.Id,
                                  TaskSpecId = listExch.Where(d => d.ExchSpecId == item.TaskSpecId && d.ProjId == item.ProjId && d.taskGroupId == item.TaskGroupId).Count()
                              });
                          });
                }
            });
            var t = (res.Skip(Record * (PageNum - 1)).Take(Record)).ToList();
            return JavaScriptSerializerUtil.getJson(new
            {
                total = res.Count,
                rows = (res.Skip(Record * (PageNum - 1)).Take(Record)).ToList()
            });

        }

        private Expression<Func<Flow, bool>> GetExpression(HttpRequestBase request)
        {
            List<Expression> exprList = new List<Expression>();
            ParameterExpression paramExpr = Expression.Parameter(typeof(Flow), "f");
            if (request.Form["FlowStatus[]"] != null)//表单状态 参见DataModel.FlowStatus
            {
                MemberExpression selector = Expression.Property(paramExpr,
    typeof(Flow).GetProperty("FlowStatusID"));
                ConstantExpression nameValueExpr = Expression.Constant(Convert.ToInt32(request.Form["FlowStatus[]"].ToString()), typeof(Int32));
                Expression filter = Expression.Equal(selector, nameValueExpr);
                exprList.Add(filter);
            }
            if (request.Form["text"] != null && !string.IsNullOrEmpty(request.Form["text"].ToString()))//表单名称
            {
                MemberExpression namePropExpr = Expression.Property(paramExpr, typeof(Flow).GetProperty("FlowName"));
                MethodInfo containsMethod = typeof(string).GetMethod("Contains");
                ConstantExpression nameValueExpr = Expression.Constant(request.Form["text"].ToString(), typeof(string));
                MethodCallExpression nameContainsExpr = Expression.Call(namePropExpr, containsMethod, nameValueExpr);
                exprList.Add(nameContainsExpr);
            }
            if (request.Form["FormIDs[]"] != null && !string.IsNullOrEmpty(request.Form["FormIDs[]"]))//表单类型
            {
                List<int> list = new List<int>();
                var b = request.Form["FormIDs[]"].Trim().ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in b)
                {
                    list.Add(int.Parse(item));
                }
                MemberExpression namePropExpr = Expression.Property(paramExpr, typeof(Flow).GetProperty("FlowModelID"));
                MethodInfo containsMethod = typeof(List<int>).GetMethod("Contains");
                ConstantExpression nameValueExpr = Expression.Constant(list, typeof(List<int>));
                MethodCallExpression nameContainsExpr = Expression.Call(nameValueExpr, containsMethod, namePropExpr);
                exprList.Add(nameContainsExpr);
            }
            Expression whereExpr = null;
            foreach (var expr in exprList)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.And(whereExpr, expr);
            }
            Expression<Func<Flow, bool>> lambda;
            if (whereExpr != null)
                lambda = Expression.Lambda<Func<Flow, bool>>(whereExpr, paramExpr);
            else
                lambda = f => true;
            return lambda;
        }

        public string getFlowStatusEnum()
        {
            List<object> list = new List<object>();
            foreach (int item in System.Enum.GetValues(typeof(DataModel.FlowStatus)))
            {
                if (item == (int)DataModel.FlowStatus.未提交审批)
                {
                    continue;
                }
                string strName = System.Enum.GetName(typeof(DataModel.FlowStatus), item);//获取名称
                int strVaule = item;//获取值
                list.Add(new { id = strVaule, text = strName });
            }
            return JavaScriptSerializerUtil.getJson(list);
        }

        /// <summary>
        /// 专业提资记录列表
        /// </summary>
        /// <returns></returns>
        [Description("专业提资记录列表")]
        public ActionResult ProjDesSpecExchList()
        {
            ViewBag.taskId = Request.Params["taskId"] == null ? "0" : Request.Params["taskId"].ToString();
            if (ViewBag.taskId != "0")
            {
                DataModel.Models.DesTask _TaskModel = new DBSql.Design.DesTask().Get(Convert.ToInt32(ViewBag.taskId));
                ViewBag.TaskModel = _TaskModel;
            }
            return View();
        }

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

        #region 添加
        [Description("添加")]
        public ActionResult add()
        {
            var model = new IsoForm();
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
            var model = new IsoForm();
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
            reuslt = model.FormID;
            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(model.FormID, "操作成功") : DoResultHelper.doSuccess(model.FormID, "操作失败");
            return Json(dr);
        }
        #endregion

    }
}
