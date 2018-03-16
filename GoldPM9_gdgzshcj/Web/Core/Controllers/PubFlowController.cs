using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBSql.PubFlow;
using System.Xml;

namespace Core.Controllers
{
    public class PubFlowController : CoreController
    {
        public ActionResult ModelList()
        {
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.IsRefProject = "0";
            if (id == 0)
            {
                ViewBag.DialogHeight = 600;
                ViewBag.DialogWidth = 800;
                //是否允许撤销
                ViewBag.IsAllowUndo = "0";
                return View(new DataModel.Models.FlowModel());
            }
            else
            {
                var model = new DBSql.PubFlow.FlowModel().Get(id);
                ViewBag.FinishSendEmpNames = new DBSql.Sys.BaseEmployee().GetEmpName(model.ModelFinishSend);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(model.ModelXml);
                if (xmlDocument.DocumentElement.GetAttribute("IsRefProject") == "1")
                {
                    ViewBag.IsRefProject = "1";
                }
                var xDialog = (XmlElement)xmlDocument.SelectSingleNode("Root/ProcessDialog");
                if (xDialog == null)
                {
                    ViewBag.DialogHeight = 600;
                    ViewBag.DialogWidth = 800;
                }
                else
                {
                    ViewBag.DialogHeight = JQ.Util.TypeParse.parse<int>(xDialog.GetAttribute("Height"));
                    ViewBag.DialogWidth = JQ.Util.TypeParse.parse<int>(xDialog.GetAttribute("Width"));
                }
                var approvedEditRoles = xmlDocument.DocumentElement.GetAttribute("ApprovedEditRoles").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (approvedEditRoles.Contains("Administrator"))
                {
                    ViewBag.ApproveEdit_Administrator = "checked";
                }
                if (approvedEditRoles.Contains("Creator"))
                {
                    ViewBag.ApproveEdit_Creator = "checked";
                }
                ViewBag.IsAllowUndo = xmlDocument.DocumentElement.GetAttribute("IsAllowUndo");
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Save(int Id)
        {
            DataModel.Models.FlowModel model = null;
            var bpir = new DBSql.PubFlow.FlowModel();
            XmlDocument xmlDocument = new XmlDocument();
            if (Id > 0)
            {
                model = bpir.FirstOrDefault(m => m.Id == Id);
                xmlDocument.LoadXml(model.ModelXml);
            }
            else
            {
                model = new DataModel.Models.FlowModel();
                xmlDocument.LoadXml("<Root></Root>");
            }
            model.MvcSetValue();
            var xDialog = (XmlElement)xmlDocument.SelectSingleNode("Root/ProcessDialog");
            if (xDialog == null)
            {
                xDialog = xmlDocument.CreateElement("ProcessDialog");
                xmlDocument.DocumentElement.AppendChild(xDialog);
            }
            xDialog.SetAttribute("Width", JQ.Util.TypeParse.parse<int>(Request.Form["DialogWidth"], 800).ToString());
            xDialog.SetAttribute("Height", JQ.Util.TypeParse.parse<int>(Request.Form["DialogHeight"], 600).ToString());
            if (Request.Form["IsRefProject"] == "1")
            {
                xmlDocument.DocumentElement.SetAttribute("IsRefProject", "1");
            }
            else
            {
                xmlDocument.DocumentElement.SetAttribute("IsRefProject", "0");
            }
            xmlDocument.DocumentElement.SetAttribute("ApprovedEditRoles", Request.Form["cbApprovedEditRoles"] ?? "");
            xmlDocument.DocumentElement.SetAttribute("IsAllowUndo", Request.Form["IsAllowUndo"] ?? "");
            model.ModelXml = xmlDocument.OuterXml;
            bpir.CreateOrUpdate(model);
            DoResult dr = model.Id > 0 ? DoResultHelper.doSuccess(model.Id, "操作成功") : DoResultHelper.doError("操作失败");
            return Json(dr);
        }

        public JsonResult GetModelList()
        {
            Common.SqlPageInfo PageModel = new Common.SqlPageInfo();
            if (!PageModel.SelectOrder.isNotEmpty())
            {
                PageModel.SelectOrder = "Id";
            }
            var list = new DBSql.PubFlow.FlowModel().GetPagedDynamic(PageModel).ToList();
            return Json(new { total = PageModel.PageTotleRowCount, rows = list });
        }

        public ActionResult ModelNode(int flowModelID)
        {
            ViewBag.FlowModelID = flowModelID;
            if (flowModelID == 0 && !string.IsNullOrEmpty(Request.QueryString["refTable"]))
            {
                var refTable = Request.QueryString["refTable"] ?? "";
                var data = new DBSql.PubFlow.FlowModel().FirstOrDefault(m => m.ModelRefTable == refTable);
                if (data != null)
                {
                    ViewBag.FlowModelID = data.Id;
                }
            }
            return View();
        }

        public JsonResult Delete(int[] id)
        {
            var result = new DBSql.PubFlow.FlowModel().Delete(id) ? 1 : 0;
            return Json(result > 0 ? DoResultHelper.doSuccess(result, "操作成功") : DoResultHelper.doError("操作失败"));
        }

        public JsonResult GetModelNodes()
        {
            var flowModelID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["flowModelID"]);
            if (flowModelID == 0)
            {
                return Json("[]");
            }
            return Json(from m in new DBSql.PubFlow.FlowModelNode().GetList(m => m.FlowModelID == flowModelID)
                        select new
                        {
                            m.FlowModelID,
                            m.Id,
                            m.NodeParentID,
                            m.NodeName
                        });
        }

        public string GetModelNodesHtml()
        {
            var flowModelID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["flowModelID"]);
            if (flowModelID == 0)
            {
                return "";
            }
            var dsHtml = new DBSql.PubFlow.FlowModelNodeHtml();
            dsHtml.ImgPath = Url.Content("~/Content/Images/Flow/");
            return dsHtml.GetNodeHtml(flowModelID);
        }

