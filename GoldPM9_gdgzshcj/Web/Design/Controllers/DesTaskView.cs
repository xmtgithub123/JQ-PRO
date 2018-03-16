using DataModel;
using DataModel.Models;
using DBSql.Design.Dto;
using JQ.Util;
using JQ.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using Common.Data.Extenstions;
using System.Data;

namespace Design.Controllers
{
    public partial class DesTaskController : CoreController
    {
        /// <summary>
        /// 转到 项目策划主页 页面
        /// </summary>
        /// <returns></returns>
        [Description("项目策划主页")]
        public ActionResult ProjectPlanMain(int projID, long taskGroupId, long tabId)
        {
            var taskSpecId = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["taskSpecId"]);
            var from = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["from"]);

            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            ViewBag.taskSpecId = taskSpecId;
            ViewBag.tabId = tabId;
            ViewBag.from = from;
            return View();
        }


        /**********************项目计划表********************/

        /// <summary>
        /// 转到 项目计划表列表 页面
        /// </summary>
        /// <returns></returns>
        [Description("项目计划表列表")]
        public ActionResult PlanTableList()
        {
            ViewBag.currentUserId = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("PlanTableList")));
            return View();
        }

        /// <summary>
        /// 转到 项目计划表详情 页面
        /// </summary>
        /// <returns></returns>
        [Description("项目计划表详情")]
        public ActionResult PlanTableInfo(int projID, long taskGroupId)
        {
            DataModel.Models.Project ProjModel = new DBSql.Project.Project().Get(projID);
            DataModel.Models.DesTaskGroup TaskGroupModel = new DBSql.Design.DesTaskGroup().FirstOrDefault(p => p.Id == taskGroupId);
            DataModel.Models.DesPlanTable planTable = new DBSql.Design.DesPlanTable().FirstOrDefault(p => p.ProjId == projID && p.TaskGroupId == taskGroupId);

            ViewBag.currentUserId = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("PlanTableList")));

            ViewBag.mainTabsUsed = JavaScriptSerializerUtil.getJson(new
            {
                PlanTableList = ContainPermission("PlanTableList"),
                ProjectPlanList = ContainPermission("ProjectPlanList"),
                ExchPlanList = ContainPermission("ExchPlanList"),
                SpecPlanList = ContainPermission("SpecPlanList"),
                ProjGanttList = ContainPermission("ProjGanttList")
            });


            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;

            ViewBag.ProjModel = ProjModel;
            ViewBag.TaskGroupModel = TaskGroupModel;

            var taskSpecId = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["taskSpecId"]);
            ViewBag.taskSpecId = taskSpecId;

            ViewBag.PhaseName = new DBSql.Sys.BaseData().GetOrDefault(TaskGroupModel.TaskGroupPhaseId).BaseName;
            ViewBag.allSpec = new DBSql.Sys.BaseData().GetDataSetByEngName("Special");
            ViewBag.JoinSpecList = new DBSql.Design.DesTask().GetList(p => p.ProjId == projID && p.TaskGroupId == taskGroupId).Select(p => p.TaskSpecId).ToList();

            if (planTable == null)
            {
                planTable = new DesPlanTable();
                planTable.ProjId = TaskGroupModel.ProjId;
                planTable.PhaseId = TaskGroupModel.TaskGroupPhaseId;
                planTable.TaskGroupId = (int)taskGroupId;
            }

            #region 绑定导出pdf
            //人员列表
            List<string> Tr = new List<string>();
            int PersonIndex = 0;
            List<DataModel.Models.DesTask> TaskDesignList = new DBSql.Design.DesTask().GetList(p => p.TaskGroupId == taskGroupId && p.TaskType == 0 && p.DeleterEmpId == 0).ToList();
            foreach (DataModel.Models.DesTask item in TaskDesignList)
            {
                PersonIndex++;
                List<string> PersonStr = new List<string>();
                //PersonStr.Add(string.Format("<tr>"));
                PersonStr.Add(string.Format("<td style=\"font - weight:bold; text - align:center; border: 0.5pt solid windowtext; padding: 2px; \">{0}</td>", item.TaskSpecId == 0 ? "汇总" : (new DBSql.Sys.BaseData().GetBaseDataInfo(item.TaskSpecId)).BaseName));
                XmlDocument xml = new XmlDocument();
                xml.InnerXml = item.TaskFlowModel;
                if (xml.ChildNodes[0].ChildNodes.Count > 0)
                {
                    foreach (XmlElement elem in xml.ChildNodes[0].ChildNodes)
                    {
                        if (elem.GetAttribute("FlowNodeTypeID") == ((int)DataModel.NodeType.设计).ToString())
                        {
                            int EmpID = Common.ExtensionMethods.Value<int>(elem.GetAttribute(""));
                            PersonStr.Add(string.Format("<td style=\"font - weight:bold; text - align:center; border: 0.5pt solid windowtext; padding: 2px; \">{0}</td>", elem.GetAttribute("FlowNodeEmpName")));
                        }
                        else if (elem.GetAttribute("FlowNodeTypeID") == ((int)DataModel.NodeType.校对).ToString())
                        {
                            PersonStr.Add(string.Format("<td style=\"font - weight:bold; text - align:center; border: 0.5pt solid windowtext; padding: 2px; \">{0}</td>", elem.GetAttribute("FlowNodeEmpName")));
                        }
                        else if (elem.GetAttribute("FlowNodeTypeID") == ((int)DataModel.NodeType.审核).ToString())
                        {
                            PersonStr.Add(string.Format("<td style=\"font - weight:bold; text - align:center; border: 0.5pt solid windowtext; padding: 2px; \">{0}</td>", elem.GetAttribute("FlowNodeEmpName")));
                        }
                        else if (elem.GetAttribute("FlowNodeTypeID") == ((int)DataModel.NodeType.批准).ToString())
                        {
                            PersonStr.Add(string.Format("<td style=\"font - weight:bold; text - align:center; border: 0.5pt solid windowtext; padding: 2px; \">{0}</td>", elem.GetAttribute("FlowNodeEmpName")));
                        }
                    }
                    if (PersonStr.Count < 5)
                    {
                        for (int i = PersonStr.Count; i < 5; i++)
                        {
                            PersonStr.Add("<td style=\"font - weight:bold; text - align:center; border: 0.5pt solid windowtext; padding: 2px; \"></td>");
                        }
                    }
                    Tr.Add(string.Format("<tr>{0}</tr>", string.Join(" ", PersonStr.ToArray())));
                }
            }
            ViewBag.PersonList = string.Format(" {0} ", string.Join(" ", Tr.ToArray()));

            //提资列表
            string ExchStr = "";
            int ExchIndex = 0;
            var Thwere = QueryBuild<DataModel.Models.DesExch>.True();
            Thwere = Thwere.And(p => p.taskGroupId == taskGroupId && p.DeleterEmpId == 0);
            Thwere = Thwere.And(p => p.ExchIsInvalid == true);
            List<DataModel.Models.DesExch> ExchList = new DBSql.Design.DesExch().GetList(Thwere).ToList();
            foreach (DataModel.Models.DesExch item in ExchList)
            {
                ExchIndex++;
                ExchStr += string.Format("<tr><td>{0}</td>", ExchIndex);
                ExchStr += string.Format("<td>{0}</td>", item.ExchTitle);
                ExchStr += string.Format("<td>{0}</td>", item.ExchSpecName);
                DataModel.Models.Flow _flow = new DBSql.PubFlow.Flow().FirstOrDefault(p => p.FlowRefID == item.Id && p.FlowRefTable == "DesExch");
                ExchStr += string.Format("<td>{0}</td>", _flow == null ? "" : _flow.CreationTime.ToString("yyyy-MM-dd"));

                List<DataModel.Models.DesExchRec> RecList = new DBSql.Design.DesExchRec().GetList(p => p.ExchId == item.Id).ToList();
                ExchStr += string.Format("<td>{0}</td>", string.Join(",", RecList.Select(p => p.RecSpecName)));
                ExchStr += string.Format("</tr>");
            }
            ViewBag.ExchList = string.Format(" {0} ", ExchStr);

            //卷册列表
            string TaskStr = "";
            int TaskIndex = 0;
            List<DataModel.Models.DesTask> TaskList = new DBSql.Design.DesTask().GetList(p => p.TaskGroupId == taskGroupId && p.TaskType == 0 && p.DeleterEmpId == 0).ToList();
            foreach (DataModel.Models.DesTask item in TaskList)
            {
                TaskIndex++;
                TaskStr += string.Format("<tr><td>{0}</td>", TaskIndex);
                TaskStr += string.Format("<td>{0}</td>", item.TaskSpecId == 0 ? "汇总" : (new DBSql.Sys.BaseData().GetBaseDataInfo(item.TaskSpecId)).BaseName);
                TaskStr += string.Format("<td>{0}</td>", item.TaskName);
                TaskStr += string.Format("<td>{0}</td>", item.DateActualStart.Year == 1900 ? "" : item.DateActualStart.ToString("yyyy-MM-dd"));
                TaskStr += string.Format("<td>{0}</td></tr>", item.TaskEmpName);
            }
            ViewBag.TaskList = string.Format(" {0} ", TaskStr);
            #endregion


            return View("PlanTableInfo", planTable);
            //ViewBag.projID = projID;
            //ViewBag.taskGroupId = taskGroupId;
            //var _OpProj = (new DBSql.Project.Project().Get(projID));
            //ViewBag.projEmpId = _OpProj.ProjEmpId;
            //ViewBag.projEmpName = _OpProj.ProjEmpName;
            //return View();
        }

        /// <summary>
        /// 返回计划表的HTML
        /// </summary>
        /// <returns></returns>
        public ActionResult PlanTableHtml(int projID, long taskId)
        {
            ViewBag.ProjModel = new DBSql.Project.Project().Get(projID);
            DataModel.Models.DesTaskGroup TaskModel = new DBSql.Design.DesTaskGroup().FirstOrDefault(p => p.Id == taskId);
            ViewBag.TaskModel = TaskModel;
            ViewBag.PhaseName = new DBSql.Sys.BaseData().Get(TaskModel.TaskGroupPhaseId).BaseName;
            DataModel.Models.DesPlanTable planTable = new DBSql.Design.DesPlanTable().FirstOrDefault(p => p.ProjId == projID && p.TaskGroupId == taskId);
            ViewBag.allSpec = new DBSql.Sys.BaseData().GetDataSetByEngName("Special");
            ViewBag.JoinSpecList = new DBSql.Design.DesTask().GetList(p => p.ProjId == projID && p.TaskGroupId == taskId).Select(p => p.TaskSpecId).ToList();
            if (planTable == null)
            {
                planTable = new DesPlanTable();
                planTable.ProjId = TaskModel.ProjId;
                planTable.PhaseId = TaskModel.TaskGroupPhaseId;
                planTable.TaskGroupId = (int)taskId;
            }

            return View("PlanTableHtml", planTable);
        }


        /**********************项目策划********************/

        /// <summary>
        /// 转到 项目策划列表 页面
        /// </summary>
        /// <returns></returns>
        [Description("项目策划列表")]
        public ActionResult ProjectPlanList()
        {
            ViewBag.currentUserId = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectPlanList")));
            return View();
        }

        /// <summary>
        /// 转到 项目策划 页面
        /// </summary>
        /// <returns></returns>
        [Description("项目策划详情页面")]
        public ActionResult ProjectPlanInfo(int projID, long taskGroupId)
        {
            ViewBag.currentUserId = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("ProjectPlanList")));

            ViewBag.mainTabsUsed = JavaScriptSerializerUtil.getJson(new
            {
                PlanTableList = ContainPermission("PlanTableList"),
                ProjectPlanList = ContainPermission("ProjectPlanList"),
                ExchPlanList = ContainPermission("ExchPlanList"),
                SpecPlanList = ContainPermission("SpecPlanList"),
                ProjGanttList = ContainPermission("ProjGanttList")
            });

            var taskSpecId = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["taskSpecId"]);
            ViewBag.taskSpecId = taskSpecId;

            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            DataModel.Models.DesTaskGroup TaskGroupModel = new DBSql.Design.DesTaskGroup().FirstOrDefault(p => p.Id == taskGroupId);
            ViewBag.TaskGroupModel = TaskGroupModel;
            int gId = Convert.ToInt32(taskGroupId);

            DataTable dt = new DBSql.Design.DesTaskGroup().GetTaskGroupEmps(TaskGroupModel.ProjId, taskGroupId);
            if (dt.Rows.Count>0)
            {
                DataRow row = dt.Select("Permission=31").FirstOrDefault();
                if (row != null)
                {
                    ViewBag.FXMFZR = row["EmpID"];
                }
                else
                {
                    ViewBag.FXMFZR = 0;
                }
            }
            else
            {
                ViewBag.FXMFZR = 0;
            }

            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(new DBSql.Sys.BaseQualification().GetQualificationEmployee(0, 0, 0, 0));
            ViewBag.DesFlow = JavaScriptSerializerUtil.getJson(new DBSql.Design.DesFlow().GetDesFlowList());
            var _OpProj = (new DBSql.Project.Project().Get(projID));
            ViewBag.projEmpId = _OpProj.ProjEmpId;
            ViewBag.projEmpName = _OpProj.ProjEmpName;
            ViewBag.typeId = _OpProj.ColAttType6;
            StringBuilder info = new StringBuilder();
            info.Append("[");
            if (_OpProj.ColAttType6 == 1)
            {
                DBSql.Sys.BaseEmployee dbBaseEmployee = new DBSql.Sys.BaseEmployee();

                var model = dbBaseEmployee.Get(_OpProj.ColAttType9);
                if (null != model)
                {
                    info.Append("{\"EmpID\":\"" + model.EmpID + "\",\"EmpName\":\"" + model.EmpName + "\",\"SpecID\":\"" + (int)ProjectSpecial.商务部分 + "\"}");
                }
                info.Append(",");
                model = dbBaseEmployee.Get(_OpProj.ColAttType10);
                if (null != model)
                {
                    info.Append("{\"EmpID\":\"" + model.EmpID + "\",\"EmpName\":\"" + model.EmpName + "\",\"SpecID\":\"" + (int)ProjectSpecial.技术部分 + "\"}");
                }

            }
            info.Append("]");
            ViewBag.TenderQualification = info.ToString();
            return View();
        }


        /**********************专业策划********************/

        /// <summary>
        /// 转到 专业策划列表 页面
        /// </summary>
        /// <returns></returns>
        [Description("专业策划列表")]
        public ActionResult SpecPlanList()
        {
            ViewBag.permission = PermissionList("SpecPlanList");
            return View();
        }

        /// <summary>
        /// 转到 专业策划 页面
        /// </summary>
        /// <returns></returns>
        [Description("专业策划详情页面")]
        public ActionResult SpecPlanInfo(int projID, long taskGroupId)
        {
            var taskSpecId = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["taskSpecId"]);
            ViewBag.taskSpecId = taskSpecId;

            ViewBag.currentUserId = userInfo.EmpID;
            ViewBag.permission = JavaScriptSerializerUtil.getJson((PermissionList("SpecPlanList")));

            ViewBag.mainTabsUsed = JavaScriptSerializerUtil.getJson(new
            {
                PlanTableList = ContainPermission("PlanTableList"),
                ProjectPlanList = ContainPermission("ProjectPlanList"),
                ExchPlanList = ContainPermission("ExchPlanList"),
                SpecPlanList = ContainPermission("SpecPlanList"),
                ProjGanttList = ContainPermission("ProjGanttList")
            });

            DataModel.Models.DesTaskGroup TaskGroupModel = new DBSql.Design.DesTaskGroup().FirstOrDefault(p => p.Id == taskGroupId);
            ViewBag.TaskGroupModel = TaskGroupModel;

            DataTable dt = new DBSql.Design.DesTaskGroup().GetTaskGroupEmps(TaskGroupModel.ProjId, taskGroupId);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Select("Permission=31").FirstOrDefault();
                if (row != null)
                {
                    ViewBag.FXMFZR = row["EmpID"];
                }
                else
                {
                    ViewBag.FXMFZR = 0;
                }
            }
            else
            {
                ViewBag.FXMFZR = 0;
            }

            string ProjPhaseId = TaskGroupModel.TaskGroupPhaseId.ToString();
            var cl = new DBSql.Iso.IsoGCCLTJD().GetList(p => (p.ProjID == TaskGroupModel.ProjId && p.ProjPhaseId == ProjPhaseId) || p.EngineeringId == TaskGroupModel.Id ).FirstOrDefault();
            var kc = new DBSql.Iso.IsoGCDZKCTJDJZ().GetList(p => (p.ProjID == TaskGroupModel.ProjId && p.ProjPhaseId == ProjPhaseId) || p.EngineeringId == TaskGroupModel.Id ).FirstOrDefault();
            ViewBag.CL = cl == null ? -1 : 1;
            ViewBag.KC = kc == null ? -1 : 1;

            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(new DBSql.Sys.BaseQualification().GetQualificationEmployee(0, 0, 0, 0));
            ViewBag.DesFlow = JavaScriptSerializerUtil.getJson(new DBSql.Design.DesFlow().GetDesFlowList());
            var _OpProj = (new DBSql.Project.Project().Get(projID));
            ViewBag.projEmpId = _OpProj.ProjEmpId;
            ViewBag.projEmpName = _OpProj.ProjEmpName;
            return View();
        }

        /// <summary>
        /// 转到 批量专业策划爷们（配电、技经）
        /// </summary>
        /// <returns></returns>
        [Description("批量专业策划页面")]
        public ActionResult SpecPlanBatch()
        {
            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(new DBSql.Sys.BaseQualification().GetQualificationEmployee(0, 0, 0, 0));
            return View();
        }

        /// <summary>
        /// 转到 汇总关联任务 选择页面
        /// </summary>
        /// <param name="TaskId">汇总任务ID</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult SpecPlanInfoAddSummary(long TaskId)
        {
            var jsonSpecPlanData = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["specPlanData"]);
            var specPlanData = new JavaScriptSerializer().Deserialize<List<DesTaskInput>>(jsonSpecPlanData);

            var canSumTask = specPlanData.Where(x => x.TaskType == (int)DBSql.Design.TaskType.普通任务 && x.TaskSpecId != 0 && x.TaskLevel == 1 && (x.TaskNextFlowNodeId == 0 || x.TaskNextFlowNodeId == TaskId)).Select(x => new
            {
                Id = x.Id,
                TaskSpecName = specPlanData.Where(y => y.TaskType == (int)DBSql.Design.TaskType.专业任务 && y.TaskSpecId == x.TaskSpecId).Select(y => y.TaskName).FirstOrDefault(),
                TaskName = x.TaskName,
                TaskEmpName = x.TaskEmpName,
                DatePlanStart = x.DatePlanStart,
                DatePlanFinish = x.DatePlanFinish
            });

            var selTaskIds = String.Join(",", specPlanData.Where(x => x.TaskNextFlowNodeId == TaskId).Select(x => x.Id).ToArray());

            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;
            ViewBag.selTaskIds = selTaskIds;
            ViewBag.specPlanData = JavaScriptSerializerUtil.getJson(canSumTask.ToList());

            return View();
        }


        /**********************任务处理********************/

        /// <summary>
        /// 转到 任务阶段详情 页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Description("任务阶段详情页面")]
        public ActionResult TaskPhaseInfo(long Id)
        {
            DataModel.Models.DesTaskGroup TaskPhaseModel = new DBSql.Design.DesTaskGroup().FirstOrDefault(p => p.Id == Id);
            return View(TaskPhaseModel);
        }

        /// <summary>
        /// 转到 任务分组详情 页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Description("任务分组详情页面")]
        public ActionResult TaskGroupInfo(long Id)
        {
            DataModel.Models.DesTaskGroup TaskGroupModel = new DBSql.Design.DesTaskGroup().FirstOrDefault(p => p.Id == Id);
            return View(TaskGroupModel);
        }

        /// <summary>
        /// 转到 专业详情 页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Description("专业详情页面")]
        public ActionResult TaskSpecInfo(long Id)
        {
            DataModel.Models.DesTask TaskModel = new DBSql.Design.DesTask().FirstOrDefault(p => p.Id == Id);
            return View(TaskModel);
        }

        /// <summary>
        /// 任务详情（设计）
        /// </summary>
        public ActionResult TaskInfo(long Id)
        {
            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;

            string DesTaskApproveMode = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("DesTaskApproveMode").ConfigValue; // 任务校审模式   单步 0；同步 1
            ViewBag._DesTaskApproveMode = DesTaskApproveMode;

            DataModel.Models.DesTask model = (new DBSql.Design.DesTask().Get(Id));
            ViewBag.TaskModel = model;
            ViewBag.TaskGroupModel = desTaskGroupDB.Get(model.TaskGroupId);
            ViewBag.ProjModel = new DBSql.Project.Project().Get(model.ProjId);
            ViewBag.TaskStatus = model.TaskStatus;
            ViewBag.TaskPhaseName = (new DBSql.Sys.BaseData().Get(ViewBag.TaskModel.TaskPhaseId) == null) ? "" : (new DBSql.Sys.BaseData().Get(ViewBag.TaskModel.TaskPhaseId).BaseName);
            ViewBag.TaskSpecName = (new DBSql.Design.DesTask().FirstOrDefault(x => x.TaskGroupId == model.TaskGroupId && x.TaskSpecId == model.TaskSpecId && x.TaskType == (int)DBSql.Design.TaskType.专业任务)).TaskName;
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.ViewEmpID = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["ViewEmpID"]); // 固定以某人的身份查看他的校审内容

            DataModel.Models.BaseData SpecModel = new DBSql.Sys.BaseData().GetBaseDataInfo(model.TaskSpecId);
            //ViewBag.AltHtml = SpecModel == null ? "" : Url.Content("~/" + SpecModel.BaseAtt2) + "?taskId=" + model.Id;
            ViewBag.AltHtml = SpecModel == null ? "" : Url.Action(SpecModel.BaseAtt2, "DesTaskAltInfo") + "?taskId=" + model.Id + "&divid=" + divid;
            return View();
        }

        /// <summary>
        /// 显示 提交校审 页面（设计）
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskInfoPostApprove(long Id, string AttachIds)
        {
            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;
            ViewBag.TaskModel = desTaskDB.GetDesTaskInfo(Id);
            ViewBag.AttachIds = string.Join(",", AttachIds.Split(',').Where(p => p != "").Select(p => p.ToString()));
            ViewBag.EmpName = JavaScriptSerializerUtil.getJson(new DBSql.Sys.BaseEmployee().GetList(p => p.EmpID != 0).Select(p => new { p.EmpID, p.EmpName }).ToList());

            ViewBag.Qualification = JavaScriptSerializerUtil.getJson(new DBSql.Sys.BaseQualification().GetQualificationEmployee(0, 0, 0, 0));
            DataModel.Models.Project dmProj = new DBSql.Project.Project().Get(ViewBag.TaskModel.ProjId);
            ViewBag.projEmpId = dmProj.ProjEmpId;
            ViewBag.projEmpName = dmProj.ProjEmpName;

            return View();
        }

        /// <summary>
        /// 任务 校审批 页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult TaskInfoApprove(long Id)
        {
            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;

            var requestFrom = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["from"]); // Remind 来自任务提醒页面  SummaryTask 来自汇总任务页面
            ViewBag.RequestForm = requestFrom;

            string DesTaskApproveMode = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("DesTaskApproveMode").ConfigValue; // 任务校审模式   单步 0；同步 1
            ViewBag._DesTaskApproveMode = DesTaskApproveMode;

            DataModel.Models.DesTask model = (new DBSql.Design.DesTask().Get(Id));
            ViewBag.TaskModel = model;
            ViewBag.ProjModel = new DBSql.Project.Project().Get(model.ProjId);
            ViewBag.TaskStatus = model.TaskStatus;
            ViewBag.TaskPhaseName = (new DBSql.Sys.BaseData().Get(ViewBag.TaskModel.TaskPhaseId) == null) ? "" : (new DBSql.Sys.BaseData().Get(ViewBag.TaskModel.TaskPhaseId).BaseName);
            ViewBag.TaskSpecName = (new DBSql.Design.DesTask().FirstOrDefault(x => x.TaskGroupId == model.TaskGroupId && x.TaskSpecId == model.TaskSpecId && x.TaskType == (int)DBSql.Design.TaskType.专业任务)).TaskName;
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.ViewEmpID = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["ViewEmpID"]); // 固定以某人的身份查看他的校审内容

            DataModel.Models.BaseData SpecModel = new DBSql.Sys.BaseData().GetBaseDataInfo(model.TaskSpecId);
            //ViewBag.AltHtml = SpecModel == null ? "" : Url.Content("~/" + SpecModel.BaseAtt2) + "?taskId=" + model.Id;
            ViewBag.AltHtml = SpecModel == null ? "" : Url.Action(SpecModel.BaseAtt2, "DesTaskAltInfo") + "?taskId=" + model.Id + "&divid=" + divid; ;
            return View();
        }

        /// <summary>
        /// 任务 校审批  APP
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string TaskInfoApproveInfo(long Id)
        {
            var requestFrom = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["from"]); // Remind 来自任务提醒页面  SummaryTask 来自汇总任务页面
            ViewBag.RequestForm = requestFrom;

            string DesTaskApproveMode = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("DesTaskApproveMode").ConfigValue; // 任务校审模式   单步 0；同步 1
            ViewBag._DesTaskApproveMode = DesTaskApproveMode;

            DataModel.Models.DesTask model = (new DBSql.Design.DesTask().Get(Id));
            DataModel.Models.DesTask mdlEvent = new DataModel.Models.DesTask();
            Common.ModelConvert.MvcChangeTarget<DataModel.Models.DesTask, DataModel.Models.DesTask>(mdlEvent, model);
            ViewBag.TaskModel = mdlEvent;

            var projModel = new DBSql.Project.Project().Get(model.ProjId);
            DataModel.Models.Project mdlProjEvent = new DataModel.Models.Project();
            Common.ModelConvert.MvcChangeTarget<DataModel.Models.Project, DataModel.Models.Project>(mdlProjEvent, projModel);
            ViewBag.ProjModel = mdlProjEvent;

            ViewBag.TaskStatus = model.TaskStatus;
            ViewBag.TaskPhaseName = (new DBSql.Sys.BaseData().Get(ViewBag.TaskModel.TaskPhaseId) == null) ? "" : (new DBSql.Sys.BaseData().Get(ViewBag.TaskModel.TaskPhaseId).BaseName);
            ViewBag.TaskSpecName = (new DBSql.Design.DesTask().FirstOrDefault(x => x.TaskGroupId == model.TaskGroupId && x.TaskSpecId == model.TaskSpecId && x.TaskType == (int)DBSql.Design.TaskType.专业任务)).TaskName;
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.ViewEmpID = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["ViewEmpID"]); // 固定以某人的身份查看他的校审内容

            DataModel.Models.BaseData SpecModel = new DBSql.Sys.BaseData().GetBaseDataInfo(model.TaskSpecId);
            //ViewBag.AltHtml = SpecModel == null ? "" : Url.Content("~/" + SpecModel.BaseAtt2) + "?taskId=" + model.Id;
            //ViewBag.AltHtml = SpecModel == null ? "" : Url.Action(SpecModel.BaseAtt2, "DesTaskAltInfo") + "?taskId=" + model.Id + "&divid=" + divid; ;
            return JavaScriptSerializerUtil.getJson(ViewBag); ;
        }

        /// <summary>
        /// 显示 校审批提交通过 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskInfoApprovePostPass(long Id, string AttachIds)
        {
            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;
            ViewBag.TaskModel = desTaskDB.GetDesTaskInfo(Id);
            ViewBag.AttachIds = string.Join(",", AttachIds.Split(',').Where(p => p != "").Select(p => p.ToString()));
            ViewBag.EmpName = JavaScriptSerializerUtil.getJson(new DBSql.Sys.BaseEmployee().GetList(p => p.EmpID != 0).Select(p => new { p.EmpID, p.EmpName }).ToList());
            return View();
        }

        /// <summary>
        /// 显示 校审批提交回退 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskInfoApprovePostBack(long Id, string AttachIds)
        {
            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;
            ViewBag.TaskModel = desTaskDB.GetDesTaskInfo(Id);
            ViewBag.AttachIds = string.Join(",", AttachIds.Split(',').Where(p => p != "").Select(p => p.ToString()));
            ViewBag.EmpName = JavaScriptSerializerUtil.getJson(new DBSql.Sys.BaseEmployee().GetList(p => p.EmpID != 0).Select(p => new { p.EmpID, p.EmpName }).ToList());
            return View();
        }

        /// <summary>
        /// 任务设计页面附件列表
        /// </summary>
        public ActionResult TaskAttach(long TaskId)
        {
            ViewBag.TaskId = TaskId;

            return View();
        }

        /// <summary>
        /// 任务校审页面附件列表
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskAttachApprove(long TaskId)
        {
            ViewBag.TaskId = TaskId;

            return View();
        }

        /// <summary>
        /// 查看 进度图中的某任务的文件校审状态
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult TaskAttachProgress(long Id)
        {
            var divid = Common.ModelConvert.ConvertToDefaultType<string>(Request.Params["divid"]); // 所在对话框ID
            ViewBag._DialogId = divid;

            DataModel.Models.DesTask model = (new DBSql.Design.DesTask().Get(Id));
            ViewBag.TaskModel = model;
            ViewBag.ProjModel = new DBSql.Project.Project().Get(model.ProjId);
            ViewBag.TaskStatus = model.TaskStatus;
            ViewBag.TaskPhaseName = (new DBSql.Sys.BaseData().Get(ViewBag.TaskModel.TaskPhaseId) == null) ? "" : (new DBSql.Sys.BaseData().Get(ViewBag.TaskModel.TaskPhaseId).BaseName);
            ViewBag.TaskSpecName = (new DBSql.Design.DesTask().FirstOrDefault(x => x.TaskGroupId == model.TaskGroupId && x.TaskSpecId == model.TaskSpecId && x.TaskType == (int)DBSql.Design.TaskType.专业任务)).TaskName;
            ViewBag.EmpID = userInfo.EmpID;

            DataModel.Models.BaseData SpecModel = new DBSql.Sys.BaseData().GetBaseDataInfo(model.TaskSpecId);
            //ViewBag.AltHtml = SpecModel == null ? "" : Url.Content("~/" + SpecModel.BaseAtt2) + "?taskId=" + model.Id;
            ViewBag.AltHtml = SpecModel == null ? "" : Url.Action(SpecModel.BaseAtt2, "DesTaskAltInfo") + "?taskId=" + model.Id + "&divid=" + divid;
            return View();
        }

        /// <summary>
        /// 显示 任务附件拆图转换后的 文件列表
        /// </summary>
        /// <param name="AttachID"></param>
        /// <param name="AttachVer"></param>
        /// <returns></returns>
        public ActionResult TaskAttachSplitFilesList()
        {
            return View();
        }


        /**********************任务提醒********************/

        /// <summary>
        /// 任务提醒列表
        /// </summary>
        /// <returns></returns>
        [Description("任务提醒列表")]
        public ActionResult TaskRemindList()
        {
            string DesTaskApproveMode = new DBSql.Sys.BaseConfig().GetBaseConfigInfo("DesTaskApproveMode").ConfigValue;
            ViewBag._DesTaskApproveMode = DesTaskApproveMode;
            ViewBag.TimeNow = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        /// <summary>
        /// 转到 工作查询主 页面
        /// </summary>
        /// <param name="ListType"></param>
        /// <returns></returns>
        [Description("工作查询主页面")]
        public ActionResult TaskWorkMain(string ListType)
        {
            ViewBag.ListType = ListType;
            List<string> permis = new List<string>();
            switch (ListType.ToLower())
            {
                case "waiting":
                    permis = PermissionList("TaskWorkListWaiting");
                    break;
                case "now":
                    permis = PermissionList("TaskWorkListNow");
                    break;
                case "finished":
                    permis = PermissionList("TaskWorkListFinished");
                    break;
                default:
                    break;
            }
            string strPremis = "";
            if (!(permis.Contains("allview") || permis.Contains("alledit")))
            {
                if (permis.Contains("dep"))
                {
                    strPremis = "dep";
                }
                else
                {
                    strPremis = "emp";
                }
            }
            ViewBag.permission = strPremis;
            ViewBag.EmpID = userInfo.EmpID;
            ViewBag.EmpDepID = userInfo.EmpDepID;

            return View();
        }

        /// <summary>
        /// 转到 工作查询页面
        /// </summary>
        /// <param name="ListType"></param>
        /// <returns></returns>
        public ActionResult TaskWorkList(string ListType, int EmpId)
        {
            ViewBag.ListType = ListType;
            ViewBag.EmpId = EmpId;

            return View();
        }



        /**********************任务进度**********************/

        /// <summary>
        /// 转到 项目进度列表 页面
        /// </summary>
        /// <returns></returns>
        [Description("项目进度列表")]
        public ActionResult ProjectProgressList()
        {
            return View();
        }

        /// <summary>
        /// 配网项目进度列表
        /// </summary>
        /// <returns></returns>
        [Description("配网项目进度列表")]
        public ActionResult ProjectProgressListPW()
        {
            return View();
        }

        /// <summary>
        /// 转到 项目进度查看 页面
        /// </summary>
        /// <returns></returns>
        [Description("项目进度查看页面")]
        public ActionResult ProjectProgressInfo(int projID, long taskGroupId)
        {
            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            return View();
        }

        /// <summary>
        /// 转到 项目节点进度查看 页面
        /// </summary>
        /// <returns></returns>
        [Description("项目节点进度查看页面")]
        public ActionResult ProjectProgressInfoOrg(int projID, long taskGroupId)
        {
            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            return View();
        }

        /// <summary>
        /// 转到 项目策划进度查看 页面
        /// </summary>
        /// <returns></returns>
        [Description("项目策划进度查看页面")]
        public ActionResult ProjectProgressInfoPlan(int projID, long taskGroupId)
        {
            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;
            return View();
        }

        /// <summary>
        /// 转到 设计文件列表 页面
        /// </summary>
        /// <returns></returns>
        [Description("设计文件列表页面")]
        public ActionResult ProjectProgressInfoAttach(int projID, long taskGroupId, long taskId)
        {
            ViewBag.projId = projID;
            ViewBag.taskGroupId = taskGroupId;
            ViewBag.taskId = taskId;
            return View();
        }

        /// <summary>
        /// 转到 任务开展情况统计列表 页面
        /// </summary>
        /// <returns></returns>
        [Description("任务开展情况统计列表")]
        public ActionResult TaskProgressList()
        {
            return View();
        }


        /**********************关键节点********************/

        /// <summary>
        /// 显示 项目关键节点配置 页面
        /// </summary>
        /// <param name="projID"></param>
        /// <param name="taskGroupId"></param>
        /// <returns></returns>
        [Description("项目关键节点配置页面")]
        public ActionResult ProjectGantt(int projID, long taskGroupId)
        {
            var taskSpecId = Common.ModelConvert.ConvertToDefaultType<int>(Request.Params["taskSpecId"]);
            ViewBag.taskSpecId = taskSpecId;

            ViewBag.projID = projID;
            ViewBag.taskGroupId = taskGroupId;

            ViewBag.edit = 0;
            List<string> Permission = PermissionList("ProjGanttList");
            if (Permission.Contains("edit") || Permission.Contains("alledit"))//编辑权和维护权
            {
                ViewBag.edit = 1;
            }
            DataModel.Models.Project dmProj = new DBSql.Project.Project().Get(projID);
            if (dmProj != null && dmProj.ProjEmpId == userInfo.EmpID)
            {
                ViewBag.edit = 1;
            }
            DataModel.Models.DesTaskGroup dmGroup = new DBSql.Design.DesTaskGroup().Get(taskGroupId);
            if (dmGroup != null && dmGroup.TaskGroupEmpID == userInfo.EmpID)
            {
                ViewBag.edit = 1;
            }

            ViewBag.mainTabsUsed = JavaScriptSerializerUtil.getJson(new
            {
                PlanTableList = ContainPermission("PlanTableList"),
                ProjectPlanList = ContainPermission("ProjectPlanList"),
                ExchPlanList = ContainPermission("ExchPlanList"),
                SpecPlanList = ContainPermission("SpecPlanList"),
                ProjGanttList = ContainPermission("ProjGanttList")
            });

            return View();
        }

        /// <summary>
        /// 删除关键中的节点
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult delTaskGantt(long Id)
        {
            int reuslt = 0;
            try
            {
                desTaskGanttDB.delTaskGantt((int)Id, userInfo);
                reuslt = 1;
            }
            catch (Exception)
            {

            }

            DoResult dr = reuslt > 0 ? DoResultHelper.doSuccess(reuslt, "操作成功") : DoResultHelper.doSuccess(reuslt, "操作失败");
            return Json(dr);
        }


        /**********************其他********************/
        public ActionResult TaskWorkPubList()
        {
            //判断是多选1还是单选0
            ViewBag.isMust = Request.Params["isMust"] == null ? 0 : Common.ExtensionMethods.Value<int>(Request.Params["isMust"]);
            //判断类别--可自定义(Type=ShowSpecTask 是仅显示 专业任务列表；  默认仅显示 设计任务列表)
            ViewBag.Type = Request.Params["Type"] == null ? 0 : Common.ExtensionMethods.Value<int>(Request.Params["Type"]);

            //特殊条件
            ViewBag.ProjID = Request.Params["ProjID"] == null ? 0 : Common.ExtensionMethods.Value<int>(Request.Params["ProjID"]);
            ViewBag.TaskPhaseID = Request.Params["TaskPhaseID"] == null ? 0 : Common.ExtensionMethods.Value<int>(Request.Params["TaskPhaseID"]);
            return View();
        }

        /// <summary>
        /// 选择导入危险源、应急事项
        /// </summary>
        /// <returns></returns>
        public ActionResult selectContent()
        {
            ViewBag.contentType = Request.Params["Type"] == null ? "" : Request.Params["Type"].ToString();
            return View();
        }

        [Description("校审完成文件列表")]
        public ActionResult ChooseCheckFile()
        {
            return View();
        }
    }
}
