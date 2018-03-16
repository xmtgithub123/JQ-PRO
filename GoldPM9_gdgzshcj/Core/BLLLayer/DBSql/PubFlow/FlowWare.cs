using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Xml;

namespace DBSql.PubFlow
{
    public class FlowWare : IDisposable
    {
        private bool _isDisponsed = false;
        private DbContext _dbContext;
        protected DbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    GetDefaultDbContext();
                }
                return _dbContext;
            }
        }

        protected FlowWareOptions Options
        {
            get; set;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void GetDefaultDbContext()
        {
            if (_dbContext == null)
            {
                _dbContext = new DAL.JQPM9_DefaultContext();
            }
        }

        public FlowWare(FlowWareOptions options)
        {
            this.Options = options;
        }

        private DBSql.Sys.BaseEmployee _baseEmployee;
        protected DBSql.Sys.BaseEmployee BaseEmployeeDB
        {
            get
            {
                if (_baseEmployee == null)
                {
                    _baseEmployee = new Sys.BaseEmployee();
                    //_baseEmployee.DbContextRepository(this.DbContext);
                }
                return _baseEmployee;
            }
        }

        /// <summary>
        /// 执行流程
        /// </summary>
        public void Execute()
        {
            this.Result = new FlowWareResult();
            try
            {
                this.Result.UserID = this.Options.CurrentUser.EmpID;
                this.Result.UserName = this.Options.CurrentUser.EmpName;
                this.Result.DepartmentID = this.Options.CurrentUser.EmpDepID;
                this.Result.DepartmentName = this.Options.CurrentUser.EmpDepName;
                this.Result.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.Result.AgentEmpName = this.Options.CurrentUser.AgenEmpName;
                //var processor = System.Reflection.Assembly.GetAssembly(typeof(FlowWare)).CreateInstance(Options.Processor) as IFlowProcessor;
                switch (this.Options.Action)
                {
                    case Action.Load:
                        Load();
                        break;
                    case Action.LoadNext:
                        LoadNext();
                        break;
                    case Action.LoadBack:
                        LoadBack();
                        break;
                    case Action.LoadTransfter:
                        LoadTransfer();
                        break;
                    case Action.Save:
                        Save();
                        break;
                    case Action.Next:
                        Next();
                        break;
                    case Action.Back:
                        Back();
                        break;
                    case Action.Finish:
                        Finish();
                        break;
                    case Action.Reject:
                        Reject();
                        break;
                    case Action.Transfer:
                        Transfer();
                        break;
                    case Action.LoadUndo:
                        LoadUndo();
                        break;
                    case Action.Undo:
                        Undo();
                        break;
                    default:
                        throw new Common.JQException("无法找到流程请求的实现" + this.Options.Action);
                }
            }
            catch (Common.JQException jqe)
            {
                this.Result.Result = false;
                this.Result.Message = jqe.Message;
            }
            catch (Exception ex)
            {
                //TODO 记录异常日志
                this.Result.Result = false;
                this.Result.Message = "发生未经处理的异常" + ex.Message + " " + ex.StackTrace;
            }
        }

        public FlowWareResult Result
        {
            get; set;
        }

        /// <summary>
        /// 当加载时的触发的方法
        /// </summary>
        protected virtual void Load()
        {
            if (Options.FlowNodeID == 0)
            {
                ProcessLoadWithFlowModel();
            }
            else
            {
                ProcessLoadWithFlow();
            }
        }

        private void ProcessLoadWithFlowModel()
        {
            if (string.IsNullOrEmpty(this.Options.RefTable))
            {
                throw new Common.JQException("参数RefTable错误");
            }
            if (Options.RefID != 0)
            {
                var flow = this.DbContext.Set<DataModel.Models.Flow>().FirstOrDefault(m => m.FlowRefID == this.Options.RefID && m.FlowRefTable == this.Options.RefTable);
                if (flow != null)
                {
                    //已经发起过流程，并且未确定节点，当流程结束处理
                    var allNodes1 = this.DbContext.Set<DataModel.Models.FlowNode>().Where(m => m.FlowID == flow.Id).OrderBy(m => m.FlowNodeOrder).ToList();
                    //验证当前节点有没有数据
                    var node1 = allNodes1.FirstOrDefault(m => m.FlowNodeStatusID == (int)DataModel.NodeStatus.轮到);
                    if (node1 != null)
                    {
                        if (node1.FlowNodeTypeID == -1)
                        {
                            //会签
                            var flowMultiSign = this.DbContext.Set<DataModel.Models.FlowMultiSign>().FirstOrDefault(m => m.FlowNodeID == node1.Id && m.SignEmpId == this.Options.CurrentUser.EmpID);
                            if (flowMultiSign != null)
                            {
                                this.Options.FlowNodeID = node1.Id;
                                this.Options.FlowMultiSignID = flowMultiSign.Id;
                                this.Result.FlowMultiSignID = flowMultiSign.Id;
                                ProcessLoadWithFlow(node1, flow);
                                return;
                            }
                        }
                        else if (node1.FlowNodeEmpId == this.Options.CurrentUser.EmpID)
                        {
                            this.Options.FlowNodeID = node1.Id;
                            ProcessLoadWithFlow(node1, flow);
                            return;
                        }
                    }
                    this.Result.Result = true;
                    //this.Result.IsFlowFinished = true;
                    this.Result.IsFinished = true;
                    this.Result.FlowName = flow.FlowName;
                    this.Result.FlowID = flow.Id;
                    //获取出当前轮到的节点
                    if (!string.IsNullOrEmpty(flow.FlowSign))
                    {
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml("<Root></Root>");
                        xmlDocument.DocumentElement.SetAttribute("SignControlID", flow.FlowSign);
                        //获取出需要签名的节点
                        foreach (var toSignNode in allNodes1.Where(m => m.FlowNodeValidateGroup == "1"))
                        {
                            XmlDocument xNode = new XmlDocument();
                            xNode.LoadXml(toSignNode.FlowNodeXml);
                            if (toSignNode.FlowNodeTypeID == -1)
                            {
                                //处理会签的显示
                                var xSignNotes = xNode.SelectNodes("Root/SignNote");
                                if (xSignNotes.Count == 0)
                                {
                                    //添加默认空项
                                    var xItem = xmlDocument.CreateElement("SignNote");
                                    xItem.SetAttribute("ID", toSignNode.Id.ToString());
                                    xItem.SetAttribute("Name", toSignNode.FlowNodeName.ToString());
                                    xItem.SetAttribute("TypeID", "-1");
                                    xmlDocument.DocumentElement.AppendChild(xItem);
                                }
                                else
                                {
                                    for (var i = 0; i < xSignNotes.Count; i++)
                                    {
                                        var xSignNote = (XmlElement)xSignNotes[i];
                                        var xItem = xmlDocument.CreateElement("SignNote");
                                        xItem.SetAttribute("ID", toSignNode.Id.ToString());
                                        xItem.SetAttribute("Name", toSignNode.FlowNodeName.ToString());
                                        xItem.SetAttribute("TypeID", "-1");
                                        xItem.SetAttribute("Order", (i + 1).ToString());
                                        xItem.SetAttribute("MultiSignID", xSignNote.GetAttribute("MultiSignID").ToString());
                                        xItem.SetAttribute("EmpID", xSignNote.GetAttribute("EmpID"));
                                        xItem.SetAttribute("EmpName", xSignNote.GetAttribute("EmpName"));
                                        xItem.SetAttribute("Date", xSignNote.GetAttribute("Date"));
                                        if (!string.IsNullOrEmpty(xSignNote.GetAttribute("AgentEmpID")))
                                        {
                                            xItem.SetAttribute("AgentEmpID", xSignNote.GetAttribute("AgentEmpID"));
                                            xItem.SetAttribute("AgentEmpName", xSignNote.GetAttribute("AgentEmpName"));
                                        }
                                        xItem.InnerText = xSignNote.InnerText;
                                        xmlDocument.DocumentElement.AppendChild(xItem);
                                    }
                                }
                            }
                            else
                            {
                                var xItem = xmlDocument.CreateElement("SignNote");
                                xItem.SetAttribute("ID", toSignNode.Id.ToString());
                                xItem.SetAttribute("Name", toSignNode.FlowNodeName.ToString());
                                var xSignNote = (XmlElement)xNode.SelectSingleNode("Root/SignNote");
                                if (xSignNote != null)
                                {
                                    xItem.SetAttribute("EmpID", xSignNote.GetAttribute("EmpID"));
                                    xItem.SetAttribute("EmpName", xSignNote.GetAttribute("EmpName"));
                                    xItem.SetAttribute("Date", xSignNote.GetAttribute("Date"));
                                    if (!string.IsNullOrEmpty(xSignNote.GetAttribute("AgentEmpID")))
                                    {
                                        xItem.SetAttribute("AgentEmpID", xSignNote.GetAttribute("AgentEmpID"));
                                        xItem.SetAttribute("AgentEmpName", xSignNote.GetAttribute("AgentEmpName"));
                                    }
                                    xItem.InnerText = xSignNote.InnerText;
                                }
                                xmlDocument.DocumentElement.AppendChild(xItem);
                            }
                        }
                        this.Result.SignDatas = xmlDocument.OuterXml;
                    }
                    if (flow.FlowStatusID != (int)DataModel.FlowStatus.审批中)
                    {
                        // todo 广州首汇
                        //this.Result.IsFlowFinished = true;
                        //XmlDocument flowXmlDocument = new XmlDocument();
                        //flowXmlDocument.LoadXml(flow.FlowXml);
                        //if (AllowEditWhenApproved(flowXmlDocument, flow.CreatorEmpId))
                        //{
                        //    this.Result.AllowEditControls = flow.FlowFinishControls;
                        //    this.Result.IsAllowUndo = flowXmlDocument.DocumentElement.GetAttribute("IsAllowUndo") == "1";
                        //}
                        // 标准版
                        this.Result.IsFlowFinished = true;
                        XmlDocument flowXmlDocument = new XmlDocument();
                        flowXmlDocument.LoadXml(flow.FlowXml);
                        this.Result.AllowEditControls = flow.FlowFinishControls;
                        this.Result.IsAllowUndo = flowXmlDocument.DocumentElement.GetAttribute("IsAllowUndo") == "1";
                    }
                    return;
                }
            }
            //获取出RefTable的第一个有效节点的数据
            var flowModel = this.DbContext.Set<DataModel.Models.FlowModel>().FirstOrDefault(m => m.ModelRefTable == this.Options.RefTable);
            if (flowModel == null)
            {
                throw new Common.JQException("在加载时无法找到有效的流程模板！");
            }
            var allNodes = this.DbContext.Set<DataModel.Models.FlowModelNode>().Where(m => m.FlowModelID == flowModel.Id).OrderBy(m => m.NodeOrder);
            var modelNode = allNodes.FirstOrDefault(m => m.NodeOrder == 1);
            if (modelNode == null)
            {
                throw new Common.JQException("在加载时无法找到有效的流程模板节点！");
            }
            if (modelNode.NodeTypeID == -1)
            {
                throw new Common.JQException("在流程模版中第一个有效节点无法为会签节点！");
            }
            this.Result.Result = true;
            this.Result.NodeName = modelNode.NodeName + (modelNode.NodeTypeID == -1 ? "（会签）" : "");
            this.Result.AllowEditControls = modelNode.NodeWriteControlsName;
            this.Result.SignControls = modelNode.NodeSignControlName;
            this.Result.StepOrder = modelNode.NodeOrder;
            this.Result.IsNew = true;
            this.Result.FlowName = flowModel.ModelName;
            this.Result.FlowNodeID = modelNode.Id;
            if (modelNode.NodeSelectToSkip)
            {
                this.Result.NextAction = 2;
            }
            else
            {
                this.Result.NextAction = 1;
            }
            if (modelNode.NodeSelectToBack)
            {
                this.Result.BackAction = 2;
            }
            else if (modelNode.NodeBackID > 0)
            {
                this.Result.BackAction = 1;
            }
            if (modelNode.NodeIsToFinish)
            {
                this.Result.FinishAction = 1;
            }
            if (modelNode.NodeIsToPass)
            {
                this.Result.RejectAction = 1;
            }
            //处理签名列表
            if (!string.IsNullOrEmpty(flowModel.ModelSign))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml("<Root></Root>");
                xmlDocument.DocumentElement.SetAttribute("SignControlID", flowModel.ModelSign);
                //获取出需要签名的节点
                foreach (var toSignNode in allNodes.Where(m => m.NodeValidateGroup == "1"))
                {
                    var xItem = xmlDocument.CreateElement("SignNote");
                    xItem.SetAttribute("ID", toSignNode.Id.ToString());
                    xItem.SetAttribute("Name", toSignNode.NodeName.ToString());
                    if (toSignNode.NodeTypeID == -1)
                    {
                        //会签标记
                        xItem.SetAttribute("TypeID", "-1");
                    }
                    xmlDocument.DocumentElement.AppendChild(xItem);
                }
                this.Result.SignDatas = xmlDocument.OuterXml;
            }
        }

        private string RecordSignNote(string xml, int flowMultiSignID = 0)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            XmlElement xSignNote = null;
            if (flowMultiSignID == 0)
            {
                xSignNote = (XmlElement)xmlDocument.SelectSingleNode("Root/SignNote");
            }
            else
            {
                xSignNote = (XmlElement)xmlDocument.SelectSingleNode("Root/SignNote[@MultiSignID=\"" + flowMultiSignID + "\"]");
            }
            if (xSignNote == null)
            {
                xSignNote = xmlDocument.CreateElement("SignNote");
                if (flowMultiSignID > 0)
                {
                    xSignNote.SetAttribute("MultiSignID", flowMultiSignID.ToString());
                }
                xmlDocument.DocumentElement.AppendChild(xSignNote);
            }
            xSignNote.SetAttribute("EmpID", this.Options.CurrentUser.EmpID.ToString());
            xSignNote.SetAttribute("EmpName", this.Options.CurrentUser.EmpName);
            xSignNote.SetAttribute("Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            xSignNote.InnerText = this.Options.Note;
            if (this.Options.CurrentUser.AgenEmpID > 0)
            {
                xSignNote.SetAttribute("AgentEmpID", this.Options.CurrentUser.AgenEmpID.ToString());
                xSignNote.SetAttribute("AgentEmpName", this.Options.CurrentUser.AgenEmpName);
            }
            return xmlDocument.OuterXml;
        }

        private void ProcessLoadWithFlow(DataModel.Models.FlowNode currentNode = null, DataModel.Models.Flow flow = null)
        {
            if (currentNode == null)
            {
                currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == this.Options.FlowNodeID);
            }
            if (currentNode == null)
            {
                throw new Common.JQException("找不到当前有效的节点！");
            }
            if (flow == null)
            {
                flow = this.DbContext.Set<DataModel.Models.Flow>().FirstOrDefault(m => m.Id == currentNode.FlowID);
            }
            if (flow == null)
            {
                throw new Common.JQException("找不到当前节点所属的有效流程！");
            }
            this.Result.Result = true;
            this.Result.FlowID = currentNode.FlowID;
            this.Result.FlowName = flow.FlowName;
            this.Result.NodeName = currentNode.FlowNodeName + (currentNode.FlowNodeTypeID == -1 ? "（会签）" : "");
            this.Result.StepOrder = currentNode.FlowNodeOrder;
            this.Result.DefaultNote = currentNode.FlowNodeAppDefaultValue;
            this.Result.FlowNodeID = currentNode.Id;
            if (!string.IsNullOrEmpty(flow.FlowSign))
            {
                var allNodes = this.DbContext.Set<DataModel.Models.FlowNode>().Where(m => m.FlowID == currentNode.FlowID).OrderBy(m => m.FlowNodeOrder);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml("<Root></Root>");
                xmlDocument.DocumentElement.SetAttribute("SignControlID", flow.FlowSign);
                //获取出需要签名的节点
                foreach (var toSignNode in allNodes.Where(m => m.FlowNodeValidateGroup == "1"))
                {
                    XmlDocument xNode = new XmlDocument();
                    xNode.LoadXml(toSignNode.FlowNodeXml);
                    if (toSignNode.FlowNodeTypeID == -1)
                    {
                        //处理会签的显示
                        var xSignNotes = xNode.SelectNodes("Root/SignNote");
                        if (xSignNotes.Count == 0)
                        {
                            //添加默认空项
                            var xItem = xmlDocument.CreateElement("SignNote");
                            xItem.SetAttribute("ID", toSignNode.Id.ToString());
                            xItem.SetAttribute("Name", toSignNode.FlowNodeName.ToString());
                            xItem.SetAttribute("TypeID", "-1");
                            xmlDocument.DocumentElement.AppendChild(xItem);
                        }
                        else
                        {
                            for (var i = 0; i < xSignNotes.Count; i++)
                            {
                                var xSignNote = (XmlElement)xSignNotes[i];
                                var xItem = xmlDocument.CreateElement("SignNote");
                                xItem.SetAttribute("ID", toSignNode.Id.ToString());
                                xItem.SetAttribute("Name", toSignNode.FlowNodeName.ToString());
                                xItem.SetAttribute("TypeID", "-1");
                                xItem.SetAttribute("Order", (i + 1).ToString());
                                xItem.SetAttribute("MultiSignID", xSignNote.GetAttribute("MultiSignID").ToString());
                                xItem.SetAttribute("EmpID", xSignNote.GetAttribute("EmpID"));
                                xItem.SetAttribute("EmpName", xSignNote.GetAttribute("EmpName"));
                                xItem.SetAttribute("Date", xSignNote.GetAttribute("Date"));
                                if (!string.IsNullOrEmpty(xSignNote.GetAttribute("AgentEmpID")))
                                {
                                    xItem.SetAttribute("AgentEmpID", xSignNote.GetAttribute("AgentEmpID"));
                                    xItem.SetAttribute("AgentEmpName", xSignNote.GetAttribute("AgentEmpName"));
                                }
                                xItem.InnerText = xSignNote.InnerText;
                                xmlDocument.DocumentElement.AppendChild(xItem);
                            }
                        }
                    }
                    else
                    {
                        var xItem = xmlDocument.CreateElement("SignNote");
                        xItem.SetAttribute("ID", toSignNode.Id.ToString());
                        xItem.SetAttribute("Name", toSignNode.FlowNodeName.ToString());
                        var xSignNote = (XmlElement)xNode.SelectSingleNode("Root/SignNote");
                        if (xSignNote != null)
                        {
                            xItem.SetAttribute("EmpID", xSignNote.GetAttribute("EmpID"));
                            xItem.SetAttribute("EmpName", xSignNote.GetAttribute("EmpName"));
                            xItem.SetAttribute("Date", xSignNote.GetAttribute("Date"));
                            if (!string.IsNullOrEmpty(xSignNote.GetAttribute("AgentEmpID")))
                            {
                                xItem.SetAttribute("AgentEmpID", xSignNote.GetAttribute("AgentEmpID"));
                                xItem.SetAttribute("AgentEmpName", xSignNote.GetAttribute("AgentEmpName"));
                            }
                            xItem.InnerText = xSignNote.InnerText;
                        }
                        xmlDocument.DocumentElement.AppendChild(xItem);
                    }
                }
                this.Result.SignDatas = xmlDocument.OuterXml;
            }
            if (currentNode.FlowNodeAppIsLastTime)
            {
                //显示上次意见，需要从流转记录中获取出本节点的相对应的上一条记录
                this.Result.LastNote = this.DbContext.Set<DataModel.Models.FlowNodeExe>().Where(m => m.FlowID == currentNode.FlowID && m.FlowNodeID != currentNode.Id && (m.ExeActionID == (int)DataModel.NodeAction.回退 || m.ExeActionID == (int)DataModel.NodeAction.完成)).OrderByDescending(m => m.ExeActionDate).Select(m => m.ExeEmpName + (m.ExeArgEmpName != "" ? ("（" + m.ExeArgEmpName + "）") : "") + "\n" + m.ExeNote).FirstOrDefault();
            }
            if (flow.FlowStatusID != (int)DataModel.FlowStatus.审批中)
            {
                this.Result.IsFlowFinished = true;
                XmlDocument flowXmlDocument = new XmlDocument();
                flowXmlDocument.LoadXml(flow.FlowXml);
                if (AllowEditWhenApproved(flowXmlDocument, flow.CreatorEmpId))
                {
                    this.Result.AllowEditControls = flow.FlowFinishControls;
                    this.Result.IsAllowUndo = flowXmlDocument.DocumentElement.GetAttribute("IsAllowUndo") == "1";
                }
            }
            if (currentNode.FlowNodeTypeID == -1)
            {
                if (this.Options.FlowMultiSignID == 0)
                {
                    throw new Common.JQException("找不到当前节点所属的有效会签节点！");
                }
                //获取出对应的会签节点的状态
                var flowMultiSign = this.DbContext.Set<DataModel.Models.FlowMultiSign>().FirstOrDefault(m => m.Id == this.Options.FlowMultiSignID);
                if (flowMultiSign == null)
                {
                    throw new Common.JQException("找不到当前节点所属的有效会签节点！");
                }
                this.Result.IsFinished = flowMultiSign.SignStatus == 1;
            }
            else
            {
                if (currentNode.FlowNodeStatusID != (int)DataModel.NodeStatus.轮到)
                {
                    this.Result.IsFinished = true;
                }
            }
            if (this.Result.IsFinished || this.Result.IsFlowFinished)
            {
                return;
            }
            this.Result.AllowEditControls = currentNode.FlowNodeWriteControlsName;
            this.Result.SignControls = currentNode.FlowNodeSignControlName;
            if (currentNode.FlowNodeSelectToSkip)
            {
                this.Result.NextAction = 2;
            }
            else
            {
                this.Result.NextAction = 1;
            }
            if (currentNode.FlowNodeSelectToBack)
            {
                this.Result.BackAction = 2;
            }
            else if (currentNode.FlowNodeBackID > 0)
            {
                this.Result.BackAction = 1;
            }
            if (currentNode.FlowNodeTypeID == 0)
            {
                if (currentNode.FlowNodeIsToFinish)
                {
                    this.Result.FinishAction = 1;
                }
                if (currentNode.FlowNodeIsToPass)
                {
                    this.Result.RejectAction = 1;
                }
            }
        }

        protected virtual void LoadNext()
        {
            if (Options.FlowNodeID == 0)
            {
                //新流程
                if (string.IsNullOrEmpty(this.Options.RefTable))
                {
                    throw new Common.JQException("参数RefTable错误");
                }
                var allNodes = this.DbContext.Set<DataModel.Models.FlowModelNode>().Where(m => m.FK_FlowModelNode_FlowModelID.ModelRefTable == this.Options.RefTable).OrderBy(m => m.NodeOrder);
                var currentNode = allNodes.FirstOrDefault(m => m.NodeOrder == 1);
                if (currentNode == null)
                {
                    throw new Common.JQException("无法找到当前有效的节点");
                }
                this.Result.Result = true;
                this.Result.NextSteps = new List<FlowWareStep>();
                this.Result.DefaultNote = currentNode.NodeAppDefaultValue;
                this.Result.IsNoteRequired = currentNode.NodeAppIsRequired;
                this.Result.FlowNodeID = currentNode.Id;
                if (currentNode.NodeSelectToSkip)
                {
                    var nextNodes = allNodes.Where(m => m.NodeOrder > currentNode.NodeOrder).ToList();
                    if (nextNodes.Count > 0)
                    {
                        foreach (var nextNode in nextNodes)
                        {
                            this.Result.NextSteps.Add(FlowWareStep.GetStep(nextNode, BaseEmployeeDB, this.Options.CurrentUser, this.DbContext));
                        }
                    }
                    this.Result.DefaultChoosedStep = currentNode.NodeNodeSkipID;
                }
                else
                {
                    var nextNode = allNodes.FirstOrDefault(m => m.NodeParentID == currentNode.Id);
                    if (nextNode == null)
                    {
                        this.Result.IsLastStep = true;
                        return;
                    }
                    this.Result.NextSteps.Add(FlowWareStep.GetStep(nextNode, BaseEmployeeDB, this.Options.CurrentUser, this.DbContext));
                    if (currentNode.NodeNodeSkipID > 0)
                    {
                        var skipNode = this.DbContext.Set<DataModel.Models.FlowModelNode>().FirstOrDefault(m => m.Id == currentNode.NodeNodeSkipID);
                        if (skipNode != null)
                        {
                            this.Result.NextSteps.Add(FlowWareStep.GetStep(skipNode, BaseEmployeeDB, this.Options.CurrentUser, this.DbContext));
                        }
                    }
                }
            }
            else
            {
                //从现有数据中读出
                var currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == this.Options.FlowNodeID);
                if (currentNode == null)
                {
                    throw new Common.JQException("找不到指定的流程节点");
                }
                this.Result.Result = true;
                this.Result.NextSteps = new List<FlowWareStep>();
                this.Result.DefaultNote = currentNode.FlowNodeAppDefaultValue;
                this.Result.IsNoteRequired = currentNode.FlowNodeAppIsRequired;
                var allNodes = this.DbContext.Set<DataModel.Models.FlowNode>().Where(m => m.FlowID == currentNode.FlowID).OrderBy(m => m.FlowNodeOrder);
                var nextNode = allNodes.FirstOrDefault(m => m.FlowNodeParentID == currentNode.Id);
                if (nextNode == null)
                {
                    this.Result.IsLastStep = true;
                    return;
                }
                if (currentNode.FlowNodeSelectToSkip)
                {
                    var nextNodes = allNodes.Where(m => m.FlowID == currentNode.FlowID && m.FlowNodeOrder > currentNode.FlowNodeOrder).ToList();
                    if (nextNodes.Count > 0)
                    {
                        foreach (var tempNode in nextNodes)
                        {
                            this.Result.NextSteps.Add(FlowWareStep.GetStep(tempNode, BaseEmployeeDB, this.Options.CurrentUser.EmpDepID, this.DbContext, true));
                        }
                    }
                }
                else
                {
                    this.Result.NextSteps.Add(FlowWareStep.GetStep(nextNode, BaseEmployeeDB, this.Options.CurrentUser.EmpDepID, this.DbContext, true));
                    if (currentNode.FlowNodeNodeSkipID > 0)
                    {
                        var skipNode = allNodes.FirstOrDefault(m => m.Id == currentNode.FlowNodeNodeSkipID);
                        if (skipNode != null)
                        {
                            this.Result.NextSteps.Add(FlowWareStep.GetStep(skipNode, BaseEmployeeDB, this.Options.CurrentUser.EmpDepID, this.DbContext, true));
                        }
                    }
                }
            }
        }

        protected virtual void LoadBack()
        {
            var currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == this.Options.FlowNodeID);
            if (currentNode == null)
            {
                throw new Common.JQException("无法找到当前的有效节点");
            }
            this.Result.Result = true;
            this.Result.NextSteps = new List<FlowWareStep>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(currentNode.FlowNodeXml);
            var xBackValue = (XmlElement)xmlDocument.SelectSingleNode("Root/NodeAppDefaultBackValue");
            if (xBackValue != null)
            {
                this.Result.DefaultNote = xBackValue.InnerText;
            }
            else
            {
                this.Result.DefaultNote = "";
            }
            if (currentNode.FlowNodeSelectToBack)
            {
                var previousNodes = this.DbContext.Set<DataModel.Models.FlowNode>().Where(m => m.FlowID == currentNode.FlowID && m.FlowNodeOrder < currentNode.FlowNodeOrder).OrderByDescending(m => m.FlowNodeOrder).ToList();
                if (previousNodes.Count() > 0)
                {
                    foreach (var previousNode in previousNodes)
                    {
                        this.Result.NextSteps.Add(FlowWareStep.GetStep(previousNode, BaseEmployeeDB, this.Options.CurrentUser.EmpDepID, this.DbContext, true));
                    }
                }
                this.Result.DefaultChoosedStep = currentNode.FlowNodeBackID;
            }
            else if (currentNode.FlowNodeBackID > 0)
            {
                var backNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == currentNode.FlowNodeBackID);
                if (backNode != null)
                {
                    this.Result.NextSteps.Add(FlowWareStep.GetStep(backNode, BaseEmployeeDB, this.Options.CurrentUser.EmpDepID, this.DbContext, true));
                }
            }
        }

        protected virtual void LoadTransfer()
        {
            var currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().Find(this.Options.FlowNodeID);
            if (currentNode == null)
            {
                throw new Common.JQException("无法找到当前的有效节点");
            }
            this.Result.Result = true;
            this.Result.NextSteps = new List<FlowWareStep>();
            this.Result.NextSteps.Add(FlowWareStep.GetStep(currentNode, BaseEmployeeDB, this.Options.CurrentUser.EmpDepID, this.DbContext, false, true));
            if (this.Result.NextSteps.Count > 0 && this.Result.NextSteps[0].Users.Count > 0)
            {
                //去除掉当前登录的人员
                if (currentNode.FlowNodeTypeID != -1)
                {
                    //不为会签
                    foreach (var item in this.Result.NextSteps[0].Users)
                    {
                        if (item.ID == currentNode.FlowNodeEmpId)
                        {
                            this.Result.NextSteps[0].Users.Remove(item);
                            break;
                        }
                    }
                }
                else if (this.Options.FlowMultiSignID > 0)
                {
                    var flowMultiSignNode = this.DbContext.Set<DataModel.Models.FlowMultiSign>().FirstOrDefault(m => m.Id == this.Options.FlowMultiSignID);
                    if (flowMultiSignNode != null)
                    {
                        foreach (var item in this.Result.NextSteps[0].Users)
                        {
                            if (item.ID == flowMultiSignNode.SignEmpId)
                            {
                                this.Result.NextSteps[0].Users.Remove(item);
                                break;
                            }
                        }
                    }
                }
            }
            this.Result.DefaultNote = currentNode.FlowNodeAppDefaultValue;
        }

        protected virtual void LoadUndo()
        {
            var flow = this.DbContext.Set<DataModel.Models.Flow>().FirstOrDefault(m => m.FlowRefID == this.Options.RefID && m.FlowRefTable == this.Options.RefTable);
            if (flow == null || flow.FlowStatusID == (int)DataModel.FlowStatus.审批中)
            {
                throw new Common.JQException("流程状态无效");
            }
            this.Result.Result = true;
            this.Result.NextSteps = new List<FlowWareStep>();
            var allNodes = this.DbContext.Set<DataModel.Models.FlowNode>().Where(m => m.FlowID == flow.Id && m.FlowNodeStatusID == (int)DataModel.NodeStatus.完成).OrderBy(m => m.FlowNodeOrder).ToList();
            foreach (var node in allNodes)
            {
                var step = FlowWareStep.GetStep(node, BaseEmployeeDB, this.Options.CurrentUser.EmpDepID, this.DbContext, true);
                this.Result.NextSteps.Add(step);
            }
        }

        protected virtual void Undo()
        {
            var currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == this.Options.NextNodeID);
            if (currentNode == null || currentNode.FlowNodeStatusID == (int)DataModel.NodeStatus.轮到 || currentNode.FlowNodeStatusID == (int)DataModel.NodeStatus.安排 || currentNode.FlowNodeStatusID == (int)DataModel.NodeStatus.未安排 || currentNode.FlowNodeStatusID == (int)DataModel.NodeStatus.跳过)
            {
                throw new Common.JQException("节点状态不对！");
            }
            var flow = this.DbContext.Set<DataModel.Models.Flow>().FirstOrDefault(m => m.Id == currentNode.FlowID);
            if (flow == null || flow.FlowStatusID == (int)DataModel.FlowStatus.审批中)
            {
                throw new Common.JQException("会签节点状态不对！");
            }
            var flowProcessor = GetProcessor();
            var flowXmlDocument = new XmlDocument();
            flowXmlDocument.LoadXml(flow.FlowXml);
            InitProjectIDs(flowXmlDocument);
            using (var tran = this.DbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    OnExecuting(flowProcessor);
                    OnSaveForm(flowProcessor);
                    if (!string.IsNullOrEmpty(this.Options.Title))
                    {
                        flow.FlowTitle = this.Options.Title;
                    }
                    //插入一条撤销审批的流转记录
                    var flowNodeExe = new DataModel.Models.FlowNodeExe()
                    {
                        ExeActionDate = DateTime.Now,
                        ExeActionID = (int)DataModel.NodeAction.变更,
                        FlowID = currentNode.FlowID,
                        FlowNodeID = 0,
                        ExeEmpId = this.Options.CurrentUser.EmpID,
                        ExeEmpName = this.Options.CurrentUser.EmpName,
                        ExeNote = this.Options.Note
                    };
                    if (this.Options.CurrentUser.AgenEmpID > 0)
                    {
                        flowNodeExe.ExeArgEmpId = this.Options.CurrentUser.AgenEmpID;
                        flowNodeExe.ExeArgEmpName = this.Options.CurrentUser.AgenEmpName;
                    }
                    this.DbContext.Set<DataModel.Models.FlowNodeExe>().Add(flowNodeExe);
                    var flowStatusName = "";
                    currentNode.FlowNodeFromDateTime = DateTime.Now;
                    currentNode.FlowNodeStatusID = (int)DataModel.NodeStatus.轮到;
                    currentNode.FlowNodeFromEmpId = this.Options.CurrentUser.EmpID;
                    if (currentNode.FlowNodeTypeID == -1)
                    {
                        //会签处理
                        flowStatusName = "轮到（" + ProcessMultiSignNode(flowXmlDocument, currentNode, true) + "）";
                    }
                    else
                    {
                        flowStatusName = "轮到（" + currentNode.FlowNodeEmpName + "）";
                        CreateNodeExe(currentNode);
                        SetTurned(flowXmlDocument, currentNode.FlowNodeEmpId.ToString());
                    }
                    flow.FlowStatusID = (int)DataModel.FlowStatus.审批中;
                    flow.FlowStatusName = flowStatusName;
                    flow.LastModificationTime = DateTime.Now;
                    flow.LastModifierEmpId = this.Options.CurrentUser.EmpID;
                    flow.LastModifierEmpName = this.Options.CurrentUser.EmpName;
                    OnExecuted(flowProcessor, true);
                    SetProjectIDs(flowXmlDocument);
                    flow.FlowXml = flowXmlDocument.OuterXml;
                    this.DbContext.SaveChanges();
                    tran.Commit();
                    this.Result.Result = true;
                    this.Result.FlowRefID = this.Options.RefID;
                }
                catch
                {
                    tran.Rollback();
                    OnExecuted(flowProcessor, false);
                    throw;
                }
            }
            Flow.RemoveCache();
            SendNotify(flow.FlowModular, 0, true);
            //推送极光消息
            if (currentNode != null)
            {
                if (this.Options.MobilePushFlowRefTables.Contains(this.Options.RefTable))
                {
                    Common.JPush.push(flow.FlowName, flow.FlowTitle, this.Options.RefTable, this.Options.RefID, currentNode.FlowNodeEmpId.ToString(), currentNode.Id, 0);
                }
            }
        }

        /// <summary>
        /// 保存表单
        /// </summary>
        protected virtual void Save()
        {
            var flowProcessor = GetProcessor();
            if (flowProcessor == null)
            {
                return;
            }
            using (var tran = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    this.Options.ProjectIDs = new List<int>();
                    this.Options.ProjectPhases = new Dictionary<int, List<int>>();
                    //暂存时步骤默认为1
                    this.Options.FlowNodeOrder = 1;
                    OnExecuting(flowProcessor);
                    OnSaveForm(flowProcessor);
                    if (this.Options.FlowNodeID > 0 && !string.IsNullOrEmpty(this.Options.Note))
                    {
                        var currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == this.Options.FlowNodeID);
                        if (currentNode != null)
                        {
                            currentNode.FlowNodeXml = RecordSignNote(currentNode.FlowNodeXml);
                        }
                    }
                    OnExecuted(flowProcessor, true);
                    this.DbContext.SaveChanges();
                    tran.Commit();
                    this.Result.Result = true;
                    this.Result.FlowRefID = this.Options.RefID;
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                    OnExecuted(flowProcessor, false);
                    throw;
                }
            }
        }

        /// <summary>
        /// 提交审批
        /// </summary>
        protected virtual void Next()
        {
            if (Options.FlowNodeID == 0)
            {
                //加入新的流程
                ProcessNextWithNewFlow();
            }
            else
            {
                //使用已经存在的流程
                ProcessNextWithExistsFlow();
            }
        }

        /// <summary>
        /// 处理提交审批（发起新流程）
        /// </summary>
        private void ProcessNextWithNewFlow()
        {
            //新流程
            if (string.IsNullOrEmpty(this.Options.RefTable))
            {
                throw new Common.JQException("参数RefTable错误");
            }
            var model = this.DbContext.Set<DataModel.Models.FlowModel>().FirstOrDefault(m => m.ModelRefTable == this.Options.RefTable);
            if (model == null)
            {
                throw new Common.JQException("找不到可使用的模板");
            }
            var modelNodes = this.DbContext.Set<DataModel.Models.FlowModelNode>().Where(m => m.FK_FlowModelNode_FlowModelID.ModelRefTable == this.Options.RefTable).ToList();
            if (modelNodes.Count < 2)
            {
                throw new Common.JQException("模板中找不到有效的节点");
            }
            var startNode = modelNodes.FirstOrDefault(m => m.NodeParentID == 0);
            if (startNode == null)
            {
                throw new Common.JQException("找不到模板中的开始节点");
            }
            var firstNode = modelNodes.FirstOrDefault(m => m.NodeParentID == startNode.Id);
            if (firstNode == null)
            {
                throw new Common.JQException("找不到模板中的第一个有效节点");
            }
            this.Options.FlowNodeOrder = firstNode.NodeOrder;
            var flow = GetFlow(model);
            var list = new List<DataModel.Models.FlowNode>();
            while (firstNode != null)
            {
                list.Add(GetFlowNode(firstNode));
                firstNode = modelNodes.FirstOrDefault(m => m.NodeParentID == firstNode.Id);
            }
            var temp = list.FirstOrDefault(m => m.FlowNodeParentID == startNode.Id);
            Dictionary<int, int> idsrec = new Dictionary<int, int>();
            Dictionary<int, string> empsToSendFinish = null;
            var flowProcessor = GetProcessor();
            var statusName = "";
            this.Options.IsNew = true;
            this.Options.ProjectIDs = new List<int>();
            this.Options.ProjectPhases = new Dictionary<int, List<int>>();
            using (var tran = this.DbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    OnExecuting(flowProcessor);
                    //flowProcessor?.OnExecuting();
                    this.DbContext.Set<DataModel.Models.Flow>().Add(flow);
                    this.DbContext.SaveChanges();
                    this.Options.FlowID = flow.Id;
                    OnSaveForm(flowProcessor);
                    flow.FlowRefID = this.Options.RefID;
                    if (!string.IsNullOrEmpty(this.Options.Title))
                    {
                        flow.FlowTitle = this.Options.Title;
                    }
                    XmlDocument flowXmlDocument = new XmlDocument();
                    flowXmlDocument.LoadXml(flow.FlowXml);
                    this.Options.FlowID = flow.Id;
                    var isFirst = true;
                    var isFindNext = false;
                    while (temp != null)
                    {
                        temp.FlowID = flow.Id;
                        if (idsrec.ContainsKey(temp.FlowNodeParentID))
                        {
                            temp.FlowNodeParentID = idsrec[temp.FlowNodeParentID];
                        }
                        else
                        {
                            temp.FlowNodeParentID = 0;
                        }
                        if (idsrec.ContainsKey(temp.FlowNodeBackID))
                        {
                            temp.FlowNodeBackID = idsrec[temp.FlowNodeBackID];
                        }
                        else
                        {
                            temp.FlowNodeBackID = 0;
                        }
                        if (idsrec.ContainsKey(temp.FlowNodeSameId))
                        {
                            temp.FlowNodeSameId = idsrec[temp.FlowNodeSameId];
                        }
                        else
                        {
                            temp.FlowNodeSameId = 0;
                        }
                        if (isFirst)
                        {
                            isFirst = false;
                            temp.FlowNodeStatusID = (int)DataModel.NodeStatus.完成;
                            temp.FlowNodeDate = DateTime.Now;
                            temp.FlowNodeEmpId = this.Options.CurrentUser.EmpID;
                            temp.FlowNodeEmpName = this.Options.CurrentUser.EmpName;
                            temp.FlowNodeFromDateTime = DateTime.Now;
                            if (temp.FlowNodeValidateGroup == "1")
                            {
                                //需要存储数据
                                temp.FlowNodeXml = RecordSignNote(temp.FlowNodeXml);
                            }
                        }
                        else if (!isFindNext)
                        {
                            temp.FlowNodeFromDateTime = DateTime.Now;
                            temp.FlowNodeFromEmpId = this.Options.CurrentUser.EmpID;
                            temp.FlowNodeFromDateTime = DateTime.Now;
                            if (temp.FlowModelNodeID != this.Options.NextNodeID)
                            {
                                temp.FlowNodeStatusID = (int)DataModel.NodeStatus.跳过;
                                temp.FlowNodeDate = DateTime.Now;
                            }
                            else
                            {
                                isFindNext = true;
                                temp.FlowNodeStatusID = (int)DataModel.NodeStatus.轮到;
                                if (temp.FlowNodeTypeID == 0)
                                {
                                    temp.FlowNodeEmpId = Common.ModelConvert.ConvertToDefault<int>(this.Options.NextEmpIDs);
                                    if (temp.FlowNodeEmpId == 0)
                                    {
                                        throw new NotImplementedException("请选择有效的下一步人员！");
                                    }
                                    temp.FlowNodeEmpName = GetEmpName(temp.FlowNodeEmpId);
                                    statusName = "轮到（" + temp.FlowNodeEmpName + "）";
                                    SetTurned(flowXmlDocument, temp.FlowNodeEmpId.ToString());
                                }
                            }
                        }
                        else
                        {
                            temp.FlowNodeStatusID = (int)DataModel.NodeStatus.未安排;
                        }
                        this.DbContext.Set<DataModel.Models.FlowNode>().Add(temp);
                        this.DbContext.SaveChanges();
                        if (!isFindNext || (temp.FlowNodeStatusID == (int)DataModel.NodeStatus.轮到 && temp.FlowNodeTypeID == 0))
                        {
                            CreateNodeExe(temp);
                        }
                        else if (temp.FlowNodeStatusID == (int)DataModel.NodeStatus.轮到 && temp.FlowNodeTypeID == -1)
                        {
                            //处理会签
                            var nextEmpNames = ProcessMultiSignNode(flowXmlDocument, temp);
                            if (string.IsNullOrEmpty(nextEmpNames))
                            {
                                throw new Common.JQException("请选择有效的下一步会签人员");
                            }
                            statusName = "轮到（" + nextEmpNames + "）";
                        }
                        idsrec.Add(temp.FlowModelNodeID, temp.Id);
                        temp = list.FirstOrDefault(m => m.FlowNodeParentID == temp.FlowModelNodeID);
                    }
                    //处理SkipId
                    foreach (var node in list)
                    {
                        if (node.FlowNodeNodeSkipID > 0 && idsrec.ContainsKey(node.FlowNodeNodeSkipID))
                        {
                            node.FlowNodeNodeSkipID = idsrec[node.FlowNodeNodeSkipID];
                            //将状态再次重置为编辑状态
                            this.DbContext.Entry(node).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            node.FlowNodeNodeSkipID = 0;
                        }
                    }
                    if (!isFindNext)
                    {
                        //未找到下一个节点，代表本流程已经结束
                        flow.FlowStatusID = (int)DataModel.FlowStatus.审批结束;
                        flow.FlowStatusName = "审批结束";
                        flow.FlowFinishDate = DateTime.Now;
                        OnAfterApproveFinished(flowProcessor);
                        empsToSendFinish = SendFinishMessage(flow);
                    }
                    else if (!string.IsNullOrEmpty(statusName))
                    {
                        flow.FlowStatusName = statusName;
                    }
                    flow.LastModificationTime = DateTime.Now;
                    flow.LastModifierEmpId = this.Options.CurrentUser.EmpID;
                    flow.LastModifierEmpName = this.Options.CurrentUser.EmpName;
                    this.DbContext.Entry<DataModel.Models.Flow>(flow).State = System.Data.Entity.EntityState.Modified;
                    OnExecuted(flowProcessor, true);
                    SetProjectIDs(flowXmlDocument);
                    flow.FlowXml = flowXmlDocument.OuterXml;
                    //插入表单的创建记录（项目动态）
                    if (this.Options.ProjectPhases != null && this.Options.ProjectPhases.Count > 0)
                    {
                        //插入项目动态
                        foreach (var pp in this.Options.ProjectPhases)
                        {
                            if (pp.Value == null || pp.Value.Count == 0)
                            {
                                //插入单工程的
                                var projectTaskGroup = this.DbContext.Set<DataModel.Models.DesTaskGroup>().FirstOrDefault(m => m.ProjId == pp.Key && m.TaskGroupType == 1);
                                if (projectTaskGroup == null)
                                {
                                    continue;
                                }
                                var data = new DataModel.Models.ProjectDynamic()
                                {
                                    CreationTime = DateTime.Now,
                                    CreatorDepId = this.Options.CurrentUser.EmpDepID,
                                    CreatorDepName = this.Options.CurrentUser.EmpDepName,
                                    CreatorEmpId = this.Options.CurrentUser.EmpID,
                                    Content = "[" + projectTaskGroup.ProjNumber + "]" + projectTaskGroup.ProjName + " 由 " + this.Options.CurrentUser.EmpName + (this.Options.CurrentUser.AgenEmpID > 0 ? "(" + this.Options.CurrentUser.AgenEmpName + ")" : "") + " 发起了表单 " + flow.FlowName,
                                    IsInvalid = false,
                                    RefID = JQ.Util.TypeParse.parse<int>(projectTaskGroup.Id),
                                    RefTable = "ISOForm",
                                    ProjectID = projectTaskGroup.ProjId,
                                    CreatorEmpName = this.Options.CurrentUser.EmpName
                                };
                                if (this.Options.CurrentUser.AgenEmpID > 0)
                                {
                                    data.AgenEmpId = this.Options.CurrentUser.AgenEmpID;
                                    data.AgenEmpName = this.Options.CurrentUser.AgenEmpName;
                                }
                                else
                                {
                                    data.AgenEmpId = 0;
                                    data.AgenEmpName = "";
                                }
                                this.DbContext.Set<DataModel.Models.ProjectDynamic>().Add(data);
                            }
                            else
                            {
                                //插入阶段的
                                foreach (var phase in pp.Value)
                                {
                                    var phaseTaskGroup = this.DbContext.Set<DataModel.Models.DesTaskGroup>().FirstOrDefault(m => m.ProjId == pp.Key && m.TaskGroupPhaseId == phase && m.TaskGroupType == 1);
                                    if (phaseTaskGroup == null)
                                    {
                                        continue;
                                    }
                                    //通过工程ID，和阶段ID获取出taskgroup
                                    var data = new DataModel.Models.ProjectDynamic()
                                    {
                                        CreationTime = DateTime.Now,
                                        CreatorDepId = this.Options.CurrentUser.EmpDepID,
                                        CreatorDepName = this.Options.CurrentUser.EmpDepName,
                                        CreatorEmpId = this.Options.CurrentUser.EmpID,
                                        Content = "[" + phaseTaskGroup.ProjNumber + "]" + phaseTaskGroup.ProjName + " > " + phaseTaskGroup.TaskGroupName + " 由 " + this.Options.CurrentUser.EmpName + (this.Options.CurrentUser.AgenEmpID > 0 ? "(" + this.Options.CurrentUser.AgenEmpName + ")" : "") + " 发起了表单 " + flow.FlowName,
                                        IsInvalid = false,
                                        RefID = JQ.Util.TypeParse.parse<int>(phaseTaskGroup.Id),
                                        RefTable = "ISOForm",
                                        ProjectID = phaseTaskGroup.ProjId,
                                        CreatorEmpName = this.Options.CurrentUser.EmpName
                                    };
                                    if (this.Options.CurrentUser.AgenEmpID > 0)
                                    {
                                        data.AgenEmpId = this.Options.CurrentUser.AgenEmpID;
                                        data.AgenEmpName = this.Options.CurrentUser.AgenEmpName;
                                    }
                                    else
                                    {
                                        data.AgenEmpId = 0;
                                        data.AgenEmpName = "";
                                    }
                                    this.DbContext.Set<DataModel.Models.ProjectDynamic>().Add(data);
                                }
                            }
                        }
                    }
                    this.DbContext.SaveChanges();
                    tran.Commit();
                    this.Result.Result = true;
                    this.Result.FlowRefID = this.Options.RefID;
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                    OnExecuted(flowProcessor, false);
                    throw;
                }
            }
            Flow.RemoveCache();
            SendNotify(flow.FlowModular, 0, true);
            SendFinishMessageNotify(empsToSendFinish);
            //推送极光消息
            if (!string.IsNullOrEmpty(this.Options.NextEmpIDs))
            {
                if (this.Options.MobilePushFlowRefTables.Contains(this.Options.RefTable))
                {
                    Common.JPush.push(flow.FlowName, flow.FlowTitle, this.Options.RefTable, this.Options.RefID, this.Options.NextEmpIDs, this.Options.FlowNodeID, this.Options.FlowMultiSignID);
                }
            }
        }

        private void SendFinishMessageNotify(Dictionary<int, string> empsToSendFinish)
        {
            if (empsToSendFinish == null || empsToSendFinish.Count == 0)
            {
                return;
            }
            Oa.OaMessRead.CacheRemove();
            foreach (var s in empsToSendFinish)
            {
                var t = JQ.Util.IO.MessageMonitor.NotifyAsync(s.Key, delegate (int empID) { return new DBSql.Oa.OaMessRead().GetNotifyDatas(empID); });
            }
        }

        private void SendNotify(string flowModular, int currentEmpID = 0, bool isNewFlow = false)
        {
            if (currentEmpID == 0)
            {
                currentEmpID = Options.CurrentUser.EmpID;
            }
            if (flowModular == "1")
            {
                //工程表单
                if (!isNewFlow)
                {
                    var t = JQ.Util.IO.MessageMonitor.NotifyAsync(currentEmpID, delegate (int _empID, JQ.Util.IO.MessageMonitor.NotifyOption _option)
                    {
                        if (_option == null || (_option != null && ("," + _option.AgentFlows + ",").IndexOf("," + this.Options.RefTable + ",") > -1))
                        {
                            return new { action = "ChangeTodoProjectAmount", amount = -1 };
                        }
                        return null;
                    });
                }
                var empID = 0;
                foreach (var str in this.Options.NextEmpIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (int.TryParse(str, out empID) && empID > 0)
                    {
                        var t = JQ.Util.IO.MessageMonitor.NotifyAsync(empID, delegate (int _empID, JQ.Util.IO.MessageMonitor.NotifyOption _option)
                        {
                            if (_option == null || (_option != null && ("," + _option.AgentFlows + ",").IndexOf("," + this.Options.RefTable + ",") > -1))
                            {
                                return new { action = "ChangeTodoProjectAmount", amount = 1 };
                            }
                            return null;
                        });
                    }
                }
            }
            else if (flowModular == "2")
            {
                //办公表单
                if (!isNewFlow)
                {
                    var t = JQ.Util.IO.MessageMonitor.NotifyAsync(currentEmpID, delegate (int _empID, JQ.Util.IO.MessageMonitor.NotifyOption _option)
                    {
                        if (_option == null || (_option != null && ("," + _option.AgentFlows + ",").IndexOf("," + this.Options.RefTable + ",") > -1))
                        {
                            return new { action = "ChangeTodoOAAmount", amount = -1 };
                        }
                        return null;
                    });
                }
                var empID = 0;
                foreach (var str in this.Options.NextEmpIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (int.TryParse(str, out empID) && empID > 0)
                    {
                        var t = JQ.Util.IO.MessageMonitor.NotifyAsync(empID, delegate (int _empID, JQ.Util.IO.MessageMonitor.NotifyOption _option)
                        {
                            if (_option == null || (_option != null && ("," + _option.AgentFlows + ",").IndexOf("," + this.Options.RefTable + ",") > -1))
                            {
                                return new { action = "ChangeTodoOAAmount", amount = 1 };
                            }
                            return null;
                        });
                    }
                }
            }
        }

        private Dictionary<int, string> SendFinishMessage(DataModel.Models.Flow flow)
        {
            if (flow == null || string.IsNullOrEmpty(this.Options.Message))
            {
                return null;
            }
            Dictionary<int, string> emps = new Dictionary<int, string>();
            //获取出需要发送消息的人员
            if (!string.IsNullOrEmpty(flow.FlowFinishSend))
            {
                foreach (var empID in flow.FlowFinishSend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var temp = 0;
                    if (int.TryParse(empID, out temp) && temp > 0 && !emps.ContainsKey(temp))
                    {
                        var emp = BaseEmployeeDB.GetBaseEmployeeInfo(temp);
                        if (emp != null)
                        {
                            emps.Add(emp.EmpID, emp.EmpName);
                        }
                    }
                }
            }
            var result = this.DbContext.Set<DataModel.Models.FlowNode>().GroupJoin(this.DbContext.Set<DataModel.Models.FlowMultiSign>(), t => t.Id, t1 => t1.FlowNodeID, (t, t1) => new { t.FlowID, t.FlowNodeIsToMessage, t.Id, t.FlowNodeTypeID, t.FlowNodeEmpId, t.FlowNodeEmpName, MultiSign = t1.Select(m => new { m.SignEmpId, m.SignEmpName }) }).Where(m => m.FlowID == this.Options.FlowID && m.FlowNodeIsToMessage == true).Select(m => m).ToList();
            foreach (var item in result)
            {
                if (item.FlowNodeTypeID == -1)
                {
                    foreach (var multi in item.MultiSign)
                    {
                        if (multi.SignEmpId == 0 || emps.ContainsKey(multi.SignEmpId))
                        {
                            continue;
                        }
                        emps.Add(multi.SignEmpId, multi.SignEmpName);
                    }
                }
                else
                {
                    if (item.FlowNodeEmpId == 0 || emps.ContainsKey(item.FlowNodeEmpId))
                    {
                        continue;
                    }
                    emps.Add(item.FlowNodeEmpId, item.FlowNodeEmpName);
                }
            }
            if (emps.Count == 0)
            {
                return emps;
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(flow.FlowXml);
            var oaMess = new DataModel.Models.OaMess()
            {
                MessDate = DateTime.Now,
                MessEmpId = this.Options.CurrentUser.EmpID,
                MessEmpName = this.Options.CurrentUser.EmpName,
                MessIsAutoReturn = false,
                MessIsDeleted = false,
                MessIsSystem = true,
                MessLinkTitle = flow.FlowName,
                MessLinkUrl = string.Format(flow.FlowUrl, flow.FlowRefID, 0, 0),
                MessNote = "",
                MessRefID = this.Options.RefID,
                MessRefTable = this.Options.RefTable,
                MessTag = "",
                MessTitle = this.Options.Message
            };
            var xProcessDialog = (XmlElement)xmlDocument.SelectSingleNode("Root/ProcessDialog");
            if (xProcessDialog == null)
            {
                oaMess.MessDialogWidth = JQ.Util.TypeParse.parse<int>(xProcessDialog.GetAttribute("Width"));
                oaMess.MessDialogHeight = JQ.Util.TypeParse.parse<int>(xProcessDialog.GetAttribute("Height"));
            }
            else
            {
                oaMess.MessDialogWidth = 800;
                oaMess.MessDialogHeight = 600;
            }
            this.DbContext.Set<DataModel.Models.OaMess>().Add(oaMess);
            this.DbContext.SaveChanges();
            foreach (var item in emps)
            {
                this.DbContext.Set<DataModel.Models.OaMessRead>().Add(new DataModel.Models.OaMessRead()
                {
                    Id = oaMess.Id,
                    MessReadDate = JQ.Util.TypeParse.DefaultDateTime,
                    MessReadEmpId = item.Key,
                    MessReadEmpName = item.Value,
                    MessReadIsDeleted = false,
                    MessReadNote = ""
                });
            }
            return emps;
        }

        /// <summary>
        /// 处理提交审批（在现有流程上）
        /// </summary>
        private void ProcessNextWithExistsFlow()
        {
            var currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == this.Options.FlowNodeID);
            EnsureCurrentNode(currentNode);
            DataModel.Models.FlowMultiSign flowMultiSign = null;
            if (currentNode.FlowNodeTypeID == -1)
            {
                //当前为会签，获取出会签节点
                flowMultiSign = this.DbContext.Set<DataModel.Models.FlowMultiSign>().FirstOrDefault(m => m.Id == this.Options.FlowMultiSignID);
                if (flowMultiSign == null || flowMultiSign.FlowNodeID != currentNode.Id)
                {
                    throw new Common.JQException("会签节点状态不对！");
                }
                EnsureProcessor(flowMultiSign.SignEmpId, flowMultiSign.SignEmpName);
            }
            else
            {
                EnsureProcessor(currentNode.FlowNodeEmpId, currentNode.FlowNodeEmpName);
            }
            var flow = this.DbContext.Set<DataModel.Models.Flow>().FirstOrDefault(m => m.Id == currentNode.FlowID);
            EnsureFlow(flow);
            this.Options.FlowID = flow.Id;
            this.Options.FlowNodeOrder = currentNode.FlowNodeOrder;
            Dictionary<int, string> empsToSendFinish = null;
            var flowProcessor = GetProcessor();
            var flowStatusName = "";
            var flowXmlDocument = new XmlDocument();
            flowXmlDocument.LoadXml(flow.FlowXml);
            InitProjectIDs(flowXmlDocument);
            using (var tran = this.DbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    OnExecuting(flowProcessor);
                    OnSaveForm(flowProcessor);
                    if (!string.IsNullOrEmpty(this.Options.Title))
                    {
                        flow.FlowTitle = this.Options.Title;
                    }
                    bool isNextTurn = true;
                    if (currentNode.FlowNodeTypeID == -1)
                    {
                        isNextTurn = false;
                        flowMultiSign.SignStatus = 1;
                        flowMultiSign.SignDate = DateTime.Now;
                        flowMultiSign.SignNote = "";
                        CreateNodeExe(flowMultiSign, GetNodeAction((int)DataModel.NodeStatus.完成), this.Options.Note);
                        //判断是否当前节点的会签人员节点都已经完成了
                        var names = this.DbContext.Set<DataModel.Models.FlowMultiSign>().Where(m => m.FlowNodeID == flowMultiSign.FlowNodeID && m.Id != flowMultiSign.Id && m.SignStatus == 0).Select(m => m.SignEmpName).ToList<string>();
                        //存在会签，处理
                        if (names.Count == 0)
                        {
                            isNextTurn = true;
                        }
                        else
                        {
                            flowStatusName = "轮到（" + string.Join(",", names.ToArray()) + "）";
                        }
                        if (currentNode.FlowNodeValidateGroup == "1")
                        {
                            //记录会签的记录
                            currentNode.FlowNodeXml = RecordSignNote(currentNode.FlowNodeXml, flowMultiSign.Id);
                        }
                    }
                    if (isNextTurn)
                    {
                        currentNode.FlowNodeStatusID = (int)DataModel.NodeStatus.完成;
                        currentNode.FlowNodeDate = DateTime.Now;
                        if (this.Options.CurrentUser.AgenEmpID > 0)
                        {
                            currentNode.AgenEmpId = this.Options.CurrentUser.AgenEmpID;
                            currentNode.AgenEmpName = this.Options.CurrentUser.AgenEmpName;
                        }
                        if (currentNode.FlowNodeTypeID != -1)
                        {
                            //会签节点完成时不需要生成流转记录
                            CreateNodeExe(currentNode);
                            if (currentNode.FlowNodeValidateGroup == "1")
                            {
                                currentNode.FlowNodeXml = RecordSignNote(currentNode.FlowNodeXml);
                            }
                        }
                        //将当前节点的状态更新为
                        var nextNodes = this.DbContext.Set<DataModel.Models.FlowNode>().Where(m => m.FlowID == currentNode.FlowID && m.FlowNodeOrder > currentNode.FlowNodeOrder);
                        var nextNode = nextNodes.FirstOrDefault(m => m.FlowNodeParentID == currentNode.Id);
                        while (nextNode != null)
                        {
                            nextNode.FlowNodeFromDateTime = DateTime.Now;
                            nextNode.FlowNodeFromEmpId = this.Options.CurrentUser.EmpID;
                            if (nextNode.Id == this.Options.NextNodeID)
                            {
                                nextNode.FlowNodeStatusID = (int)DataModel.NodeStatus.轮到;
                                if (nextNode.FlowNodeTypeID == 0)
                                {
                                    nextNode.FlowNodeEmpId = Common.ModelConvert.ConvertToDefault<int>(this.Options.NextEmpIDs);
                                    if (nextNode.FlowNodeEmpId == 0)
                                    {
                                        throw new Common.JQException("请选择有效的下一步人员");
                                    }
                                    nextNode.FlowNodeEmpName = GetEmpName(nextNode.FlowNodeEmpId);
                                    flowStatusName = "轮到（" + nextNode.FlowNodeEmpName + "）";
                                    CreateNodeExe(nextNode);
                                    SetTurned(flowXmlDocument, nextNode.FlowNodeEmpId.ToString());
                                }
                                else
                                {
                                    //会签
                                    var names = ProcessMultiSignNode(flowXmlDocument, nextNode);
                                    if (string.IsNullOrEmpty(names))
                                    {
                                        throw new Common.JQException("请选择有效的下一步会签人员");
                                    }
                                    flowStatusName = "轮到（" + names + "）";
                                }
                                break;
                            }
                            else
                            {
                                nextNode.FlowNodeStatusID = (int)DataModel.NodeStatus.跳过;
                                nextNode.FlowNodeDate = DateTime.Now;
                                nextNode.FlowNodeEmpId = 0;
                                nextNode.FlowNodeEmpName = "";
                                nextNode.AgenEmpId = 0;
                                nextNode.AgenEmpName = "";
                            }
                            CreateNodeExe(nextNode);
                            nextNode = nextNodes.FirstOrDefault(m => m.FlowNodeParentID == nextNode.Id);
                        }
                        if (nextNode != null)
                        {
                            //包含下一步骤
                            flow.FlowStatusID = (int)DataModel.FlowStatus.审批中;
                            flow.FlowStatusName = flowStatusName;
                        }
                        else
                        {
                            //流程结束
                            flow.FlowStatusID = (int)DataModel.FlowStatus.审批结束;
                            flow.FlowStatusName = "审批结束";
                            flow.FlowFinishDate = DateTime.Now;
                            OnAfterApproveFinished(flowProcessor);
                            empsToSendFinish = SendFinishMessage(flow);
                        }
                    }
                    else
                    {
                        //更新节点名称
                        flow.FlowStatusID = (int)DataModel.FlowStatus.审批中;
                        flow.FlowStatusName = flowStatusName;
                    }
                    flow.LastModificationTime = DateTime.Now;
                    flow.LastModifierEmpId = this.Options.CurrentUser.EmpID;
                    flow.LastModifierEmpName = this.Options.CurrentUser.EmpName;
                    OnExecuted(flowProcessor, true);
                    SetProjectIDs(flowXmlDocument);
                    flow.FlowXml = flowXmlDocument.OuterXml;
                    this.DbContext.SaveChanges();
                    tran.Commit();
                    this.Result.Result = true;
                    this.Result.FlowRefID = this.Options.RefID;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    OnExecuted(flowProcessor, false);
                    throw;
                }
            }
            Flow.RemoveCache();
            SendNotify(flow.FlowModular);
            SendFinishMessageNotify(empsToSendFinish);
            //推送极光消息
            if (!string.IsNullOrEmpty(this.Options.NextEmpIDs))
            {
                if (this.Options.MobilePushFlowRefTables.Contains(this.Options.RefTable))
                {
                    Common.JPush.push(flow.FlowName, flow.FlowTitle, this.Options.RefTable, this.Options.RefID, this.Options.NextEmpIDs, this.Options.FlowNodeID, this.Options.FlowMultiSignID);
                }
            }
        }

        private string ProcessMultiSignNode(XmlDocument flowXmlDocument, DataModel.Models.FlowNode node, bool isUndo = false)
        {
            if (string.IsNullOrEmpty(this.Options.NextEmpIDs))
            {
                return "";
            }
            List<int> empIDs = null;
            if (!isUndo)
            {
                empIDs = new List<int>();
                foreach (var item in this.Options.NextEmpIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var empID = 0;
                    if (int.TryParse(item, out empID) && !empIDs.Contains(empID))
                    {
                        empIDs.Add(empID);
                    }
                }
                if (empIDs.Count == 0)
                {
                    return "";
                }
            }
            var turnedEmpNames = "";
            var turnedEmpIDs = "";
            //获取出原先的
            XmlDocument xNode = new XmlDocument();
            if (node.FlowNodeValidateGroup == "1")
            {
                xNode.LoadXml(node.FlowNodeXml);
            }
            var multiSignNodes = this.DbContext.Set<DataModel.Models.FlowMultiSign>().Where(m => m.FlowNodeID == node.Id);
            if (isUndo)
            {
                //撤销取原来的人
                empIDs = multiSignNodes.Select(m => m.SignEmpId).ToList();
            }
            foreach (var empID in empIDs)
            {
                var multiSignNode = multiSignNodes.FirstOrDefault(m => m.SignEmpId == empID);
                if (multiSignNode == null)
                {
                    multiSignNode = new DataModel.Models.FlowMultiSign()
                    {
                        FlowID = node.FlowID,
                        FlowNodeID = node.Id,
                        SignEmpId = empID,
                        SignEmpName = GetEmpName(empID),
                    };
                }
                multiSignNode.SignDate = JQ.Util.TypeParse.DefaultDateTime;
                multiSignNode.SignStatus = 0;
                multiSignNode.SignNote = "";
                if (multiSignNode.Id == 0)
                {
                    this.DbContext.Set<DataModel.Models.FlowMultiSign>().Add(multiSignNode);
                    this.DbContext.SaveChanges();
                    if (node.FlowNodeValidateGroup == "1")
                    {
                        var xMultiSignNote = xNode.CreateElement("SignNote");
                        xMultiSignNote.SetAttribute("MultiSignID", multiSignNode.Id.ToString());
                        xMultiSignNote.SetAttribute("EmpID", multiSignNode.SignEmpId.ToString());
                        xMultiSignNote.SetAttribute("EmpName", multiSignNode.SignEmpName);
                        xNode.DocumentElement.AppendChild(xMultiSignNote);
                    }
                }
                if (!string.IsNullOrEmpty(turnedEmpNames))
                {
                    turnedEmpNames += ",";
                    turnedEmpIDs += ",";
                }
                turnedEmpNames += multiSignNode.SignEmpName;
                turnedEmpIDs += multiSignNode.SignEmpId;
                CreateNodeExe(multiSignNode, GetNodeAction((int)DataModel.NodeStatus.轮到), "");
            }
            SetTurned(flowXmlDocument, turnedEmpIDs);
            //删除掉多余的
            foreach (var multiSignNode in multiSignNodes)
            {
                if (!empIDs.Contains(multiSignNode.SignEmpId))
                {
                    this.DbContext.Set<DataModel.Models.FlowMultiSign>().Remove(multiSignNode);
                    //去除多余的
                    if (node.FlowNodeValidateGroup == "1")
                    {
                        var xMultiSignNote = xNode.SelectSingleNode("Root/SignNote[@MultiSignID=\"" + multiSignNode.Id + "\"]");
                        if (xMultiSignNote != null)
                        {
                            xNode.DocumentElement.RemoveChild(xMultiSignNote);
                        }
                    }
                }
            }
            if (node.FlowNodeValidateGroup == "1")
            {
                node.FlowNodeXml = xNode.OuterXml;
            }
            return turnedEmpNames;
        }
        /// <summary>
        /// 处理流程退回
        /// </summary>
        protected virtual void Back()
        {
            var currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == this.Options.FlowNodeID);
            EnsureCurrentNode(currentNode);
            if (currentNode.FlowNodeTypeID == 0)
            {
                EnsureProcessor(currentNode.FlowNodeEmpId, currentNode.FlowNodeEmpName);
            }
            var backNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == this.Options.NextNodeID);
            if (backNode == null)
            {
                throw new Common.JQException("找不到有效的退回节点");
            }
            DataModel.Models.FlowMultiSign flowMultiSign = null;
            if (currentNode.FlowNodeTypeID == -1)
            {
                //当前为会签，获取出会签节点
                flowMultiSign = this.DbContext.Set<DataModel.Models.FlowMultiSign>().FirstOrDefault(m => m.Id == this.Options.FlowMultiSignID);
                if (flowMultiSign == null || flowMultiSign.FlowNodeID != currentNode.Id)
                {
                    throw new Common.JQException("会签节点状态不对！");
                }
                EnsureProcessor(flowMultiSign.SignEmpId, flowMultiSign.SignEmpName);
            }
            else
            {
                EnsureProcessor(currentNode.FlowNodeEmpId, currentNode.FlowNodeEmpName);
            }
            var flow = this.DbContext.Set<DataModel.Models.Flow>().FirstOrDefault(m => m.Id == currentNode.FlowID);
            EnsureFlow(flow);
            this.Options.FlowID = flow.Id;
            this.Options.FlowNodeOrder = currentNode.FlowNodeOrder;
            var flowProcessor = GetProcessor();
            var flowXmlDocument = new XmlDocument();
            flowXmlDocument.LoadXml(flow.FlowXml);
            InitProjectIDs(flowXmlDocument);
            using (var tran = this.DbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    OnExecuting(flowProcessor);
                    OnSaveForm(flowProcessor);
                    if (!string.IsNullOrEmpty(this.Options.Title))
                    {
                        flow.FlowTitle = this.Options.Title;
                    }
                    if (currentNode.FlowNodeTypeID == -1)
                    {
                        //退回当前节点时，重置所有会签节点
                        var flowMultiSigns = this.DbContext.Set<DataModel.Models.FlowMultiSign>().Where(m => m.FlowNodeID == currentNode.Id);
                        foreach (var temp in flowMultiSigns)
                        {
                            temp.SignStatus = 0;
                            temp.SignDate = JQ.Util.TypeParse.DefaultDateTime;
                            temp.SignNote = "";
                        }
                        CreateNodeExe(flowMultiSign, GetNodeAction((int)DataModel.NodeStatus.回退), this.Options.Note);
                        if (currentNode.FlowNodeValidateGroup == "1")
                        {
                            //记录会签的记录
                            currentNode.FlowNodeXml = RecordSignNote(currentNode.FlowNodeXml, flowMultiSign.Id);
                        }
                    }
                    else
                    {
                        //需要存储数据
                        currentNode.FlowNodeXml = RecordSignNote(currentNode.FlowNodeXml);
                    }
                    //更新当前节点为完成
                    currentNode.FlowNodeStatusID = (int)DataModel.NodeStatus.回退;
                    currentNode.FlowNodeDate = DateTime.Now;
                    if (this.Options.CurrentUser.AgenEmpID > 0)
                    {
                        currentNode.AgenEmpId = this.Options.CurrentUser.AgenEmpID;
                        currentNode.AgenEmpName = this.Options.CurrentUser.AgenEmpName;
                    }
                    if (currentNode.FlowNodeTypeID == 0)
                    {
                        CreateNodeExe(currentNode);
                    }
                    backNode.FlowNodeStatusID = (int)DataModel.NodeStatus.轮到;
                    backNode.FlowNodeDate = JQ.Util.TypeParse.DefaultDateTime;
                    backNode.FlowNodeEmpId = Common.ModelConvert.ConvertToDefault<int>(this.Options.NextEmpIDs);
                    backNode.FlowNodeEmpName = GetEmpName(backNode.FlowNodeEmpId);
                    backNode.AgenEmpId = 0;
                    backNode.AgenEmpName = "";
                    backNode.FlowNodeFromDateTime = DateTime.Now;
                    backNode.FlowNodeFromEmpId = this.Options.CurrentUser.EmpID;
                    backNode.FlowNodeFromEmpName = this.Options.CurrentUser.EmpName;
                    if (backNode.FlowNodeTypeID == -1)
                    {
                        //处理退回到会签节点
                        flow.FlowStatusName = "退回至（" + ProcessMultiSignNode(flowXmlDocument, backNode) + "）";
                    }
                    else
                    {
                        flow.FlowStatusName = "退回至（" + backNode.FlowNodeEmpName + "）";
                        CreateNodeExe(backNode);
                        SetTurned(flowXmlDocument, backNode.FlowNodeEmpId.ToString());
                    }
                    flow.LastModificationTime = DateTime.Now;
                    flow.LastModifierEmpId = this.Options.CurrentUser.EmpID;
                    flow.LastModifierEmpName = this.Options.CurrentUser.EmpName;
                    OnExecuted(flowProcessor, true);
                    SetProjectIDs(flowXmlDocument);
                    flow.FlowXml = flowXmlDocument.OuterXml;
                    this.DbContext.SaveChanges();
                    tran.Commit();
                    this.Result.Result = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    OnExecuted(flowProcessor, false);
                    throw;
                }
            }
            Flow.RemoveCache();
            if (currentNode.FlowNodeStatusID == (int)DataModel.NodeStatus.完成)
            {
                SendNotify(flow.FlowModular);
            }
            if (!string.IsNullOrEmpty(this.Options.NextEmpIDs))
            {
                if (this.Options.MobilePushFlowRefTables.Contains(this.Options.RefTable))
                {
                    Common.JPush.push(flow.FlowName, flow.FlowTitle, this.Options.RefTable, this.Options.RefID, this.Options.NextEmpIDs, this.Options.FlowNodeID, this.Options.FlowMultiSignID);
                }
            }
        }

        /// <summary>
        /// 验证当前节点的人员是否满足人当前登录人员或者代理登录人员
        /// </summary>
        /// <exception cref="Common.JQException"></exception>
        private void EnsureProcessor(int empID, string empName)
        {
            if (!this.Options.IsValidateEmp)
            {
                return;
            }
            if (empID != this.Options.CurrentUser.EmpID)
            {
                throw new Common.JQException("当前节点的正确处理人员应为（" + empName + "）");
            }
        }

        /// <summary>
        /// 验证当前节点是否为null、是否处于有效状态
        /// </summary>
        /// <exception cref="Common.JQException"></exception>
        private void EnsureCurrentNode(DataModel.Models.FlowNode flowNode)
        {
            if (flowNode == null)
            {
                throw new Common.JQException("找不到当前有效的节点！");
            }
            if (flowNode.FlowNodeStatusID != (int)DataModel.NodeStatus.轮到)
            {
                throw new Common.JQException("当前的有效节点状态不对！");
            }
        }

        /// <summary>
        /// 验证当前是流程否为null、是否处于有效状态
        /// </summary>
        /// <param name="flow"></param>
        private void EnsureFlow(DataModel.Models.Flow flow)
        {
            if (flow == null)
            {
                throw new Common.JQException("找不到当前节点所属的有效流程！");
            }
            if (flow.FlowStatusID != (int)DataModel.FlowStatus.审批中)
            {
                throw new Common.JQException("当前节点的流程状态不对！");
            }
        }

        /// <summary>
        /// 处理流程 直接完成
        /// </summary>
        protected virtual void Finish()
        {
            var currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == this.Options.FlowNodeID);
            if (currentNode == null)
            {
                ProcessNextWithNewFlow();
                this.Options.Action = Action.Undo; ;
                currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.FlowID == this.Options.FlowID && m.FlowNodeOrder == 1);
                //throw new Common.JQException("找不到当前的有效节点");
            }
            EnsureProcessor(currentNode.FlowNodeEmpId, currentNode.FlowNodeEmpName);
            if (currentNode.FlowNodeTypeID == -1)
            {
                throw new Common.JQException("会签节点上无法使用直接否决功能！");
            }
            var flow = this.DbContext.Set<DataModel.Models.Flow>().FirstOrDefault(m => m.Id == currentNode.FlowID);
            if (flow == null)
            {
                throw new Common.JQException("找不到当前节点所属的有效流程");
            }
            this.Options.FlowID = flow.Id;
            this.Options.FlowNodeOrder = currentNode.FlowNodeOrder;
            Dictionary<int, string> empsToSendFinish = null;
            var flowXmlDocument = new XmlDocument();
            flowXmlDocument.LoadXml(flow.FlowXml);
            InitProjectIDs(flowXmlDocument);
            var flowProcessor = GetProcessor();
            using (var tran = this.DbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    OnExecuting(flowProcessor);
                    OnSaveForm(flowProcessor);
                    if (!string.IsNullOrEmpty(this.Options.Title))
                    {
                        flow.FlowTitle = this.Options.Title;
                    }
                    if (currentNode.FlowNodeValidateGroup == "1")
                    {
                        //需要存储数据
                        currentNode.FlowNodeXml = RecordSignNote(currentNode.FlowNodeXml);
                    }
                    //更新当前节点为完成
                    currentNode.FlowNodeStatusID = (int)DataModel.NodeStatus.完成;
                    currentNode.FlowNodeDate = DateTime.Now;
                    if (this.Options.CurrentUser.AgenEmpID > 0)
                    {
                        currentNode.AgenEmpId = this.Options.CurrentUser.AgenEmpID;
                        currentNode.AgenEmpName = this.Options.CurrentUser.AgenEmpName;
                    }
                    CreateNodeExe(currentNode);
                    //获取后续节点
                    var nextNodes = this.DbContext.Set<DataModel.Models.FlowNode>().Where(m => m.FlowID == currentNode.FlowID && m.FlowNodeOrder > currentNode.FlowNodeOrder);
                    var nextNode = nextNodes.FirstOrDefault(m => m.FlowNodeParentID == currentNode.Id);
                    //将后续节点全部设置为跳过
                    while (nextNode != null)
                    {
                        nextNode.FlowNodeFromDateTime = DateTime.Now;
                        nextNode.FlowNodeFromEmpId = this.Options.CurrentUser.EmpID;
                        nextNode.FlowNodeFromDateTime = DateTime.Now;
                        nextNode.FlowNodeEmpId = 0;
                        nextNode.FlowNodeEmpName = "";
                        nextNode.AgenEmpId = 0;
                        nextNode.AgenEmpName = "";
                        nextNode.FlowNodeStatusID = (int)DataModel.NodeStatus.跳过;
                        nextNode.FlowNodeDate = DateTime.Now;
                        CreateNodeExe(nextNode);
                        nextNode = nextNodes.FirstOrDefault(m => m.FlowNodeParentID == nextNode.Id);
                    }
                    SetTurned(flowXmlDocument, "");
                    //流程结束
                    flow.FlowStatusID = (int)DataModel.FlowStatus.审批结束;
                    flow.FlowStatusName = "审批结束";
                    flow.FlowFinishDate = DateTime.Now;
                    flow.LastModificationTime = DateTime.Now;
                    flow.LastModifierEmpId = this.Options.CurrentUser.EmpID;
                    flow.LastModifierEmpName = this.Options.CurrentUser.EmpName;
                    OnAfterApproveFinished(flowProcessor);
                    empsToSendFinish = SendFinishMessage(flow);
                    OnExecuted(flowProcessor, true);
                    SetProjectIDs(flowXmlDocument);
                    flow.FlowXml = flowXmlDocument.OuterXml;
                    this.DbContext.SaveChanges();
                    tran.Commit();
                    this.Result.Result = true;
                }
                catch
                {
                    tran.Rollback();
                    OnExecuted(flowProcessor, false);
                    throw;
                }
            }
            Flow.RemoveCache();
            SendNotify(flow.FlowModular);
            SendFinishMessageNotify(empsToSendFinish);
        }

        /// <summary>
        /// 处理流程 直接否决
        /// </summary>
        protected virtual void Reject()
        {
            var currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == this.Options.FlowNodeID);
            EnsureCurrentNode(currentNode);
            EnsureProcessor(currentNode.FlowNodeEmpId, currentNode.FlowNodeEmpName);
            if (currentNode.FlowNodeTypeID == -1)
            {
                throw new Common.JQException("会签节点上无法使用直接否决功能！");
            }
            var flow = this.DbContext.Set<DataModel.Models.Flow>().FirstOrDefault(m => m.Id == currentNode.FlowID);
            EnsureFlow(flow);
            this.Options.FlowID = flow.Id;
            this.Options.RefTable = flow.FlowRefTable;
            this.Options.RefID = flow.FlowRefID;
            this.Options.FlowNodeOrder = currentNode.FlowNodeOrder;
            Dictionary<int, string> empsToSendFinish = null;
            var flowProcessor = GetProcessor();
            var flowXmlDocument = new XmlDocument();
            flowXmlDocument.LoadXml(flow.FlowXml);
            InitProjectIDs(flowXmlDocument);
            using (var tran = this.DbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    OnExecuting(flowProcessor);
                    OnSaveForm(flowProcessor);
                    if (!string.IsNullOrEmpty(this.Options.Title))
                    {
                        flow.FlowTitle = this.Options.Title;
                    }
                    if (currentNode.FlowNodeValidateGroup == "1")
                    {
                        //需要存储数据
                        currentNode.FlowNodeXml = RecordSignNote(currentNode.FlowNodeXml);
                    }
                    //更新当前节点为完成
                    currentNode.FlowNodeStatusID = (int)DataModel.NodeStatus.完成;
                    currentNode.FlowNodeDate = DateTime.Now;
                    if (this.Options.CurrentUser.AgenEmpID > 0)
                    {
                        currentNode.AgenEmpId = this.Options.CurrentUser.AgenEmpID;
                        currentNode.AgenEmpName = this.Options.CurrentUser.AgenEmpName;
                    }
                    CreateNodeExe(currentNode);
                    //获取后续节点
                    var nextNodes = this.DbContext.Set<DataModel.Models.FlowNode>().Where(m => m.FlowID == currentNode.FlowID && m.FlowNodeOrder > currentNode.FlowNodeOrder);
                    var nextNode = nextNodes.FirstOrDefault(m => m.FlowNodeParentID == currentNode.Id);
                    //将后续节点全部设置为跳过
                    while (nextNode != null)
                    {
                        nextNode.FlowNodeFromDateTime = DateTime.Now;
                        nextNode.FlowNodeFromEmpId = this.Options.CurrentUser.EmpID;
                        nextNode.FlowNodeFromDateTime = DateTime.Now;
                        nextNode.FlowNodeEmpId = 0;
                        nextNode.FlowNodeEmpName = "";
                        nextNode.AgenEmpId = 0;
                        nextNode.AgenEmpName = "";
                        nextNode.FlowNodeStatusID = (int)DataModel.NodeStatus.跳过;
                        nextNode.FlowNodeDate = DateTime.Now;
                        CreateNodeExe(nextNode);
                        nextNode = nextNodes.FirstOrDefault(m => m.FlowNodeParentID == nextNode.Id);
                    }
                    SetTurned(flowXmlDocument, "");
                    //流程结束
                    flow.FlowStatusID = (int)DataModel.FlowStatus.审批不同意;
                    flow.FlowStatusName = "审批不同意";
                    flow.FlowFinishDate = DateTime.Now;
                    flow.LastModificationTime = DateTime.Now;
                    flow.LastModifierEmpId = this.Options.CurrentUser.EmpID;
                    flow.LastModifierEmpName = this.Options.CurrentUser.EmpName;
                    OnAfterApproveFinished(flowProcessor);
                    empsToSendFinish = SendFinishMessage(flow);
                    OnExecuted(flowProcessor, true);
                    SetProjectIDs(flowXmlDocument);
                    flow.FlowXml = flowXmlDocument.OuterXml;
                    this.DbContext.SaveChanges();
                    tran.Commit();
                    this.Result.Result = true;
                }
                catch
                {
                    tran.Rollback();
                    OnExecuted(flowProcessor, false);
                    throw;
                }
            }
            Flow.RemoveCache();
            SendNotify(flow.FlowModular);
            SendFinishMessageNotify(empsToSendFinish);
        }

        /// <summary>
        /// 工作移交
        /// </summary>
        protected virtual void Transfer()
        {
            var newEmpID = Common.ModelConvert.ConvertToDefault<int>(this.Options.NextEmpIDs);
            if (newEmpID == 0 || newEmpID == this.Options.CurrentUser.EmpID)
            {
                throw new Common.JQException("要移交的人员选择错误！");
            }
            var currentNode = this.DbContext.Set<DataModel.Models.FlowNode>().FirstOrDefault(m => m.Id == this.Options.FlowNodeID);
            EnsureCurrentNode(currentNode);
            if (currentNode.FlowNodeTypeID != -1)
            {
                EnsureProcessor(currentNode.FlowNodeEmpId, currentNode.FlowNodeEmpName);
            }
            List<DataModel.Models.FlowMultiSign> flowMulituSigns = null;
            DataModel.Models.FlowMultiSign flowMultiSign = null;
            if (currentNode.FlowNodeTypeID == -1)
            {
                //当前为会签，获取出会签节点
                flowMulituSigns = this.DbContext.Set<DataModel.Models.FlowMultiSign>().Where(m => m.FlowNodeID == currentNode.Id).ToList();
                flowMultiSign = flowMulituSigns.FirstOrDefault(m => m.Id == this.Options.FlowMultiSignID);
                if (flowMultiSign == null || flowMultiSign.FlowNodeID != currentNode.Id)
                {
                    throw new Common.JQException("会签节点状态不对！");
                }
                EnsureProcessor(flowMultiSign.SignEmpId, flowMultiSign.SignEmpName);
            }
            else
            {
                EnsureProcessor(currentNode.FlowNodeEmpId, currentNode.FlowNodeEmpName);
            }
            var flow = this.DbContext.Set<DataModel.Models.Flow>().FirstOrDefault(m => m.Id == currentNode.FlowID);
            EnsureFlow(flow);
            var flowXmlDocument = new XmlDocument();
            flowXmlDocument.LoadXml(flow.FlowXml);
            InitProjectIDs(flowXmlDocument);
            var flowProcessor = GetProcessor();
            var flowStatusName = "";
            int originalEmpID = 0;
            using (var tran = this.DbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    OnExecuting(flowProcessor);
                    OnSaveForm(flowProcessor);
                    if (!string.IsNullOrEmpty(this.Options.Title))
                    {
                        flow.FlowTitle = this.Options.Title;
                    }
                    if (currentNode.FlowNodeTypeID == -1)
                    {
                        CreateNodeExe(flowMultiSign, (int)DataModel.NodeAction.换人, this.Options.Note);
                        originalEmpID = flowMultiSign.SignEmpId;
                        flowMultiSign.SignEmpId = newEmpID;
                        flowMultiSign.SignEmpName = GetEmpName(newEmpID);
                        var turnedEmpNames = "";
                        var turnedEmpIDs = "";
                        foreach (var data in flowMulituSigns)
                        {
                            if (data.SignStatus != 0)
                            {
                                continue;
                            }
                            if (!string.IsNullOrEmpty(turnedEmpIDs))
                            {
                                turnedEmpIDs += ",";
                                turnedEmpNames += ",";
                            }
                            turnedEmpIDs += data.SignEmpId.ToString();
                            turnedEmpNames += data.SignEmpName;
                        }
                        flowStatusName = "轮到（" + turnedEmpNames + "）";
                        SetTurned(flowXmlDocument, turnedEmpIDs);
                        CreateNodeExe(flowMultiSign, (int)DataModel.NodeAction.轮到, "");
                    }
                    else
                    {
                        CreateNodeExe(currentNode, (int)DataModel.NodeAction.换人);
                        originalEmpID = currentNode.FlowNodeEmpId;
                        currentNode.FlowNodeEmpId = newEmpID;
                        currentNode.FlowNodeEmpName = GetEmpName(newEmpID);
                        flowStatusName = "轮到（" + currentNode.FlowNodeEmpName + "）";
                        SetTurned(flowXmlDocument, currentNode.FlowNodeEmpId.ToString());
                        CreateNodeExe(currentNode);
                    }
                    if (this.Options.CurrentUser.AgenEmpID > 0)
                    {
                        currentNode.AgenEmpId = this.Options.CurrentUser.AgenEmpID;
                        currentNode.AgenEmpName = this.Options.CurrentUser.AgenEmpName;
                    }
                    flow.FlowStatusID = (int)DataModel.FlowStatus.审批中;
                    flow.FlowStatusName = flowStatusName;
                    flow.LastModificationTime = DateTime.Now;
                    flow.LastModifierEmpId = this.Options.CurrentUser.EmpID;
                    flow.LastModifierEmpName = this.Options.CurrentUser.EmpName;
                    OnExecuted(flowProcessor, true);
                    SetProjectIDs(flowXmlDocument);
                    flow.FlowXml = flowXmlDocument.OuterXml;
                    this.DbContext.SaveChanges();
                    tran.Commit();
                    this.Result.Result = true;
                    this.Result.FlowRefID = this.Options.RefID;
                }
                catch
                {
                    tran.Rollback();
                    OnExecuted(flowProcessor, false);
                }
            }
            Flow.RemoveCache();
            SendNotify(flow.FlowModular, originalEmpID);
            if (!string.IsNullOrEmpty(this.Options.NextEmpIDs))
            {
                if (this.Options.MobilePushFlowRefTables.Contains(this.Options.RefTable))
                {
                    Common.JPush.push(flow.FlowName, flow.FlowTitle, this.Options.RefTable, this.Options.RefID, this.Options.NextEmpIDs, this.Options.FlowNodeID, this.Options.FlowMultiSignID);
                }
            }
        }

        private IFlowProcessor GetProcessor()
        {
            if (string.IsNullOrEmpty(this.Options.Processor))
            {
                return null;
            }
            var temp = this.Options.Processor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length != 2)
            {
                return null;
            }
            try
            {
                //var obj = Activator.CreateInstance(temp[0], temp[1]).Unwrap();
                var obj = System.Reflection.Assembly.Load(temp[0]).CreateInstance(temp[1]);
                if (obj != null && obj is IFlowProcessor)
                {
                    var flowProcessor = (IFlowProcessor)obj;
                    flowProcessor.Options = this.Options;
                    flowProcessor.CurrentDbContext = this.DbContext;
                    flowProcessor.SetCreateProperties = delegate (object instance)
                    {
                        if (instance == null)
                        {
                            return;
                        }
                        var type = instance.GetType();
                        SetPropertyValue(type.GetProperty("CreationTime"), instance, DateTime.Now);
                        SetPropertyValue(type.GetProperty("CreatorDepId"), instance, this.Options.CurrentUser.EmpDepID);
                        SetPropertyValue(type.GetProperty("CreatorDepName"), instance, this.Options.CurrentUser.EmpDepName);
                        SetPropertyValue(type.GetProperty("CreatorEmpId"), instance, this.Options.CurrentUser.EmpID);
                        SetPropertyValue(type.GetProperty("CreatorEmpName"), instance, this.Options.CurrentUser.EmpName);
                        if (this.Options.CurrentUser.AgenEmpID > 0)
                        {
                            SetPropertyValue(type.GetProperty("AgenEmpId"), instance, this.Options.CurrentUser.AgenEmpID);
                            SetPropertyValue(type.GetProperty("AgenEmpName"), instance, this.Options.CurrentUser.AgenEmpName);
                            SetPropertyValue(type.GetProperty("AgenDepID"), instance, this.Options.CurrentUser.AgenDepID);
                            SetPropertyValue(type.GetProperty("AgenDepName"), instance, this.Options.CurrentUser.AgenDepName);
                        }
                    };
                    flowProcessor.SetModifyProperties = delegate (object instance)
                    {
                        if (instance == null)
                        {
                            return;
                        }
                        var type = instance.GetType();
                        SetPropertyValue(type.GetProperty("LastModificationTime"), instance, DateTime.Now);
                        SetPropertyValue(type.GetProperty("LastModifierEmpId"), instance, this.Options.CurrentUser.EmpDepID);
                        SetPropertyValue(type.GetProperty("LastModifierEmpName"), instance, this.Options.CurrentUser.EmpDepName);
                        if (this.Options.CurrentUser.AgenEmpID > 0)
                        {
                            SetPropertyValue(type.GetProperty("AgenEmpId"), instance, this.Options.CurrentUser.AgenEmpID);
                            SetPropertyValue(type.GetProperty("AgenEmpName"), instance, this.Options.CurrentUser.AgenEmpName);
                            SetPropertyValue(type.GetProperty("AgenDepID"), instance, this.Options.CurrentUser.AgenDepID);
                            SetPropertyValue(type.GetProperty("AgenDepName"), instance, this.Options.CurrentUser.AgenDepName);
                        }
                    };
                    if (System.Web.HttpContext.Current != null)
                    {
                        flowProcessor.Request = System.Web.HttpContext.Current.Request;
                        flowProcessor.Session = System.Web.HttpContext.Current.Session;
                    }
                    return flowProcessor;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public void SetPropertyValue(PropertyInfo propertyInfo, object source, object value, params object[] index)
        {
            if (propertyInfo == null || value == null || value == DBNull.Value || !propertyInfo.CanWrite)
            {
                return;
            }
            if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propertyInfo.SetValue(source, Convert.ChangeType(value, propertyInfo.PropertyType.GetGenericArguments()[0]), null);
                return;
            }
            propertyInfo.SetValue(source, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }

        private void OnExecuting(IFlowProcessor processor)
        {
            if (processor == null)
            {
                return;
            }
            processor.OnExecuting();
        }

        private void OnSaveForm(IFlowProcessor processor)
        {
            if (processor == null)
            {
                return;
            }
            processor.OnSaveForm();
        }

        private void OnExecuted(IFlowProcessor processor, bool isSuccess)
        {
            if (processor == null)
            {
                return;
            }
            processor.OnExecuted(isSuccess);
        }

        private void OnAfterApproveFinished(IFlowProcessor processor)
        {
            if (processor == null)
            {
                return;
            }
            processor.OnAfterApproveFinished();
        }

        private DataModel.Models.Flow GetFlow(DataModel.Models.FlowModel flowModel)
        {
            var result = new DataModel.Models.Flow()
            {
                CreationTime = DateTime.Now,
                CreatorDepId = this.Options.CurrentUser.EmpDepID,
                CreatorDepName = this.Options.CurrentUser.EmpDepName,
                CreatorEmpId = this.Options.CurrentUser.EmpID,
                CreatorEmpName = this.Options.CurrentUser.EmpName,
                FlowByte = null,
                FlowControlXml = "<Root></Root>",
                FlowFinishControls = flowModel.ModelFinishControls,
                FlowFinishDate = JQ.Util.TypeParse.DefaultDateTime,
                FlowFinishSend = flowModel.ModelFinishSend,
                FlowIsRun = flowModel.ModelIsRun,
                FlowIsWord = flowModel.ModelIsWord,
                FlowListUrl = flowModel.ModelListUrl,
                FlowModelID = flowModel.Id,
                FlowName = flowModel.ModelName,
                FlowNum = flowModel.ModelNum,
                FlowNumber = flowModel.ModelNumber,
                FlowRefID = this.Options.RefID,
                FlowRefTable = this.Options.RefTable,
                FlowRole = flowModel.ModelRole,
                FlowStartDate = DateTime.Now,
                FlowStatusID = (int)DataModel.FlowStatus.审批中,
                FlowStatusName = "审批中",
                FlowUrl = flowModel.ModelUrl,
                FlowVersion = flowModel.ModelVersion,
                FlowXml = flowModel.ModelXml,
                LastModificationTime = JQ.Util.TypeParse.DefaultDateTime,
                LastModifierEmpId = 0,
                LastModifierEmpName = "",
                FlowTitle = "",
                FlowModular = flowModel.ModeModular,
                FlowSign = flowModel.ModelSign
            };
            if (this.Options.CurrentUser.AgenEmpID == 0)
            {
                result.AgenEmpId = 0;
                result.AgenEmpName = "";
            }
            else
            {
                result.AgenEmpId = this.Options.CurrentUser.AgenEmpID;
                result.AgenEmpName = this.Options.CurrentUser.AgenEmpName;
            }
            return result;
        }

        private DataModel.Models.FlowNode GetFlowNode(DataModel.Models.FlowModelNode flowModelNode)
        {
            var result = new DataModel.Models.FlowNode()
            {
                CreatorDepId = this.Options.CurrentUser.EmpDepID,
                CreatorDepName = this.Options.CurrentUser.EmpDepName,
                FlowModelNodeID = flowModelNode.Id,
                FlowNodeAppDefaultValue = flowModelNode.NodeAppDefaultValue,
                FlowNodeAppIsLastTime = flowModelNode.NodeAppIsLastTime,
                FlowNodeAppIsRequired = flowModelNode.NodeAppIsRequired,
                FlowNodeAutoFinished = flowModelNode.NodeAutoFinished,
                FlowNodeAutoStatus = false,
                FlowNodeBackID = flowModelNode.NodeBackID,
                FlowNodeDate = JQ.Util.TypeParse.DefaultDateTime,
                FlowNodeDays = flowModelNode.NodeDays,
                FlowNodeFinishToNext = flowModelNode.NodeFinishToNext,
                FlowNodeFromDateTime = JQ.Util.TypeParse.DefaultDateTime,
                FlowNodeFromEmpId = 0,
                FlowNodeFromEmpName = "",
                FlowNodeIsFactNext = flowModelNode.NodeIsFactNext,
                FlowNodeIsRemind = false,
                FlowNodeIsToFinish = flowModelNode.NodeIsToFinish,
                FlowNodeIsToMessage = flowModelNode.NodeIsToMessage,
                FlowNodeIsToPass = flowModelNode.NodeIsToPass,
                FlowNodeName = flowModelNode.NodeName,
                FlowNodeNodeRoles = flowModelNode.NodeRoleS,
                FlowNodeNodeSkipID = flowModelNode.NodeNodeSkipID,
                FlowNodeNodeThisDep = flowModelNode.NodeThisDep,
                FlowNodeNote = "",
                FlowNodeOrder = flowModelNode.NodeOrder,
                FlowNodeParentID = flowModelNode.NodeParentID,
                FlowNodeRefIsDelete = false,
                FlowNodeSameId = flowModelNode.NodeSameId,
                FlowNodeSelectToBack = flowModelNode.NodeSelectToBack,
                FlowNodeSelectToSkip = flowModelNode.NodeSelectToSkip,
                FlowNodeSignControlName = flowModelNode.NodeSignControlName,
                FlowNodeStatusID = (int)DataModel.NodeStatus.未安排,
                FlowNodeTypeID = flowModelNode.NodeTypeID,
                FlowNodeUrl = flowModelNode.NodeUrl,
                FlowNodeEmpId = 0,
                FlowNodeEmpIDs = flowModelNode.NodeEmpIDs,
                FlowNodeEmpName = "",
                FlowNodeValidateGroup = flowModelNode.NodeValidateGroup,
                FlowNodeWriteControlsName = flowModelNode.NodeWriteControlsName,
                FlowNodeXml = flowModelNode.NodeXml
            };
            if (this.Options.CurrentUser.AgenEmpID == 0)
            {
                result.AgenEmpId = 0;
                result.AgenEmpName = "";
            }
            else
            {
                result.AgenEmpId = this.Options.CurrentUser.AgenEmpID;
                result.AgenEmpName = this.Options.CurrentUser.AgenEmpName;
            }
            return result;
        }

        public XmlDocument SetTurned(XmlDocument xmlDocument, string turnedEmpIDs)
        {
            var xTurned = xmlDocument.SelectSingleNode("Root/TurnedEmpIDs");
            if (xTurned == null)
            {
                xTurned = xmlDocument.CreateElement("TurnedEmpIDs");
                xmlDocument.DocumentElement.AppendChild(xTurned);
            }
            xTurned.InnerText = turnedEmpIDs;
            return xmlDocument;
        }

        /// <summary>
        /// 插入流转记录
        /// </summary>
        /// <param name="node"></param>
        private void CreateNodeExe(DataModel.Models.FlowNode node, int action = 0)
        {
            var flowNodeExe = new DataModel.Models.FlowNodeExe()
            {
                ExeActionDate = DateTime.Now,
                ExeActionID = (action == 0 ? GetNodeAction(node.FlowNodeStatusID) : action),
                FlowID = node.FlowID,
                FlowNodeID = node.Id
            };
            if (node.FlowNodeEmpId == 0)
            {
                flowNodeExe.ExeEmpId = this.Options.CurrentUser.EmpID;
                flowNodeExe.ExeEmpName = this.Options.CurrentUser.EmpName;
            }
            else
            {
                flowNodeExe.ExeEmpId = node.FlowNodeEmpId;
                flowNodeExe.ExeEmpName = node.FlowNodeEmpName;
            }
            if (node.FlowNodeStatusID == (int)DataModel.NodeStatus.完成 || node.FlowNodeStatusID == (int)DataModel.NodeStatus.回退 || flowNodeExe.ExeActionID == (int)DataModel.NodeAction.换人)
            {
                flowNodeExe.ExeNote = this.Options.Note;
            }
            else
            {
                flowNodeExe.ExeNote = "";
            }
            if (this.Options.CurrentUser.AgenEmpID > 0)
            {
                flowNodeExe.ExeArgEmpId = this.Options.CurrentUser.AgenEmpID;
                flowNodeExe.ExeArgEmpName = this.Options.CurrentUser.AgenEmpName;
            }
            this.DbContext.Set<DataModel.Models.FlowNodeExe>().Add(flowNodeExe);
        }

        private void CreateNodeExe(DataModel.Models.FlowMultiSign flowMultiSign, int action, string note)
        {
            var flowNodeExe = new DataModel.Models.FlowNodeExe()
            {
                ExeActionDate = DateTime.Now,
                ExeEmpId = flowMultiSign.SignEmpId,
                ExeEmpName = flowMultiSign.SignEmpName,
                FlowID = flowMultiSign.FlowID,
                FlowNodeID = flowMultiSign.FlowNodeID,
                ExeActionID = action,
                ExeNote = note
            };
            if (this.Options.CurrentUser.AgenEmpID > 0)
            {
                flowNodeExe.ExeArgEmpId = this.Options.CurrentUser.AgenEmpID;
                flowNodeExe.ExeArgEmpName = this.Options.CurrentUser.AgenEmpName;
            }
            this.DbContext.Set<DataModel.Models.FlowNodeExe>().Add(flowNodeExe);
        }

        private int GetNodeAction(int statusID)
        {
            switch (statusID)
            {
                case (int)DataModel.NodeStatus.完成:
                    return (int)DataModel.NodeAction.完成;
                case (int)DataModel.NodeStatus.轮到:
                    return (int)DataModel.NodeAction.轮到;
                case (int)DataModel.NodeStatus.安排:
                    return (int)DataModel.NodeAction.安排;
                case (int)DataModel.NodeStatus.回退:
                    return (int)DataModel.NodeAction.回退;
                case (int)DataModel.NodeStatus.跳过:
                    return (int)DataModel.NodeAction.跳过;
                default:
                    return 0;
            }
        }

        private string GetEmpName(int empID)
        {
            var emp = BaseEmployeeDB.GetBaseEmployeeInfo(empID);
            if (emp != null)
            {
                return emp.EmpName;
            }
            return "";
        }

        private void SetProjectIDs(XmlDocument xmlDocument)
        {
            var xProjectIDs = xmlDocument.SelectSingleNode("Root/Projects");
            if (xProjectIDs == null)
            {
                xProjectIDs = xmlDocument.CreateElement("Projects");
                xmlDocument.DocumentElement.AppendChild(xProjectIDs);
            }
            else
            {
                xProjectIDs.RemoveAll();
            }
            if (this.Options.ProjectIDs != null)
            {
                foreach (var projectID in this.Options.ProjectIDs)
                {
                    var xProjectID = xmlDocument.CreateElement("Project");
                    xProjectID.SetAttribute("id", projectID.ToString());
                    xProjectID.SetAttribute("level", "0");
                    xProjectIDs.AppendChild(xProjectID);
                }
            }
            if (this.Options.ProjectPhases != null)
            {
                foreach (var projectPhase in this.Options.ProjectPhases)
                {
                    var xProjectID = xmlDocument.CreateElement("Project");
                    xProjectID.SetAttribute("id", projectPhase.Key.ToString());
                    //如果是子项，获取出父ID做缓存
                    //UPDATE Flow SET FlowXml.modify('replace value of (Root/Projects/Project[@value=$projectID$]/@parentID) with "$parentProectID$"') WHERE FlowXml.exist('Root/Projects/Project/@Value[.="$projectID$"]') = 1
                    xProjectID.SetAttribute("parentID", this.DbContext.Database.SqlQuery<int>("SELECT ISNULL(ParentId,0) FROM Project WHERE Id=" + projectPhase.Key).FirstOrDefault().ToString());
                    xProjectID.SetAttribute("level", "1");
                    if (projectPhase.Value != null && projectPhase.Value.Count > 0)
                    {
                        foreach (var phaseID in projectPhase.Value)
                        {
                            var xPhaseID = xmlDocument.CreateElement("Phase");
                            xPhaseID.SetAttribute("value", phaseID.ToString());
                            xProjectID.AppendChild(xPhaseID);
                        }
                    }
                    xProjectIDs.AppendChild(xProjectID);
                }
            }
        }

        private void InitProjectIDs(XmlDocument xmlDocument)
        {
            this.Options.ProjectIDs = new List<int>();
            this.Options.ProjectPhases = new Dictionary<int, List<int>>();
            foreach (XmlElement xmlElement in xmlDocument.SelectNodes("Root/Projects/Project"))
            {
                var temp = 0;
                if (xmlElement.GetAttribute("level") == "0")
                {
                    //大项
                    if (int.TryParse(xmlElement.GetAttribute("value"), out temp) && temp > 0)
                    {
                        this.Options.ProjectIDs.Add(temp);
                    }
                }
                else if (xmlElement.GetAttribute("level") == "1")
                {
                    //子项以及阶段
                    if (int.TryParse(xmlElement.GetAttribute("value"), out temp) && temp > 0)
                    {
                        var xPhases = xmlElement.SelectNodes("Phase");
                        if (xPhases.Count > 0)
                        {
                            //获取出阶段
                            var phaseIDs = new List<int>();
                            var temp1 = 0;
                            foreach (XmlElement xPhase in xPhases)
                            {
                                if (int.TryParse(xPhase.GetAttribute("value"), out temp1) && temp1 > 0)
                                {
                                    phaseIDs.Add(temp);
                                }
                            }
                            this.Options.ProjectPhases.Add(temp, phaseIDs);
                        }
                        else
                        {
                            this.Options.ProjectPhases.Add(temp, null);
                        }
                    }
                }
            }
        }

        private bool AllowEditWhenApproved(XmlDocument flowXmlDocument, int creator)
        {
            var approvedEditRoles = flowXmlDocument.DocumentElement.GetAttribute("ApprovedEditRoles").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (approvedEditRoles.Contains("Creator") && this.Options.CurrentUser.EmpID == creator)
            {
                return true;
            }
            if (approvedEditRoles.Contains("Administrator") && new DBSql.Sys.BaseEmpPermission().getRolesEmpList(new int[] { (int)DataModel.KeyPointType.系统管理员 }).FirstOrDefault(m1 => m1.PermissionEmpID == this.Options.CurrentUser.EmpID) != null)
            {
                //判断当前登录用户是否为系统管理员
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            if (_isDisponsed)
            {
                return;
            }
            _isDisponsed = true;
            if (_dbContext != null)
            {
                this._dbContext.Dispose();
            }
            GC.Collect();
        }
    }
}