        public string GetNodesHtml()
        {
            var flowID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["flowID"]);
            if (flowID == 0)
            {
                return "";
            }
            using (var dsHtml = new DBSql.PubFlow.FlowNodeHtml(flowID))
            {
                dsHtml.ImagePath = Url.Content("~/Content/Images/Flow/");
                return dsHtml.GetNodeHtml();
            }
        }

        public ActionResult EditNode(int modelNodeID)
        {
            if (modelNodeID == 0)
            {
                return View(new DataModel.Models.FlowModelNode());
            }
            var snode = new DBSql.PubFlow.FlowModelNode();
            var modelNode = snode.Get(modelNodeID);
            if (modelNode == null)
            {
                return View(new DataModel.Models.FlowModelNode());
            }
            ViewBag.NodeEmpNames = new DBSql.Sys.BaseEmployee().GetEmpName(modelNode.NodeEmpIDs);
            var allNodes = snode.GetList(m => m.FlowModelID == modelNode.FlowModelID && m.NodeParentID != 0).OrderBy(m => m.NodeOrder).ToList();
            Dictionary<int, string> previousNodes = new Dictionary<int, string>();
            foreach (var item in allNodes.Where(m => m.NodeOrder < modelNode.NodeOrder))
            {
                previousNodes.Add(item.Id, item.NodeName);
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(modelNode.NodeXml);
            var xProcessDialog = (XmlElement)xmlDocument.SelectSingleNode("Root/ProcessDialog");
            if (xProcessDialog != null)
            {
                ViewBag.DialogWidth = xProcessDialog.GetAttribute("Width");
                ViewBag.DialogHeight = xProcessDialog.GetAttribute("Height");
            }
            //获取出前面的节点
            ViewBag.PreviousNodes = previousNodes;
            var xBackValue = (XmlElement)xmlDocument.SelectSingleNode("Root/NodeAppDefaultBackValue");
            if (xBackValue != null)
            {
                ViewBag.NodeAppDefaultBackValue = xBackValue.InnerText;
            }
            Dictionary<int, string> nextNodes = new Dictionary<int, string>();
            foreach (var item in allNodes.Where(m => m.NodeOrder > modelNode.NodeOrder))
            {
                nextNodes.Add(item.Id, item.NodeName);
            }
            ViewBag.NextNodes = nextNodes;
            ViewBag.IsDepDirector = xmlDocument.DocumentElement.GetAttribute("IsDepDirector");
            return View(modelNode);
        }

        public JsonResult AppendNode()
        {
            var modelNodeID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["modelNodeID"]);
            if (modelNodeID == 0)
            {
                return Json(new { Result = "0" });
            }
            var mode = Request.Form["mode"] ?? "";
            var flowModelID = 0;
            if (mode == "next")
            {
                flowModelID = new DBSql.PubFlow.FlowModelNode().AppendNextNode(modelNodeID);
            }
            else if (mode == "previous")
            {
                flowModelID = new DBSql.PubFlow.FlowModelNode().AppendPreviousNode(modelNodeID);
            }
            if (flowModelID == 0)
            {
                return Json(new { Result = "0" });
            }
            var dsHtml = new DBSql.PubFlow.FlowModelNodeHtml();
            dsHtml.ImgPath = Url.Content("~/Content/Images/Flow/");
            return Json(new { Result = "1", HTML = dsHtml.GetNodeHtml(flowModelID) });
        }

        public JsonResult DeleteNode()
        {
            var modelNodeID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["modelNodeID"]);
            if (modelNodeID == 0)
            {
                return Json(new { Result = "0", Message = "参数错误" });
            }
            try
            {
                var flowModelID = new DBSql.PubFlow.FlowModelNode().DeleteNode(modelNodeID);
                var dsHtml = new DBSql.PubFlow.FlowModelNodeHtml();
                dsHtml.ImgPath = Url.Content("~/Content/Images/Flow/");
                return Json(new { Result = "1", HTML = dsHtml.GetNodeHtml(flowModelID) });
            }
            catch (Common.JQException jqe)
            {
                return Json(new { Result = "0", Message = jqe.Message });
            }
            catch (Exception)
            {
                return Json(new { Result = "0", Message = "未知异常" });
            }
        }

        public JsonResult SaveModelNode(int Id)
        {
            if (Id == 0)
            {
                return Json(DoResultHelper.doError("参数错误"));
            }
            var bpir = new DBSql.PubFlow.FlowModelNode();
            var node = bpir.Get(Id);
            if (node == null)
            {
                return Json(DoResultHelper.doError("找不到该节点"));
            }
            node.MvcSetValue();
            if (Request.Form["NodeRoles"] == null)
            {
                node.NodeRoleS = "";
            }
            bpir.Edit(node);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(node.NodeXml);
            var xDialog = (XmlElement)xmlDocument.SelectSingleNode("Root/ProcessDialog");
            if (xDialog == null)
            {
                xDialog = xmlDocument.CreateElement("ProcessDialog");
                xmlDocument.DocumentElement.AppendChild(xDialog);
            }
            if (Request.Form["DialogWidth"] == "")
            {
                xDialog.SetAttribute("Width", "");
            }
            else
            {
                xDialog.SetAttribute("Width", JQ.Util.TypeParse.parse<int>(Request.Form["DialogWidth"]).ToString());
            }
            if (Request.Form["DialogHeight"] == "")
            {
                xDialog.SetAttribute("Height", "");
            }
            else
            {
                xDialog.SetAttribute("Height", JQ.Util.TypeParse.parse<int>(Request.Form["DialogHeight"]).ToString());
            }
            var xBackValue = (XmlElement)xmlDocument.SelectSingleNode("Root/NodeAppDefaultBackValue");
            if (xBackValue == null)
            {
                xBackValue = xmlDocument.CreateElement("NodeAppDefaultBackValue");
                xmlDocument.DocumentElement.AppendChild(xBackValue);
            }
            xmlDocument.DocumentElement.SetAttribute("IsDepDirector", Request.Form["IsDepDirector"] ?? "");
            xBackValue.InnerText = Request.Form["NodeAppDefaultBackValue"] ?? "";
            node.NodeXml = xmlDocument.OuterXml;
            bpir.UnitOfWork.SaveChanges();
            return Json(node.Id > 0 ? DoResultHelper.doSuccess(node.Id, "操作成功") : DoResultHelper.doError("操作失败"));
        }

        public ActionResult FlowTest()
        {
            return View();
        }

        [ValidateInput(false)]
        public JsonResult FlowWidget()
        {
            var action = Request.Form["_action"] ?? "";
            if (string.IsNullOrEmpty(action))
            {
                return Json(new DBSql.PubFlow.FlowWareResult(false, "参数错误"));
            }
            var options = new DBSql.PubFlow.FlowWareOptions();
            SetFlowAction(action, options);
            options.RefID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["_refID"]);
            options.RefTable = Request.Form["_refTable"];
            options.FlowNodeID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["_flowNodeID"]);
            options.FlowMultiSignID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["_flowMultiSignID"]);
            options.CurrentUser = userInfo;
            options.Note = Request.Form["_note"] ?? "";
            options.Processor = Request.Form["_processor"];
            options.NextNodeID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["_nextNodeID"]);
            options.NextEmpIDs = Request.Form["_nextEmpIDs"] ?? "0";
            options.BridgeID = Common.ModelConvert.ConvertToDefault<int>(Request.Form["_bridgeID"]);
            options.Guid = Request.Form["_guid"] == null ? System.Guid.NewGuid() : new Guid(Request.Form["_guid"] ?? "");
            //处理程序
            try
            {
                using (DBSql.PubFlow.FlowWare flowware = new DBSql.PubFlow.FlowWare(options))
                {
                    flowware.Execute();
                    if (options.Action == DBSql.PubFlow.Action.Load)
                    {
                        flowware.Result.ActionUrl = Url.Action("Display", "PubFlow", new { @area = "Core" });
                        if (flowware.Result.IsNew)
                        {
                            flowware.Result.ProgressUrl = Url.Action("ModelNode", "PubFlow", new { @area = "Core" }) + "?flowModelID=0&view=1&refTable=";
                        }
                        else
                        {
                            flowware.Result.ProgressUrl = Url.Action("Progress", "PubFlow", new { @area = "Core" }) + "?flowID=";
                        }
                    }
                    return Json(flowware.Result);
                }
            }
            catch (Exception)
            {
                return Json(new DBSql.PubFlow.FlowWareResult(false, "发生未经处理的错误"));
            }
        }

        private static void SetFlowAction(string action, FlowWareOptions options)
        {
            switch (action)
            {
                case "load":
                    options.Action = DBSql.PubFlow.Action.Load;
                    break;
                case "load_next":
                    options.Action = DBSql.PubFlow.Action.LoadNext;
                    break;
                case "load_back":
                    options.Action = DBSql.PubFlow.Action.LoadBack;
                    break;
                case "load_finish":
                    options.Action = DBSql.PubFlow.Action.LoadFinish;
                    break;
                case "load_reject":
                    options.Action = DBSql.PubFlow.Action.LoadReject;
                    break;
                case "load_transfer":
                    options.Action = DBSql.PubFlow.Action.LoadTransfter;
                    break;
                case "save":
                    options.Action = DBSql.PubFlow.Action.Save;
                    break;
                case "next":
                    options.Action = DBSql.PubFlow.Action.Next;
                    break;
                case "back":
                    options.Action = DBSql.PubFlow.Action.Back;
                    break;
                case "finish":
                    options.Action = DBSql.PubFlow.Action.Finish;
                    break;
                case "reject":
                    options.Action = DBSql.PubFlow.Action.Reject;
                    break;
                case "transfer":
                    options.Action = DBSql.PubFlow.Action.Transfer;
                    break;
                case "load_undo":
                    options.Action = DBSql.PubFlow.Action.LoadUndo;
                    break;
                case "undo":
                    options.Action = DBSql.PubFlow.Action.Undo;
                    break;
            }
        }

        public ActionResult Display()
        {
            ViewBag.Result = Request.QueryString["action"];
            return View();
        }

        public ActionResult ToDoList()
        {
            return View();
        }

        public ActionResult Progress()
        {
            return View();
        }

        public JsonResult GetToDoList()
        {
            Common.SqlPageInfo queryContext = new Common.SqlPageInfo();
            if (userInfo.AgenTypeID == 0)
            {

            }
            else if (userInfo.AgenTypeID == -1 || userInfo.AgenTypeID == 2)
            {
                //根据代理来
                if (string.IsNullOrEmpty(userInfo.AgenFlow))
                {
                    return Json(new { total = 0, rows = new object[] { } });
                }
                queryContext.SelectCondtion.Add("RefTables", "'" + userInfo.AgenFlow.Replace(",", "','") + "'");
            }
            else
            {
                return Json(new { total = 0, rows = new object[] { } });
            }
            queryContext.SelectOrder = "fn.FlowNodeFromDateTime DESC";
            queryContext.TextCondtion = Request.Form["text"] ?? "";
            if (Request.Form["useEmpID"] != null)
            {
                queryContext.SelectCondtion.Add("EmpID", JQ.Util.TypeParse.parse<int>(Request.Form["empID"]));
            }
            else
            {
                queryContext.SelectCondtion.Add("EmpID", userInfo.EmpID);
            }
            if (Request.Form["statusID"] != null)
            {
                queryContext.SelectCondtion.Add("FlowStatus", Request.Form["statusID"].ToString());
            }
            if (Request.Form["FormID[]"] != null)
            {
                queryContext.SelectCondtion.Add("FormModelID", Request.Form["FormID[]"].ToString());
            }
            if (Request.Form["FlowStatusID"] != null)
            {
                queryContext.SelectCondtion.Add("FlowStatusID", Request.Form["FlowStatusID"].ToString());
            }
            if (!string.IsNullOrEmpty(Request.Form["modular"]))
            {
                queryContext.SelectCondtion.Add("Modular", JQ.Util.TypeParse.parse<int>(Request.Form["modular"]));
            }
            using (var dt = new DBSql.PubFlow.Flow().GetToDoList(queryContext))
            {
                dt.Columns.Add("FlowNodeStatusName");
                //dt.Columns.Add("DialogWidth");
                dt.Columns.Add("DialogHeight");
                foreach (DataRow row in dt.Rows)
                {
                    if (row.Field<int>("FlowType") == 0)
                    {
                        switch (row.Field<int>("FlowNodeStatusID"))
                        {
                            case (int)DataModel.NodeStatus.轮到:
                            case 0:
                                row["FlowNodeStatusName"] = "轮到";
                                break;
                            case (int)DataModel.NodeStatus.完成:
                            case 1:
                                row["FlowNodeStatusName"] = "完成";
                                break;
                            case (int)DataModel.NodeStatus.未安排:
                                row["FlowNodeStatusName"] = "未安排";
                                break;
                            case (int)DataModel.NodeStatus.安排:
                                row["FlowNodeStatusName"] = "安排";
                                break;
                            case (int)DataModel.NodeStatus.跳过:
                                row["FlowNodeStatusName"] = "跳过";
                                break;
                            default:
                                row["FlowNodeStatusName"] = "";
                                break;
                        }
                        var flowNodeUrl = row["FlowNodeUrl"].ToString();
                        if (string.IsNullOrEmpty(flowNodeUrl))
                        {
                            var flowUrl = row["FlowUrl"].ToString();
                            if (!string.IsNullOrEmpty(flowUrl))
                            {
                                row["FlowNodeUrl"] = string.Format(flowUrl, row["FlowRefID"].ToString(), row["FlowNodeID"].ToString(), row["FlowMultiSignID"].ToString());
                            }
                        }
                        else
                        {
                            row["FlowNodeUrl"] = string.Format(flowNodeUrl, row["FlowRefID"].ToString(), row["FlowNodeID"].ToString(), row["FlowMultiSignID"].ToString());
                        }
                        XmlDocument flowXml = new XmlDocument();
                        flowXml.LoadXml(row["FlowXml"].ToString());
                        var xFlowDialog = (XmlElement)flowXml.SelectSingleNode("Root/ProcessDialog");
                        XmlDocument flowNodeXml = new XmlDocument();
                        flowNodeXml.LoadXml(row["FlowNodeXml"].ToString());
                        var xFlowNodeDialog = (XmlElement)flowNodeXml.SelectSingleNode("Root/ProcessDialog");
                        if (xFlowNodeDialog != null && xFlowNodeDialog.GetAttribute("Width") != "")
                        {
                            row["DialogWidth"] = JQ.Util.TypeParse.parse<int>(xFlowNodeDialog.GetAttribute("Width"));
                        }
                        else if (xFlowDialog != null && xFlowDialog.GetAttribute("Width") != "")
                        {
                            row["DialogWidth"] = JQ.Util.TypeParse.parse<int>(xFlowDialog.GetAttribute("Width"));
                        }
                        else
                        {
                            row["DialogWidth"] = 800;
                        }
                        if (xFlowNodeDialog != null && xFlowNodeDialog.GetAttribute("Height") != "")
                        {
                            row["DialogHeight"] = JQ.Util.TypeParse.parse<int>(xFlowNodeDialog.GetAttribute("Height"));
                        }
                        else if (xFlowDialog != null && xFlowDialog.GetAttribute("Height") != "")
                        {
                            row["DialogHeight"] = JQ.Util.TypeParse.parse<int>(xFlowDialog.GetAttribute("Height"));
                        }
                        else
                        {
                            row["DialogHeight"] = 600;
                        }
                    }
                    else
                    {
                        row["DialogHeight"] = 680;
                        var width = row.Field<int>("DialogWidth") + 40;
                        if (width < 600)
                        {
                            width = 600;
                        }
                        else if (width > 1200)
                        {
                            width = 1200;
                        }
                        row.SetField<int>("DialogWidth", width);
                        row["FlowNodeStatusName"] = JQ.BPM.Model.TextGetter.GetActivityStatusText((JQ.BPM.Model.ActivityStatus)row.Field<int>("FlowNodeStatusID"));
                    }
                }
                dt.Columns.Remove("FlowXml");
                dt.Columns.Remove("FlowNodeXml");
                return Json(new { total = queryContext.PageTotleRowCount, rows = dt.ToList<FlowTask>() });
            }
        }

        public string GetFlowExe()
        {
            Common.SqlPageInfo quertContext = new Common.SqlPageInfo();
            if (string.IsNullOrEmpty(quertContext.SelectOrder))
            {
                quertContext.SelectOrder = "ExeActionDate";
            }
            quertContext.SelectCondtion.Add("FlowID", Common.ModelConvert.ConvertToDefault<int>(Request.Form["flowID"]));
            DataTable list = new DBSql.PubFlow.Flow().GetFlowNodeExeTable(quertContext);
            return JavaScriptSerializerUtil.getJson(new { total = quertContext.PageTotleRowCount, rows = list });
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFlowStatuses()
        {
            if (string.IsNullOrEmpty(Request.Form["refTable"]) || string.IsNullOrEmpty(Request.Form["refIDs"]))
            {
                return Json(null);
            }
            var refTable = Request.Form["refTable"];
            var refIDs = Request.Form["refIDs"];
            var s = new List<int>();
            foreach (var i in refIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var temp = 0;
                if (int.TryParse(i, out temp) && temp > 0)
                {
                    s.Add(temp);
                }
            }
            return Json(new { url = Url.Action("Progress", "PubFlow", new { @area = "Core" }) + "?flowID=", datas = new DBSql.PubFlow.Flow().GetFlowStatuses(refTable, s) });
        }

        public JsonResult GetAmount()
        {
            if (userInfo.EmpID == 0)
            {
                return Json(new { Result = false, Amount = 0, ExchRec = 0 });
            }
            var flow = new DBSql.PubFlow.Flow();
            var eschRec = new DBSql.Design.DesExchRec();
            try
            {
                return Json(new { Result = true, ProjectAmount = flow.GetToDoAmount(userInfo.EmpID, 1, userInfo.AgenTypeID, userInfo.AgenFlow), OAAmount = flow.GetToDoAmount(userInfo.EmpID, 2, userInfo.AgenTypeID, userInfo.AgenFlow), ExchRec = eschRec.GetRecCountByEmpID(userInfo.EmpID, userInfo.AgenTypeID, userInfo.AgenFlow) });
            }
            catch
            {
                return Json(new { Result = false, ProjectAmount = 0, OAAmount = 0, ExchRec = 0 });
            }
        }

        public JsonResult BatchTransfer()
        {
            var id = JQ.Util.TypeParse.parse<int>(Request.Form["handOverID"]);
            if (id == 0)
            {
                return Json(new DBSql.PubFlow.FlowWareResult(false, "参数错误"));
            }
            var handover = new DBSql.Sys.BaseHandover().FirstOrDefault(m => m.Id == id);
            if (handover == null)
            {
                return Json(new DBSql.PubFlow.FlowWareResult(false, "无法确认此条工作移交是否存在"));
            }
            var ids = (Request.Form["flowNodeIDs"] ?? "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (ids.Length == 0)
            {
                return Json(new DBSql.PubFlow.FlowWareResult(false, "参数错误"));
            }
            var empID = JQ.Util.TypeParse.parse<int>(Request.Form["empID"]);
            //处理程序
            if (empID == 0)
            {
                return Json(new DBSql.PubFlow.FlowWareResult(false, "无法获取出正确的处理人员"));
            }
            var options = new DBSql.PubFlow.FlowWareOptions();
            options.IsValidateEmp = false;
            options.Action = DBSql.PubFlow.Action.Transfer;
            options.NextEmpIDs = empID.ToString();
            options.Note = handover.HandNote;
            options.CurrentUser = userInfo;
            try
            {

                using (DBSql.PubFlow.FlowWare flowware = new DBSql.PubFlow.FlowWare(options))
                {
                    foreach (var data in ids)
                    {
                        var temp = data.Split('|');
                        if (temp.Length != 2)
                        {
                            continue;
                        }
                        var flowNodeID = JQ.Util.TypeParse.parse<int>(temp[0]);
                        var flowMultiSignID = JQ.Util.TypeParse.parse<int>(temp[1]);
                        if (flowNodeID == 0)
                        {
                            continue;
                        }
                        options.FlowNodeID = flowNodeID;
                        options.FlowMultiSignID = flowMultiSignID;
                        flowware.Execute();
                    }
                }
                return Json(new DBSql.PubFlow.FlowWareResult(true, ""));
            }
            catch (Exception)
            {
                return Json(new DBSql.PubFlow.FlowWareResult(false, "发生未经处理的错误"));
            }
        }
    }
}